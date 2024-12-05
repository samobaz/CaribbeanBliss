using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Caribbean2.Models;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;

namespace Caribbean2.Controllers
{
    [Route("[controller]")]

    public class UsuariosController : Controller
    {
        private readonly CaribbeanContext _context;

        public UsuariosController(CaribbeanContext context)
        {
            _context = context;
        }

        private static readonly Dictionary<string, string> _resetTokens = new Dictionary<string, string>();

        // GET: Usuarios
        [RoleAuthorize(2,3,4)]
        [HttpGet]
        public async Task<IActionResult> Index(string searchString, int? pageNumber)
        {
            ViewData["CurrentFilter"] = searchString;
            var usuarios = from u in _context.Usuarios select u;

            if (!String.IsNullOrEmpty(searchString))
            {
                usuarios = usuarios.Where(s => s.NombresApellidos.Contains(searchString)
                                           || s.Correo.Contains(searchString)
                                           || s.Identificacion.Contains(searchString));
            }

            int pageSize = 5;
            return View(await PaginatedList<Usuarios>.CreateAsync(usuarios, pageNumber ?? 1, pageSize));
        }

        // GET: Usuarios/Details/5
        [HttpGet("Details/{id?}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuarios = await _context.Usuarios
                .FirstOrDefaultAsync(m => m.UsuarioID == id);
            if (usuarios == null)
            {
                return NotFound();
            }

            return View(usuarios);
        }

        // GET: Usuarios/Create
        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Usuarios/Create
        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UsuarioViewModel model)
        {
            if (ModelState.IsValid)
            {
                var existingEmail = await _context.Usuarios
                    .FirstOrDefaultAsync(u => u.Correo == model.Correo);
                var existingIdentificacion = await _context.Usuarios
                    .FirstOrDefaultAsync(u => u.Identificacion == model.Identificacion);
                var existingNombreUsuario = await _context.Usuarios
                    .FirstOrDefaultAsync(u => u.NombresApellidos == model.NombreUsuario);

                if (existingEmail != null)
                {
                    return Json(new { success = false, message = "Este correo ya está registrado" });
                }
                if (existingIdentificacion != null)
                {
                    return Json(new { success = false, message = "Esta identificación ya está registrada" });
                }
                if (existingNombreUsuario != null)
                {
                    return Json(new { success = false, message = "Este nombre de usuario ya está registrado" });
                }

                var usuario = new Usuarios
                {
                    NombresApellidos = model.NombreUsuario,
                    TipoIdentificacion = model.TipoIdentificacion,
                    Identificacion = model.Identificacion,
                    Telefono = model.Telefono,
                    Correo = model.Correo,
                    Contrasena = model.Contrasena,
                    FechaRegistro = DateTime.Now,
                    Estado = true,
                    IdRol = 1 // Set default role
                };

                _context.Add(usuario);
                await _context.SaveChangesAsync();
                
                // Agregar mensaje para SweetAlert
                TempData["Success"] = "Usuario creado exitosamente";
                return Json(new { success = true, message = "Usuario creado exitosamente" });
            }
            return Json(new { success = false, message = "Datos inválidos" });
        }

        // GET: Usuarios/Edit/5
        [HttpGet("Edit/{id?}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuarios = await _context.Usuarios.FindAsync(id);
            if (usuarios == null)
            {
                return NotFound();
            }

            // Load roles for dropdown
            ViewBag.Roles = await _context.Roles
                .Select(r => new SelectListItem 
                { 
                    Value = r.IdRol.ToString(), 
                    Text = r.DescripcionRol,
                    Selected = r.IdRol == usuarios.IdRol
                })
                .ToListAsync();

            return View(usuarios);
        }

        // POST: Usuarios/Edit/5
        [HttpPost("Edit/{id?}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UsuarioID,NombresApellidos,TipoIdentificacion,Identificacion,Telefono,Correo,FechaRegistro,Estado,IdRol")] Usuarios usuario)
        {
            if (id != usuario.UsuarioID)
            {
                return Json(new { success = false, message = "ID de usuario no coincide" });
            }

            // Remove password validation
            ModelState.Remove("Contrasena");

            if (ModelState.IsValid)
            {
                try
                {
                    var existingUser = await _context.Usuarios.AsNoTracking().FirstOrDefaultAsync(u => u.UsuarioID == id);
                    if (existingUser == null)
                    {
                        return Json(new { success = false, message = "Usuario no encontrado" });
                    }

                    // Preserve the existing password
                    usuario.Contrasena = existingUser.Contrasena;
                    
                    // Update including role
                    _context.Update(usuario);
                    await _context.SaveChangesAsync();
                    return Json(new { success = true, message = "Usuario actualizado correctamente" });
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuariosExists(usuario.UsuarioID))
                    {
                        return Json(new { success = false, message = "Usuario no encontrado" });
                    }
                    else
                    {
                        return Json(new { success = false, message = "Error de concurrencia al actualizar" });
                    }
                }
            }

            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
            return Json(new { success = false, message = "Datos inválidos: " + string.Join(", ", errors) });
        }

