using Caribbean2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;

namespace Caribbean2.Controllers
{
    public class ClientesController : Controller
    {
        private readonly CaribbeanContext _context;

        public ClientesController(CaribbeanContext context)
        {
            _context = context;
        }

        [RoleAuthorize(2, 3, 4)]
        // Acción para listar clientes
        public async Task<IActionResult> Index()
        {
            var clientes = await _context.Clientes.ToListAsync();
            return View(clientes);
        }

        // Acción para mostrar el formulario de registro de un nuevo cliente
        public IActionResult Create()
        {
            ViewBag.RolId = new SelectList(_context.Roles, "IdRol", "NombreRol");
            return View();
        }

        // Acción para registrar un nuevo cliente
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cliente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.RolId = new SelectList(_context.Roles, "IdRol", "NombreRol", cliente.idRol);
            return View(cliente);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAjax([FromForm] Cliente cliente)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(cliente);
                    await _context.SaveChangesAsync();
                    return Json(new { 
                        success = true, 
                        message = "Cliente creado correctamente",
                        cliente = new { 
                            id = cliente.idCliente, 
                            nombre = cliente.nombre 
                        }
                    });
                }

                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return Json(new { 
                    success = false, 
                    message = "Datos inválidos", 
                    errors = errors 
                });
            }
            catch (Exception ex)
            {
                return Json(new { 
                    success = false, 
                    message = "Error al crear el cliente: " + ex.Message 
                });
            }
        }

        // Acción para mostrar los detalles de un cliente
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .FirstOrDefaultAsync(m => m.idCliente == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // Acción para mostrar el formulario de edición de un cliente
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            ViewBag.RolId = new SelectList(_context.Roles, "IdRol", "NombreRol", cliente.idRol);
            return View(cliente);
        }

        // Acción para editar un cliente
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Cliente cliente)
        {
            if (id != cliente.idCliente)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cliente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteExists(cliente.idCliente))
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
            ViewBag.RolId = new SelectList(_context.Roles, "IdRol", "NombreRol", cliente.idRol);
            return View(cliente);
        }

        // Acción para mostrar el formulario de eliminación de un cliente
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .FirstOrDefaultAsync(m => m.idCliente == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // Acción para eliminar un cliente
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Acción para mostrar la vista ClientesAdmin
        public async Task<IActionResult> ClientesAdmin()
        {
            var clientes = await _context.Clientes.ToListAsync();
            return View(clientes);
        }

        private bool ClienteExists(int id)
        {
            return _context.Clientes.Any(e => e.idCliente == id);
        }
    }
}
