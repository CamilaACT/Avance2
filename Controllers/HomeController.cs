using Avance1.Models;
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
    }
}



