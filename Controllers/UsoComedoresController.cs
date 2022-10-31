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
    public class UsoComedoresController : Controller
    {
        private readonly NinnoFelizContext _context;

        public UsoComedoresController(NinnoFelizContext context)
        {
            _context = context;
        }

        // GET: UsoComedores
        public async Task<IActionResult> Index()
        {
            var ninnoFelizContext = _context.UsoComedores.Include(u => u.IdMesNavigation).Include(u => u.IdNinnoNavigation);
            return View(await ninnoFelizContext.ToListAsync());
        }

        // GET: UsoComedores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usoComedore = await _context.UsoComedores
                .Include(u => u.IdMesNavigation)
                .Include(u => u.IdNinnoNavigation)
                .FirstOrDefaultAsync(m => m.IdUsoComedor == id);
            if (usoComedore == null)
            {
                return NotFound();
            }

            return View(usoComedore);
        }

        // GET: UsoComedores/Create
        public IActionResult Create()
        {
            ViewData["IdMes"] = new SelectList(_context.Meses, "IdMes", "NombreMes");
            ViewData["IdNinno"] = new SelectList(_context.Ninnos, "IdNinno", "Apell1Ninno");
            return View();
        }

        // POST: UsoComedores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdUsoComedor,CantidadDias,IdMes,IdNinno")] UsoComedore usoComedore)
        {
            if (ModelState.IsValid)
            {
                _context.Add(usoComedore);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdMes"] = new SelectList(_context.Meses, "IdMes", "NombreMes", usoComedore.IdMes);
            ViewData["IdNinno"] = new SelectList(_context.Ninnos, "IdNinno", "Apell1Ninno", usoComedore.IdNinno);
            return View(usoComedore);
        }

        // GET: UsoComedores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usoComedore = await _context.UsoComedores.FindAsync(id);
            if (usoComedore == null)
            {
                return NotFound();
            }
            ViewData["IdMes"] = new SelectList(_context.Meses, "IdMes", "NombreMes", usoComedore.IdMes);
            ViewData["IdNinno"] = new SelectList(_context.Ninnos, "IdNinno", "Apell1Ninno", usoComedore.IdNinno);
            return View(usoComedore);
        }

        // POST: UsoComedores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdUsoComedor,CantidadDias,IdMes,IdNinno")] UsoComedore usoComedore)
        {
            if (id != usoComedore.IdUsoComedor)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usoComedore);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsoComedoreExists(usoComedore.IdUsoComedor))
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
            ViewData["IdMes"] = new SelectList(_context.Meses, "IdMes", "NombreMes", usoComedore.IdMes);
            ViewData["IdNinno"] = new SelectList(_context.Ninnos, "IdNinno", "Apell1Ninno", usoComedore.IdNinno);
            return View(usoComedore);
        }

        // GET: UsoComedores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usoComedore = await _context.UsoComedores
                .Include(u => u.IdMesNavigation)
                .Include(u => u.IdNinnoNavigation)
                .FirstOrDefaultAsync(m => m.IdUsoComedor == id);
            if (usoComedore == null)
            {
                return NotFound();
            }

            return View(usoComedore);
        }

        // POST: UsoComedores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usoComedore = await _context.UsoComedores.FindAsync(id);
            _context.UsoComedores.Remove(usoComedore);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsoComedoreExists(int id)
        {
            return _context.UsoComedores.Any(e => e.IdUsoComedor == id);
        }
    }
}
