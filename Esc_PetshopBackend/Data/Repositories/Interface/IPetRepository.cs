using Esc_PetshopBackend.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Esc_PetshopBackend.Data.Repositories.Interface
{
    public interface IPetRepository
    {
        Task<IEnumerable<Pet>> GetAllAsync();
        Task<Pet> GetByIdAsync(int id);
        Task<IEnumerable<Pet>> GetByClienteIdAsync(int clienteId);
        Task<IEnumerable<Pet>> GetByTipoAnimalIdAsync(int tipoAnimalId);
        Task AddAsync(Pet pet);
        Task UpdateAsync(Pet pet);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}