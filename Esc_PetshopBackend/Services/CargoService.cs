using AutoMapper;
using Esc_PetshopBackend.Data.Entities;
using Esc_PetshopBackend.Data.Repositories.Interface;
using Esc_PetshopBackend.DTOs;
using Esc_PetshopBackend.Services.Interface;

namespace Esc_PetshopBackend.Services
{
    public class CargoService : ICargoService
    {
        private readonly ICargoRepository _cargoRepository;
        private readonly IMapper _mapper;

        public CargoService(ICargoRepository cargoRepository, IMapper mapper)
        {
            _cargoRepository = cargoRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CargoDto>> GetAllAsync()
        {
            var cargos = await _cargoRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CargoDto>>(cargos);
        }

        public async Task<CargoDto> GetByIdAsync(int id)
        {
            var cargo = await _cargoRepository.GetByIdAsync(id);
            return _mapper.Map<CargoDto>(cargo);
        }

        public async Task<CargoDto> CreateAsync(CargoCreateDto cargoCreateDto)
        {
            var cargo = _mapper.Map<Cargo>(cargoCreateDto);
            await _cargoRepository.AddAsync(cargo);
            return _mapper.Map<CargoDto>(cargo);
        }

        public async Task UpdateAsync(int id, CargoUpdateDto cargoUpdateDto)
        {
            var cargo = await _cargoRepository.GetByIdAsync(id);
            if (cargo == null)
                throw new KeyNotFoundException("Cargo não encontrado");

            _mapper.Map(cargoUpdateDto, cargo);
            await _cargoRepository.UpdateAsync(cargo);
        }

        public async Task DeleteAsync(int id)
        {
            var cargo = await _cargoRepository.GetByIdAsync(id);
            if (cargo == null)
                throw new KeyNotFoundException("Cargo não encontrado");

            await _cargoRepository.DeleteAsync(id);
        }
    }
}
