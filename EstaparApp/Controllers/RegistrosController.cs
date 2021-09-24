using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EstaparApp.Data;
using EstaparApp.Models;

namespace EstaparApp.Controllers
{
    public class RegistrosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RegistrosController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(BuscaRegistroLista());
        }

        public IActionResult Details(long id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var registro = BuscaRegistroId(id);
            if (registro == null)
            {
                return NotFound();
            }

            return View(registro);
        }

        public IActionResult Create()
        {
            ListaDados();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Entrada,HoraEntrada,Saida,HoraSaida,ModeloId,Modelo,ManobristaId,Manobrista,Placa,Manobrado")] Registro registro)
        {
            if (ModelState.IsValid)
            {
                _context.Add(registro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(registro);
        }

        public IActionResult Edit(long id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var registro = BuscaRegistroId(id);
            if (registro == null)
            {
                return NotFound();
            }

            ListaDados();
            return View(registro);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Entrada,HoraEntrada,Saida,HoraSaida,ModeloId,Modelo,ManobristaId,Manobrista,Placa,Manobrado")] Registro registro)
        {
            if (id != registro.Id)
            {
                return NotFound();
            }

            ModelState.Remove("Placa");
            if (ModelState.IsValid)
            {
                try
                {
                    var reg = BuscaRegistroId(id);
                    reg.Saida = registro.Saida;
                    reg.HoraSaida = registro.HoraSaida;
                    reg.Manobrado = registro.Manobrado;

                    _context.Update(reg);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RegistroExists(registro.Id))
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
            return View(registro);
        }

        public IActionResult Delete(long id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var registro = BuscaRegistroId(id);
            if (registro == null)
            {
                return NotFound();
            }

            return View(registro);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var registro = BuscaRegistroId(id);
            _context.Registro.Remove(registro);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RegistroExists(long id)
        {
            return _context.Registro.Any(e => e.Id == id);
        }

        private void ListaDados()
        {
            var listaManobristas = new List<SelectListItem>();
            listaManobristas.Add(new SelectListItem() { Text = "-- Selecione --", Value = "0" });
            listaManobristas.AddRange(_context.Manobrista.ToList().Select(c => new SelectListItem() { Text = c.Nome, Value = c.Id.ToString() }).ToList());
            ViewBag.Manobristas = listaManobristas;

            var listaMarcas = new List<SelectListItem>();
            listaMarcas.Add(new SelectListItem() { Text = "-- Selecione --", Value = "0" });
            listaMarcas.AddRange(_context.Marca.ToList().Select(c => new SelectListItem() { Text = c.Nome, Value = c.Id.ToString() }).ToList());
            ViewBag.Marcas = listaMarcas;
        }

        public JsonResult ListaModelo(long id)
        {
            var listaModelo = new List<SelectListItem>();
            listaModelo.Add(new SelectListItem() { Text = "-- Selecione --", Value = "0" });
            listaModelo.AddRange(_context.Modelo.Join(
                    _context.Marca,
                        mod => mod.MarcaId,
                        mar => mar.Id,
                        (mod, mar) => new Modelo()
                        {
                            Id = mod.Id,
                            MarcaId = mod.MarcaId,
                            Marca = mar,
                            Nome = mod.Nome
                        }).Where(c => c.MarcaId == id)
                .ToList().Select(c => new SelectListItem() { Text = c.Nome, Value = c.Id.ToString() }).ToList());
            ViewBag.Modelos = listaModelo;

            return Json(ViewBag.Modelos);
        }

        private Registro BuscaRegistroId(long id)
        {
            return _context.Registro.Join(
                    _context.Modelo,
                        reg => reg.ModeloId,
                        mod => mod.Id,
                        (reg, mod) => new { reg, mod }
                    )
                    .Join(
                    _context.Manobrista,
                        comb => comb.reg.ManobristaId,
                        man => man.Id,
                        (comb, man) => new Registro()
                        {

                            Id = comb.reg.Id,
                            Entrada = comb.reg.Entrada,
                            HoraEntrada = comb.reg.HoraEntrada,
                            Saida = comb.reg.Saida,
                            HoraSaida = comb.reg.HoraSaida,
                            ManobristaId = comb.reg.ManobristaId,
                            Manobrista = man.Nome,
                            ModeloId = comb.reg.ModeloId,
                            Modelo = comb.mod.Nome,
                            Placa = comb.reg.Placa,
                            Manobrado = comb.reg.Manobrado
                        }).Where(
                            c => c.Id == id
                    ).FirstOrDefault();
        }

        private List<Registro> BuscaRegistroLista()
        {
            return _context.Registro.Join(
                    _context.Modelo,
                        reg => reg.ModeloId,
                        mod => mod.Id,
                        (reg, mod) => new { reg, mod }
                    )
                    .Join(
                    _context.Manobrista,
                        comb => comb.reg.ManobristaId,
                        man => man.Id,
                        (comb, man) => new Registro() { 
                        
                            Id = comb.reg.Id,
                            Entrada = comb.reg.Entrada,
                            HoraEntrada = comb.reg.HoraEntrada,
                            Saida = comb.reg.Saida,
                            HoraSaida = comb.reg.HoraSaida,
                            ManobristaId = comb.reg.ManobristaId,
                            Manobrista = man.Nome,
                            ModeloId = comb.reg.ModeloId,
                            Modelo = comb.mod.Nome,
                            Placa = comb.reg.Placa,
                            Manobrado  = comb.reg.Manobrado
                        }).ToList();
        }
    }
}
