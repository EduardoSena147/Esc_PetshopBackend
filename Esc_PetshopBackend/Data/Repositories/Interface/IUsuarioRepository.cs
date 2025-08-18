using Esc_PetshopBackend.Data.Entities;

namespace Esc_PetshopBackend.Data.Repositories.Interface
{
    public interface IUsuarioRepository
    {
        Task<IEnumerable<Usuario>> GetAllAsync();
        Task<Usuario> GetByIdAsync(int id);
        Task<Usuario> GetByUsernameAsync(string username);
        Task<Usuario> GetByEmailAsync(string email);
        Task AddAsync(Usuario usuario);
        Task UpdateAsync(Usuario usuario);
        Task DeleteAsync(int id);
    }
}