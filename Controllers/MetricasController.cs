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
    public class MetricasController : Controller
    {
        private readonly CaribbeanContext _context;

        public MetricasController(CaribbeanContext context)
        {
            _context = context;
        }

        [RoleAuthorize(2, 3, 4)]
        // GET: Metricas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Metricas.ToListAsync());
        }

        // GET: Metricas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var metrica = await _context.Metricas
                .FirstOrDefaultAsync(m => m.IdMetrica == id);
            if (metrica == null)
            {
                return NotFound();
            }

            return View(metrica);
        }

        // GET: Metricas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Metricas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdMetrica,Fecha,IngresosTotales,TasaOcupacion,OcupacionDiaria,OcupacionSemanal,OcupacionMensual,ReservasNuevas,Cancelaciones,ImpactoFinancieroCancelaciones,PromedioDiasAnticipacionReserva,NumeroHuespedes,SuscritosBoletin,DuracionPromedioEstadia")] Metrica metrica)
        {
            if (ModelState.IsValid)
            {
                _context.Add(metrica);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(metrica);
        }

        // GET: Metricas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var metrica = await _context.Metricas.FindAsync(id);
            if (metrica == null)
            {
                return NotFound();
            }
            return View(metrica);
        }

        // POST: Metricas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdMetrica,Fecha,IngresosTotales,TasaOcupacion,OcupacionDiaria,OcupacionSemanal,OcupacionMensual,ReservasNuevas,Cancelaciones,ImpactoFinancieroCancelaciones,PromedioDiasAnticipacionReserva,NumeroHuespedes,SuscritosBoletin,DuracionPromedioEstadia")] Metrica metrica)
        {
            if (id != metrica.IdMetrica)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(metrica);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MetricaExists(metrica.IdMetrica))
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
            return View(metrica);
        }

        // GET: Metricas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var metrica = await _context.Metricas
                .FirstOrDefaultAsync(m => m.IdMetrica == id);
            if (metrica == null)
            {
                return NotFound();
            }

            return View(metrica);
        }

        // POST: Metricas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var metrica = await _context.Metricas.FindAsync(id);
            if (metrica != null)
            {
                _context.Metricas.Remove(metrica);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MetricaExists(int id)
        {
            return _context.Metricas.Any(e => e.IdMetrica == id);
        }

        [HttpGet]
        public async Task<IActionResult> GetMetricasFiltradas(DateTime? startDate, DateTime? endDate)
        {
            try
            {
                // Case 1: No dates provided - get most recent record
                if (!startDate.HasValue && !endDate.HasValue)
                {
                    var mostReciente = await _context.Metricas
                        .OrderByDescending(m => m.Fecha)
                        .FirstOrDefaultAsync();

                    if (mostReciente == null)
                        return Json(new { success = false, error = "No hay registros disponibles" });

                    return Json(new
                    {
                        success = true,
                        data = new
                        {
                            ingresosTotales = mostReciente.IngresosTotales,
                            tasaOcupacion = mostReciente.TasaOcupacion,
                            reservasNuevas = mostReciente.ReservasNuevas,
                            cancelaciones = mostReciente.Cancelaciones,
                            huespedes = mostReciente.NumeroHuespedes,
                            datosGraficos = new[]
                            {
                                new
                                {
                                    fecha = mostReciente.Fecha.ToString("yyyy-MM-dd"),
                                    ingresos = mostReciente.IngresosTotales,
                                    ocupacion = mostReciente.TasaOcupacion,
                                    reservas = mostReciente.ReservasNuevas,
                                    cancelaciones = mostReciente.Cancelaciones,
                                    huespedes = mostReciente.NumeroHuespedes
                                }
                            }
                        }
                    });
                }

                // Case 2: Single date provided
                if (startDate.HasValue && (!endDate.HasValue || startDate.Value.Date == endDate.Value.Date))
                {
                    var registro = await _context.Metricas
                        .Where(m => m.Fecha.Date == startDate.Value.Date)
                        .FirstOrDefaultAsync();

                    if (registro == null)
                        return Json(new { success = false, error = $"No hay registros para la fecha {startDate.Value:yyyy-MM-dd}" });

                    return Json(new
                    {
                        success = true,
                        data = new
                        {
                            ingresosTotales = registro.IngresosTotales,
                            tasaOcupacion = registro.TasaOcupacion,
                            reservasNuevas = registro.ReservasNuevas,
                            cancelaciones = registro.Cancelaciones,
                            huespedes = registro.NumeroHuespedes,
                            datosGraficos = new[]
                            {
                                new
                                {
                                    fecha = registro.Fecha.ToString("yyyy-MM-dd"),
                                    ingresos = registro.IngresosTotales,
                                    ocupacion = registro.TasaOcupacion,
                                    reservas = registro.ReservasNuevas,
                                    cancelaciones = registro.Cancelaciones,
                                    huespedes = registro.NumeroHuespedes
                                }
                            }
                        }
                    });
                }

                // Case 3: Date range provided
                if (startDate.HasValue && endDate.HasValue)
                {
                    var metricas = await _context.Metricas
                        .Where(m => m.Fecha.Date >= startDate.Value.Date && m.Fecha.Date <= endDate.Value.Date)
                        .OrderBy(m => m.Fecha)
                        .ToListAsync();

                    if (!metricas.Any())
                        return Json(new { success = false, error = "No hay registros para el rango de fechas seleccionado" });

                    return Json(new
                    {
                        success = true,
                        data = new
                        {
                            ingresosTotales = metricas.Sum(m => m.IngresosTotales),
                            reservasNuevas = metricas.Sum(m => m.ReservasNuevas),
                            cancelaciones = metricas.Sum(m => m.Cancelaciones),
                            huespedes = metricas.Sum(m => m.NumeroHuespedes),
                            tasaOcupacion = Math.Round(metricas.Average(m => m.TasaOcupacion), 2), // Mantenemos el promedio para la tasa
                            datosGraficos = metricas.Select(m => new
                            {
                                fecha = m.Fecha.ToString("yyyy-MM-dd"),
                                ingresos = m.IngresosTotales,
                                ocupacion = m.TasaOcupacion,
                                reservas = m.ReservasNuevas,
                                cancelaciones = m.Cancelaciones,
                                huespedes = m.NumeroHuespedes
                            }).ToList()
                        }
                    });
                }

                return Json(new { success = false, error = "Parámetros de fecha inválidos" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = "Error al procesar la solicitud: " + ex.Message });
            }
        }
        
        [RoleAuthorize(2, 3, 4)]
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}