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
    public class ColaboradorService
    {
        private readonly ApplicationDbContext _context;

        public ColaboradorService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Colaborador>> FindAllAsync()
        {
            return await _context.Colaborador.ToListAsync();
        }
        
        public async Task Insert(Colaborador obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task<Colaborador> FindByIdAsync(int id)
        {
            return await _context.Colaborador.Include(obj=>obj.Empresa).FirstOrDefaultAsync(obj => obj.Id == id);
        }

        public async Task RemoveAsync(int id)
        {
            try
            {
                var obj = await _context.Colaborador.FindAsync(id);
                _context.Colaborador.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new IntegrityException(e.Message);
            }
        }

        public async Task UpdateAsync(Colaborador obj)
        {
            bool hasAny = await _context.Colaborador.AnyAsync(x => x.Id == obj.Id);
            if (!hasAny)
            {
                throw new NotFoundException("Colaborador não Encontrado");
            }
            try
            {
                _context.Update(obj);
                await _context.SaveChangesAsync();
            }
            catch(DbConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }

        internal Task<List<Avaliacao>> FindAllAssync()
        {
            throw new NotImplementedException();
        }
    }
}
