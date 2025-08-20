using Esc_PetshopBackend.DTOs;
using Esc_PetshopBackend.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Esc_PetshopBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TipoAnimalController : ControllerBase
    {
        private readonly ITipoAnimalService _tipoAnimalService;
        public TipoAnimalController(ITipoAnimalService tipoAnimalService)
        {
            _tipoAnimalService = tipoAnimalService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoAnimalDto>>> GetAll()
        {
            var tiposAnimais = await _tipoAnimalService.GetAllAsync();
            return Ok(tiposAnimais);
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<TipoAnimalDto>> GetById(int id)
        {
            var tipoAnimal = await _tipoAnimalService.GetByIdAsync(id);
            if (tipoAnimal == null)
                return NotFound("Tipo de animal não encontrado");
            return Ok(tipoAnimal);
        }
    }
}
