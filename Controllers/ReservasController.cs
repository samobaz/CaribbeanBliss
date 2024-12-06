using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Caribbean2.Models;
using Microsoft.Extensions.Logging;

namespace Caribbean2.Controllers
{
    public class ReservasController : Controller
    {
        private readonly CaribbeanContext _context;

        public ReservasController(CaribbeanContext context)
        {
            _context = context;
        }

        [RoleAuthorize(2, 3, 4)]
        // GET: Reservas
        public async Task<IActionResult> Index()
        {
            var caribbeanContext = _context.Reservas
                .Include(r => r.Cliente)
                .Include(r => r.Habitacion)
                .Include(r => r.Estado)
                .Include(r => r.Huespedes)
                .Include(r => r.Servicios)
                .Include(r => r.Pagos);
            return View(await caribbeanContext.ToListAsync());
        }

        // GET: Reservas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reservas
                .Include(r => r.Cliente)
                .Include(r => r.Habitacion)
                .Include(r => r.Estado)
                .Include(r => r.Huespedes)
                .Include(r => r.Servicios)
                .Include(r => r.Pagos)
                .FirstOrDefaultAsync(m => m.IdReserva == id);
            if (reserva == null)
            {
                return NotFound();
            }

            return View(reserva);
        }

        // GET: Reservas/Create
        public IActionResult Create()
        {
            try 
            {
                // Crear un generador de números aleatorios
                Random random = new Random();

                // Modificar la consulta de habitaciones
                var habitaciones = _context.Habitaciones
                    .Where(h => h.IdEstado == 1)
                    .Select(h => new
                    {
                        h.IdHabitacion,
                        NumeroHabitacion = random.Next(1, 101),
                        h.Nombre,
                        h.PrecioHabitacion,
                        h.Capacidad
                    })
                    .ToList();

                // Crear SelectList para habitaciones
                ViewBag.IdHabitacion = habitaciones.Select(h => new SelectListItem
                {
                    Value = h.IdHabitacion.ToString(),
                    Text = $"Habitación {h.NumeroHabitacion} - {h.Nombre} - ${h.PrecioHabitacion:N2}"
                });

                // Guardar los precios para JavaScript
                ViewBag.HabitacionesPrecios = habitaciones.ToDictionary(
                    h => h.IdHabitacion,
                    h => h.PrecioHabitacion
                );

                // Preparar el resto de ViewBag
                ViewBag.IdCliente = new SelectList(_context.Clientes.Where(c => c.ClienteEstado), "idCliente", "nombre");
                ViewBag.Huespedes = new SelectList(_context.Huespedes, "Id", "NombreCompleto");
                ViewBag.IdEstado = new SelectList(_context.ReservaEstados, "IdEstado", "Nombre");
                ViewBag.ServiciosActivos = _context.Servicios
                    .Where(s => s.EstadoServicio)
                    .Select(s => new
                    {
                        s.IdServicio,
                        s.Nombre,
                        s.PrecioServicio,
                        DisplayText = $"{s.Nombre} - ${s.PrecioServicio:N2}"
                    })
                    .ToList();

                ViewBag.RolId = new SelectList(_context.Roles, "IdRol", "NombreRol");
                ViewBag.IdEstadoHuesped = new SelectList(_context.HuespedEstados, "IdEstadoHuesped", "NombreEstado");

                // Guardar los precios y capacidades como diccionarios serializados
                ViewBag.HabitacionesPrecios = habitaciones.ToDictionary(h => h.IdHabitacion, h => h.PrecioHabitacion);
                ViewBag.HabitacionesCapacidad = habitaciones.ToDictionary(h => h.IdHabitacion, h => h.Capacidad);

                return View();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ocurrió un error al cargar los datos: " + ex.Message);
                return View();
            }
        }        

