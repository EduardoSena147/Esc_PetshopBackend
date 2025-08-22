using Esc_PetshopBackend.Data.Entities;

namespace Esc_PetshopBackend.Data.Repositories.Interface
{
    public interface ITipoAgendamentoRepository
    {
        Task<IEnumerable<TipoAgendamento>> GetAllAsync();
        Task<TipoAgendamento> GetByIdAsync(int id);
        Task<TipoAgendamento> GetByDescricaoAsync(string descricao);
        Task AddAsync(TipoAgendamento tipoAgendamento);
        Task UpdateAsync(TipoAgendamento tipoAgendamento);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
