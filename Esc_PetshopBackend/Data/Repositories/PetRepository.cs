using Esc_PetshopBackend.Data.Context;
using Esc_PetshopBackend.Data.Entities;
using Esc_PetshopBackend.Data.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Esc_PetshopBackend.Data.Repositories
{
    public class PetRepository : IPetRepository
    {
        private readonly AppDbContext _context;

        public PetRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Pet>> GetAllAsync()
        {
            return await _context.Pets
                .Include(p => p.Cliente)
                .Include(p => p.TipoAnimal)
                .Where(p => p.Ativo)
                .OrderBy(p => p.Nome)
                .ToListAsync();
        }

        public async Task<Pet> GetByIdAsync(int id)
        {
            return await _context.Pets
                .Include(p => p.Cliente)
                .Include(p => p.TipoAnimal)
                .FirstOrDefaultAsync(p => p.Id == id && p.Ativo);
        }

        public async Task<IEnumerable<Pet>> GetByClienteIdAsync(int clienteId)
        {
            return await _context.Pets
                .Include(p => p.TipoAnimal)
                .Where(p => p.ClienteId == clienteId && p.Ativo)
                .OrderBy(p => p.Nome)
                .ToListAsync();
        }

        public async Task<IEnumerable<Pet>> GetByTipoAnimalIdAsync(int tipoAnimalId)
        {
            return await _context.Pets
                .Include(p => p.Cliente)
                .Where(p => p.TipoAnimalId == tipoAnimalId && p.Ativo)
                .OrderBy(p => p.Nome)
                .ToListAsync();
        }

        public async Task AddAsync(Pet pet)
        {
            await _context.Pets.AddAsync(pet);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Pet pet)
        {
            _context.Pets.Update(pet);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var pet = await _context.Pets.FindAsync(id);
            if (pet != null)
            {
                pet.Ativo = false; // Soft delete
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Pets.AnyAsync(p => p.Id == id && p.Ativo);
        }
    }
}