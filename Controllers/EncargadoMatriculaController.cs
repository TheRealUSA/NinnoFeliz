using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NinnoFeliz.Data;
using NinnoFeliz.Models;

namespace NinnoFeliz.Controllers
{
    public class EncargadoMatriculaController : Controller
    {
        private readonly NinnoFelizContext _context;

        public EncargadoMatriculaController(NinnoFelizContext context)
        {
            _context = context;
        }

        // GET: EncargadoMatricula
        public async Task<IActionResult> Index()
        {
            var ninnoFelizContext = _context.EncargadoMatriculas.Include(e => e.IdEncargadoNavigation).Include(e => e.NumeroMatriculaNavigation);
            return View(await ninnoFelizContext.ToListAsync());
        }

        // GET: EncargadoMatricula/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var encargadoMatricula = await _context.EncargadoMatriculas
                .Include(e => e.IdEncargadoNavigation)
                .Include(e => e.NumeroMatriculaNavigation)
                .FirstOrDefaultAsync(m => m.IdEncargadoMatricula == id);
            if (encargadoMatricula == null)
            {
                return NotFound();
            }

            return View(encargadoMatricula);
        }

        // GET: EncargadoMatricula/Create
        public IActionResult Create()
        {
            ViewData["IdEncargado"] = new SelectList(_context.Encargados, "IdEncargado", "Apell1Encargado");
            ViewData["NumeroMatricula"] = new SelectList(_context.Matriculas, "NumeroMatricula", "NumeroMatricula");
            return View();
        }

        // POST: EncargadoMatricula/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdEncargadoMatricula,NumeroMatricula,IdEncargado")] EncargadoMatricula encargadoMatricula)
        {
            if (ModelState.IsValid)
            {
                _context.Add(encargadoMatricula);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdEncargado"] = new SelectList(_context.Encargados, "IdEncargado", "Apell1Encargado", encargadoMatricula.IdEncargado);
            ViewData["NumeroMatricula"] = new SelectList(_context.Matriculas, "NumeroMatricula", "NumeroMatricula", encargadoMatricula.NumeroMatricula);
            return View(encargadoMatricula);
        }

        // GET: EncargadoMatricula/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var encargadoMatricula = await _context.EncargadoMatriculas.FindAsync(id);
            if (encargadoMatricula == null)
            {
                return NotFound();
            }
            ViewData["IdEncargado"] = new SelectList(_context.Encargados, "IdEncargado", "Apell1Encargado", encargadoMatricula.IdEncargado);
            ViewData["NumeroMatricula"] = new SelectList(_context.Matriculas, "NumeroMatricula", "NumeroMatricula", encargadoMatricula.NumeroMatricula);
            return View(encargadoMatricula);
        }

        // POST: EncargadoMatricula/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdEncargadoMatricula,NumeroMatricula,IdEncargado")] EncargadoMatricula encargadoMatricula)
        {
            if (id != encargadoMatricula.IdEncargadoMatricula)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(encargadoMatricula);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EncargadoMatriculaExists(encargadoMatricula.IdEncargadoMatricula))
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
            ViewData["IdEncargado"] = new SelectList(_context.Encargados, "IdEncargado", "Apell1Encargado", encargadoMatricula.IdEncargado);
            ViewData["NumeroMatricula"] = new SelectList(_context.Matriculas, "NumeroMatricula", "NumeroMatricula", encargadoMatricula.NumeroMatricula);
            return View(encargadoMatricula);
        }

        // GET: EncargadoMatricula/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var encargadoMatricula = await _context.EncargadoMatriculas
                .Include(e => e.IdEncargadoNavigation)
                .Include(e => e.NumeroMatriculaNavigation)
                .FirstOrDefaultAsync(m => m.IdEncargadoMatricula == id);
            if (encargadoMatricula == null)
            {
                return NotFound();
            }

            return View(encargadoMatricula);
        }

        // POST: EncargadoMatricula/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var encargadoMatricula = await _context.EncargadoMatriculas.FindAsync(id);
            _context.EncargadoMatriculas.Remove(encargadoMatricula);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EncargadoMatriculaExists(int id)
        {
            return _context.EncargadoMatriculas.Any(e => e.IdEncargadoMatricula == id);
        }
    }
}
