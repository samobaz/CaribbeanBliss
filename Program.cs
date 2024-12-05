using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Configurar servicios
builder.Services.AddControllersWithViews();

// Configurar la conexión a la base de datos
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<CaribbeanContext>(options =>
    options.UseSqlServer(connectionString));

// Configurar sesiones
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Construir la aplicación
var app = builder.Build();

// Configuración del pipeline de manejo de solicitudes
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Middleware para sesiones
app.UseSession();

// Middleware global para redirigir usuarios no autenticados
app.Use(async (context, next) =>
{
    var path = context.Request.Path.ToString().ToLower();
    
    // Lista de rutas públicas permitidas
    var publicPaths = new[]
    {
        "/",  // Add root path
        "/caribbean/index",
        "/caribbean",  // Add base Caribbean controller path
        "/caribbean/login",
        "/usuarios/login",
        "/usuarios/create",
        "/usuarios/forgotpassword",
        "/usuarios/resetpassword",
        "/usuarios/accessdenied",
        "/css/",
        "/js/",
        "/lib/",
        "/img/"
    };

    // Permitir acceso a rutas públicas
    if (context.Session.GetInt32("UserRoleId") != null || 
        publicPaths.Any(p => path.StartsWith(p)) || 
        path == "/")
    {
        await next();
        return;
    }

    // Redirigir a login solo si se intenta acceder a rutas protegidas
    context.Response.Redirect("/Caribbean/Login");
});

// Autorizar después de configurar el middleware global
app.UseAuthorization();

// Configuración de rutas
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Caribbean}/{action=Index}/{id?}");

app.Run();

// Clase RoleAuthorizeAttribute
public class RoleAuthorizeAttribute : ActionFilterAttribute
{
    private readonly int[] _roles;

    public RoleAuthorizeAttribute(params int[] roles)
    {
        _roles = roles;
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var userRoleId = context.HttpContext.Session.GetInt32("UserRoleId");

        if (userRoleId == null || !_roles.Contains(userRoleId.Value))
        {
            // Redirigir directamente a AccessDenied
            context.Result = new RedirectToActionResult("AccessDenied", "Usuarios", null);
            return;
        }

        base.OnActionExecuting(context);
    }
}
