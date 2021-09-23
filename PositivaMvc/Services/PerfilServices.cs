using PositivaMvc.Data;
using PositivaMvc.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PositivaMvc.Services
{
    public class PerfilService
    {
        private readonly ApplicationDbContext _context;

        public PerfilService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Perfil>> FindAllAsync()
        {
            return await _context.Perfil.OrderBy(x => x.TipoPerfil).ToListAsync();
        }
    }
}
