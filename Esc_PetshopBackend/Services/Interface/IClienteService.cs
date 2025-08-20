using Esc_PetshopBackend.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Esc_PetshopBackend.Services
{
    public interface IClienteService
    {
        Task<IEnumerable<ClienteDto>> GetAllAsync();
        Task<ClienteDto> GetByIdAsync(int id);
        Task<ClienteDto> GetByUsuarioIdAsync(int usuarioId);
        Task<ClienteDto> CreateAsync(int usuarioId, ClienteCreateDto clienteCreateDto);
        Task<ClienteDto> UpdateAsync(int id, ClienteUpdateDto clienteUpdateDto);
        Task<bool> DeleteAsync(int id);
        Task<bool> CompletarCadastroAsync(int usuarioId, ClienteCreateDto clienteCreateDto);
        Task<bool> ValidarCadastroCompletoAsync(int usuarioId);
    }
}