using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Caribbean2.Models;

namespace Caribbean2.Controllers
{
    public class SuscripcionesController : Controller
    {
        private readonly CaribbeanContext _context;

        public SuscripcionesController(CaribbeanContext context)
        {
            _context = context;
        }
        [RoleAuthorize(2, 3, 4)]
        // GET: Suscripciones
        public async Task<IActionResult> Index()
        {
            return View(await _context.Suscripciones.ToListAsync());
        }

        // GET: Suscripciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                TempData["ErrorMessage"] = "ID de suscriptor no proporcionado.";
                return NotFound();
            }

            var suscripcion = await _context.Suscripciones
                .FirstOrDefaultAsync(m => m.IdSuscripcion == id);
            if (suscripcion == null)
            {
                TempData["ErrorMessage"] = "Suscriptor no encontrado.";
                return NotFound();
            }

            return View(suscripcion);
        }

        // GET: Suscripciones/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Suscripciones/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdSuscripcion,Nombre,Email,FechaSuscripcion")] Suscripcion suscripcion)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(suscripcion);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Suscriptor creado exitosamente.";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = $"Error al crear el suscriptor: {ex.Message}";
                }
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                TempData["ErrorMessage"] = "Datos inválidos: " + string.Join(", ", errors);
            }

            return View(suscripcion);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateIndex([Bind("IdSuscripcion,Nombre,Email,FechaSuscripcion")] Suscripcion suscripcion)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(suscripcion);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Suscriptor creado exitosamente.";
                    return RedirectToAction("Index","Caribbean");
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = $"Error al crear el suscriptor: {ex.Message}";
                }
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                TempData["ErrorMessage"] = "Datos inválidos: " + string.Join(", ", errors);
            }

            return View("Index","Caribbean");
        }

        // GET: Suscripciones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                TempData["ErrorMessage"] = "ID de suscriptor no proporcionado.";
                return NotFound();
            }

            var suscripcion = await _context.Suscripciones.FindAsync(id);
            if (suscripcion == null)
            {
                TempData["ErrorMessage"] = "Suscriptor no encontrado.";
                return NotFound();
            }
            return View(suscripcion);
        }

        // POST: Suscripciones/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdSuscripcion,Nombre,Email,FechaSuscripcion")] Suscripcion suscripcion)
        {
            if (id != suscripcion.IdSuscripcion)
            {
                TempData["ErrorMessage"] = "ID de suscriptor no coincide.";
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(suscripcion);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Suscriptor editado exitosamente.";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SuscripcionExists(suscripcion.IdSuscripcion))
                    {
                        TempData["ErrorMessage"] = "Suscriptor no encontrado.";
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                TempData["ErrorMessage"] = "Datos inválidos: " + string.Join(", ", errors);
            }

            return View(suscripcion);
        }

        // GET: Suscripciones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                TempData["ErrorMessage"] = "ID de suscriptor no proporcionado.";
                return NotFound();
            }

            var suscripcion = await _context.Suscripciones
                .FirstOrDefaultAsync(m => m.IdSuscripcion == id);
            if (suscripcion == null)
            {
                TempData["ErrorMessage"] = "Suscriptor no encontrado.";
                return NotFound();
            }

            return View(suscripcion);
        }

        // POST: Suscripciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var suscripcion = await _context.Suscripciones.FindAsync(id);
            if (suscripcion != null)
            {
                _context.Suscripciones.Remove(suscripcion);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Suscriptor eliminada exitosamente.";
            }
            else
            {
                TempData["ErrorMessage"] = "Suscriptor no encontrada.";
            }

            return RedirectToAction(nameof(Index));
        }

        private bool SuscripcionExists(int id)
        {
            return _context.Suscripciones.Any(e => e.IdSuscripcion == id);
        }
    }
}