        // GET: Usuarios/Delete/5
        [HttpGet("Delete/{id?}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuarios = await _context.Usuarios
                .FirstOrDefaultAsync(m => m.UsuarioID == id);
            if (usuarios == null)
            {
                return NotFound();
            }

            return View(usuarios);
        }

        // POST: Usuarios/Delete/5
        [HttpPost("Delete/{id?}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try 
            {
                var usuarios = await _context.Usuarios.FindAsync(id);
                if (usuarios == null)
                {
                    return Json(new { success = false, message = "Usuario no encontrado" });
                }

                _context.Usuarios.Remove(usuarios);
                await _context.SaveChangesAsync();
                
                return Json(new { success = true, message = "Usuario eliminado correctamente" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error al eliminar el usuario: " + ex.Message });
            }
        }

        private bool UsuariosExists(int id)
        {
            return _context.Usuarios.Any(e => e.UsuarioID == id);
        }

        // GET: Usuarios/Login
        [HttpGet("Login")]
        public IActionResult Login()
        {
            return View(); // Buscará en /Views/Usuarios/Login.cshtml
        }

        [HttpPost("Login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([FromForm] string Correo, [FromForm] string Contrasena)
        {
            try
            {
                if (string.IsNullOrEmpty(Correo) || string.IsNullOrEmpty(Contrasena))
                {
                    return Json(new { success = false, message = "Por favor ingrese correo y contraseña" });
                }

                var usuario = await _context.Usuarios
                    .FirstOrDefaultAsync(u => u.Correo == Correo && u.Contrasena == Contrasena);

                if (usuario == null)
                {
                    return Json(new { success = false, message = "Usuario o contraseña incorrectos" });
                }

                if (!usuario.Estado)
                {
                    return Json(new { success = false, message = "Usuario inactivo" });
                }

                HttpContext.Session.SetInt32("UserId", usuario.UsuarioID);
                HttpContext.Session.SetInt32("UserRoleId", usuario.IdRol);
                
                var userRole = await _context.Roles.FirstOrDefaultAsync(r => r.IdRol == usuario.IdRol);
                if (userRole != null)
                {
                    HttpContext.Session.SetString("UserRole", userRole.DescripcionRol);
                }

                // Determine redirect URL based on role
                string redirectUrl = usuario.IdRol switch
                {
                    1 or 2 => Url.Action("Profile", "Usuarios"),
                    3 or 4 => Url.Action("Index", "Usuarios"),
                    _ => Url.Action("Index", "Caribbean")
                };

                return Json(new { 
                    success = true, 
                    message = "Inicio de sesión exitoso",
                    redirectUrl = redirectUrl
                });
            }
            catch (Exception ex)
            {
                return Json(new { 
                    success = false, 
                    message = $"Error al iniciar sesión: {ex.Message}" 
                });
            }
        }

        // UsuariosController.cs - Modificar método Register
        [HttpPost("Register")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UsuarioViewModel model)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _context.Usuarios
                    .FirstOrDefaultAsync(u => u.Correo == model.Correo);
                    
                if (existingUser != null)
                {
                    ModelState.AddModelError("Correo", "Este correo ya está registrado");
                    return View("Login", model);
                }

                var usuario = new Usuarios
                {
                    NombresApellidos = model.NombreUsuario,
                    TipoIdentificacion = model.TipoIdentificacion,
                    Identificacion = model.Identificacion,
                    Telefono = model.Telefono,
                    Correo = model.Correo,
                    Contrasena = model.Contrasena,
                    FechaRegistro = DateTime.Now,
                    Estado = true,
                    IdRol = 1 // Set default role
                };
                try
                {
                    _context.Add(usuario);
                    await _context.SaveChangesAsync();
                    return Json(new { success = true, message = "Usuario registrado exitosamente" });
                }
                catch (DbUpdateException)
                {
                    return Json(new { success = false, message = "Error al registrar el usuario" });
                }
            }
            return Json(new { success = false, message = "Datos inválidos" });
        }

        // Add this method to UsuariosController.cs
        [HttpGet("Profile")]
        public async Task<IActionResult> Profile()
        {
            // Get logged in user ID from session (you'll need to implement this)
            var userId = HttpContext.Session.GetInt32("UserId");
            
            if (userId == null)
            {
                return RedirectToAction("Login");
            }

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.UsuarioID == userId);
                
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        [HttpPost("Profile")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Profile([Bind("UsuarioID,NombresApellidos,TipoIdentificacion,Identificacion,Telefono,Correo")] Usuarios usuario, string Contrasena, string CurrentPassword)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return Json(new { success = false, message = "Sesión no válida" });
            }

            usuario.UsuarioID = userId.Value;

            try
            {
                var existingUser = await _context.Usuarios.FindAsync(usuario.UsuarioID);
                if (existingUser == null)
                {
                    return Json(new { success = false, message = "Usuario no encontrado" });
                }

                // Verificar la contraseña actual
                if (existingUser.Contrasena != CurrentPassword)
                {
                    return Json(new { success = false, message = "La contraseña actual es incorrecta" });
                }

                // Actualizar campos básicos
                existingUser.NombresApellidos = usuario.NombresApellidos;
                existingUser.TipoIdentificacion = usuario.TipoIdentificacion;
                existingUser.Identificacion = usuario.Identificacion;
                existingUser.Telefono = usuario.Telefono;
                existingUser.Correo = usuario.Correo;

                // Actualizar contraseña solo si se proporciona una nueva
                if (!string.IsNullOrEmpty(Contrasena))
                {
                    existingUser.Contrasena = Contrasena;
                }

                ModelState.Remove("Contrasena");

                if (!ModelState.IsValid)
                {
                    var errors = string.Join(" | ", ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage));
                    return Json(new { success = false, message = errors });
                }

                _context.Update(existingUser);
                await _context.SaveChangesAsync();
                
                return Json(new { success = true, message = "Perfil actualizado exitosamente" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error al actualizar: " + ex.Message });
            }
        }

        [HttpPost("ForgotPassword")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword([FromForm]string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return Json(new { success = false, message = "El correo es requerido" });
            }

            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Correo == email);
            
            if (usuario == null)
            {
                return Json(new { success = false, message = "No se encontró ninguna cuenta con ese correo" });
            }

            try
            {
                string token = Guid.NewGuid().ToString();
                _resetTokens[token] = email;

                var resetLink = Url.Action("ResetPassword", "Usuarios", 
                    new { token = token, email = email }, Request.Scheme);

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Caribbean Bliss", "garciaperezjuan026@gmail.com"));
                message.To.Add(new MailboxAddress("", email));
                message.Subject = "Restablecer Contraseña";
                message.Body = new TextPart("html")
                {
                    Text = $@"
                        <h2>Restablecer Contraseña</h2>
                        <p>Para restablecer tu contraseña, haz clic en el siguiente enlace:</p>
                        <p><a href='{resetLink}'>Restablecer Contraseña</a></p>"
                };

                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                    await client.AuthenticateAsync("garciaperezjuan026@gmail.com", "ibjz qhyk mgqy zpmv");
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                }

                return Json(new { success = true, message = "Se ha enviado un enlace a tu correo" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error al enviar el correo: " + ex.Message });
            }
        }

        [HttpGet("ResetPassword")]
        public IActionResult ResetPassword(string token, string email)
        {
            if (string.IsNullOrEmpty(token) || !_resetTokens.ContainsKey(token))
            {
                return RedirectToAction("Login");
            }

            ViewBag.Token = token;
            ViewBag.Email = email;
            return View();
        }

        [HttpPost("ResetPassword")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "Datos inválidos" });
            }

            if (!_resetTokens.ContainsKey(model.Token))
            {
                return Json(new { success = false, message = "Token inválido o expirado" });
            }

            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Correo == model.Email);
            if (usuario == null)
            {
                return Json(new { success = false, message = "Usuario no encontrado" });
            }

            try
            {
                usuario.Contrasena = model.NewPassword;
                _context.Update(usuario);
                await _context.SaveChangesAsync();
                _resetTokens.Remove(model.Token);

                return Json(new { 
                    success = true, 
                    message = "Contraseña actualizada correctamente"
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error: {ex.Message}" });
            }
        }

        [HttpPost("ToggleStatus")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleStatus(int id, bool status)
        {
            try
            {
                var usuario = await _context.Usuarios.FindAsync(id);
                if (usuario == null)
                {
                    
                    return Json(new { success = false, message = "Usuario no encontrado" });
                }

                usuario.Estado = status;
                await _context.SaveChangesAsync();

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error al actualizar el estado: " + ex.Message });
            }
        }

        [HttpGet("Logout")]
        public IActionResult Logout()
        {
            try
            {
                HttpContext.Session.Clear();
                TempData["AlertType"] = "info";
                TempData["AlertMessage"] = "Has cerrado sesión exitosamente.";
                return RedirectToAction("Login", "Caribbean");
            }
            catch (Exception ex)
            {
                TempData["AlertType"] = "error";
                TempData["AlertMessage"] = $"Error al cerrar sesión: {ex.Message}";
                return RedirectToAction("Usuarios", "Profile");
            }
        }


        [HttpGet("AccessDenied")]
        public IActionResult AccessDenied()
        {
            return View(); // Retorna directamente la vista
        }
    }
}
