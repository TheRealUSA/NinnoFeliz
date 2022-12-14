using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using NinnoFeliz.Data;
using NinnoFeliz.Models;

namespace NinnoFeliz.Controllers
{
    public class EncargadoController : Controller
    {
        private readonly NinnoFelizContext _context;

        public EncargadoController(NinnoFelizContext context)
        {
            _context = context;
        }

        // GET: Encargadoes
        public async Task<IActionResult> Index()
        {
            var ninnoFelizContext = _context.Encargados.Include(e => e.IdParentezcoNavigation);
            return View(await ninnoFelizContext.ToListAsync());
        }

        // GET: Encargadoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var encargado = await _context.Encargados
                .Include(e => e.IdParentezcoNavigation)
                .FirstOrDefaultAsync(m => m.IdEncargado == id);
            if (encargado == null)
            {
                return NotFound();
            }

            return View(encargado);
        }

        // GET: Encargadoes/Create
        public IActionResult Create()
        {
            ViewData["IdParentezco"] = new SelectList(_context.Parentezcos, "IdParentezco", "DetallePar");
            return View();
        }

        // POST: Encargadoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdEncargado,NombreEncargado,Apell1Encargado,Apell2Encargado,TelefonoEncargado,DirecciónEncargado,IdParentezco")] Encargado encargado)
        {
            if (ModelState.IsValid)
            {
                SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
                SqlCommand cmd = conn.CreateCommand();
                conn.Open();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "sp_IngresarEncargados";
                cmd.Parameters.Add("@idEncargado", System.Data.SqlDbType.Int).Value = encargado.IdEncargado;
                cmd.Parameters.Add("@nombreEncargado", System.Data.SqlDbType.VarChar, 15).Value = encargado.NombreEncargado;
                cmd.Parameters.Add("@apell1Encargado", System.Data.SqlDbType.VarChar, 15).Value = encargado.Apell1Encargado;
                cmd.Parameters.Add("@apell2Encargado", System.Data.SqlDbType.VarChar, 15).Value = encargado.Apell2Encargado;
                cmd.Parameters.Add("@telefonoEncargado", System.Data.SqlDbType.VarChar, 15).Value = encargado.TelefonoEncargado;
                cmd.Parameters.Add("@direcciónEncargado", System.Data.SqlDbType.VarChar, 50).Value = encargado.DirecciónEncargado;
                cmd.Parameters.Add("@idParentezco", System.Data.SqlDbType.Int).Value = encargado.IdParentezco;
                await cmd.ExecuteNonQueryAsync();
                conn.Close();
                //_context.Add(encargado);
                //await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdParentezco"] = new SelectList(_context.Parentezcos, "IdParentezco", "DetallePar", encargado.IdParentezco);
            return View(encargado);
        }

        // GET: Encargadoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var encargado = await _context.Encargados.FindAsync(id);
            if (encargado == null)
            {
                return NotFound();
            }
            ViewData["IdParentezco"] = new SelectList(_context.Parentezcos, "IdParentezco", "DetallePar", encargado.IdParentezco);
            return View(encargado);
        }

        // POST: Encargadoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdEncargado,NombreEncargado,Apell1Encargado,Apell2Encargado,TelefonoEncargado,DirecciónEncargado,IdParentezco")] Encargado encargado)
        {
            if (id != encargado.IdEncargado)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(encargado);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EncargadoExists(encargado.IdEncargado))
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
            ViewData["IdParentezco"] = new SelectList(_context.Parentezcos, "IdParentezco", "DetallePar", encargado.IdParentezco);
            return View(encargado);
        }

        // GET: Encargadoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var encargado = await _context.Encargados
                .Include(e => e.IdParentezcoNavigation)
                .FirstOrDefaultAsync(m => m.IdEncargado == id);
            if (encargado == null)
            {
                return NotFound();
            }

            return View(encargado);
        }

        // POST: Encargadoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var encargado = await _context.Encargados.FindAsync(id);
            _context.Encargados.Remove(encargado);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EncargadoExists(int id)
        {
            return _context.Encargados.Any(e => e.IdEncargado == id);
        }
    }
}
