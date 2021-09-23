using PositivaMvc.Data;
using PositivaMvc.Models;
using PositivaMvc.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PositivaMvc.Services.Exceptions;

namespace PositivaMvc.Services
{
    public class LoginService
    {
        private readonly ApplicationDbContext _context;

        public LoginService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Login>> FindAllAsync()
        {
            return await _context.Login.ToListAsync();
        }

        public async Task Insert(Login obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task<Login> FindByIdAsync(int id)
        {
            return await _context.Login.Include(obj => obj.Perfil).FirstOrDefaultAsync(obj => obj.Id == id);
        }

        public async Task RemoveAsync(int id)
        {
            try
            {
                var obj = await _context.Login.FindAsync(id);
                _context.Login.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new IntegrityException(e.Message);
            }
        }

        public async Task UpdateAsync(Login obj)
        {
            bool hasAny = await _context.Login.AnyAsync(x => x.Id == obj.Id);
            if (!hasAny)
            {
                throw new NotFoundException("Usuario não Encontrado");
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

        internal Task<List<Colaborador>> FindAllAssync()
        {
            throw new NotImplementedException();
        }
    }
}
