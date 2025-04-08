using Microsoft.AspNetCore.Mvc;
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
    }
}