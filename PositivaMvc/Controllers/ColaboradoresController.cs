using Microsoft.AspNetCore.Mvc;
using PositivaMvc.Models;
using PositivaMvc.Services;
using PositivaMvc.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PositivaMvc.Services.Exceptions;
using System.Diagnostics;

namespace PositivaMvc.Controllers
{
    public class ColaboradoresController : Controller
    {
        private readonly ColaboradorService _colaboradorService;
        private readonly EmpresaService _empresaService;
        private readonly LoginService _loginService;


        public ColaboradoresController(ColaboradorService colaboradorService, EmpresaService empresaService, LoginService loginService)
        {
            _colaboradorService = colaboradorService;
            _empresaService = empresaService;
            _loginService = loginService;
            
        }

        public async Task<IActionResult> Index()
        {
            var list = await _colaboradorService.FindAllAsync();
            return View(list);
        }

        public async Task<IActionResult> Create()
        {
            var empresas = await _empresaService.FindAllAssync();
            var logins = await _loginService.FindAllAsync();
            var viewModel = new ColaboradorFormViewModel { Empresas = empresas, Logins= logins };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Colaborador colaborador)
        {
            await _colaboradorService.Insert(colaborador);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não fornecido" });
            }

            var obj = await _colaboradorService.FindByIdAsync(id.Value);
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
                await _colaboradorService.RemoveAsync(id);
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

            var obj = await _colaboradorService.FindByIdAsync(id.Value);
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
                return RedirectToAction(nameof(Error), new { message = "Id não fornecido" });
            }
            var obj = await _colaboradorService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não localizado" });
            }

            List<Empresa> empresas = await _empresaService.FindAllAssync();
            List<Login> logins = await _loginService.FindAllAsync();
            ColaboradorFormViewModel viewModel = new ColaboradorFormViewModel { Colaborador = obj, Empresas = empresas, Logins= logins };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Colaborador colaborador)
        {
            if (id != colaborador.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não corresponde" });
            }
            try
            {
                await _colaboradorService.UpdateAsync(colaborador);
                return RedirectToAction(nameof(Index));
            }
            catch (NotFoundException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
            catch (DbConcurrencyException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return View(viewModel);
        }
    }
}

