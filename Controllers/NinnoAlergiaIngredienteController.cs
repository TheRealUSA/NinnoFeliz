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
    public class NinnoAlergiaIngredienteController : Controller
    {
        private readonly NinnoFelizContext _context;

        public NinnoAlergiaIngredienteController(NinnoFelizContext context)
        {
            _context = context;
        }

        // GET: NinnoAlergiaIngredientes
        public async Task<IActionResult> Index()
        {
            var ninnoFelizContext = _context.NinnoAlergiaIngredientes.Include(n => n.IdAlergiaNavigation).Include(n => n.IdIngredienteNavigation).Include(n => n.IdNinnoNavigation);
            return View(await ninnoFelizContext.ToListAsync());
        }

        // GET: NinnoAlergiaIngredientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ninnoAlergiaIngrediente = await _context.NinnoAlergiaIngredientes
                .Include(n => n.IdAlergiaNavigation)
                .Include(n => n.IdIngredienteNavigation)
                .Include(n => n.IdNinnoNavigation)
                .FirstOrDefaultAsync(m => m.IdNinnoAlergiaIngrediente == id);
            if (ninnoAlergiaIngrediente == null)
            {
                return NotFound();
            }

            return View(ninnoAlergiaIngrediente);
        }

        // GET: NinnoAlergiaIngredientes/Create
        public IActionResult Create()
        {
            ViewData["IdAlergia"] = new SelectList(_context.Alergias, "IdAlergia", "NombreAlergia");
            ViewData["IdIngrediente"] = new SelectList(_context.Ingredientes, "IdIngrediente", "NombreIngrediente");
            ViewData["IdNinno"] = new SelectList(_context.Ninnos, "IdNinno", "NombreNinno");
            return View();
        }

        // POST: NinnoAlergiaIngredientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdNinnoAlergiaIngrediente,IdAlergia,IdIngrediente,IdNinno")] NinnoAlergiaIngrediente ninnoAlergiaIngrediente)
        {
            if (ModelState.IsValid)
            {
                SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
                SqlCommand cmd = conn.CreateCommand();
                conn.Open();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "sp_IngresarNinno_Alergia_Ingredientes";
                cmd.Parameters.Add("@idNinnoAlergiaIngrediente", System.Data.SqlDbType.Int).Value = ninnoAlergiaIngrediente.IdNinnoAlergiaIngrediente;
                cmd.Parameters.Add("@idNinno ", System.Data.SqlDbType.Int).Value = ninnoAlergiaIngrediente.IdNinno;
                cmd.Parameters.Add("@idAlergia", System.Data.SqlDbType.Int).Value = ninnoAlergiaIngrediente.IdNinno;
                cmd.Parameters.Add("@idIngrediente", System.Data.SqlDbType.Int).Value = ninnoAlergiaIngrediente.IdIngrediente;
                await cmd.ExecuteNonQueryAsync();
                conn.Close();
                //_context.Add(ninnoAlergiaIngrediente);
                //await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdAlergia"] = new SelectList(_context.Alergias, "IdAlergia", "NombreAlergia", ninnoAlergiaIngrediente.IdAlergia);
            ViewData["IdIngrediente"] = new SelectList(_context.Ingredientes, "IdIngrediente", "NombreIngrediente", ninnoAlergiaIngrediente.IdIngrediente);
            ViewData["IdNinno"] = new SelectList(_context.Ninnos, "IdNinno", "NombreNinno", ninnoAlergiaIngrediente.IdNinno);
            return View(ninnoAlergiaIngrediente);
        }

        // GET: NinnoAlergiaIngredientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ninnoAlergiaIngrediente = await _context.NinnoAlergiaIngredientes.FindAsync(id);
            if (ninnoAlergiaIngrediente == null)
            {
                return NotFound();
            }
            ViewData["IdAlergia"] = new SelectList(_context.Alergias, "IdAlergia", "NombreAlergia", ninnoAlergiaIngrediente.IdAlergia);
            ViewData["IdIngrediente"] = new SelectList(_context.Ingredientes, "IdIngrediente", "NombreIngrediente", ninnoAlergiaIngrediente.IdIngrediente);
            ViewData["IdNinno"] = new SelectList(_context.Ninnos, "IdNinno", "NombreNinno", ninnoAlergiaIngrediente.IdNinno);
            return View(ninnoAlergiaIngrediente);
        }

        // POST: NinnoAlergiaIngredientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdNinnoAlergiaIngrediente,IdAlergia,IdIngrediente,IdNinno")] NinnoAlergiaIngrediente ninnoAlergiaIngrediente)
        {
            if (id != ninnoAlergiaIngrediente.IdNinnoAlergiaIngrediente)
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
                    cmd.CommandText = "sp_ModificarNinno_Alergia_Ingredientes";
                    cmd.Parameters.Add("@idNinnoAlergiaIngrediente", System.Data.SqlDbType.Int).Value = ninnoAlergiaIngrediente.IdNinnoAlergiaIngrediente;
                    cmd.Parameters.Add("@idNinno ", System.Data.SqlDbType.Int).Value = ninnoAlergiaIngrediente.IdNinno;
                    cmd.Parameters.Add("@idAlergia", System.Data.SqlDbType.Int).Value = ninnoAlergiaIngrediente.IdNinno;
                    cmd.Parameters.Add("@idIngrediente", System.Data.SqlDbType.Int).Value = ninnoAlergiaIngrediente.IdIngrediente;
                    await cmd.ExecuteNonQueryAsync();
                    conn.Close();
                    //_context.Update(ninnoAlergiaIngrediente);
                    //await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NinnoAlergiaIngredienteExists(ninnoAlergiaIngrediente.IdNinnoAlergiaIngrediente))
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
            ViewData["IdAlergia"] = new SelectList(_context.Alergias, "IdAlergia", "NombreAlergia", ninnoAlergiaIngrediente.IdAlergia);
            ViewData["IdIngrediente"] = new SelectList(_context.Ingredientes, "IdIngrediente", "NombreIngrediente", ninnoAlergiaIngrediente.IdIngrediente);
            ViewData["IdNinno"] = new SelectList(_context.Ninnos, "IdNinno", "NombreNinno", ninnoAlergiaIngrediente.IdNinno);
            return View(ninnoAlergiaIngrediente);
        }

        // GET: NinnoAlergiaIngredientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ninnoAlergiaIngrediente = await _context.NinnoAlergiaIngredientes
                .Include(n => n.IdAlergiaNavigation)
                .Include(n => n.IdIngredienteNavigation)
                .Include(n => n.IdNinnoNavigation)
                .FirstOrDefaultAsync(m => m.IdNinnoAlergiaIngrediente == id);
            if (ninnoAlergiaIngrediente == null)
            {
                return NotFound();
            }

            return View(ninnoAlergiaIngrediente);
        }

        // POST: NinnoAlergiaIngredientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "sp_EliminarNinno_Alergia_Ingredientes";
            cmd.Parameters.Add("@idNinnoAlergiaIngrediente", System.Data.SqlDbType.Int).Value = id;
            await cmd.ExecuteNonQueryAsync();
            conn.Close(); 
            //var ninnoAlergiaIngrediente = await _context.NinnoAlergiaIngredientes.FindAsync(id);
            //_context.NinnoAlergiaIngredientes.Remove(ninnoAlergiaIngrediente);
            //await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NinnoAlergiaIngredienteExists(int id)
        {
            return _context.NinnoAlergiaIngredientes.Any(e => e.IdNinnoAlergiaIngrediente == id);
        }
    }
}
