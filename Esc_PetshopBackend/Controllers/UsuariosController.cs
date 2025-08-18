using Microsoft.AspNetCore.Mvc;
using Esc_PetshopBackend.DTOs;
using Esc_PetshopBackend.Services;
using Microsoft.AspNetCore.Authorization;

namespace Esc_PetshopBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuariosController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioDto>>> GetAll()
        {
            var usuarios = await _usuarioService.GetAllAsync();
            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioDto>> GetById(int id)
        {
            var usuario = await _usuarioService.GetByIdAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return Ok(usuario);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<UsuarioDto>> Create(UsuarioCreateDto usuarioCreateDto)
        {
            var usuario = await _usuarioService.CreateAsync(usuarioCreateDto);
            return CreatedAtAction(nameof(GetById), new { id = usuario.Id }, usuario);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UsuarioUpdateDto usuarioUpdateDto)
        {
            try
            {
                await _usuarioService.UpdateAsync(id, usuarioUpdateDto);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _usuarioService.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}