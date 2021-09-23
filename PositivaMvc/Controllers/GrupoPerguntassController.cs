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
    public class GrupoPerguntasController : Controller
    {
        private readonly GrupoPerguntasService _grupoPerguntasService;
        private readonly AvaliacaoService _avaliacaoService;

        public GrupoPerguntasController(GrupoPerguntasService grupoPerguntasService, AvaliacaoService avaliacaoService)
        {
            _grupoPerguntasService = grupoPerguntasService;
            _avaliacaoService = avaliacaoService;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _grupoPerguntasService.FindAllAsync();
            return View(list);
        }

        public async Task<IActionResult> Create()
        {
            var avaliacaos = await _avaliacaoService.FindAllAsync();
            var viewModel = new GrupoPerguntasFormViewModel { Avaliacaos = avaliacaos };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GrupoPerguntas grupoPerguntas)
        {
            await _grupoPerguntasService.Insert(grupoPerguntas);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não fornecido" });
            }

            var obj = await _grupoPerguntasService.FindByIdAsync(id.Value);
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
                await _grupoPerguntasService.RemoveAsync(id);
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

            var obj = await _grupoPerguntasService.FindByIdAsync(id.Value);
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
            var obj = await _grupoPerguntasService.FindByIdAsync (id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não localizado" });
            }

            List<Avaliacao> avaliacaos = await _avaliacaoService.FindAllAsync();
            GrupoPerguntasFormViewModel viewModel = new GrupoPerguntasFormViewModel { GrupoPerguntas = obj, Avaliacaos = avaliacaos };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, GrupoPerguntas grupoPerguntas)
        {
            if (id != grupoPerguntas.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não corresponde" });
            }
            try
            {
                await _grupoPerguntasService.UpdateAsync(grupoPerguntas);
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
