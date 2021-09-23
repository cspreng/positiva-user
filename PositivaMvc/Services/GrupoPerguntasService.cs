using Microsoft.EntityFrameworkCore;
using PositivaMvc.Data;
using PositivaMvc.Models;
using PositivaMvc.Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PositivaMvc.Services
{
    public class GrupoPerguntasService
    {

        private readonly ApplicationDbContext _context;

        public GrupoPerguntasService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<GrupoPerguntas>> FindAllAsync()
        {
            return await _context.GrupoPerguntas.ToListAsync();
        }

        public async Task Insert(GrupoPerguntas obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task<GrupoPerguntas>FindByIdAsync(int id)
        {
            return await _context.GrupoPerguntas.Include(obj => obj.Avaliacao).FirstOrDefaultAsync(obj => obj.Id == id);
        }

        public async Task RemoveAsync(int id)
        {
            try
            {
                var obj = await _context.GrupoPerguntas.FindAsync(id);
                _context.GrupoPerguntas.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new IntegrityException(e.Message);
            }
        }

        public async Task UpdateAsync(GrupoPerguntas obj)
        {
            bool hasAny = await _context.GrupoPerguntas.AnyAsync(x => x.Id == obj.Id);
            if (!hasAny)
            {
                throw new DllNotFoundException("Avaliação não Encontrada");
            }
            try
            {
                _context.Update(obj);
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }
        

    }
}
