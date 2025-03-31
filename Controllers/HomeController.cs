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

        public HomeController(ILogger<HomeController> logger, AuthService authService)
        {
            _logger = logger;
            _authService = authService;
        }

        public IActionResult Index()
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

        public IActionResult Dashboard()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