        // POST: Reservas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromBody] ReservaViewModel reservaData)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                if (ModelState.IsValid)
                {
                    var reserva = new Reserva
                    {
                        IdCliente = reservaData.IdCliente,
                        IdHabitacion = reservaData.IdHabitacion,
                        FechaInicio = reservaData.FechaInicio,
                        FechaFin = reservaData.FechaFin,
                        NumeroPersonas = reservaData.NumeroPersonas,
                        PrecioTotal = reservaData.PrecioTotal,
                        Anticipo = reservaData.Anticipo,
                        Notas = reservaData.Notas,
                        IdEstado = reservaData.IdEstado,
                        Huespedes = new List<Huesped>(),
                        Servicios = new List<Servicio>()
                    };

                    // Agregar huéspedes
                    if (reservaData.HuespedesSeleccionados != null)
                    {
                        foreach (var huespedId in reservaData.HuespedesSeleccionados)
                        {
                            var huesped = await _context.Huespedes.FindAsync(huespedId);
                            if (huesped != null)
                            {
                                reserva.Huespedes.Add(huesped);
                            }
                        }
                    }

                    // Agregar servicios
                    if (reservaData.ServiciosSeleccionados != null)
                    {
                        foreach (var servicioId in reservaData.ServiciosSeleccionados)
                        {
                            var servicio = await _context.Servicios.FindAsync(servicioId);
                            if (servicio != null)
                            {
                                reserva.Servicios.Add(servicio);
                            }
                        }
                    }

                    _context.Reservas.Add(reserva);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    return Json(new { success = true, message = "Reserva creada correctamente" });
                }

