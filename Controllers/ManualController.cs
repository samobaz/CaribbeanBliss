using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace Caribbean2.Controllers
{
    public class ManualController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ManualController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Descargar()
        {
            var rutaArchivo = Path.Combine(_webHostEnvironment.WebRootPath, "manuales", "manual-usuario.pdf");
            
            if (!System.IO.File.Exists(rutaArchivo))
            {
                return NotFound("El manual no está disponible en este momento.");
            }

            var tipoContenido = "application/pdf";
            var nombreArchivo = "Manual_Usuario_Caribbean_Bliss.pdf";

            return PhysicalFile(rutaArchivo, tipoContenido, nombreArchivo);
        }

        public IActionResult VerEnLinea()
        {
            var rutaArchivo = Path.Combine(_webHostEnvironment.WebRootPath, "manuales", "manual-usuario.html");
            
            if (!System.IO.File.Exists(rutaArchivo))
            {
                return NotFound("La versión en línea del manual no está disponible.");
            }

            return File(System.IO.File.ReadAllBytes(rutaArchivo), "text/html");
        }
    }
}