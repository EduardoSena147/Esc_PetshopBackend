using Esc_PetshopBackend.DTOs;
using System.Threading.Tasks;

namespace Esc_PetshopBackend.Services
{
    public interface IAuthService
    {
        Task<string?> Authenticate(string email, string senha);
        Task<bool> Register(UsuarioCreateDto usuarioDto);
        Task<bool> ChangePassword(int usuarioId, string currentPassword, string newPassword);
    }
}