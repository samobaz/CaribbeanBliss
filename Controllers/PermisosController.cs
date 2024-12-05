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
    public class PermisosController : Controller
    {
        private readonly CaribbeanContext _context;

        
        public PermisosController(CaribbeanContext context)
        {
            _context = context;
        }
        [RoleAuthorize( 3, 4)]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Permisos.ToListAsync());
        }
        
        [RoleAuthorize(3, 4)]
         // GET: Permisos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                TempData["ErrorMessage"] = "Permiso no encontrado.";
                return NotFound();
            }

            var permiso = await _context.Permisos
                .FirstOrDefaultAsync(m => m.IdPermiso == id);
            if (permiso == null)
            {
                TempData["ErrorMessage"] = "Permiso no encontrado.";
                return NotFound();
            }

            return View(permiso);
        }

        // GET: Permisos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Permisos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPermiso,NombrePermiso,DescripcionPermiso")] Permiso permiso)
        {
            if (ModelState.IsValid)
            {
                _context.Add(permiso);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Permiso creado exitosamente.";
                return RedirectToAction(nameof(Index));
            }
            TempData["ErrorMessage"] = "Error al crear el permiso.";
            return View(permiso);
        }

        // GET: Permisos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                TempData["ErrorMessage"] = "Permiso no encontrado.";
                return NotFound();
            }

            var permiso = await _context.Permisos.FindAsync(id);
            if (permiso == null)
            {
                TempData["ErrorMessage"] = "Permiso no encontrado.";
                return NotFound();
            }
            return View(permiso);
        }

        // POST: Permisos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPermiso,NombrePermiso,DescripcionPermiso")] Permiso permiso)
        {
            if (id != permiso.IdPermiso)
            {
                TempData["ErrorMessage"] = "Permiso no encontrado.";
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(permiso);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Permiso actualizado exitosamente.";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PermisoExists(permiso.IdPermiso))
                    {
                        TempData["ErrorMessage"] = "Permiso no encontrado.";
                        return NotFound();
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Error al actualizar el permiso.";
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            TempData["ErrorMessage"] = "Error al actualizar el permiso.";
            return View(permiso);
        }

        // GET: Permisos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                TempData["ErrorMessage"] = "Permiso no encontrado.";
                return NotFound();
            }

            var permiso = await _context.Permisos
                .FirstOrDefaultAsync(m => m.IdPermiso == id);
            if (permiso == null)
            {
                TempData["ErrorMessage"] = "Permiso no encontrado.";
                return NotFound();
            }

            return View(permiso);
        }

        // POST: Permisos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var permiso = await _context.Permisos.FindAsync(id);
            if (permiso != null)
            {
                _context.Permisos.Remove(permiso);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Permiso eliminado exitosamente.";
            }
            else
            {
                TempData["ErrorMessage"] = "Permiso no encontrado.";
            }

            return RedirectToAction(nameof(Index));
        }

        private bool PermisoExists(int id)
        {
            return _context.Permisos.Any(e => e.IdPermiso == id);
        }
    }
}