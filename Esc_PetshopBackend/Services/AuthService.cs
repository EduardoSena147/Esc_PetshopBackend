using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Esc_PetshopBackend.Data.Entities;
using Esc_PetshopBackend.Data.Models;
using Esc_PetshopBackend.Data.Repositories.Interface;
using Esc_PetshopBackend.DTOs;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;

namespace Esc_PetshopBackend.Services
{
    public class AuthService : IAuthService
    {
        private readonly JwtSettings _jwtSettings;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IClienteRepository _clienteRepository;
        private readonly PasswordHasher<Usuario> _passwordHasher;

        public AuthService(JwtSettings jwtSettings, IUsuarioRepository usuarioRepository, IClienteRepository clienteRepository)
        {
            _jwtSettings = jwtSettings;
            _usuarioRepository = usuarioRepository;
            _passwordHasher = new PasswordHasher<Usuario>();
            _clienteRepository = clienteRepository;
        }

        public async Task<string?> Authenticate(string email, string senha)
        {
            email = email?.Trim();
            senha = senha?.Trim();

            var usuario = await _usuarioRepository.GetByEmailAsync(email);
            if (usuario == null || string.IsNullOrEmpty(usuario.Senha))
                return null;

            // Verificação da senha com PasswordHasher
            var result = _passwordHasher.VerifyHashedPassword(usuario, usuario.Senha, senha);
            if (result != PasswordVerificationResult.Success)
                return null;

            // Geração do token JWT
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Email, usuario.Email),
                new Claim(ClaimTypes.Name, usuario.Username),
                new Claim("DataCriacao", usuario.DataCriacao.ToString("o"))
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpireMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<bool> Register(UsuarioCreateDto usuarioDto)
        {
            usuarioDto.Email = usuarioDto.Email?.Trim().ToLower();
            usuarioDto.Senha = usuarioDto.Senha?.Trim();

            if (await _usuarioRepository.GetByEmailAsync(usuarioDto.Email) != null)
                return false;

            var usuario = new Usuario
            {
                Username = usuarioDto.Username?.Trim(),
                Nome = usuarioDto.Nome?.Trim(),
                Email = usuarioDto.Email,
                DataCriacao = DateTime.UtcNow,
                Ativo = true,
                CargoId = usuarioDto.CargoId
            };

            // Geração do hash da senha
            usuario.Senha = _passwordHasher.HashPassword(usuario, usuarioDto.Senha);

            await _usuarioRepository.AddAsync(usuario);

            // SE FOR CLIENTE (cargo_id = 4), CRIA REGISTRO NA TABELA clientes
            if (usuarioDto.CargoId == 4)
            {
                await CriarClienteAutomaticamente(usuario.Id);
            }

            return true;
        }

        private async Task CriarClienteAutomaticamente(int usuarioId)
        {
            try
            {
                var cliente = new Cliente
                {
                    UsuarioId = usuarioId,
                    CadastroPendente = true,
                    Cep = null,
                    Endereco = null,
                    Numero = null,
                    Bairro = null,
                    Cidade = null,
                    Estado = null
                };

                await _clienteRepository.AddAsync(cliente);
                Console.WriteLine($"Cliente criado automaticamente para usuário ID: {usuarioId}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao criar cliente automático: {ex.Message}");
                // Não lança exceção para não quebrar o registro do usuário
            }
        }

        public async Task<bool> ChangePassword(int usuarioId, string currentPassword, string newPassword)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(usuarioId);
            if (usuario == null)
                return false;

            // Verifica a senha atual
            var verificationResult = _passwordHasher.VerifyHashedPassword(
                usuario,
                usuario.Senha,
                currentPassword);

            if (verificationResult != PasswordVerificationResult.Success)
                return false;

            // Atualiza com a nova senha
            usuario.Senha = _passwordHasher.HashPassword(usuario, newPassword);
            await _usuarioRepository.UpdateAsync(usuario);
            return true;
        }
    }
}