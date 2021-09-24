using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EstaparApp.Data;
using EstaparApp.Models;
using EstaparApp.Util;

namespace EstaparApp.Controllers
{
    public class ModelosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ModelosController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(BuscaModeloLista());
        }

        public IActionResult Details(long id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var modelo = BuscaModeloId(id);
            if (modelo == null)
            {
                return NotFound();
            }

            return View(modelo);
        }

        public IActionResult Create()
        {
            ListaMarca();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,MarcaId,Marca")] Modelo modelo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(modelo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(modelo);
        }

        public IActionResult Edit(long id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var modelo = BuscaModeloId(id);
            if (modelo == null)
            {
                return NotFound();
            }

            ListaMarca();
            return View(modelo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Nome,MarcaId,Marca")] Modelo modelo)
        {
            if (id != modelo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(modelo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ModeloExists(modelo.Id))
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
            return View(modelo);
        }

        public IActionResult Delete(long id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var modelo = BuscaModeloId(id);
            if (modelo == null)
            {
                return NotFound();
            }

            return View(modelo);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var modelo = BuscaModeloId(id);

            if (ModeloVinculado(id))
            {
                ViewBag.Message = Messages.MODELOVINCULADO;
                return View(modelo);
            }

            _context.Modelo.Remove(modelo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ModeloExists(long id)
        {
            return _context.Modelo.Any(e => e.Id == id);
        }

        private void ListaMarca()
        {
            var listaMarcas = new List<SelectListItem>();
            listaMarcas.Add(new SelectListItem() { Text = "-- Selecione --", Value = "0" });
            listaMarcas.AddRange(_context.Marca.ToList().Select(c => new SelectListItem() { Text = c.Nome, Value = c.Id.ToString() }).ToList());
            ViewBag.Marcas = listaMarcas;
        }

        private Modelo BuscaModeloId(long id)
        {
            return _context.Modelo.Join(
                    _context.Marca,
                        mod => mod.MarcaId,
                        mar => mar.Id,
                        (mod, mar) => new Modelo()
                        {
                            Id = mod.Id,
                            MarcaId = mod.MarcaId,
                            Marca = mar,
                            Nome = mod.Nome
                        }).Where(
                            c => c.Id == id
                    ).FirstOrDefault();
        }

        private List<Modelo> BuscaModeloLista()
        {
            return _context.Modelo.Join(
                    _context.Marca,
                        mod => mod.MarcaId,
                        mar => mar.Id,
                        (mod, mar) => new Modelo()
                        {
                            Id = mod.Id,
                            MarcaId = mod.MarcaId,
                            Marca = mar,
                            Nome = mod.Nome
                        }).ToList();
        }

        private bool ModeloVinculado(long id)
        {
            return _context.Registro.Any(e => e.ModeloId == id);
        }
    }
}
