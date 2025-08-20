using Esc_PetshopBackend.Data.Entities;

namespace Esc_PetshopBackend.Data.Repositories.Interface
{
    public interface ITipoAnimalRepository
    {
        Task<IEnumerable<TipoAnimal>> GetAllAsync();
        Task<TipoAnimal> GetByIdAsync(int id);
        Task<TipoAnimal> GetByDescricaoAsync(string descricao);
        Task AddAsync(TipoAnimal tipoAnimal);
        Task UpdateAsync(TipoAnimal tipoAnimal);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);

    }
}
