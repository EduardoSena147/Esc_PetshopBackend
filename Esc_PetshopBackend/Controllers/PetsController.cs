using Esc_PetshopBackend.DTOs;
using Esc_PetshopBackend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Esc_PetshopBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PetsController : ControllerBase
    {
        private readonly IPetService _petService;

        public PetsController(IPetService petService)
        {
            _petService = petService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PetDto>>> GetAll()
        {
            var pets = await _petService.GetAllAsync();
            return Ok(pets);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PetDto>> GetById(int id)
        {
            try
            {
                var pet = await _petService.GetByIdAsync(id);
                return Ok(pet);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpGet("cliente/{clienteId}")]
        public async Task<ActionResult<IEnumerable<PetDto>>> GetByClienteId(int clienteId)
        {
            var pets = await _petService.GetByClienteIdAsync(clienteId);
            return Ok(pets);
        }

        [HttpGet("tipo-animal/{tipoAnimalId}")]
        public async Task<ActionResult<IEnumerable<PetDto>>> GetByTipoAnimalId(int tipoAnimalId)
        {
            var pets = await _petService.GetByTipoAnimalIdAsync(tipoAnimalId);
            return Ok(pets);
        }

        [HttpPost]
        public async Task<ActionResult<PetDto>> Create([FromBody] PetCreateDto petCreateDto)
        {
            try
            {
                var pet = await _petService.CreateAsync(petCreateDto);
                return CreatedAtAction(nameof(GetById), new { id = pet.Id }, pet);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PetDto>> Update(int id, [FromBody] PetUpdateDto petUpdateDto)
        {
            try
            {
                var pet = await _petService.UpdateAsync(id, petUpdateDto);
                return Ok(pet);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _petService.DeleteAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}