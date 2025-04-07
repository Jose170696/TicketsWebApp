using System.Text;
using System.Text.Json;
using TicketsWebApp.Models;

namespace TicketsWebApp.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> AuthenticateAsync(LoginModel loginModel)
        {
            var json = JsonSerializer.Serialize(loginModel);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/usuarios/login", content);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> RegistrarUsuarioAsync(Usuarios usuario)
        {
            var json = JsonSerializer.Serialize(usuario);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/usuarios/registrar", content);

            return response.IsSuccessStatusCode;
        }
    }
}