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
    public class NinnoController : Controller
    {
        private readonly NinnoFelizContext _context;

        public NinnoController(NinnoFelizContext context)
        {
            _context = context;
        }

        // GET: Ninno
        public async Task<IActionResult> Index()
        {
            var ninnoFelizContext = _context.Ninnos.Include(n => n.IdGeneroNavigation);
            return View(await ninnoFelizContext.ToListAsync());
        }

        // GET: Ninno/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ninno = await _context.Ninnos
                .Include(n => n.IdGeneroNavigation)
                .FirstOrDefaultAsync(m => m.IdNinno == id);
            if (ninno == null)
            {
                return NotFound();
            }

            return View(ninno);
        }

        // GET: Ninno/Create
        public IActionResult Create()
        {
            ViewData["IdGenero"] = new SelectList(_context.Generos, "IdGenero", "DetalleGen");
            return View();
        }

        // POST: Ninno/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdNinno,NombreNinno,Apell1Ninno,Apell2Ninno,FechaNacimiento,DireccionNinno,IdGenero")] Ninno ninno)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ninno);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdGenero"] = new SelectList(_context.Generos, "IdGenero", "DetalleGen", ninno.IdGenero);
            return View(ninno);
        }

        // GET: Ninno/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ninno = await _context.Ninnos.FindAsync(id);
            if (ninno == null)
            {
                return NotFound();
            }
            ViewData["IdGenero"] = new SelectList(_context.Generos, "IdGenero", "DetalleGen", ninno.IdGenero);
            return View(ninno);
        }

        // POST: Ninno/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdNinno,NombreNinno,Apell1Ninno,Apell2Ninno,FechaNacimiento,DireccionNinno,IdGenero")] Ninno ninno)
        {
            if (id != ninno.IdNinno)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ninno);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NinnoExists(ninno.IdNinno))
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
            ViewData["IdGenero"] = new SelectList(_context.Generos, "IdGenero", "DetalleGen", ninno.IdGenero);
            return View(ninno);
        }

        // GET: Ninno/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ninno = await _context.Ninnos
                .Include(n => n.IdGeneroNavigation)
                .FirstOrDefaultAsync(m => m.IdNinno == id);
            if (ninno == null)
            {
                return NotFound();
            }

            return View(ninno);
        }

        // POST: Ninno/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ninno = await _context.Ninnos.FindAsync(id);
            _context.Ninnos.Remove(ninno);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NinnoExists(int id)
        {
            return _context.Ninnos.Any(e => e.IdNinno == id);
        }
    }
}
