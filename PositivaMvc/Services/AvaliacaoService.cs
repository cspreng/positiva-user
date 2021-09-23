using PositivaMvc.Data;
using PositivaMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PositivaMvc.Services.Exceptions;

namespace PositivaMvc.Services
{
    public class AvaliacaoService
    {
        private readonly ApplicationDbContext _context;

        public AvaliacaoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Avaliacao>> FindAllAsync()
        {
            return await _context.Avaliacao.ToListAsync();
        }

        public async Task Insert(Avaliacao obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task<Avaliacao> FindByIdAsync(int id)
        {
            return await _context.Avaliacao.Include(obj => obj.Colaborador).FirstOrDefaultAsync(obj => obj.Id == id);
        }

        public async Task RemoveAsync(int id)
        {
            try
            {
                var obj = await _context.Avaliacao.FindAsync(id);
                _context.Avaliacao.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new IntegrityException(e.Message);
            }
        }

        public async Task UpdateAsync(Avaliacao obj)
        {
            bool hasAny = await _context.Avaliacao.AnyAsync(x => x.Id == obj.Id);
            if (!hasAny)
            {
                throw new NotFoundException("Colaborador não Encontrado");
            }
            try
            {
                _context.Update(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }

    }
}