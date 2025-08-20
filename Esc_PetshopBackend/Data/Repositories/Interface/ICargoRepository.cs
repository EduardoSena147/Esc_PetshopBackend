using Esc_PetshopBackend.Data.Entities;

namespace Esc_PetshopBackend.Data.Repositories.Interface
{
    public interface ICargoRepository
    {
        Task<IEnumerable<Cargo>> GetAllAsync();
        Task<Cargo> GetByIdAsync(int id);
        Task<Cargo> GetByDescricaoAsync(string descricao);
        Task AddAsync(Cargo cargo);
        Task UpdateAsync(Cargo cargo);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
