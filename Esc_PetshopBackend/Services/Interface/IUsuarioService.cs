using Esc_PetshopBackend.DTOs;

namespace Esc_PetshopBackend.Services
{
    public interface IUsuarioService
    {
        Task<IEnumerable<UsuarioDto>> GetAllAsync();
        Task<UsuarioDto> GetByIdAsync(int id);
        Task<UsuarioDto> CreateAsync(UsuarioCreateDto usuarioCreateDto);
        Task UpdateAsync(int id, UsuarioUpdateDto usuarioUpdateDto);
        Task DeleteAsync(int id);
    }
}