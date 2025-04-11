using Microsoft.AspNetCore.Mvc;
using TicketsWebApp.Models;
using TicketsWebApp.Services;

namespace TicketsWebApp.Controllers
{
    public class TiquetesController : Controller
    {
        private readonly TicketService _ticketService;

        public TiquetesController(TicketService ticketService)
        {
            _ticketService = ticketService;
        }

        public async Task<IActionResult> Actualizar(int id)
        {
            var tiquetes = await _ticketService.ListarTiquetesAsync();
            var tiquete = tiquetes.FirstOrDefault(t => t.ti_identificador == id);

            if (tiquete == null)
                return NotFound();

            // Verificar si el tiquete ya está resuelto
            if (tiquete.ti_solucion == "resuelto")
            {
                TempData["MensajeExito"] = "Este tiquete ya está resuelto y no puede ser actualizado.";
                return RedirectToAction("Dashboard", "Home", new { id = tiquete.ti_identificador });
            }

            return View(tiquete);
        }

        [HttpPost]
        public async Task<IActionResult> Actualizar(Tiquetes tiquete)
        {
            // Verificar si el tiquete ya está resuelto, en caso de que se haya enviado el formulario
            var tiqueteExistente = await _ticketService.ListarTiquetesAsync();
            var tiqueteOriginal = tiqueteExistente.FirstOrDefault(t => t.ti_identificador == tiquete.ti_identificador);

            if (tiqueteOriginal != null && tiqueteOriginal.ti_solucion == "resuelto")
            {
                ModelState.AddModelError(string.Empty, "Este tiquete ya está resuelto y no puede ser actualizado.");
                return View(tiquete);
            }

            if (!ModelState.IsValid)
                return View(tiquete);

            var exito = await _ticketService.ActualizarTiqueteAsync(tiquete.ti_identificador, tiquete);

            if (!exito)
            {
                ModelState.AddModelError(string.Empty, "No se pudo actualizar el tiquete.");
                return View(tiquete);
            }

            TempData["MensajeExito"] = "El tiquete se actualizó correctamente.";
            return RedirectToAction("Dashboard", "Home");
        }



        // Acción para mostrar el formulario de creación
        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }

        // Acción para manejar la creación del tiquete
        [HttpPost]
        public async Task<IActionResult> Crear(Tiquetes tiquete)
        {
            if (ModelState.IsValid)
            {
                var success = await _ticketService.CrearTiqueteAsync(tiquete);

                if (success)
                {
                    TempData["MensajeExito"] = "Tiquete creado con éxito.";
                    return RedirectToAction("Dashboard", "Home");
                }

                TempData["MensajeError"] = "Hubo un error al crear el tiquete.";
            }

            return View(tiquete);
        }
    }
}