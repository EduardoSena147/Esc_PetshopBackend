using Esc_PetshopBackend.DTOs;

namespace Esc_PetshopBackend.Services.Interface
{
    public interface ITipoAgendamentoService
    {
        Task<IEnumerable<TipoAgendamentoDto>> GetAllAsync();
        Task<TipoAgendamentoDto> GetByIdAsync(int id);
        Task<TipoAgendamentoDto> CreateAsync(TipoAgendamentoCreateDto tipoAnimalCreateDto);
        Task UpdateAsync(int id, TipoAgendamentoUpdateDto tipoAnimalUpdateDto);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
