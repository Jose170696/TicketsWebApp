using Microsoft.AspNetCore.Mvc;
using TicketsWebApp.Models;
using TicketsWebApp.Services;

namespace TicketsWebApp.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly AuthService _authService;

        public UsuariosController(AuthService authService)
        {
            _authService = authService;
        }

        public async Task<IActionResult> Users()
        {
            // Obtener la lista de usuarios
            var usuarios = await _authService.ListarUsuariosAsync();

            // Pasar la lista de usuarios a la vista
            return View(usuarios);
        }


        public async Task<IActionResult> Edit(int id)
        {
            var usuarios = await _authService.ListarUsuariosAsync();
            var usuario = usuarios.FirstOrDefault(u => u.us_identificador == id);

            if (usuario == null)
                return NotFound();

            return View(usuario);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Usuarios usuario)
        {
            if (!ModelState.IsValid)
            {
                return View(usuario);
            }

            var exito = await _authService.ActualizarUsuarioAsync(usuario.us_identificador, usuario);

            if (!exito)
            {
                ModelState.AddModelError(string.Empty, "No se pudo actualizar el usuario.");
                return View(usuario);
            }
            TempData["MensajeExito"] = "El usuario se actualizó correctamente.";
            return RedirectToAction("Users");
        }
    }
}