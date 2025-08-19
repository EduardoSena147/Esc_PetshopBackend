using Esc_PetshopBackend.Data.Context;
using Esc_PetshopBackend.Data.Entities;
using Esc_PetshopBackend.Data.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Esc_PetshopBackend.Data.Repositories
{
    public class CargoRepository : ICargoRepository
    {
        private readonly AppDbContext _context;

        public CargoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Cargo>> GetAllAsync()
        {
            return await _context.Cargos
                .OrderBy(c => c.Descricao)
                .ToListAsync();
        }

        public async Task<Cargo> GetByIdAsync(int id)
        {
            return await _context.Cargos
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Cargo> GetByDescricaoAsync(string descricao)
        {
            return await _context.Cargos
                .FirstOrDefaultAsync(c => c.Descricao.ToLower() == descricao.ToLower());
        }

        public async Task AddAsync(Cargo cargo)
        {
            await _context.Cargos.AddAsync(cargo);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Cargo cargo)
        {
            _context.Cargos.Update(cargo);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var cargo = await _context.Cargos.FindAsync(id);
            if (cargo != null)
            {
                _context.Cargos.Remove(cargo);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Cargos
                .AnyAsync(c => c.Id == id);
        }
    }
}
