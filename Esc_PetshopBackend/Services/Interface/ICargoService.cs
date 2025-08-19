using Esc_PetshopBackend.DTOs;

namespace Esc_PetshopBackend.Services.Interface
{
    public interface ICargoService
    {
        Task<IEnumerable<CargoDto>> GetAllAsync();
        Task<CargoDto> GetByIdAsync(int id);
        Task<CargoDto> CreateAsync(CargoCreateDto cargoCreateDto);
        Task UpdateAsync(int id, CargoUpdateDto cargoUpdateDto);
        Task DeleteAsync(int id);
    }
}
