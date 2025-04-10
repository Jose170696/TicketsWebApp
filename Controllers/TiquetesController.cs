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

            return View(tiquete);
        }

        [HttpPost]
        public async Task<IActionResult> Actualizar(Tiquetes tiquete)
        {
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
    }
}