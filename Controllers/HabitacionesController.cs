using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Caribbean2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Caribbean2.Controllers
{
    public class HabitacionesController : Controller
    {
        private readonly CaribbeanContext _context;

        public HabitacionesController(CaribbeanContext context)
        {
            _context = context;
        }

        [RoleAuthorize(2, 3, 4)]
        // GET: Habitaciones
        public async Task<IActionResult> Index()
        {
            var caribbeanContext = _context.Habitaciones.Include(h => h.EstadoHabitacion);
            return View(await caribbeanContext.ToListAsync());
        }

        // GET: Habitaciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var habitacion = await _context.Habitaciones
                .Include(h => h.EstadoHabitacion)
                .FirstOrDefaultAsync(m => m.IdHabitacion == id);
            if (habitacion == null)
            {
                return NotFound();
            }

            return View(habitacion);
        }

        // GET: Habitaciones/Create
        public IActionResult Create()
        {
            ViewData["IdEstado"] = new SelectList(_context.HabitacionEstados, "IdEstado", "Nombre");
            return View();
        }

        // POST: Habitaciones/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdHabitacion,Nombre,Descripcion,Capacidad,NumeroHabitacion,PrecioHabitacion,IdEstado")] Habitacion habitacion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(habitacion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdEstado"] = new SelectList(_context.HabitacionEstados, "IdEstado", "Nombre", habitacion.IdEstado);
            return View(habitacion);
        }

        // GET: Habitaciones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var habitacion = await _context.Habitaciones.FindAsync(id);
            if (habitacion == null)
            {
                return NotFound();
            }
            ViewData["IdEstado"] = new SelectList(_context.HabitacionEstados, "IdEstado", "Nombre", habitacion.IdEstado);
            return View(habitacion);
        }

        // POST: Habitaciones/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdHabitacion,Nombre,Descripcion,Capacidad,NumeroHabitacion,PrecioHabitacion,IdEstado")] Habitacion habitacion)
        {
            if (id != habitacion.IdHabitacion)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(habitacion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HabitacionExists(habitacion.IdHabitacion))
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
            ViewData["IdEstado"] = new SelectList(_context.HabitacionEstados, "IdEstado", "Nombre", habitacion.IdEstado);
            return View(habitacion);
        }

        // GET: Habitaciones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var habitacion = await _context.Habitaciones
                .Include(h => h.EstadoHabitacion)
                .FirstOrDefaultAsync(m => m.IdHabitacion == id);
            if (habitacion == null)
            {
                return NotFound();
            }

            return View(habitacion);
        }

        // POST: Habitaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var habitacion = await _context.Habitaciones.FindAsync(id);
            if (habitacion != null)
            {
                _context.Habitaciones.Remove(habitacion);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HabitacionExists(int id)
        {
            return _context.Habitaciones.Any(e => e.IdHabitacion == id);
        }
    }
}