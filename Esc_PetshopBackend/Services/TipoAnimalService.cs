using AutoMapper;
using Esc_PetshopBackend.Data.Repositories.Interface;
using Esc_PetshopBackend.DTOs;
using Esc_PetshopBackend.Services.Interface;

namespace Esc_PetshopBackend.Services
{
    public class TipoAnimalService : ITipoAnimalService
    {
        private readonly ITipoAnimalRepository _tipoAnimalRepository;
        private readonly IMapper _mapper;

        public TipoAnimalService(ITipoAnimalRepository tipoAnimalRepository, IMapper mapper)
        {
            _tipoAnimalRepository = tipoAnimalRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TipoAnimalDto>> GetAllAsync()
        {
            var tiposAnimal = await _tipoAnimalRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<TipoAnimalDto>>(tiposAnimal);
        }

        public async Task<TipoAnimalDto> GetByIdAsync(int id)
        {
            var tipoAnimal = await _tipoAnimalRepository.GetByIdAsync(id);
            return _mapper.Map<TipoAnimalDto>(tipoAnimal);
        }

        public async Task<TipoAnimalDto> CreateAsync(TipoAnimalCreateDto tipoAnimalCreateDto)
        {
            var tipoAnimal = _mapper.Map<Data.Entities.TipoAnimal>(tipoAnimalCreateDto);
            await _tipoAnimalRepository.AddAsync(tipoAnimal);
            return _mapper.Map<TipoAnimalDto>(tipoAnimal);
        }

        public async Task UpdateAsync(int id, TipoAnimalUpdateDto tipoAnimalUpdateDto)
        {
            var tipoAnimal = await _tipoAnimalRepository.GetByIdAsync(id);
            if (tipoAnimal == null)
                throw new KeyNotFoundException("Tipo de animal não encontrado");
            _mapper.Map(tipoAnimalUpdateDto, tipoAnimal);
            await _tipoAnimalRepository.UpdateAsync(tipoAnimal);
        }

        public async Task DeleteAsync(int id)
        {
            var tipoAnimal = await _tipoAnimalRepository.GetByIdAsync(id);
            if (tipoAnimal == null)
                throw new KeyNotFoundException("Tipo de animal não encontrado");
            await _tipoAnimalRepository.DeleteAsync(id);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _tipoAnimalRepository.ExistsAsync(id);
        }
       }
}
