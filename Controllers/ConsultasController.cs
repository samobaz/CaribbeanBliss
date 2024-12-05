using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Caribbean2.Models;

namespace Caribbean2.Controllers
{
    public class ConsultasController : Controller
    {
        private readonly CaribbeanContext _context;

        public ConsultasController(CaribbeanContext context)
        {
            _context = context;
        }

        [RoleAuthorize(2, 3, 4)]
        // GET: Consultas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Consultas.ToListAsync());
        }

        // GET: Consultas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                TempData["ErrorMessage"] = "ID de consulta no especificado";
                return NotFound();
            }

            var consulta = await _context.Consultas
                .FirstOrDefaultAsync(m => m.IdConsulta == id);
            if (consulta == null)
            {
                TempData["ErrorMessage"] = "Consulta no encontrada";
                return NotFound();
            }

            return View(consulta);
        }

        // GET: Consultas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Consultas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NombresApellidos,Email,Asunto,Mensaje")] Consulta consulta)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    consulta.FechaConsulta = DateTime.Now;
                    _context.Add(consulta);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Mensaje enviado exitosamente";
                    return RedirectToAction("Contacto", "Caribbean"); // Redirect to Contact page
                }
                catch (Exception)
                {
                    TempData["ErrorMessage"] = "Error al enviar el mensaje";
                }
            }
            return View("~/Views/Caribbean/Contacto.cshtml", consulta); // Return to Contact page with model if validation fails
        }

        // GET: Consultas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                TempData["ErrorMessage"] = "ID de consulta no especificado";
                return NotFound();
            }

            var consulta = await _context.Consultas.FindAsync(id);
            if (consulta == null)
            {
                TempData["ErrorMessage"] = "Consulta no encontrada";
                return NotFound();
            }
            return View(consulta);
        }

        // POST: Consultas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdConsulta,NombresApellidos,Email,Asunto,Mensaje,FechaConsulta")] Consulta consulta)
        {
            if (id != consulta.IdConsulta)
            {
                TempData["ErrorMessage"] = "ID de consulta no coincide";
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(consulta);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Consulta actualizada exitosamente";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConsultaExists(consulta.IdConsulta))
                    {
                        TempData["ErrorMessage"] = "Consulta no encontrada";
                        return NotFound();
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Error al actualizar la consulta";
                        throw;
                    }
                }
            }
            return View(consulta);
        }

        // GET: Consultas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                TempData["ErrorMessage"] = "ID de consulta no especificado";
                return NotFound();
            }

            var consulta = await _context.Consultas
                .FirstOrDefaultAsync(m => m.IdConsulta == id);
            if (consulta == null)
            {
                TempData["ErrorMessage"] = "Consulta no encontrada";
                return NotFound();
            }

            return View(consulta);
        }

        // POST: Consultas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var consulta = await _context.Consultas.FindAsync(id);
                if (consulta == null)
                {
                    TempData["ErrorMessage"] = "Consulta no encontrada";
                    return NotFound();
                }
                _context.Consultas.Remove(consulta);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Consulta eliminada exitosamente";
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Error al eliminar la consulta";
            }
            return RedirectToAction(nameof(Index));
        }

        private bool ConsultaExists(int id)
        {
            return _context.Consultas.Any(e => e.IdConsulta == id);
        }
    }
}
