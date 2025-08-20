using AutoMapper;
using Esc_PetshopBackend.Data.Entities;
using Esc_PetshopBackend.Data.Repositories.Interface;
using Esc_PetshopBackend.DTOs;

namespace Esc_PetshopBackend.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;

        public ClienteService(
            IClienteRepository clienteRepository,
            IUsuarioRepository usuarioRepository,
            IMapper mapper)
        {
            _clienteRepository = clienteRepository;
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ClienteDto>> GetAllAsync()
        {
            var clientes = await _clienteRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ClienteDto>>(clientes);
        }

        public async Task<ClienteDto> GetByIdAsync(int id)
        {
            var cliente = await _clienteRepository.GetByIdAsync(id);
            return _mapper.Map<ClienteDto>(cliente);
        }

        public async Task<ClienteDto> GetByUsuarioIdAsync(int usuarioId)
        {
            var cliente = await _clienteRepository.GetByUsuarioIdAsync(usuarioId);
            return _mapper.Map<ClienteDto>(cliente);
        }

        public async Task<ClienteDto> CreateAsync(int usuarioId, ClienteCreateDto clienteCreateDto)
        {
            // Verifica se usuário existe
            var usuario = await _usuarioRepository.GetByIdAsync(usuarioId);
            if (usuario == null)
                throw new KeyNotFoundException("Usuário não encontrado");

            // Verifica se já existe cliente para este usuário
            var clienteExistente = await _clienteRepository.GetByUsuarioIdAsync(usuarioId);
            if (clienteExistente != null)
                throw new InvalidOperationException("Já existe um cliente para este usuário");

            var cliente = _mapper.Map<Cliente>(clienteCreateDto);
            cliente.UsuarioId = usuarioId;
            cliente.CadastroPendente = false;

            await _clienteRepository.AddAsync(cliente);
            return _mapper.Map<ClienteDto>(cliente);
        }

        public async Task<ClienteDto> UpdateAsync(int id, ClienteUpdateDto clienteUpdateDto)
        {
            var cliente = await _clienteRepository.GetByIdAsync(id);
            if (cliente == null)
                throw new KeyNotFoundException("Cliente não encontrado");

            _mapper.Map(clienteUpdateDto, cliente);

            // Verifica se o cadastro está completo após atualização
            cliente.CadastroPendente = !ValidarCadastroCompleto(cliente);

            await _clienteRepository.UpdateAsync(cliente);
            return _mapper.Map<ClienteDto>(cliente);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var cliente = await _clienteRepository.GetByIdAsync(id);
            if (cliente == null)
                return false;

            await _clienteRepository.DeleteAsync(id);
            return true;
        }

        public async Task<bool> CompletarCadastroAsync(int usuarioId, ClienteCreateDto clienteCreateDto)
        {
            var cliente = await _clienteRepository.GetByUsuarioIdAsync(usuarioId);
            if (cliente == null)
                throw new KeyNotFoundException("Cliente não encontrado");

            _mapper.Map(clienteCreateDto, cliente);
            cliente.CadastroPendente = !ValidarCadastroCompleto(cliente);

            await _clienteRepository.UpdateAsync(cliente);
            return !cliente.CadastroPendente;
        }

        public async Task<bool> ValidarCadastroCompletoAsync(int usuarioId)
        {
            var cliente = await _clienteRepository.GetByUsuarioIdAsync(usuarioId);
            return cliente != null && !cliente.CadastroPendente;
        }

        private bool ValidarCadastroCompleto(Cliente cliente)
        {
            return !string.IsNullOrEmpty(cliente.Cep) &&
                   !string.IsNullOrEmpty(cliente.Endereco) &&
                   !string.IsNullOrEmpty(cliente.Numero) &&
                   !string.IsNullOrEmpty(cliente.Bairro) &&
                   !string.IsNullOrEmpty(cliente.Cidade) &&
                   !string.IsNullOrEmpty(cliente.Estado);
        }
    }
}