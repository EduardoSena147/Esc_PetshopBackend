using AutoMapper;
using Esc_PetshopBackend.Data.Entities;
using Esc_PetshopBackend.Data.Repositories.Interface;
using Esc_PetshopBackend.DTOs;
using Esc_PetshopBackend.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Esc_PetshopBackend.Services
{
    public class PetService : IPetService
    {
        private readonly IPetRepository _petRepository;
        private readonly IClienteRepository _clienteRepository;
        private readonly ITipoAnimalRepository _tipoAnimalRepository;
        private readonly IMapper _mapper;

        public PetService(
            IPetRepository petRepository,
            IClienteRepository clienteRepository,
            ITipoAnimalRepository tipoAnimalRepository,
            IMapper mapper)
        {
            _petRepository = petRepository;
            _clienteRepository = clienteRepository;
            _tipoAnimalRepository = tipoAnimalRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PetDto>> GetAllAsync()
        {
            var pets = await _petRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<PetDto>>(pets);
        }

        public async Task<PetDto> GetByIdAsync(int id)
        {
            var pet = await _petRepository.GetByIdAsync(id);
            return _mapper.Map<PetDto>(pet);
        }

        public async Task<IEnumerable<PetDto>> GetByClienteIdAsync(int clienteId)
        {
            var pets = await _petRepository.GetByClienteIdAsync(clienteId);
            return _mapper.Map<IEnumerable<PetDto>>(pets);
        }

        public async Task<IEnumerable<PetDto>> GetByTipoAnimalIdAsync(int tipoAnimalId)
        {
            var pets = await _petRepository.GetByTipoAnimalIdAsync(tipoAnimalId);
            return _mapper.Map<IEnumerable<PetDto>>(pets);
        }

        public async Task<PetDto> CreateAsync(PetCreateDto petCreateDto)
        {
            if (!await _clienteRepository.ExistsAsync(petCreateDto.ClienteId))
                throw new KeyNotFoundException("Cliente não encontrado");

            if (!await _tipoAnimalRepository.ExistsAsync(petCreateDto.TipoAnimalId))
                throw new KeyNotFoundException("Tipo de animal não encontrado");

            var pet = _mapper.Map<Pet>(petCreateDto);
            pet.DataCadastro = DateTime.UtcNow;
            pet.Ativo = true;

            await _petRepository.AddAsync(pet);
            var petCompleto = await _petRepository.GetByIdAsync(pet.Id);
            return _mapper.Map<PetDto>(petCompleto);
        }

        public async Task<PetDto> UpdateAsync(int id, PetUpdateDto petUpdateDto)
        {
            var pet = await _petRepository.GetByIdAsync(id);
            if (pet == null)
                throw new KeyNotFoundException("Pet não encontrado");

            _mapper.Map(petUpdateDto, pet);
            await _petRepository.UpdateAsync(pet);

            return _mapper.Map<PetDto>(pet);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var pet = await _petRepository.GetByIdAsync(id);
            if (pet == null)
                return false;

            await _petRepository.DeleteAsync(id);
            return true;
        }
    }
}