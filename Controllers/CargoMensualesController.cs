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
    public class CargoMensualesController : Controller
    {
        private readonly NinnoFelizContext _context;

        public CargoMensualesController(NinnoFelizContext context)
        {
            _context = context;
        }

        // GET: CargoMensuales
        public async Task<IActionResult> Index()
        {
            var ninnoFelizContext = _context.CargoMensuales.Include(c => c.IdUsoComedorNavigation);
            return View(await ninnoFelizContext.ToListAsync());
        }

        // GET: CargoMensuales/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cargoMensuale = await _context.CargoMensuales
                .Include(c => c.IdUsoComedorNavigation)
                .FirstOrDefaultAsync(m => m.IdCargo == id);
            if (cargoMensuale == null)
            {
                return NotFound();
            }

            return View(cargoMensuale);
        }

        // GET: CargoMensuales/Create
        public IActionResult Create()
        {
            ViewData["IdUsoComedor"] = new SelectList(_context.UsoComedores, "IdUsoComedor", "IdUsoComedor");
            return View();
        }

        // POST: CargoMensuales/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCargo,CargoMensual,IdUsoComedor")] CargoMensuale cargoMensuale)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cargoMensuale);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdUsoComedor"] = new SelectList(_context.UsoComedores, "IdUsoComedor", "IdUsoComedor", cargoMensuale.IdUsoComedor);
            return View(cargoMensuale);
        }

        // GET: CargoMensuales/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cargoMensuale = await _context.CargoMensuales.FindAsync(id);
            if (cargoMensuale == null)
            {
                return NotFound();
            }
            ViewData["IdUsoComedor"] = new SelectList(_context.UsoComedores, "IdUsoComedor", "IdUsoComedor", cargoMensuale.IdUsoComedor);
            return View(cargoMensuale);
        }

        // POST: CargoMensuales/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCargo,CargoMensual,IdUsoComedor")] CargoMensuale cargoMensuale)
        {
            if (id != cargoMensuale.IdCargo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cargoMensuale);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CargoMensualeExists(cargoMensuale.IdCargo))
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
            ViewData["IdUsoComedor"] = new SelectList(_context.UsoComedores, "IdUsoComedor", "IdUsoComedor", cargoMensuale.IdUsoComedor);
            return View(cargoMensuale);
        }

        // GET: CargoMensuales/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cargoMensuale = await _context.CargoMensuales
                .Include(c => c.IdUsoComedorNavigation)
                .FirstOrDefaultAsync(m => m.IdCargo == id);
            if (cargoMensuale == null)
            {
                return NotFound();
            }

            return View(cargoMensuale);
        }

        // POST: CargoMensuales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cargoMensuale = await _context.CargoMensuales.FindAsync(id);
            _context.CargoMensuales.Remove(cargoMensuale);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CargoMensualeExists(int id)
        {
            return _context.CargoMensuales.Any(e => e.IdCargo == id);
        }
    }
}
