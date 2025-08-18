using Esc_PetshopBackend.Services;
using Esc_PetshopBackend.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Esc_PetshopBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Autentica um usuário e retorna um token JWT
        /// </summary>
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var token = await _authService.Authenticate(request.Email, request.Senha);

            if (token == null)
                return Unauthorized("Credenciais inválidas");

            return Ok(new { Token = token });
        }

        /// <summary>
        /// Registra um novo usuário
        /// </summary>
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] UsuarioCreateDto usuarioDto)
        {
            var success = await _authService.Register(usuarioDto);

            if (!success)
                return BadRequest("Email já está em uso");

            return Ok("Usuário registrado com sucesso");
        }

        /// <summary>
        /// Altera a senha do usuário autenticado
        /// </summary>
        [HttpPost("change-password")]
        [Authorize] // Requer autenticação
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var success = await _authService.ChangePassword(userId, request.CurrentPassword, request.NewPassword);

            if (!success)
                return BadRequest("Senha atual incorreta");

            return Ok("Senha alterada com sucesso");
        }
    }
}