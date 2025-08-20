using Esc_PetshopBackend.DTOs;

namespace Esc_PetshopBackend.Services.Interface
{
    public interface ITipoAnimalService
    {
        Task<IEnumerable<TipoAnimalDto>> GetAllAsync();
        Task<TipoAnimalDto> GetByIdAsync(int id);
        Task<TipoAnimalDto> CreateAsync(TipoAnimalCreateDto tipoAnimalCreateDto);
        Task UpdateAsync(int id, TipoAnimalUpdateDto tipoAnimalUpdateDto);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
