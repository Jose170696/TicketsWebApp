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
                        TempData["MensajeBienvenida"] = "�Bienvenido/a al sistema!";
                        return RedirectToAction("Dashboard");
                    }
                    else
                    {
                        _logger.LogWarning($"Intento de inicio de sesi�n fallido para el correo: {model.us_correo}");
                        ViewBag.ErrorMessage = "Correo o contrase�a incorrectos.";
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error al autenticar usuario: {ex.Message}");
                    ViewBag.ErrorMessage = "Ocurri� un problema al iniciar sesi�n. Int�ntalo de nuevo.";
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
                        TempData["MensajeExito"] = "�Registro exitoso! Ahora pod�s iniciar sesi�n.";
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


        public IActionResult Dashboard()
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
