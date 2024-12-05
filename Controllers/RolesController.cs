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
    [RoleAuthorize(3,4)] // 2 siendo el ID del rol de administrador
    public class RolesController : Controller
    {
        private readonly CaribbeanContext _context;

        public RolesController(CaribbeanContext context)
        {
            _context = context;
        }

        [RoleAuthorize(3, 4)]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Roles.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rol = await _context.Roles
                .FirstOrDefaultAsync(m => m.IdRol == id);
            if (rol == null)
            {
                return NotFound();
            }

            return View(rol);
        }

        public IActionResult Create()
        {
            ViewBag.Permisos = _context.Permisos.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdRol,NombreRol,DescripcionRol,EstadoRol")] Rol rol, List<int> permisosSeleccionados)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rol);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Rol creado exitosamente.";
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Permisos = _context.Permisos.ToList();
            return View(rol);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rol = await _context.Roles
                .Include(r => r.Permisos)
                .FirstOrDefaultAsync(m => m.IdRol == id);

            if (rol == null)
            {
                return NotFound();
            }
            ViewBag.Permisos = _context.Permisos.ToList();
            return View(rol);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdRol,NombreRol,DescripcionRol,EstadoRol")] Rol rol, List<int> permisosSeleccionados)
        {
            if (id != rol.IdRol)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Actualizar permisos
                    var rolToUpdate = await _context.Roles
                        .Include(r => r.Permisos)
                        .FirstOrDefaultAsync(r => r.IdRol == id);

                    if (rolToUpdate == null)
                    {
                        return NotFound();
                    }

                    rolToUpdate.NombreRol = rol.NombreRol;
                    rolToUpdate.DescripcionRol = rol.DescripcionRol;
                    rolToUpdate.EstadoRol = rol.EstadoRol;

                    // Actualizar la lista de permisos
                    rolToUpdate.Permisos.Clear();
                    foreach (var permisoId in permisosSeleccionados)
                    {
                        var permiso = await _context.Permisos.FindAsync(permisoId);
                        if (permiso != null)
                        {
                            rolToUpdate.Permisos.Add(permiso);
                        }
                    }

                    _context.Update(rolToUpdate);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Rol actualizado exitosamente.";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RolExists(rol.IdRol))
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
            ViewBag.Permisos = _context.Permisos.ToList();
            return View(rol);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rol = await _context.Roles
                .FirstOrDefaultAsync(m => m.IdRol == id);
            if (rol == null)
            {
                return NotFound();
            }
            ViewBag.Permisos = _context.Permisos.ToList();
            return View(rol);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var rol = await _context.Roles
                    .Include(r => r.Permisos)
                    .FirstOrDefaultAsync(r => r.IdRol == id);

                if (rol == null)
                {
                    TempData["ErrorMessage"] = "Rol no encontrado.";
                    return RedirectToAction("Index");
                }

                // Check for protected roles
                if (id <= 4)
                {
                    TempData["ErrorMessage"] = "No se pueden eliminar roles protegidos del sistema.";
                    return RedirectToAction("Index");
                }

                // Check for associated users
                var hasUsers = await _context.Usuarios.AnyAsync(u => u.IdRol == id);
                if (hasUsers)
                {
                    TempData["ErrorMessage"] = "No se puede eliminar el rol porque tiene usuarios asociados.";
                    return RedirectToAction("Index");
                }

                // Clear role permissions
                rol.Permisos.Clear();
                await _context.SaveChangesAsync();

                // Remove the role
                _context.Roles.Remove(rol);
                await _context.SaveChangesAsync();
                
                await transaction.CommitAsync();
                TempData["SuccessMessage"] = "Rol eliminado exitosamente.";
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                TempData["ErrorMessage"] = "Error al eliminar el rol: " + ex.InnerException?.Message ?? ex.Message;
            }
            
            return RedirectToAction("Index");
        }

        private bool RolExists(int id)
        {
            return _context.Roles.Any(e => e.IdRol == id);
        }
    }
}