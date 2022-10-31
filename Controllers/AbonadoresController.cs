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
    public class AbonadoresController : Controller
    {
        private readonly NinnoFelizContext _context;

        public AbonadoresController(NinnoFelizContext context)
        {
            _context = context;
        }

        // GET: Abonadores
        public async Task<IActionResult> Index()
        {
            var ninnoFelizContext = _context.Abonadores.Include(a => a.IdEncargadoNavigation);
            return View(await ninnoFelizContext.ToListAsync());
        }

        // GET: Abonadores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var abonadore = await _context.Abonadores
                .Include(a => a.IdEncargadoNavigation)
                .FirstOrDefaultAsync(m => m.IdAbonador == id);
            if (abonadore == null)
            {
                return NotFound();
            }

            return View(abonadore);
        }

        // GET: Abonadores/Create
        public IActionResult Create()
        {
            ViewData["IdEncargado"] = new SelectList(_context.Encargados, "IdEncargado", "Apell1Encargado");
            return View();
        }

        // POST: Abonadores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdAbonador,NumeroCuenta,IdEncargado")] Abonadore abonadore)
        {
            if (ModelState.IsValid)
            {
                _context.Add(abonadore);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdEncargado"] = new SelectList(_context.Encargados, "IdEncargado", "Apell1Encargado", abonadore.IdEncargado);
            return View(abonadore);
        }

        // GET: Abonadores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var abonadore = await _context.Abonadores.FindAsync(id);
            if (abonadore == null)
            {
                return NotFound();
            }
            ViewData["IdEncargado"] = new SelectList(_context.Encargados, "IdEncargado", "Apell1Encargado", abonadore.IdEncargado);
            return View(abonadore);
        }

        // POST: Abonadores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdAbonador,NumeroCuenta,IdEncargado")] Abonadore abonadore)
        {
            if (id != abonadore.IdAbonador)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(abonadore);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AbonadoreExists(abonadore.IdAbonador))
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
            ViewData["IdEncargado"] = new SelectList(_context.Encargados, "IdEncargado", "Apell1Encargado", abonadore.IdEncargado);
            return View(abonadore);
        }

        // GET: Abonadores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var abonadore = await _context.Abonadores
                .Include(a => a.IdEncargadoNavigation)
                .FirstOrDefaultAsync(m => m.IdAbonador == id);
            if (abonadore == null)
            {
                return NotFound();
            }

            return View(abonadore);
        }

        // POST: Abonadores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var abonadore = await _context.Abonadores.FindAsync(id);
            _context.Abonadores.Remove(abonadore);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AbonadoreExists(int id)
        {
            return _context.Abonadores.Any(e => e.IdAbonador == id);
        }
    }
}
