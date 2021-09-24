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
    public class ManobristasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ManobristasController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Manobrista.ToListAsync());
        }

        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var manobrista = await _context.Manobrista.FirstOrDefaultAsync(m => m.Id == id);
            if (manobrista == null)
            {
                return NotFound();
            }

            return View(manobrista);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,DataNascimento,Cpf")] Manobrista manobrista)
        {
            if (ModelState.IsValid)
            {

                if (!Validator.ValidaCpf(manobrista.Cpf.Replace(".", "").Replace("-", ""))){
                    ViewBag.Message = Messages.CPFINVALIDO;
                    return View(manobrista);
                }

                if (ManobristaCpf(manobrista.Cpf.Replace(".", "").Replace("-", ""), 0))
                {
                    ViewBag.Message = Messages.CPFEXISTENTE;
                    return View(manobrista);
                }

                manobrista.Cpf = manobrista.Cpf.Replace(".", "").Replace("-", "");

                _context.Add(manobrista);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(manobrista);
        }

        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Manobrista = await _context.Manobrista.FindAsync(id);
            if (Manobrista == null)
            {
                return NotFound();
            }
            return View(Manobrista);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Nome,DataNascimento,Cpf")] Manobrista manobrista)
        {
            if (id != manobrista.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (!Validator.ValidaCpf(manobrista.Cpf.Replace(".", "").Replace("-", ""))){
                        ViewBag.Message = Messages.CPFINVALIDO;
                        return View(manobrista);
                    }

                    if (ManobristaCpf(manobrista.Cpf.Replace(".", "").Replace("-", ""), manobrista.Id))
                    {
                        ViewBag.Message = Messages.CPFEXISTENTE;
                        return View(manobrista);
                    }

                    _context.Update(manobrista);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ManobristaId(manobrista.Id))
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
            return View(manobrista);
        }

        public async Task<IActionResult> Delete(long id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var Manobrista = await _context.Manobrista.FirstOrDefaultAsync(m => m.Id == id);
            if (Manobrista == null)
            {
                return NotFound();
            }

            return View(Manobrista);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var manobrista = await _context.Manobrista.FindAsync(id);

            if (ManobristaVinculado(id))
            {
                ViewBag.Message = Messages.MANOBRISTAVINCULADO;
                return View(manobrista);
            }
            
            _context.Manobrista.Remove(manobrista);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ManobristaId(long id)
        {
            return _context.Manobrista.Any(e => e.Id == id);
        }

        private bool ManobristaCpf(string cpf, long id)
        {
            return _context.Manobrista.Any(e => e.Cpf == cpf && e.Id != id);
        }

        private bool ManobristaVinculado(long id)
        {
            return _context.Registro.Any(e => e.ManobristaId == id);
        }
    }
}
