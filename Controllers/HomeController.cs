using Avance.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Avance.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            string url = "http://apiservicios.ecuasolmovsa.com:3009/api/Varios/GetEmisor";
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var emisores = JsonSerializer.Deserialize<IEnumerable<EmisorModel>>(data);

                    var nombresEmisores = emisores.Select(e => e.NombreEmisor); // Obtain only the names of the emisores

                    return View("Index", nombresEmisores);
                }
                else
                {
                    return View("Exception", new { message = "Error retrieving data" });
                }
            }
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password, string emisor)
        {
            try
            {
                string encodedUsername = Uri.EscapeDataString(username);
                string encodedPassword = Uri.EscapeDataString(password);
                string url = $"http://apiservicios.ecuasolmovsa.com:3009/api/Usuarios?usuario={encodedUsername}&password={encodedPassword}";
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        var data = await response.Content.ReadAsStringAsync();
                        var usuarios = JsonSerializer.Deserialize<IEnumerable<Usuario>>(data);

                        // Parse the JSON response and perform necessary checks
                        // ...

                        return RedirectToAction("CentroCostos", "CentroCostos");
                    }
                    else
                    {
                        return View("Exception", new { message = "Usuario o contraseña incorrectos" });
                    }
                }
            }
            catch
            {
                return View("Exception", new { message = "Usuario o contraseña incorrectos" });
            }
        }
    }
}



