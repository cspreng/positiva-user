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
    public class PerguntasController : Controller
    {
        private readonly PerguntaSevice _perguntaService;
        private readonly GrupoPerguntasService _grupoPerguntasService;

        public PerguntasController(PerguntaSevice perguntaSevice, GrupoPerguntasService grupoPerguntasService)
        {
            _perguntaService = perguntaSevice;
            _grupoPerguntasService = grupoPerguntasService;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _perguntaService.FindAllAsync();
            return View(list);
        }

        public async Task<IActionResult> Create()
        {
            var grupoperguntass = await _grupoPerguntasService.FindAllAsync();
            var viewModel = new PerguntaFormViewModel { GrupoPerguntass = grupoperguntass };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Pergunta pergunta)
        {
            await _perguntaService.Insert(pergunta);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não fornecido" });
            }

            var obj = await _perguntaService.FindByIdAsync(id.Value);
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
                await _perguntaService.RemoveAsync(id);
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

            var obj = await _perguntaService.FindByIdAsync(id.Value);
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
            var obj = await _perguntaService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não localizado" });
            }

            List<GrupoPerguntas> grupoPerguntass = await _grupoPerguntasService.FindAllAsync();
            PerguntaFormViewModel viewModel = new PerguntaFormViewModel { Pergunta = obj, GrupoPerguntass = grupoPerguntass };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Pergunta pergunta)
        {
            if (id != pergunta.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não corresponde" });
            }
            try
            {
                await _perguntaService.UpdateAsync(pergunta);
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
