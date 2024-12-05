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
    public class ClienteReservasController : Controller
    {
        private readonly CaribbeanContext _context;

        public ClienteReservasController(CaribbeanContext context)
        {
            _context = context;
        }

        // GET: ClienteReservas
        public async Task<IActionResult> Index()
        {
            return View(await _context.ClienteReserva.ToListAsync());
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
        public async Task<IActionResult> Create([Bind("IdClienteReserva,NombreCliente,Huespedes,TipoDocumento,NumeroDocumento,fechaReserva,fechaLlegada,fechaSalida,IdHabitacion,TipodeHabitacion")] ClienteReserva clienteReserva)
        {
            if (ModelState.IsValid)
            {
                _context.Add(clienteReserva);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(clienteReserva);
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
