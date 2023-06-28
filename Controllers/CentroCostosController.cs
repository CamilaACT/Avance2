using Avance.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Avance.Controllers
{
    public class CentroCostosController : Controller
    {
        public async Task<IActionResult> CentroCostos()
        {
            string url = "http://apiservicios.ecuasolmovsa.com:3009/api/Varios/CentroCostosSelect";
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var centroCostos = JsonSerializer.Deserialize<IEnumerable<CentroCostos>>(data);
                    return View(centroCostos);
                }
                else
                {
                    return View("Exception", new { message = "Error retrieving data" });
                }
            }
        }

        [HttpPost]
        public async Task<IActionResult> PushCentroCostos(string codigo, string descripcion)
        {
            try
            {
                string url = $"http://apiservicios.ecuasolmovsa.com:3009/api/Varios/CentroCostosInsert?codigocentrocostos={codigo}&descripcioncentrocostos={descripcion}";
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("CentroCostos");
                    }
                    else
                    {
                        return View("Exception", new { message = "Error al insertar el centro de costos" });
                    }
                }
            }
            catch
            {
                return View("Exception", new { message = "Error al insertar el centro de costos" });
            }
        }

        public IActionResult AddCentroDeCostos()
        {
            return View();
        }
    }
}

