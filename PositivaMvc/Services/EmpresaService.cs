using PositivaMvc.Data;
using PositivaMvc.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PositivaMvc.Services
{
    public class EmpresaService
    {
        private readonly ApplicationDbContext _context;

        public EmpresaService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Empresa>> FindAllAssync()
        {
            return await _context.Empresa.OrderBy(x => x.CadastroEmpresa).ToListAsync();
        }
    }
}