                return Json(new { success = false, message = "Error de validación del modelo" });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return Json(new { success = false, message = ex.Message });
            }
        }

        public class ReservaViewModel
        {
            public int IdCliente { get; set; }
            public int IdHabitacion { get; set; }
            public DateTime FechaInicio { get; set; }
            public DateTime FechaFin { get; set; }
            public int NumeroPersonas { get; set; }
            public decimal PrecioTotal { get; set; }
            public decimal Anticipo { get; set; }
            public string Notas { get; set; }
            public int IdEstado { get; set; }
            public int[] HuespedesSeleccionados { get; set; }
            public int[] ServiciosSeleccionados { get; set; }
        }

        // Método auxiliar para preparar ViewBags
        private void PrepararViewBags(Reserva reserva = null)
        {
            Random random = new Random();

            var habitaciones = _context.Habitaciones
                .Where(h => h.IdEstado == 1)
                .Select(h => new
                {
                    h.IdHabitacion,
                    NumeroHabitacion = random.Next(1, 101),
                    h.Nombre,
                    h.PrecioHabitacion,
                    h.Capacidad
                })
                .ToList();

            ViewBag.IdCliente = new SelectList(_context.Clientes.Where(c => c.ClienteEstado), "idCliente", "nombre", reserva?.IdCliente);
            ViewBag.Huespedes = new SelectList(_context.Huespedes, "Id", "NombreCompleto");
            ViewBag.IdHabitacion = habitaciones.Select(h => new SelectListItem
            {
                Value = h.IdHabitacion.ToString(),
                Text = $"Habitación {h.NumeroHabitacion} - {h.Nombre} - ${h.PrecioHabitacion:N2}"
            });
            ViewBag.IdEstado = new SelectList(_context.ReservaEstados, "IdEstado", "Nombre", reserva?.IdEstado);
            ViewBag.ServiciosActivos = _context.Servicios
                .Where(s => s.EstadoServicio)
                .ToList();

            ViewBag.RolId = new SelectList(_context.Roles, "IdRol", "NombreRol");
            ViewBag.IdEstadoHuesped = new SelectList(_context.HuespedEstados, "IdEstadoHuesped", "NombreEstado");

            ViewBag.HabitacionesPrecios = habitaciones.ToDictionary(h => h.IdHabitacion, h => h.PrecioHabitacion);
            ViewBag.HabitacionesCapacidad = habitaciones.ToDictionary(h => h.IdHabitacion, h => h.Capacidad);
        }

        // GET: Reservas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Cargar la reserva con sus relaciones
            var reserva = await _context.Reservas
                .Include(r => r.Cliente)
                .Include(r => r.Habitacion)
                .Include(r => r.Estado)
                .Include(r => r.Servicios)
                .FirstOrDefaultAsync(m => m.IdReserva == id);

            if (reserva == null)
            {
                return NotFound();
            }

            // Obtener IDs de servicios seleccionados
            var serviciosSeleccionadosIds = reserva.Servicios.Select(s => s.IdServicio).ToList();

            // Preparar las listas desplegables
            ViewData["IdCliente"] = new SelectList(_context.Clientes.Where(c => c.ClienteEstado), "idCliente", "nombre", reserva.IdCliente);
            ViewData["IdHabitacion"] = new SelectList(_context.Habitaciones.Where(h => h.IdEstado == 1), "IdHabitacion", "Nombre", reserva.IdHabitacion);
            ViewData["IdEstado"] = new SelectList(_context.ReservaEstados, "IdEstado", "Nombre", reserva.IdEstado);

            // Cargar servicios activos con MaterializeAsync() para debug
            var serviciosActivos = await _context.Servicios
                .Where(s => s.EstadoServicio)
                .Select(s => new
                {
                    s.IdServicio,
                    s.Nombre,
                    s.PrecioServicio,
                    Seleccionado = serviciosSeleccionadosIds.Contains(s.IdServicio)
                })
                .ToListAsync();

            // Debug - verificar datos
            foreach (var servicio in serviciosActivos)
            {
                System.Diagnostics.Debug.WriteLine($"Servicio: {servicio.Nombre} - Precio: {servicio.PrecioServicio}");
            }

            ViewBag.ServiciosActivos = serviciosActivos;

            return View(reserva);
        }

        // POST: Reservas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Reserva reserva, int[] HuespedesSeleccionados, int[] ServiciosSeleccionados)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            
            if (id != reserva.IdReserva)
            {
                return NotFound();
            }

            try
            {
                var reservaExistente = await _context.Reservas
                    .Include(r => r.Huespedes)
                    .Include(r => r.Servicios)
                    .FirstOrDefaultAsync(r => r.IdReserva == id);

                if (reservaExistente == null)
                {
                    return NotFound();
                }

                // Actualizar propiedades básicas
                reservaExistente.IdCliente = reserva.IdCliente;
                reservaExistente.IdHabitacion = reserva.IdHabitacion;
                reservaExistente.FechaInicio = reserva.FechaInicio;
                reservaExistente.FechaFin = reserva.FechaFin;
                reservaExistente.NumeroPersonas = reserva.NumeroPersonas;
                reservaExistente.PrecioTotal = reserva.PrecioTotal;
                reservaExistente.Anticipo = reserva.Anticipo;
                reservaExistente.Notas = reserva.Notas;
                reservaExistente.IdEstado = reserva.IdEstado;

                // Actualizar huéspedes
                reservaExistente.Huespedes.Clear();
                if (HuespedesSeleccionados != null)
                {
                    var huespedes = await _context.Huespedes
                        .Where(h => HuespedesSeleccionados.Contains(h.Id))
                        .ToListAsync();
                    foreach (var huesped in huespedes)
                    {
                        reservaExistente.Huespedes.Add(huesped);
                    }
                }

                // Actualizar servicios
                reservaExistente.Servicios.Clear();
                if (ServiciosSeleccionados != null)
                {
                    var servicios = await _context.Servicios
                        .Where(s => ServiciosSeleccionados.Contains(s.IdServicio))
                        .ToListAsync();
                    foreach (var servicio in servicios)
                    {
                        reservaExistente.Servicios.Add(servicio);
                    }
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                await transaction.RollbackAsync();
                if (!ReservaExists(reserva.IdReserva))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                ModelState.AddModelError("", "Ocurrió un error al guardar los cambios.");
                PrepararViewBags(reserva);
                return View(reserva);
            }
        }

        // GET: Reservas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reservas
                .Include(r => r.Cliente)
                .Include(r => r.Habitacion)
                .Include(r => r.Estado)
                .Include(r => r.Huespedes)
                .Include(r => r.Servicios)
                .Include(r => r.Pagos)
                .FirstOrDefaultAsync(m => m.IdReserva == id);
            if (reserva == null)
            {
                return NotFound();
            }

            return View(reserva);
        }

        // POST: Reservas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reserva = await _context.Reservas.FindAsync(id);
            if (reserva != null)
            {
                _context.Reservas.Remove(reserva);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool ReservaExists(int id)
        {
            return _context.Reservas.Any(e => e.IdReserva == id);
        }

        public JsonResult GetHuespedes()
        {
            var huespedes = _context.Huespedes
                .Select(h => new { 
                    value = h.Id, 
                    text = h.NombreCompleto 
                })
                .ToList();
            return Json(huespedes);
        }
    }
}