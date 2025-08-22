using Esc_PetshopBackend.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Esc_PetshopBackend.Services
{
    public interface IPetService
    {
        Task<IEnumerable<PetDto>> GetAllAsync();
        Task<PetDto> GetByIdAsync(int id);
        Task<IEnumerable<PetDto>> GetByClienteIdAsync(int clienteId);
        Task<IEnumerable<PetDto>> GetByTipoAnimalIdAsync(int tipoAnimalId);
        Task<PetDto> CreateAsync(PetCreateDto petCreateDto);
        Task<PetDto> UpdateAsync(int id, PetUpdateDto petUpdateDto);
        Task<bool> DeleteAsync(int id);
    }
}