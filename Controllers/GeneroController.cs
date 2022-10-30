using Microsoft.AspNetCore.Mvc;
using NinnoFeliz.Data;
using NinnoFeliz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace NinnoFeliz.Controllers
{
    public class GeneroController : Controller
    {
        private readonly NinnoFelizContext _context;
        public GeneroController(NinnoFelizContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.Generos.ToList());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var generos = await _context.Generos
                .SingleOrDefaultAsync(m => m.IdGenero == id);
            if (generos == null)
            {
                return NotFound();
            }
            return View(generos);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdGenero", "DetalleGen")] Genero genero)
        {
            if (ModelState.IsValid)
            {
                SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
                SqlCommand cmd = conn.CreateCommand();
                conn.Open();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "sp_IngresarGenero";
                cmd.Parameters.Add("@detalleGen", System.Data.SqlDbType.VarChar, 15).Value = genero.DetalleGen + "sp_IngresarGenero";
                cmd.Parameters.Add("@idGenero", System.Data.SqlDbType.Int).Value = genero.IdGenero;
                await cmd.ExecuteNonQueryAsync();
                conn.Close();
                return RedirectToAction(nameof(Index));
            }
            return View(genero);
        }
    }
}