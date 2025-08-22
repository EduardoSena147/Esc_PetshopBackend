using AutoMapper;
using Esc_PetshopBackend.Data.Repositories;
using Esc_PetshopBackend.Data.Repositories.Interface;
using Esc_PetshopBackend.DTOs;
using Esc_PetshopBackend.Services.Interface;

namespace Esc_PetshopBackend.Services
{
    public class TipoAgendamentoService : ITipoAgendamentoService
    {
        private readonly ITipoAgendamentoRepository _tipoAgendamentoRepository;
        private readonly IMapper _mapper;

        public TipoAgendamentoService(ITipoAgendamentoRepository tipoAgendamentoRepository, IMapper mapper)
        {
            _tipoAgendamentoRepository = tipoAgendamentoRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TipoAgendamentoDto>> GetAllAsync()
        {
            var tiposAgendamento = await _tipoAgendamentoRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<TipoAgendamentoDto>>(tiposAgendamento);
        }

        public async Task<TipoAgendamentoDto> GetByIdAsync(int id)
        {
            var tipoAgendamento = await _tipoAgendamentoRepository.GetByIdAsync(id);
            return _mapper.Map<TipoAgendamentoDto>(tipoAgendamento);
        }

        public async Task<TipoAgendamentoDto> CreateAsync(TipoAgendamentoCreateDto tipoAgendamentoCreateDto)
        {
            var tipoAgendamento = _mapper.Map<Data.Entities.TipoAgendamento>(tipoAgendamentoCreateDto);
            await _tipoAgendamentoRepository.AddAsync(tipoAgendamento);
            return _mapper.Map<TipoAgendamentoDto>(tipoAgendamento);
        }

        public async Task UpdateAsync(int id, TipoAgendamentoUpdateDto tipoAgendamentoUpdateDto)
        {
            var tipoAgendamento = await _tipoAgendamentoRepository.GetByIdAsync(id);
            if (tipoAgendamento == null)
                throw new KeyNotFoundException("Tipo de Agendamento não encontrado");
            _mapper.Map(tipoAgendamentoUpdateDto, tipoAgendamento);
            await _tipoAgendamentoRepository.UpdateAsync(tipoAgendamento);
        }

        public async Task DeleteAsync(int id)
        {
            var tipoAgendamento = await _tipoAgendamentoRepository.GetByIdAsync(id);
            if (tipoAgendamento == null)
                throw new KeyNotFoundException("Tipo de Agendamento não encontrado");
            await _tipoAgendamentoRepository.DeleteAsync(id);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _tipoAgendamentoRepository.ExistsAsync(id);
        }
    }
}
