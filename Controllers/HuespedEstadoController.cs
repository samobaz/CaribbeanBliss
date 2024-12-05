using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Caribbean2.Models; // Add this line to include the namespace where HuespedEstado is defined

namespace Caribbean2.Controllers
{
    public class HuespedEstadoController : Controller
    {
        private readonly CaribbeanContext _context;

        public HuespedEstadoController(CaribbeanContext context)
        {
            _context = context;
        }

        // GET: HuespedEstado
        public async Task<IActionResult> Index()
        {
            return View(await _context.HuespedEstados.ToListAsync());
        }

        // GET: HuespedEstado/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var huespedEstado = await _context.HuespedEstados
                .FirstOrDefaultAsync(m => m.IdEstadoHuesped == id);
            if (huespedEstado == null)
            {
                return NotFound();
            }

            return View(huespedEstado);
        }

        // GET: HuespedEstado/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: HuespedEstado/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdEstadoHuesped,NombreEstado")] HuespedEstado huespedEstado)
        {
            if (ModelState.IsValid)
            {
                _context.Add(huespedEstado);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(huespedEstado);
        }

        // GET: HuespedEstado/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var huespedEstado = await _context.HuespedEstados.FindAsync(id);
            if (huespedEstado == null)
            {
                return NotFound();
            }
            return View(huespedEstado);
        }

        // POST: HuespedEstado/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdEstadoHuesped,NombreEstado")] HuespedEstado huespedEstado)
        {
            if (id != huespedEstado.IdEstadoHuesped)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(huespedEstado);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HuespedEstadoExists(huespedEstado.IdEstadoHuesped))
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
            return View(huespedEstado);
        }

        // GET: HuespedEstado/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var huespedEstado = await _context.HuespedEstados
                .FirstOrDefaultAsync(m => m.IdEstadoHuesped == id);
            if (huespedEstado == null)
            {
                return NotFound();
            }

            return View(huespedEstado);
        }

        // POST: HuespedEstado/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var huespedEstado = await _context.HuespedEstados.FindAsync(id);
            if (huespedEstado != null)
            {
                _context.HuespedEstados.Remove(huespedEstado);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HuespedEstadoExists(int id)
        {
            return _context.HuespedEstados.Any(e => e.IdEstadoHuesped == id);
        }
    }
}
