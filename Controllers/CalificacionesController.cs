using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Caribbean2.Models;

namespace Caribbean2.Controllers
{
    public class CalificacionesController : Controller
    {
        private readonly CaribbeanContext _context;

        public CalificacionesController(CaribbeanContext context)
        {
            _context = context;
        }

        // GET: Calificaciones
        public async Task<IActionResult> Index()
        {
            try
            {
                var calificaciones = await _context.Calificaciones
                    .Include(c => c.Cliente)
                    .Include(c => c.Reserva)
                    .Where(c => c.EstadoCalificacion)
                    .ToListAsync();
                return View(calificaciones);
            }
            catch (Exception)
            {
                TempData["Error"] = "Error al cargar las calificaciones.";
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: Calificaciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var calificacion = await _context.Calificaciones
                .Include(c => c.Cliente)
                .Include(c => c.Reserva)
                .FirstOrDefaultAsync(m => m.IdCalificacion == id && m.EstadoCalificacion);

            if (calificacion == null)
            {
                return NotFound();
            }

            return View(calificacion);
        }

        // GET: Calificaciones/Create
        public IActionResult Create(int? reservaId, int? clienteId)
        {
            // Preparar listas desplegables filtradas
            var reservas = _context.Reservas
                .Where(r => r.IdEstado == 4) // Solo reservas completadas
                .Select(r => new { r.IdReserva, Descripcion = $"Reserva #{r.IdReserva} - {r.FechaInicio:d}" });

            var clientes = _context.Clientes
                .Where(c => c.ClienteEstado)
                .Select(c => new { c.idCliente, c.nombre });

            ViewData["IdReserva"] = new SelectList(reservas, "IdReserva", "Descripcion", reservaId);
            ViewData["IdCliente"] = new SelectList(clientes, "idCliente", "nombre", clienteId);

            var calificacion = new Calificacion
            {
                FechaCalificacion = DateTime.Now,
                IdReserva = reservaId ?? 0,
                IdCliente = clienteId ?? 0
            };

            return View(calificacion);
        }

        // POST: Calificaciones/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Puntuacion,Comentario,IdReserva,IdCliente")] Calificacion calificacion)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    calificacion.FechaCalificacion = DateTime.Now;
                    calificacion.EstadoCalificacion = true;
                    
                    _context.Add(calificacion);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Calificación creada exitosamente.";
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception)
            {
                TempData["Error"] = "Error al crear la calificación.";
            }

            // Recargar las listas desplegables en caso de error
            ViewData["IdReserva"] = new SelectList(_context.Reservas.Where(r => r.IdEstado == 4), "IdReserva", "IdReserva", calificacion.IdReserva);
            ViewData["IdCliente"] = new SelectList(_context.Clientes.Where(c => c.ClienteEstado), "idCliente", "nombre", calificacion.IdCliente);
            return View(calificacion);
        }

        // GET: Calificaciones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var calificacion = await _context.Calificaciones.FindAsync(id);
            if (calificacion == null || !calificacion.EstadoCalificacion)
            {
                return NotFound();
            }

            ViewData["IdReserva"] = new SelectList(_context.Reservas.Where(r => r.IdEstado == 4), "IdReserva", "IdReserva", calificacion.IdReserva);
            ViewData["IdCliente"] = new SelectList(_context.Clientes.Where(c => c.ClienteEstado), "idCliente", "nombre", calificacion.IdCliente);
            return View(calificacion);
        }

        // POST: Calificaciones/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCalificacion,Puntuacion,Comentario,IdReserva,IdCliente,FechaCalificacion,EstadoCalificacion")] Calificacion calificacion)
        {
            if (id != calificacion.IdCalificacion)
            {
                return NotFound();
            }

            try
            {
                if (ModelState.IsValid)
                {
                    _context.Update(calificacion);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Calificación actualizada exitosamente.";
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CalificacionExists(calificacion.IdCalificacion))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "idCliente", "nombre", calificacion.IdCliente);
            ViewData["IdReserva"] = new SelectList(_context.Reservas, "IdReserva", "IdReserva", calificacion.IdReserva);
            return View(calificacion);
        }

        // GET: Calificaciones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var calificacion = await _context.Calificaciones
                .Include(c => c.Cliente)
                .Include(c => c.Reserva)
                .FirstOrDefaultAsync(m => m.IdCalificacion == id);
            if (calificacion == null)
            {
                return NotFound();
            }

            return View(calificacion);
        }

        // POST: Calificaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var calificacion = await _context.Calificaciones.FindAsync(id);
            _context.Calificaciones.Remove(calificacion);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CalificacionExists(int id)
        {
            return _context.Calificaciones.Any(e => e.IdCalificacion == id);
        }
    }
}