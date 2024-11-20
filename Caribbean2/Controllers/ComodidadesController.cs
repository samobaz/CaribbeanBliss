using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Caribbean2.Controllers
{
    public class ComodidadesController : Controller
    {
        private readonly CaribbeanContext _context;

        public ComodidadesController(CaribbeanContext context)
        {
            _context = context;
        }

        // GET: Comodidades
        public async Task<IActionResult> Index()
        {
            return View(await _context.Comodidades.ToListAsync());
        }

        // GET: Comodidades/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comodidad = await _context.Comodidades
                .FirstOrDefaultAsync(m => m.IdComodidad == id);
            if (comodidad == null)
            {
                return NotFound();
            }

            return View(comodidad);
        }

        // GET: Comodidades/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Comodidades/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdComodidad,Nombre,Descripcion")] Comodidad comodidad)
        {
            if (ModelState.IsValid)
            {
                _context.Add(comodidad);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(comodidad);
        }

        // GET: Comodidades/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comodidad = await _context.Comodidades.FindAsync(id);
            if (comodidad == null)
            {
                return NotFound();
            }
            return View(comodidad);
        }

        // POST: Comodidades/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdComodidad,Nombre,Descripcion")] Comodidad comodidad)
        {
            if (id != comodidad.IdComodidad)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(comodidad);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComodidadExists(comodidad.IdComodidad))
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
            return View(comodidad);
        }

        // GET: Comodidades/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comodidad = await _context.Comodidades
                .FirstOrDefaultAsync(m => m.IdComodidad == id);
            if (comodidad == null)
            {
                return NotFound();
            }

            return View(comodidad);
        }

        // POST: Comodidades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var comodidad = await _context.Comodidades.FindAsync(id);
            if (comodidad != null)
            {
                _context.Comodidades.Remove(comodidad);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComodidadExists(int id)
        {
            return _context.Comodidades.Any(e => e.IdComodidad == id);
        }
    }
}
