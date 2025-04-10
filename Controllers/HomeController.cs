using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TicketsWebApp.Models;
using TicketsWebApp.Services;

namespace TicketsWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AuthService _authService;
        private readonly TicketService _ticketService;

        public HomeController(ILogger<HomeController> logger, AuthService authService, TicketService ticketService)
        {
            _logger = logger;
            _authService = authService;
            _ticketService = ticketService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var isAuthenticated = await _authService.AuthenticateAsync(model);

                    if (isAuthenticated)
                    {
                        _logger.LogInformation($"Usuario autenticado: {model.us_correo}");
                        // Guardar correo en sesión
                        HttpContext.Session.SetString("UsuarioCorreo", model.us_correo);
                        TempData["MensajeBienvenida"] = "¡Bienvenido/a al sistema!";
                        return RedirectToAction("Dashboard");
                    }
                    else
                    {
                        _logger.LogWarning($"Intento de inicio de sesión fallido para el correo: {model.us_correo}");
                        ViewBag.ErrorMessage = "Correo o contraseña incorrectos.";
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error al autenticar usuario: {ex.Message}");
                    ViewBag.ErrorMessage = "Ocurrió un problema al iniciar sesión. Inténtalo de nuevo.";
                }
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Register(Usuarios nuevoUsuario)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var resultado = await _authService.RegistrarUsuarioAsync(nuevoUsuario);

                    if (resultado)
                    {
                        TempData["MensajeExito"] = "¡Registro exitoso! Ahora podés iniciar sesión.";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "No se pudo registrar el usuario.";
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = "Error al crear la cuenta: " + ex.Message;
                }
            }
            return View(nuevoUsuario);
        }

        public async Task<IActionResult> Dashboard()
        {
            var correo = HttpContext.Session.GetString("UsuarioCorreo");

            if (string.IsNullOrEmpty(correo))
            {
                return RedirectToAction("Index");
            }

            var tiquetes = await _ticketService.ObtenerTicketsPorCorreoAsync(correo);
            return View(tiquetes);
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
            {
                return View(tiquete);
            }

            var exito = await _ticketService.ActualizarTiqueteAsync(tiquete.ti_identificador, tiquete);

            if (!exito)
            {
                ModelState.AddModelError(string.Empty, "No se pudo actualizar el tiquete.");
                return View(tiquete);
            }
            TempData["MensajeExito"] = "El tiquete se actualizó correctamente.";
            return RedirectToAction("Dashboard");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
