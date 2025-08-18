using AutoMapper;
using Esc_PetshopBackend.Data.Entities;
using Esc_PetshopBackend.Data.Repositories;
using Esc_PetshopBackend.Data.Repositories.Interface;
using Esc_PetshopBackend.DTOs;

namespace Esc_PetshopBackend.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;

        public UsuarioService(IUsuarioRepository usuarioRepository, IMapper mapper)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UsuarioDto>> GetAllAsync()
        {
            var usuarios = await _usuarioRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<UsuarioDto>>(usuarios);
        }

        public async Task<UsuarioDto> GetByIdAsync(int id)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id);
            return _mapper.Map<UsuarioDto>(usuario);
        }

        public async Task<UsuarioDto> CreateAsync(UsuarioCreateDto usuarioCreateDto)
        {
            var usuario = _mapper.Map<Usuario>(usuarioCreateDto);
            await _usuarioRepository.AddAsync(usuario);
            return _mapper.Map<UsuarioDto>(usuario);
        }

        public async Task UpdateAsync(int id, UsuarioUpdateDto usuarioUpdateDto)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id);
            if (usuario == null)
            {
                throw new KeyNotFoundException("Usuário não encontrado");
            }

            // Mantém a data original (não atualiza)
            var dataOriginal = usuario.DataCriacao;

            _mapper.Map(usuarioUpdateDto, usuario);

            // Restaura a data original
            usuario.DataCriacao = dataOriginal;

            // Garante que está em UTC
            if (usuario.DataCriacao.Kind != DateTimeKind.Utc)
            {
                usuario.DataCriacao = DateTime.SpecifyKind(usuario.DataCriacao, DateTimeKind.Utc);
            }

            await _usuarioRepository.UpdateAsync(usuario);
        }

        public async Task DeleteAsync(int id)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id);
            if (usuario == null)
            {
                throw new KeyNotFoundException("Usuário não encontrado");
            }

            await _usuarioRepository.DeleteAsync(id);
        }
    }
}