using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Caribbean2.Models;
using Microsoft.AspNetCore.Http;

namespace Caribbean2.Controllers
{
    public class ClienteReservasController : Controller
    {
        private readonly CaribbeanContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ClienteReservasController(CaribbeanContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        // GET: ClienteReservas
        public async Task<IActionResult> Index()
        {
            try
            {
                // Obtener el ID del usuario de la sesión actual
                var userId = HttpContext.Session.GetInt32("UserId");
                
                if (userId == null)
                {
                    return RedirectToAction("Login", "Usuarios");
                }

                // Obtener el rol del usuario
                var userRoleId = HttpContext.Session.GetInt32("UserRoleId");

                var reservations = await _context.ClienteReserva
                    .OrderByDescending(r => r.fechaReserva)
                    .ToListAsync();

                // Si el usuario no es admin (roles 3 o 4), filtrar solo sus reservas
                if (userRoleId != 3 && userRoleId != 4)
                {
                    reservations = reservations.Where(r => r.UsuarioId == userId.ToString()).ToList();
                }

                return View(reservations);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading reservations: {ex.Message}");
                return Problem("Error loading reservations");
            }
        }

        // GET: ClienteReservas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clienteReserva = await _context.ClienteReserva
                .FirstOrDefaultAsync(m => m.IdClienteReserva == id);
            if (clienteReserva == null)
            {
                return NotFound();
            }

            return View(clienteReserva);
        }

        // GET: ClienteReservas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ClienteReservas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] ClienteReserva clienteReserva)
        {
            try
            {
                ModelState.Remove("UsuarioId");

                if (!ModelState.IsValid)
                {
                    var errors = string.Join(" | ", ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage));
                    return Json(new { success = false, message = errors });
                }

                // Obtener el ID del usuario de la sesión
                var userId = HttpContext.Session.GetInt32("UserId");
                if (userId == null)
                {
                    return Json(new { success = false, message = "Usuario no autenticado" });
                }

                // Establecer campos requeridos
                clienteReserva.fechaReserva = DateOnly.FromDateTime(DateTime.Now);
                clienteReserva.UsuarioId = userId.ToString();
                clienteReserva.IdHabitacion = new Random().Next(101, 410);

                _context.ClienteReserva.Add(clienteReserva);
                var saveResult = await _context.SaveChangesAsync();

                if (saveResult > 0)
                {
                    return Json(new
                    {
                        success = true,
                        message = "Reserva creada exitosamente",
                        redirectUrl = Url.Action("Index", "ClienteReservas"),
                        reservationId = clienteReserva.IdClienteReserva
                    });
                }
                else
                {
                    throw new Exception("No se pudo guardar la reserva en la base de datos.");
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error al crear la reserva: {ex.Message}" });
            }
        }

        // GET: ClienteReservas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clienteReserva = await _context.ClienteReserva.FindAsync(id);
            if (clienteReserva == null)
            {
                return NotFound();
            }
            return View(clienteReserva);
        }

        // POST: ClienteReservas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdClienteReserva,NombreCliente,Huespedes,TipoDocumento,NumeroDocumento,fechaReserva,fechaLlegada,fechaSalida,IdHabitacion,TipodeHabitacion")] ClienteReserva clienteReserva)
        {
            if (id != clienteReserva.IdClienteReserva)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clienteReserva);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteReservaExists(clienteReserva.IdClienteReserva))
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
            return View(clienteReserva);
        }

        // GET: ClienteReservas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clienteReserva = await _context.ClienteReserva
                .FirstOrDefaultAsync(m => m.IdClienteReserva == id);
            if (clienteReserva == null)
            {
                return NotFound();
            }

            return View(clienteReserva);
        }

        // POST: ClienteReservas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var clienteReserva = await _context.ClienteReserva.FindAsync(id);
            if (clienteReserva != null)
            {
                _context.ClienteReserva.Remove(clienteReserva);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClienteReservaExists(int id)
        {
            return _context.ClienteReserva.Any(e => e.IdClienteReserva == id);
        }
        
    }
}
