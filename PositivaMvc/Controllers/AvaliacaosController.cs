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
    public class AvaliacaosController : Controller
    {
        private readonly AvaliacaoService _avaliacaoService;
        private readonly ColaboradorService _colaboradorService;

        public AvaliacaosController(AvaliacaoService avaliacaoService, ColaboradorService colaboradorService)
        {
            _avaliacaoService = avaliacaoService;
            _colaboradorService = colaboradorService;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _avaliacaoService.FindAllAsync();
            return View(list);
        }

        public async Task<IActionResult> Create()
        {
            var colaboradors = await _colaboradorService.FindAllAsync();
            var viewModel = new AvaliacaoFormViewModel { Colaboradors = colaboradors };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Avaliacao avaliacao)
        {
            await _avaliacaoService.Insert(avaliacao);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não fornecido" });
            }

            var obj = await _avaliacaoService.FindByIdAsync(id.Value);
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
                await _avaliacaoService.RemoveAsync(id);
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

            var obj = await _avaliacaoService.FindByIdAsync(id.Value);
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
            var obj = await _avaliacaoService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não localizado" });
            }

            List<Colaborador> colaboradors = await _colaboradorService.FindAllAsync();
            AvaliacaoFormViewModel viewModel = new AvaliacaoFormViewModel { Avaliacao = obj, Colaboradors = colaboradors };
            return View(viewModel);
        }

        [HttpPost]        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Avaliacao avaliacao)
        {
            if (id != avaliacao.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não corresponde" });
            }
            try
            {
                await _avaliacaoService.UpdateAsync(avaliacao);
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
