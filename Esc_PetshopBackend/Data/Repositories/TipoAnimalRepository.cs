using Esc_PetshopBackend.Data.Context;
using Esc_PetshopBackend.Data.Entities;
using Esc_PetshopBackend.Data.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace Esc_PetshopBackend.Data.Repositories
{
    public class TipoAnimalRepository : ITipoAnimalRepository
    {
        private readonly AppDbContext _context;

        public TipoAnimalRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TipoAnimal>> GetAllAsync()
        {
            return await _context.TiposAnimais
                .OrderBy(t => t.Descricao)
                .ToListAsync();
        }
        public async Task<TipoAnimal?> GetByIdAsync(int id)
        {
            return await _context.TiposAnimais
                .FirstOrDefaultAsync(t => t.Id == id);
        }
        public async Task<TipoAnimal?> GetByDescricaoAsync(string descricao)
        {
            return await _context.TiposAnimais
                .FirstOrDefaultAsync(t => t.Descricao.ToLower() == descricao.ToLower());
        }
        public async Task AddAsync(TipoAnimal tipoAnimal)
        {
            await _context.TiposAnimais.AddAsync(tipoAnimal);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(TipoAnimal tipoAnimal)
        {
            _context.TiposAnimais.Update(tipoAnimal);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var tipoAnimal = await _context.TiposAnimais.FindAsync(id);
            if (tipoAnimal != null)
            {
                _context.TiposAnimais.Remove(tipoAnimal);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.TiposAnimais.AnyAsync(t => t.Id == id);
        }
    }
}
