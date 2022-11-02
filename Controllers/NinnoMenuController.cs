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
    public class NinnoMenuController : Controller
    {
        private readonly NinnoFelizContext _context;

        public NinnoMenuController(NinnoFelizContext context)
        {
            _context = context;
        }

        // GET: NinnoMenus
        public async Task<IActionResult> Index()
        {
            var ninnoFelizContext = _context.NinnoMenus.Include(n => n.IdNinnoNavigation).Include(n => n.IdNumeroMenuNavigation);
            return View(await ninnoFelizContext.ToListAsync());
        }

        // GET: NinnoMenus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ninnoMenu = await _context.NinnoMenus
                .Include(n => n.IdNinnoNavigation)
                .Include(n => n.IdNumeroMenuNavigation)
                .FirstOrDefaultAsync(m => m.IdNinnoMenu == id);
            if (ninnoMenu == null)
            {
                return NotFound();
            }

            return View(ninnoMenu);
        }

        // GET: NinnoMenus/Create
        public IActionResult Create()
        {
            ViewData["IdNinno"] = new SelectList(_context.Ninnos, "IdNinno", "NombreNinno");
            ViewData["IdNumeroMenu"] = new SelectList(_context.Menus, "IdNumeroMenu", "NombreMenu");
            return View();
        }

        // POST: NinnoMenus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdNinnoMenu,FechaConsumido,IdNinno,IdNumeroMenu")] NinnoMenu ninnoMenu)
        {
            if (ModelState.IsValid)
            {
                SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
                SqlCommand cmd = conn.CreateCommand();
                conn.Open();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "sp_IngresarNinno_Menus";
                cmd.Parameters.Add("@idNinnoMenu", System.Data.SqlDbType.Int).Value = ninnoMenu.IdNinnoMenu;
                cmd.Parameters.Add("@fechaConsumido", System.Data.SqlDbType.Date).Value = ninnoMenu.FechaConsumido;
                cmd.Parameters.Add("@idNumeroMenu", System.Data.SqlDbType.Int).Value = ninnoMenu.IdNumeroMenu;
                cmd.Parameters.Add("@idNinno", System.Data.SqlDbType.Int).Value = ninnoMenu.IdNinno;
                await cmd.ExecuteNonQueryAsync();
                conn.Close();
                //_context.Add(ninnoMenu);
                //await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdNinno"] = new SelectList(_context.Ninnos, "IdNinno", "NombreNinno", ninnoMenu.IdNinno);
            ViewData["IdNumeroMenu"] = new SelectList(_context.Menus, "IdNumeroMenu", "NombreMenu", ninnoMenu.IdNumeroMenu);
            return View(ninnoMenu);
        }

        // GET: NinnoMenus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ninnoMenu = await _context.NinnoMenus.FindAsync(id);
            if (ninnoMenu == null)
            {
                return NotFound();
            }
            ViewData["IdNinno"] = new SelectList(_context.Ninnos, "IdNinno", "NombreNinno", ninnoMenu.IdNinno);
            ViewData["IdNumeroMenu"] = new SelectList(_context.Menus, "IdNumeroMenu", "NombreMenu", ninnoMenu.IdNumeroMenu);
            return View(ninnoMenu);
        }

        // POST: NinnoMenus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdNinnoMenu,FechaConsumido,IdNinno,IdNumeroMenu")] NinnoMenu ninnoMenu)
        {
            if (id != ninnoMenu.IdNinnoMenu)
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
                    cmd.CommandText = "sp_ModificarNinno_Menus";
                    cmd.Parameters.Add("@idNinnoMenu", System.Data.SqlDbType.Int).Value = ninnoMenu.IdNinnoMenu;
                    cmd.Parameters.Add("@fechaConsumido", System.Data.SqlDbType.Date).Value = ninnoMenu.FechaConsumido;
                    cmd.Parameters.Add("@idNumeroMenu", System.Data.SqlDbType.Int).Value = ninnoMenu.IdNumeroMenu;
                    cmd.Parameters.Add("@idNinno", System.Data.SqlDbType.Int).Value = ninnoMenu.IdNinno;
                    await cmd.ExecuteNonQueryAsync();
                    conn.Close();
                    //_context.Update(ninnoMenu);
                    //await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NinnoMenuExists(ninnoMenu.IdNinnoMenu))
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
            ViewData["IdNinno"] = new SelectList(_context.Ninnos, "IdNinno", "NombreNinno", ninnoMenu.IdNinno);
            ViewData["IdNumeroMenu"] = new SelectList(_context.Menus, "IdNumeroMenu", "NombreMenu", ninnoMenu.IdNumeroMenu);
            return View(ninnoMenu);
        }

        // GET: NinnoMenus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ninnoMenu = await _context.NinnoMenus
                .Include(n => n.IdNinnoNavigation)
                .Include(n => n.IdNumeroMenuNavigation)
                .FirstOrDefaultAsync(m => m.IdNinnoMenu == id);
            if (ninnoMenu == null)
            {
                return NotFound();
            }

            return View(ninnoMenu);
        }

        // POST: NinnoMenus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "sp_EliminarNinno_Menus";
            cmd.Parameters.Add("@idNinnoMenu", System.Data.SqlDbType.Int).Value = id;
            await cmd.ExecuteNonQueryAsync();
            conn.Close();
            //var ninnoMenu = await _context.NinnoMenus.FindAsync(id);
            //_context.NinnoMenus.Remove(ninnoMenu);
            //await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NinnoMenuExists(int id)
        {
            return _context.NinnoMenus.Any(e => e.IdNinnoMenu == id);
        }
    }
}
