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
    public class EncargadoRegistroDeBajaController : Controller
    {
        private readonly NinnoFelizContext _context;

        public EncargadoRegistroDeBajaController(NinnoFelizContext context)
        {
            _context = context;
        }

        // GET: EncargadoRegistroDeBaja
        public async Task<IActionResult> Index()
        {
            var ninnoFelizContext = _context.EncargadoRegistroDeBajas.Include(e => e.IdEncargadoNavigation).Include(e => e.IdRegistroBajaNavigation);
            return View(await ninnoFelizContext.ToListAsync());
        }

        // GET: EncargadoRegistroDeBaja/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var encargadoRegistroDeBaja = await _context.EncargadoRegistroDeBajas
                .Include(e => e.IdEncargadoNavigation)
                .Include(e => e.IdRegistroBajaNavigation)
                .FirstOrDefaultAsync(m => m.IdEncargadoRegistroBaja == id);
            if (encargadoRegistroDeBaja == null)
            {
                return NotFound();
            }

            return View(encargadoRegistroDeBaja);
        }

        // GET: EncargadoRegistroDeBaja/Create
        public IActionResult Create()
        {
            ViewData["IdEncargado"] = new SelectList(_context.Encargados, "IdEncargado", "Apell1Encargado");
            ViewData["IdRegistroBaja"] = new SelectList(_context.RegistroBajas, "IdRegistroBaja", "IdRegistroBaja");
            return View();
        }

        // POST: EncargadoRegistroDeBaja/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdEncargadoRegistroBaja,IdRegistroBaja,IdEncargado")] EncargadoRegistroDeBaja encargadoRegistroDeBaja)
        {
            if (ModelState.IsValid)
            {
                SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
                SqlCommand cmd = conn.CreateCommand();
                conn.Open();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "sp_IngresarEncargado_RegistroDeBajas";
                cmd.Parameters.Add("@idEncargadoRegistroBaja", System.Data.SqlDbType.Int).Value = encargadoRegistroDeBaja.IdEncargadoRegistroBaja;
                cmd.Parameters.Add("@idRegistroBaja", System.Data.SqlDbType.Int).Value = encargadoRegistroDeBaja.IdRegistroBaja;
                cmd.Parameters.Add("@idEncargado", System.Data.SqlDbType.Int).Value = encargadoRegistroDeBaja.IdEncargado;
                await cmd.ExecuteNonQueryAsync();
                conn.Close();
                //_context.Add(encargadoRegistroDeBaja);
                //await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdEncargado"] = new SelectList(_context.Encargados, "IdEncargado", "Apell1Encargado", encargadoRegistroDeBaja.IdEncargado);
            ViewData["IdRegistroBaja"] = new SelectList(_context.RegistroBajas, "IdRegistroBaja", "IdRegistroBaja", encargadoRegistroDeBaja.IdRegistroBaja);
            return View(encargadoRegistroDeBaja);
        }

        // GET: EncargadoRegistroDeBaja/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var encargadoRegistroDeBaja = await _context.EncargadoRegistroDeBajas.FindAsync(id);
            if (encargadoRegistroDeBaja == null)
            {
                return NotFound();
            }
            ViewData["IdEncargado"] = new SelectList(_context.Encargados, "IdEncargado", "Apell1Encargado", encargadoRegistroDeBaja.IdEncargado);
            ViewData["IdRegistroBaja"] = new SelectList(_context.RegistroBajas, "IdRegistroBaja", "IdRegistroBaja", encargadoRegistroDeBaja.IdRegistroBaja);
            return View(encargadoRegistroDeBaja);
        }

        // POST: EncargadoRegistroDeBaja/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdEncargadoRegistroBaja,IdRegistroBaja,IdEncargado")] EncargadoRegistroDeBaja encargadoRegistroDeBaja)
        {
            if (id != encargadoRegistroDeBaja.IdEncargadoRegistroBaja)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
                    SqlCommand cmd = conn.CreateCommand();
                    conn.Open();
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "sp_ModificarEncargado_RegistroDeBajas";
                    cmd.Parameters.Add("@idEncargadoRegistroBaja", System.Data.SqlDbType.Int).Value = encargadoRegistroDeBaja.IdEncargadoRegistroBaja;
                    cmd.Parameters.Add("@idRegistroBaja", System.Data.SqlDbType.Int).Value = encargadoRegistroDeBaja.IdRegistroBaja;
                    cmd.Parameters.Add("@idEncargado", System.Data.SqlDbType.Int).Value = encargadoRegistroDeBaja.IdEncargado;
                    await cmd.ExecuteNonQueryAsync();
                    conn.Close();
                    //_context.Update(encargadoRegistroDeBaja);
                    //await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EncargadoRegistroDeBajaExists(encargadoRegistroDeBaja.IdEncargadoRegistroBaja))
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
            ViewData["IdEncargado"] = new SelectList(_context.Encargados, "IdEncargado", "Apell1Encargado", encargadoRegistroDeBaja.IdEncargado);
            ViewData["IdRegistroBaja"] = new SelectList(_context.RegistroBajas, "IdRegistroBaja", "IdRegistroBaja", encargadoRegistroDeBaja.IdRegistroBaja);
            return View(encargadoRegistroDeBaja);
        }

        // GET: EncargadoRegistroDeBaja/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var encargadoRegistroDeBaja = await _context.EncargadoRegistroDeBajas
                .Include(e => e.IdEncargadoNavigation)
                .Include(e => e.IdRegistroBajaNavigation)
                .FirstOrDefaultAsync(m => m.IdEncargadoRegistroBaja == id);
            if (encargadoRegistroDeBaja == null)
            {
                return NotFound();
            }

            return View(encargadoRegistroDeBaja);
        }

        // POST: EncargadoRegistroDeBaja/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "sp_EliminarEncargado_RegistroDeBajas";
            cmd.Parameters.Add("@idEncargadoRegistroBaja", System.Data.SqlDbType.Int).Value = id;
            await cmd.ExecuteNonQueryAsync();
            conn.Close();
            //var encargadoRegistroDeBaja = await _context.EncargadoRegistroDeBajas.FindAsync(id);
            //_context.EncargadoRegistroDeBajas.Remove(encargadoRegistroDeBaja);
            //await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EncargadoRegistroDeBajaExists(int id)
        {
            return _context.EncargadoRegistroDeBajas.Any(e => e.IdEncargadoRegistroBaja == id);
        }
    }
}
