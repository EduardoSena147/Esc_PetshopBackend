using Esc_PetshopBackend.DTOs;
using Esc_PetshopBackend.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Esc_PetshopBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CargosController : ControllerBase
    {
        private readonly ICargoService _cargoService;

        public CargosController(ICargoService cargoService)
        {
            _cargoService = cargoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CargoDto>>> GetAll()
        {
            var cargos = await _cargoService.GetAllAsync();
            return Ok(cargos);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CargoDto>> GetById(int id)
        {
            var cargo = await _cargoService.GetByIdAsync(id);
            if (cargo == null)
                return NotFound("Cargo não encontrado");
            return Ok(cargo);
        }

    }
}
