using Esc_PetshopBackend.DTOs;
using Esc_PetshopBackend.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Esc_PetshopBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TipoAgendamentoController : ControllerBase
    {
        private readonly ITipoAgendamentoService _tipoAgendamentoService;
        public TipoAgendamentoController(ITipoAgendamentoService tipoAgendamentoService)
        {
            _tipoAgendamentoService = tipoAgendamentoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoAgendamentoDto>>> GetAll()
        {
            var tiposAnimais = await _tipoAgendamentoService.GetAllAsync();
            return Ok(tiposAnimais);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<TipoAgendamentoDto>> GetById(int id)
        {
            var tipoAgendamento = await _tipoAgendamentoService.GetByIdAsync(id);
            if (tipoAgendamento == null)
                return NotFound("Tipo de Agendamento não encontrado");
            return Ok(tipoAgendamento);
        }

        [HttpPost]
        public async Task<ActionResult<TipoAgendamentoDto>> Create(TipoAgendamentoCreateDto tipoAgendamentoCreateDto)
        {
            var tipoAgendamento = await _tipoAgendamentoService.CreateAsync(tipoAgendamentoCreateDto);
            return CreatedAtAction(nameof(GetById), new { id = tipoAgendamento.Id }, tipoAgendamento);
        }
    }
}
