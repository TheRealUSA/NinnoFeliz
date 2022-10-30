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
    public class NinnoEncargadoController : Controller
    {
        private readonly NinnoFelizContext _context;

        public NinnoEncargadoController(NinnoFelizContext context)
        {
            _context = context;
        }

        // GET: NinnoEncargadoes
        public async Task<IActionResult> Index()
        {
            var ninnoFelizContext = _context.NinnoEncargados.Include(n => n.IdEncargadoNavigation).Include(n => n.IdNinnoNavigation);
            return View(await ninnoFelizContext.ToListAsync());
        }

        // GET: NinnoEncargadoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ninnoEncargado = await _context.NinnoEncargados
                .Include(n => n.IdEncargadoNavigation)
                .Include(n => n.IdNinnoNavigation)
                .FirstOrDefaultAsync(m => m.IdNiñoEncargado == id);
            if (ninnoEncargado == null)
            {
                return NotFound();
            }

            return View(ninnoEncargado);
        }

        // GET: NinnoEncargadoes/Create
        public IActionResult Create()
        {
            ViewData["IdEncargado"] = new SelectList(_context.Encargados, "IdEncargado", "Apell1Encargado");
            ViewData["IdNinno"] = new SelectList(_context.Ninnos, "IdNinno", "Apell1Ninno");
            return View();
        }

        // POST: NinnoEncargadoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdNiñoEncargado,IdEncargado,IdNinno")] NinnoEncargado ninnoEncargado)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ninnoEncargado);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdEncargado"] = new SelectList(_context.Encargados, "IdEncargado", "Apell1Encargado", ninnoEncargado.IdEncargado);
            ViewData["IdNinno"] = new SelectList(_context.Ninnos, "IdNinno", "Apell1Ninno", ninnoEncargado.IdNinno);
            return View(ninnoEncargado);
        }

        // GET: NinnoEncargadoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ninnoEncargado = await _context.NinnoEncargados.FindAsync(id);
            if (ninnoEncargado == null)
            {
                return NotFound();
            }
            ViewData["IdEncargado"] = new SelectList(_context.Encargados, "IdEncargado", "Apell1Encargado", ninnoEncargado.IdEncargado);
            ViewData["IdNinno"] = new SelectList(_context.Ninnos, "IdNinno", "Apell1Ninno", ninnoEncargado.IdNinno);
            return View(ninnoEncargado);
        }

        // POST: NinnoEncargadoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdNiñoEncargado,IdEncargado,IdNinno")] NinnoEncargado ninnoEncargado)
        {
            if (id != ninnoEncargado.IdNiñoEncargado)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ninnoEncargado);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NinnoEncargadoExists(ninnoEncargado.IdNiñoEncargado))
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
            ViewData["IdEncargado"] = new SelectList(_context.Encargados, "IdEncargado", "Apell1Encargado", ninnoEncargado.IdEncargado);
            ViewData["IdNinno"] = new SelectList(_context.Ninnos, "IdNinno", "Apell1Ninno", ninnoEncargado.IdNinno);
            return View(ninnoEncargado);
        }

        // GET: NinnoEncargadoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ninnoEncargado = await _context.NinnoEncargados
                .Include(n => n.IdEncargadoNavigation)
                .Include(n => n.IdNinnoNavigation)
                .FirstOrDefaultAsync(m => m.IdNiñoEncargado == id);
            if (ninnoEncargado == null)
            {
                return NotFound();
            }

            return View(ninnoEncargado);
        }

        // POST: NinnoEncargadoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ninnoEncargado = await _context.NinnoEncargados.FindAsync(id);
            _context.NinnoEncargados.Remove(ninnoEncargado);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NinnoEncargadoExists(int id)
        {
            return _context.NinnoEncargados.Any(e => e.IdNiñoEncargado == id);
        }
    }
}
