using Esc_PetshopBackend.Data.Context;
using Esc_PetshopBackend.Data.Entities;
using Esc_PetshopBackend.Data.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace Esc_PetshopBackend.Data.Repositories
{
    public class TipoAgendamentoRepository : ITipoAgendamentoRepository
    {
        private readonly AppDbContext _context;

        public TipoAgendamentoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TipoAgendamento>> GetAllAsync()
        {
            return await _context.TiposAgendamentos
                .OrderBy(t => t.Descricao)
                .ToListAsync();
        }
        public async Task<TipoAgendamento?> GetByIdAsync(int id)
        {
            return await _context.TiposAgendamentos
                .FirstOrDefaultAsync(t => t.Id == id);
        }
        public async Task<TipoAgendamento?> GetByDescricaoAsync(string descricao)
        {
            return await _context.TiposAgendamentos
                .FirstOrDefaultAsync(t => t.Descricao.ToLower() == descricao.ToLower());
        }
        public async Task AddAsync(TipoAgendamento tipoAgendamento)
        {
            await _context.TiposAgendamentos.AddAsync(tipoAgendamento);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(TipoAgendamento tipoAgendamento)
        {
            _context.TiposAgendamentos.Update(tipoAgendamento);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var tipoAgendamento = await _context.TiposAgendamentos.FindAsync(id);
            if (tipoAgendamento != null)
            {
                _context.TiposAgendamentos.Remove(tipoAgendamento);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.TiposAgendamentos.AnyAsync(t => t.Id == id);
        }
    }
}
