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
    public class PerguntaSevice
    {
        private readonly ApplicationDbContext _context;

        public PerguntaSevice(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Pergunta>> FindAllAsync()
        {
            return await _context.Pergunta.ToListAsync();
        }

        public async Task Insert(Pergunta obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task<Pergunta> FindByIdAsync(int id)
        {
            return await _context.Pergunta.Include(obj => obj.GrupoPerguntas).FirstOrDefaultAsync(obj => obj.Id == id);
        }

        public async Task RemoveAsync(int id)
        {
            try
            {
                var obj = await _context.Pergunta.FindAsync(id);
                _context.Pergunta.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new IntegrityException(e.Message);
            }
        }

        public async Task UpdateAsync(Pergunta obj)
        {
            bool hasAny = await _context.Pergunta.AnyAsync(x => x.Id == obj.Id);
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

        internal Task<List<GrupoPerguntas>> FindAllAssync()
        {
            throw new NotImplementedException();
        }
    }
}
