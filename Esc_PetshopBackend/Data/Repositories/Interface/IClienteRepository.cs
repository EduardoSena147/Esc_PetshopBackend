using Esc_PetshopBackend.Data.Entities;
using System.Threading.Tasks;

namespace Esc_PetshopBackend.Data.Repositories.Interface
{
    public interface IClienteRepository
    {
        Task<IEnumerable<Cliente>> GetAllAsync();
        Task<Cliente> GetByIdAsync(int id);
        Task<Cliente> GetByUsuarioIdAsync(int usuarioId);
        Task AddAsync(Cliente cliente);
        Task UpdateAsync(Cliente cliente);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
