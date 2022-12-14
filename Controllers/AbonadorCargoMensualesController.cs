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
    public class AbonadorCargoMensualesController : Controller
    {
        private readonly NinnoFelizContext _context;

        public AbonadorCargoMensualesController(NinnoFelizContext context)
        {
            _context = context;
        }

        // GET: AbonadorCargoMensuales
        public async Task<IActionResult> Index()
        {
            var ninnoFelizContext = _context.AbonadorCargoMensuales.Include(a => a.IdAbonadorNavigation).Include(a => a.IdCargoNavigation);
            return View(await ninnoFelizContext.ToListAsync());
        }

        // GET: AbonadorCargoMensuales/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var abonadorCargoMensuale = await _context.AbonadorCargoMensuales
                .Include(a => a.IdAbonadorNavigation)
                .Include(a => a.IdCargoNavigation)
                .FirstOrDefaultAsync(m => m.IdAbonadorCargoMensual == id);
            if (abonadorCargoMensuale == null)
            {
                return NotFound();
            }

            return View(abonadorCargoMensuale);
        }

        // GET: AbonadorCargoMensuales/Create
        public IActionResult Create()
        {
            ViewData["IdAbonador"] = new SelectList(_context.Abonadores, "IdAbonador", "IdAbonador");
            ViewData["IdCargo"] = new SelectList(_context.CargoMensuales, "IdCargo", "CargoMensual");
            return View();
        }

        // POST: AbonadorCargoMensuales/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdAbonadorCargoMensual,IdAbonador,IdCargo")] AbonadorCargoMensuale abonadorCargoMensuale)
        {
            if (ModelState.IsValid)
            {
                SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
                SqlCommand cmd = conn.CreateCommand();
                conn.Open();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "sp_IngresarAbonador_CargoMensuales";
                cmd.Parameters.Add("@idAbonadorCargoMensual", System.Data.SqlDbType.Int).Value = abonadorCargoMensuale.IdAbonadorCargoMensual;
                cmd.Parameters.Add("@idAbonador", System.Data.SqlDbType.Int).Value = abonadorCargoMensuale.IdAbonador;
                cmd.Parameters.Add("@idCargo", System.Data.SqlDbType.Int).Value = abonadorCargoMensuale.IdCargo; 
                await cmd.ExecuteNonQueryAsync(); 
                conn.Close();
                //_context.Add(abonadorCargoMensuale);
                //await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdAbonador"] = new SelectList(_context.Abonadores, "IdAbonador", "IdAbonador", abonadorCargoMensuale.IdAbonador);
            ViewData["IdCargo"] = new SelectList(_context.CargoMensuales, "IdCargo", "CargoMensual", abonadorCargoMensuale.IdCargo);
            return View(abonadorCargoMensuale);
        }

        // GET: AbonadorCargoMensuales/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var abonadorCargoMensuale = await _context.AbonadorCargoMensuales.FindAsync(id);
            if (abonadorCargoMensuale == null)
            {
                return NotFound();
            }
            ViewData["IdAbonador"] = new SelectList(_context.Abonadores, "IdAbonador", "IdAbonador", abonadorCargoMensuale.IdAbonador);
            ViewData["IdCargo"] = new SelectList(_context.CargoMensuales, "IdCargo", "CargoMensual", abonadorCargoMensuale.IdCargo);
            return View(abonadorCargoMensuale);
        }

        // POST: AbonadorCargoMensuales/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdAbonadorCargoMensual,IdAbonador,IdCargo")] AbonadorCargoMensuale abonadorCargoMensuale)
        {
            if (id != abonadorCargoMensuale.IdAbonadorCargoMensual)
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
                    cmd.CommandText = "sp_ModificarAbonador_CargoMensuales";
                    cmd.Parameters.Add("@idAbonadorCargoMensual", System.Data.SqlDbType.Int).Value = abonadorCargoMensuale.IdAbonadorCargoMensual;
                    cmd.Parameters.Add("@idAbonador", System.Data.SqlDbType.Int).Value = abonadorCargoMensuale.IdAbonador;
                    cmd.Parameters.Add("@idCargo", System.Data.SqlDbType.Int).Value = abonadorCargoMensuale.IdCargo;
                    await cmd.ExecuteNonQueryAsync();
                    conn.Close();
                    //_context.Update(abonadorCargoMensuale);
                    //await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AbonadorCargoMensualeExists(abonadorCargoMensuale.IdAbonadorCargoMensual))
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
            ViewData["IdAbonador"] = new SelectList(_context.Abonadores, "IdAbonador", "IdAbonador", abonadorCargoMensuale.IdAbonador);
            ViewData["IdCargo"] = new SelectList(_context.CargoMensuales, "IdCargo", "CargoMensual", abonadorCargoMensuale.IdCargo);
            return View(abonadorCargoMensuale);
        }

        // GET: AbonadorCargoMensuales/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var abonadorCargoMensuale = await _context.AbonadorCargoMensuales
                .Include(a => a.IdAbonadorNavigation)
                .Include(a => a.IdCargoNavigation)
                .FirstOrDefaultAsync(m => m.IdAbonadorCargoMensual == id);
            if (abonadorCargoMensuale == null)
            {
                return NotFound();
            }

            return View(abonadorCargoMensuale);
        }

        // POST: AbonadorCargoMensuales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "sp_EliminarAbonador_CargoMensuales"; 
            cmd.Parameters.Add("@idAbonadorCargoMensual", System.Data.SqlDbType.Int).Value = id;
            await cmd.ExecuteNonQueryAsync(); 
            conn.Close();
            //var abonadorCargoMensuale = await _context.AbonadorCargoMensuales.FindAsync(id);
           //_context.AbonadorCargoMensuales.Remove(abonadorCargoMensuale);
           //await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AbonadorCargoMensualeExists(int id)
        {
            return _context.AbonadorCargoMensuales.Any(e => e.IdAbonadorCargoMensual == id);
        }
    }
}
