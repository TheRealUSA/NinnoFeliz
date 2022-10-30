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
    public class ParentezcoController : Controller
    {
        private readonly NinnoFelizContext _context;
        public ParentezcoController(NinnoFelizContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
            {
            return View(await _context.Parentezcos.ToListAsync());
            }
        public async Task<IActionResult> Details (int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var parentezcos = await _context.Parentezcos
                .SingleOrDefaultAsync(m => m.IdParentezco == id);
            if (parentezcos == null)
            {
                return NotFound();
            }
            return View(parentezcos);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdParentezco", "DetallePar")] Parentezco parentezco)
        {
            if (ModelState.IsValid)
            {
                SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
                SqlCommand cmd = conn.CreateCommand();
                conn.Open();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "sp_IngresarParentezco";
                cmd.Parameters.Add("@detallePar", System.Data.SqlDbType.VarChar, 15).Value = parentezco.DetallePar + " sp_IngresarParentezco ";
                cmd.Parameters.Add("@idParentezco", System.Data.SqlDbType.Int).Value = parentezco.IdParentezco;
                cmd.ExecuteNonQuery();
                conn.Close();
               // _context.Add(parentezco);
               // await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(parentezco);
        }
    }
}
