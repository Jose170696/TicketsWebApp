using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using TicketsWebApp.Models;

namespace TicketsWebApp.Services
{
    public class TicketService
    {
        private readonly HttpClient _httpClient;

        public TicketService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Tiquetes>> ObtenerTicketsPorCorreoAsync(string correo)
        {
            var response = await _httpClient.GetAsync($"api/Tiquetes/usuario/{correo}");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var tickets = JsonSerializer.Deserialize<List<Tiquetes>>(json);
                return tickets ?? new List<Tiquetes>();
            }

            return new List<Tiquetes>();
        }

        public async Task<List<Tiquetes>> ListarTiquetesAsync()
        {
            var response = await _httpClient.GetAsync("api/Tiquetes");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var tiquetes = JsonSerializer.Deserialize<List<Tiquetes>>(json);
                return tiquetes;
            }

            return new List<Tiquetes>();
        }

        public async Task<bool> ActualizarTiqueteAsync(int id, Tiquetes tiquete)
        {
            var json = JsonSerializer.Serialize(tiquete);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"api/Tiquetes/{id}", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> CrearTiqueteAsync(Tiquetes tiquete)
        {
            var json = JsonSerializer.Serialize(tiquete);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/Tiquetes", content);

            return response.IsSuccessStatusCode;
        }

    }
}