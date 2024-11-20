using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Caribbean2.Controllers
{
    public class ServicioEstadoController : Controller
    {
        private readonly CaribbeanContext _context;

        public ServicioEstadoController(CaribbeanContext context)
        {
            _context = context;
        }

        // GET: ServicioEstado
        public async Task<IActionResult> Index()
        {
            return View(await _context.ServicioEstados.ToListAsync());
        }

        // GET: ServicioEstado/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servicioEstado = await _context.ServicioEstados
                .FirstOrDefaultAsync(m => m.IdEstadoServicio == id);
            if (servicioEstado == null)
            {
                return NotFound();
            }

            return View(servicioEstado);
        }

        // GET: ServicioEstado/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ServicioEstado/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdEstadoServicio,NombreEstado")] ServicioEstado servicioEstado)
        {
            if (ModelState.IsValid)
            {
                _context.Add(servicioEstado);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(servicioEstado);
        }

        // GET: ServicioEstado/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servicioEstado = await _context.ServicioEstados.FindAsync(id);
            if (servicioEstado == null)
            {
                return NotFound();
            }
            return View(servicioEstado);
        }

        // POST: ServicioEstado/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdEstadoServicio,NombreEstado")] ServicioEstado servicioEstado)
        {
            if (id != servicioEstado.IdEstadoServicio)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(servicioEstado);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServicioEstadoExists(servicioEstado.IdEstadoServicio))
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
            return View(servicioEstado);
        }

        // GET: ServicioEstado/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servicioEstado = await _context.ServicioEstados
                .FirstOrDefaultAsync(m => m.IdEstadoServicio == id);
            if (servicioEstado == null)
            {
                return NotFound();
            }

            return View(servicioEstado);
        }

        // POST: ServicioEstado/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var servicioEstado = await _context.ServicioEstados.FindAsync(id);
            if (servicioEstado != null)
            {
                _context.ServicioEstados.Remove(servicioEstado);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServicioEstadoExists(int id)
        {
            return _context.ServicioEstados.Any(e => e.IdEstadoServicio == id);
        }
    }
}
