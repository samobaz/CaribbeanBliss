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
    public class EmpleadosController : Controller
    {
        private readonly CaribbeanContext _context;

        public EmpleadosController(CaribbeanContext context)
        {
            _context = context;
        }
        
        [RoleAuthorize(3, 4)]
        // GET: Empleados
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Empleados.Include(e => e.Rol);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Empleados/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                TempData["ErrorMessage"] = "ID de empleado no especificado";
                return NotFound();
            }

            var empleado = await _context.Empleados
                .Include(e => e.Rol)
                .FirstOrDefaultAsync(m => m.IdEmpleado == id);
            if (empleado == null)
            {
                TempData["ErrorMessage"] = "Empleado no encontrado";
                return NotFound();
            }

            return View(empleado);
        }

        // GET: Empleados/Create
        public IActionResult Create()
        {
            ViewData["RolId"] = new SelectList(_context.Roles, "IdRol", "NombreRol");
            return View();
        }

        // POST: Empleados/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdEmpleado,NombreEmpleado,EmailEmpleado,EstadoEmpleado,RolId")] Empleado empleado)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(empleado);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Empleado creado exitosamente";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {
                    TempData["ErrorMessage"] = "Error al crear el empleado";
                }
            }
            ViewData["RolId"] = new SelectList(_context.Roles, "IdRol", "NombreRol", empleado.RolId);
            return View(empleado);
        }

        // GET: Empleados/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                TempData["ErrorMessage"] = "ID de empleado no especificado";
                return NotFound();
            }

            var empleado = await _context.Empleados.FindAsync(id);
            if (empleado == null)
            {
                TempData["ErrorMessage"] = "Empleado no encontrado";
                return NotFound();
            }
            ViewData["RolId"] = new SelectList(_context.Roles, "IdRol", "NombreRol", empleado.RolId);
            return View(empleado);
        }

        // POST: Empleados/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdEmpleado,NombreEmpleado,EmailEmpleado,EstadoEmpleado,RolId")] Empleado empleado)
        {
            if (id != empleado.IdEmpleado)
            {
                TempData["ErrorMessage"] = "ID de empleado no coincide";
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(empleado);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Empleado actualizado exitosamente";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmpleadoExists(empleado.IdEmpleado))
                    {
                        TempData["ErrorMessage"] = "Empleado no encontrado";
                        return NotFound();
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Error al actualizar el empleado";
                        throw;
                    }
                }
            }
            ViewData["RolId"] = new SelectList(_context.Roles, "IdRol", "NombreRol", empleado.RolId);
            return View(empleado);
        }

        // GET: Empleados/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                TempData["ErrorMessage"] = "ID de empleado no especificado";
                return NotFound();
            }

            var empleado = await _context.Empleados
                .Include(e => e.Rol)
                .FirstOrDefaultAsync(m => m.IdEmpleado == id);
            if (empleado == null)
            {
                TempData["ErrorMessage"] = "Empleado no encontrado";
                return NotFound();
            }

            return View(empleado);
        }

        // POST: Empleados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try 
            {
                var empleado = await _context.Empleados.FindAsync(id);
                if (empleado != null)
                {
                    _context.Empleados.Remove(empleado);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Empleado eliminado exitosamente";
                }
                else
                {
                    TempData["ErrorMessage"] = "Empleado no encontrado";
                }
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Error al eliminar el empleado";
            }
            return RedirectToAction(nameof(Index));
        }

        private bool EmpleadoExists(int id)
        {
            return _context.Empleados.Any(e => e.IdEmpleado == id);
        }
    }
}