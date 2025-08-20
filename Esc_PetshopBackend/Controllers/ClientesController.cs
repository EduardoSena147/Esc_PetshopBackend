using Esc_PetshopBackend.DTOs;
using Esc_PetshopBackend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Esc_PetshopBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        public ClientesController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClienteDto>>> GetAll()
        {
            var clientes = await _clienteService.GetAllAsync();
            return Ok(clientes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ClienteDto>> GetById(int id)
        {
            try
            {
                var cliente = await _clienteService.GetByIdAsync(id);
                return Ok(cliente);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpGet("usuario/{usuarioId}")]
        public async Task<ActionResult<ClienteDto>> GetByUsuarioId(int usuarioId)
        {
            try
            {
                var cliente = await _clienteService.GetByUsuarioIdAsync(usuarioId);
                return Ok(cliente);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<ActionResult<ClienteDto>> Create([FromBody] ClienteCreateDto clienteCreateDto)
        {
            try
            {
                // Extrai o ID do usuário do token JWT
                var usuarioId = GetUsuarioIdFromToken();

                var cliente = await _clienteService.CreateAsync(usuarioId, clienteCreateDto);
                return CreatedAtAction(nameof(GetById), new { id = cliente.Id }, cliente);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ClienteDto>> Update(int id, [FromBody] ClienteUpdateDto clienteUpdateDto)
        {
            try
            {
                var cliente = await _clienteService.UpdateAsync(id, clienteUpdateDto);
                return Ok(cliente);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _clienteService.DeleteAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpPost("completar-cadastro")]
        public async Task<ActionResult> CompletarCadastro([FromBody] ClienteCreateDto clienteCreateDto)
        {
            try
            {
                var usuarioId = GetUsuarioIdFromToken();
                var sucesso = await _clienteService.CompletarCadastroAsync(usuarioId, clienteCreateDto);

                if (sucesso)
                    return Ok("Cadastro completado com sucesso");
                else
                    return BadRequest("Dados incompletos para finalizar cadastro");
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Cliente não encontrado");
            }
        }

        [HttpGet("validar-cadastro-completo")]
        public async Task<ActionResult<bool>> ValidarCadastroCompleto()
        {
            var usuarioId = GetUsuarioIdFromToken();
            var cadastroCompleto = await _clienteService.ValidarCadastroCompletoAsync(usuarioId);
            return Ok(cadastroCompleto);
        }

        private int GetUsuarioIdFromToken()
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int usuarioId))
                throw new UnauthorizedAccessException("Token inválido");

            return usuarioId;
        }
    }
}