using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Caribbean2.Models; 

namespace Caribbean2.Controllers
{
    public class HuespedesController : Controller
    {
        private readonly CaribbeanContext _context;

        public HuespedesController(CaribbeanContext context)
        {
            _context = context;
        }
        
        [RoleAuthorize(2, 3, 4)]
        // GET: Huespedes
        public async Task<IActionResult> Index(string searchQuery)
        {
            // Obtiene todos los huéspedes incluyendo su estado
            var huespedes = _context.Huespedes.Include(h => h.EstadoHuesped).AsQueryable();

            // Aplica el filtro si hay un término de búsqueda
            if (!string.IsNullOrEmpty(searchQuery))
            {
                huespedes = huespedes.Where(h =>
                    h.NombreCompleto.Contains(searchQuery) ||
                    h.NumeroIdentificacion.Contains(searchQuery)); // Ajusta si tienes un campo específico para habitación
            }

            // Devuelve la lista filtrada
            return View(await huespedes.ToListAsync());
        }


        // GET: Huespedes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return BadRequest("ID no proporcionado."); // Mensaje más específico
            }

            var huesped = await _context.Huespedes
                .Include(h => h.EstadoHuesped)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (huesped == null)
            {
                return NotFound("Huesped no encontrado.");
            }

            return View(huesped);
        }

        // GET: Huespedes/Create
        public IActionResult Create()
        {
            ViewData["IdEstadoHuesped"] = new SelectList(_context.HuespedEstados, "IdEstadoHuesped", "NombreEstado");
            return View();
        }

        // POST: Huespedes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NombreCompleto,CorreoElectronico,TipoDocumento,NumeroIdentificacion,NumeroContacto,LugarResidencia,FechaLlegada,FechaSalida,TipoAcomodacion,IdEstadoHuesped")] Huesped huesped)
        {
            // Validar si ya existe un huésped con el mismo número de identificación
            bool huespedDuplicado = await _context.Huespedes.AnyAsync(h => h.NumeroIdentificacion == huesped.NumeroIdentificacion);

            if (huespedDuplicado)
            {
                // Agregar un mensaje de error al ModelState
                ModelState.AddModelError("NumeroIdentificacion", "Ya existe un huésped registrado con este número de identificación.");
            }

            // Validar que la fecha de salida sea posterior a la fecha de llegada
            if (huesped.FechaSalida <= huesped.FechaLlegada)
            {
                ModelState.AddModelError("FechaSalida", "La fecha de salida debe ser posterior a la fecha de llegada.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(huesped);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error al guardar: " + ex.Message);
                }
            }

            ViewData["IdEstadoHuesped"] = new SelectList(_context.HuespedEstados, "IdEstadoHuesped", "NombreEstado", huesped.IdEstadoHuesped);
            return View(huesped);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAjax([FromForm] Huesped huesped)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(huesped);
                    await _context.SaveChangesAsync();
                    
                    return Json(new { 
                        success = true, 
                        message = "Huésped creado correctamente",
                        huesped = new { 
                            id = huesped.Id, 
                            nombreCompleto = huesped.NombreCompleto 
                        }
                    });
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, message = ex.Message });
                }
            }
            return Json(new { 
                success = false, 
                message = "Datos inválidos", 
                errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage) 
            });
        }

        // GET: Huespedes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest("ID no proporcionado.");
            }

            var huesped = await _context.Huespedes.FindAsync(id);
            if (huesped == null)
            {
                return NotFound("Huesped no encontrado.");
            }
            ViewData["IdEstadoHuesped"] = new SelectList(_context.HuespedEstados, "IdEstadoHuesped", "NombreEstado", huesped.IdEstadoHuesped);
            return View(huesped);
        }

        // POST: Huespedes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NombreCompleto,CorreoElectronico,TipoDocumento,NumeroIdentificacion,NumeroContacto,LugarResidencia,FechaLlegada,FechaSalida,TipoAcomodacion,IdEstadoHuesped")] Huesped huesped)
        {
            if (id != huesped.Id)
            {
                return BadRequest("IDs no coinciden.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(huesped);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HuespedExists(huesped.Id))
                    {
                        return NotFound("Huesped no encontrado.");
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            ViewData["IdEstadoHuesped"] = new SelectList(_context.HuespedEstados, "IdEstadoHuesped", "NombreEstado", huesped.IdEstadoHuesped);
            return View(huesped);
        }

        // GET: Huespedes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest("ID no proporcionado.");
            }

            var huesped = await _context.Huespedes
                .Include(h => h.EstadoHuesped)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (huesped == null)
            {
                return NotFound("Huesped no encontrado.");
            }

            return View(huesped);
        }

        // POST: Huespedes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var huesped = await _context.Huespedes.FindAsync(id);
            if (huesped != null)
            {
                _context.Huespedes.Remove(huesped);
                await _context.SaveChangesAsync();
            }
            else
            {
                ModelState.AddModelError("", "Error al eliminar: huesped no encontrado.");
            }

            return RedirectToAction(nameof(Index));
        }

        private bool HuespedExists(int id)
        {
            return _context.Huespedes.Any(e => e.Id == id);
        }
    }
}
