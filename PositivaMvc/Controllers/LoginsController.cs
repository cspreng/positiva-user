using Microsoft.AspNetCore.Mvc;
using PositivaMvc.Models;

using PositivaMvc.Services;
using PositivaMvc.Models.ViewModels;
using System.Threading.Tasks;
using PositivaMvc.Services.Exceptions;
using System.Diagnostics;
using PositivaMvc.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace PositivaMvc.Controllers
{
    public class LoginsController : Controller
    {
        private readonly LoginService _loginService;
        private readonly PerfilService _perfilService;
        private readonly ApplicationDbContext _context;



        public LoginsController(LoginService loginService, PerfilService perfilService, ApplicationDbContext context)
        {
            _loginService = loginService;
            _perfilService = perfilService;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Login.Include(p => p.IdentityUser).Include(p => p.Perfil);
            return View(await applicationDbContext.ToListAsync());

        }

        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email");
            ViewData["PerfilId"] = new SelectList(_context.Perfil, "Id", "TipoPerfil");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PerfilId,UserId")] Login login)
        {
            if (ModelState.IsValid)
            {
                _context.Add(login);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email", login.UserId);
            ViewData["PerfilId"] = new SelectList(_context.Perfil, "Id", "TipoPerfil", login.PerfilId);
            return View(login);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não fornecido" });
            }

            var obj = await _context.Login
                 .Include(p => p.IdentityUser)
                 .Include(p => p.Perfil)
                 .FirstOrDefaultAsync(m => m.Id == id);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não localizado" });
            }
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _loginService.RemoveAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (IntegrityException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }



        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não fornecido" });
            }


            var obj = await _context.Login
                .Include(p => p.IdentityUser)
                .Include(p => p.Perfil)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não localizado" });
            }
            return View(obj);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var login = await _context.Login.FindAsync(id);
            if (login == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email", login.UserId);
            ViewData["PerfilId"] = new SelectList(_context.Perfil, "Id", "TipoPerfil", login.PerfilId);
            return View(login);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PerfilId,UserId")] Login login)
        {
            if (id != login.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(login);
                    await _context.SaveChangesAsync();
                }

                catch (DbUpdateConcurrencyException)
                {
                    if (!LoginExists(login.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));

            }

            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email", login.UserId);
            ViewData["PerfilId"] = new SelectList(_context.Perfil, "Id", "TipoPerfil", login.PerfilId);
            return View(login);
        }

        public IActionResult Error(string message)
        {
            var viewModel = new Models.ViewModels.ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return View(viewModel);
        }

        private bool LoginExists(int id)
        {
            return _context.Login.Any(e => e.Id == id);
        }
    }
}
