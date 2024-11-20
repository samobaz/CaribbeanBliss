using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Caribbean2.Controllers
{
    public class HabitacionEstadoController : Controller
    {
        private readonly CaribbeanContext _context;

        public HabitacionEstadoController(CaribbeanContext context)
        {
            _context = context;
        }

        // GET: HabitacionEstado
        public async Task<IActionResult> Index()
        {
            return View(await _context.HabitacionEstados.ToListAsync());
        }

        // GET: HabitacionEstado/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var habitacionEstado = await _context.HabitacionEstados
                .FirstOrDefaultAsync(m => m.IdEstado == id);
            if (habitacionEstado == null)
            {
                return NotFound();
            }

            return View(habitacionEstado);
        }

        // GET: HabitacionEstado/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: HabitacionEstado/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdEstado,Nombre")] HabitacionEstado habitacionEstado)
        {
            if (ModelState.IsValid)
            {
                _context.Add(habitacionEstado);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(habitacionEstado);
        }

        // GET: HabitacionEstado/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var habitacionEstado = await _context.HabitacionEstados.FindAsync(id);
            if (habitacionEstado == null)
            {
                return NotFound();
            }
            return View(habitacionEstado);
        }

        // POST: HabitacionEstado/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdEstado,Nombre")] HabitacionEstado habitacionEstado)
        {
            if (id != habitacionEstado.IdEstado)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(habitacionEstado);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HabitacionEstadoExists(habitacionEstado.IdEstado))
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
            return View(habitacionEstado);
        }

        // GET: HabitacionEstado/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var habitacionEstado = await _context.HabitacionEstados
                .FirstOrDefaultAsync(m => m.IdEstado == id);
            if (habitacionEstado == null)
            {
                return NotFound();
            }

            return View(habitacionEstado);
        }

        // POST: HabitacionEstado/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var habitacionEstado = await _context.HabitacionEstados.FindAsync(id);
            if (habitacionEstado != null)
            {
                _context.HabitacionEstados.Remove(habitacionEstado);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HabitacionEstadoExists(int id)
        {
            return _context.HabitacionEstados.Any(e => e.IdEstado == id);
        }
    }
}
