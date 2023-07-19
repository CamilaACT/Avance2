using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace Avance.Controllers
{
    public class TrabajadorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> PagBuscarTrabajadores()
        {
            var url = "http://apiservicios.ecuasolmovsa.com:3009/api/Varios/GetEmisor";
            var client = new HttpClient();
            var response = await client.GetStringAsync(url);
            var dataSucursal = JArray.Parse(response);

            ViewBag.dataSucursal = dataSucursal;

            return View("IndexBuscarTrabajador");
        }

        [HttpPost]
        public ActionResult PagTrabajadores()
        {
            var url = "http://apiservicios.ecuasolmovsa.com:3009/api/Varios/GetEmisor";
            var client = new HttpClient();
            var response = client.GetStringAsync(url).GetAwaiter().GetResult();
            var dataSucursal = JArray.Parse(response);

            var sucursal = Request.Form["sucursal"];

            var datos = dataSucursal[0] as IDictionary<string, JToken>;
            if (datos != null && datos.ContainsKey("NombreEmisor"))
            {
                var id = datos["Codigo"].ToString();

                url = "http://apiservicios.ecuasolmovsa.com:3009/api/Varios/TrabajadorSelect?sucursal=" + 2;
                response = client.GetStringAsync(url).GetAwaiter().GetResult();
                var data = JArray.Parse(response);
                ViewBag.data = data;

                return View("IndexInfoTrabajador", new
                {
                    data,
                    id
                });
            }

            return View();
        }


        public ActionResult PagTrabajadoresCreate(string id)
        {
            var url = "http://apiservicios.ecuasolmovsa.com:3009/api/Varios/GetEmisor";
            var client = new HttpClient();
            var response = client.GetStringAsync(url).GetAwaiter().GetResult();
            var dataSucursal = JArray.Parse(response);

            // Fetch other data objects
            var dataTipoTrabajador = GetJsonData("http://apiservicios.ecuasolmovsa.com:3009/api/Varios/TipoTrabajador");
            var dataGenero = GetJsonData("http://apiservicios.ecuasolmovsa.com:3009/api/Varios/Genero");
            var dataOcupaciones = GetJsonData("http://apiservicios.ecuasolmovsa.com:3009/api/Varios/Ocupaciones");
            var dataNivelSalarial = GetJsonData("http://apiservicios.ecuasolmovsa.com:3009/api/Varios/NivelSalarial");
            var dataTipoContrato = GetJsonData("http://apiservicios.ecuasolmovsa.com:3009/api/Varios/TipoContrato");
            var dataTipoCese = GetJsonData("http://apiservicios.ecuasolmovsa.com:3009/api/Varios/TipoCese");
            var dataEstadoCivil = GetJsonData("http://apiservicios.ecuasolmovsa.com:3009/api/Varios/EstadoCivil");
            var dataTipoComision = GetJsonData("http://apiservicios.ecuasolmovsa.com:3009/api/Varios/TipoComision");
            var dataPeriodoVacaciones = GetJsonData("http://apiservicios.ecuasolmovsa.com:3009/api/Varios/PeriodoVacaciones");
            var dataEsReingreso = GetJsonData("http://apiservicios.ecuasolmovsa.com:3009/api/Varios/EsReingreso");
            var dataTipoCuenta = GetJsonData("http://apiservicios.ecuasolmovsa.com:3009/api/Varios/TipoCuenta");
            var dataDecimoTerceroDecimoCuarto = GetJsonData("http://apiservicios.ecuasolmovsa.com:3009/api/Varios/DecimoTerceroDecimoCuarto");
            var dataCentroCostos = GetJsonData("http://apiservicios.ecuasolmovsa.com:3009/api/Varios/CentroCostosSelect");
            var dataCategoriaOcupacional = GetJsonData("http://apiservicios.ecuasolmovsa.com:3009/api/Varios/CategoriaOcupacional");

            return View("IndexAgregarTrabajador", new
            {
                id,
                dataSucursal,
                dataTipoTrabajador,
                dataGenero,
                dataOcupaciones,
                dataCategoriaOcupacional,
                dataCentroCostos,
                dataNivelSalarial,
                dataTipoContrato,
                dataTipoCese,
                dataEstadoCivil,
                dataTipoComision,
                dataPeriodoVacaciones,
                dataEsReingreso,
                dataTipoCuenta,
                dataDecimoTerceroDecimoCuarto
            });
        }

        public IActionResult TrabajadoresPost()
        {
            if (Request.Method == "POST")
            {
                string COMP_Codigo = Request.Form["COMP_Codigo"];
                string Tipo_trabajador = Request.Form["Tipo_trabajador"];
                string Apellido_Paterno = Request.Form["Apellido_Paterno"];
                string Apellido_Materno = Request.Form["Apellido_Materno"];
                string Nombres = Request.Form["Nombres"];
                string Identificacion = Request.Form["Identificacion"];
                string Entidad_Bancaria = Request.Form["Entidad_Bancaria"];
                string CarnetIESS = Request.Form["CarnetIESS"];
                string Direccion = Request.Form["Direccion"];
                string Telefono_Fijo = Request.Form["Telefono_Fijo"];
                string Telefono_Movil = Request.Form["Telefono_Movil"];
                string Genero = Request.Form["Genero"];
                string Nro_Cuenta_Bancaria = Request.Form["Nro_Cuenta_Bancaria"];
                string Codigo_Categoria_Ocupacion = Request.Form["Codigo_Categoria_Ocupacion"];
                string Ocupacion = Request.Form["Ocupacion"];
                string Centro_Costos = Request.Form["Centro_Costos"];
                string Nivel_Salarial = Request.Form["Nivel_Salarial"];
                string EstadoTrabajador = Request.Form["EstadoTrabajador"];
                string Tipo_Contrato = Request.Form["Tipo_Contrato"];
                string Tipo_Cese = Request.Form["Tipo_Cese"];
                string EstadoCivil = Request.Form["EstadoCivil"];
                string TipodeComision = Request.Form["TipodeComision"];
                string FechaNacimiento = Request.Form["FechaNacimiento"];
                string FechaIngreso = Request.Form["FechaIngreso"];
                string FechaCese = Request.Form["FechaCese"];
                string PeriododeVacaciones = Request.Form["PeriododeVacaciones"];
                string FechaReingreso = Request.Form["FechaReingreso"];
                string Fecha_Ult_Actualizacion = Request.Form["Fecha_Ult_Actualizacion"];
                string EsReingreso = Request.Form["EsReingreso"];
                string Tipo_Cuenta = Request.Form["Tipo_Cuenta"];
                string FormaCalculo13ro = Request.Form["FormaCalculo13ro"];
                string FormaCalculo14to = Request.Form["FormaCalculo14to"];
                string BoniComplementaria = Request.Form["BoniComplementaria"];
                string BoniEspecial = Request.Form["BoniEspecial"];
                string Remuneracion_Minima = Request.Form["Remuneracion_Minima"];
                string Fondo_Reserva = Request.Form["Fondo_Reserva"];
                string Mensaje = Request.Form["Mensaje"];

                var url = "http://apiservicios.ecuasolmovsa.com:3009/api/Varios/TrabajadorInsert";
                var data = GetJsonData(url);

                // Buscar código de categoría ocupacional
                url = "http://apiservicios.ecuasolmovsa.com:3009/api/Varios/CategoriaOcupacional";
                var dataCategoriaOcupacional = GetJsonData(url);

                foreach (var item in dataCategoriaOcupacional)
                {
                    if (item["Descripcion"].ToString() == Codigo_Categoria_Ocupacion)
                    {
                        Codigo_Categoria_Ocupacion = item["Codigo"].ToString();
                        break;
                    }
                }

                // Buscar código de ocupación
                url = "http://apiservicios.ecuasolmovsa.com:3009/api/Varios/Ocupaciones";
                var dataOcupaciones = GetJsonData(url);

                foreach (var item in dataOcupaciones)
                {
                    if (item["Descripcion"].ToString() == Ocupacion)
                    {
                        Ocupacion = item["Codigo"].ToString();
                        break;
                    }
                }

                // Buscar código de centro de costos
                url = "http://apiservicios.ecuasolmovsa.com:3009/api/Varios/CentroCostosSelect";
                var dataCentroCostos = GetJsonData(url);

                foreach (var item in dataCentroCostos)
                {
                    if (item["Descripcion"].ToString() == Centro_Costos)
                    {
                        Centro_Costos = item["Codigo"].ToString();
                        break;
                    }
                }

                // Buscar forma de cálculo 13
                url = "http://apiservicios.ecuasolmovsa.com:3009/api/Varios/DecimoTerceroDecimoCuarto";
                var dataDecimoTerceroDecimoCuarto = GetJsonData(url);

                foreach (var item in dataDecimoTerceroDecimoCuarto)
                {
                    if (item["Descripcion"].ToString() == FormaCalculo13ro)
                    {
                        FormaCalculo13ro = item["Codigo"].ToString();
                        break;
                    }
                }

                // Buscar forma de cálculo 14
                foreach (var item in dataDecimoTerceroDecimoCuarto)
                {
                    if (item["Descripcion"].ToString() == FormaCalculo14to)
                    {
                        FormaCalculo14to = item["Codigo"].ToString();
                        break;
                    }
                }

                url = "http://apiservicios.ecuasolmovsa.com:3009/api/Varios/TrabajadorInsert?COMP_Codigo=" + COMP_Codigo +
                      "&Tipo_trabajador=" + Tipo_trabajador + "&Apellido_Paterno=" + Apellido_Paterno + "&Apellido_Materno=" +
                      Apellido_Materno + "&Nombres=" + Nombres + "&Identificacion=" + Identificacion + "&Entidad_Bancaria=" +
                      Entidad_Bancaria + "&CarnetIESS=" + CarnetIESS + "&Direccion=" + Direccion + "&Telefono_Fijo=" +
                      Telefono_Fijo + "&Telefono_Movil=" + Telefono_Movil + "&Genero=" + Genero +
                      "&Nro_Cuenta_Bancaria=" + Nro_Cuenta_Bancaria + "&Codigo_Categoria_Ocupacion=" +
                      Codigo_Categoria_Ocupacion + "&Ocupacion=" + Ocupacion + "&Centro_Costos=" + Centro_Costos +
                      "&Nivel_Salarial=" + Nivel_Salarial + "&EstadoTrabajador=" + EstadoTrabajador + "&Tipo_Contrato=" + Tipo_Contrato +
                      "&Tipo_Cese=" + Tipo_Cese + "&EstadoCivil=" + EstadoCivil + "&TipodeComision=" + TipodeComision + "&FechaNacimiento=" + FechaNacimiento +
                      "&FechaIngreso=" + FechaIngreso + "&FechaCese=" + FechaCese + "&PeriododeVacaciones=" +
                      PeriododeVacaciones + "&FechaReingreso=" + FechaReingreso + "&Fecha_Ult_Actualizacion=" +
                      Fecha_Ult_Actualizacion + "&EsReingreso=" + EsReingreso + "&Tipo_Cuenta=" + Tipo_Cuenta +
                      "&FormaCalculo13ro=" + FormaCalculo13ro + "&FormaCalculo14ro=" + FormaCalculo14to +
                      "&BoniComplementaria=" + BoniComplementaria + "&BoniEspecial=" + BoniEspecial +
                      "&Remuneracion_Minima=" + Remuneracion_Minima + "&Fondo_Reserva=" + Fondo_Reserva +
                      "&Mensaje=" + Mensaje;

                GetJsonData(url);
                int id = 0;
                return View("IndexInfoTrabajador", new
                {
                    data = data,
                    id = id
                });
            }

            return View();
        }

        public IActionResult TrabajadoresUpdate(int id, int id2)
        {
            if (Request.Method == "POST")
            {
                string COMP_Codigo = Request.Form["COMP_Codigo"];
                string Tipo_trabajador = Request.Form["Tipo_trabajador"];
                string Apellido_Paterno = Request.Form["Apellido_Paterno"];
                string Apellido_Materno = Request.Form["Apellido_Materno"];
                string Nombres = Request.Form["Nombres"];
                string Identificacion = Request.Form["Identificacion"];
                string Entidad_Bancaria = Request.Form["Entidad_Bancaria"];
                string CarnetIESS = Request.Form["CarnetIESS"];
                string Direccion = Request.Form["Direccion"];
                string Telefono_Fijo = Request.Form["Telefono_Fijo"];
                string Telefono_Movil = Request.Form["Telefono_Movil"];
                string Genero = Request.Form["Genero"];
                string Nro_Cuenta_Bancaria = Request.Form["Nro_Cuenta_Bancaria"];
                string Codigo_Categoria_Ocupacion = Request.Form["Codigo_Categoria_Ocupacion"];
                string Ocupacion = Request.Form["Ocupacion"];
                string Centro_Costos = Request.Form["Centro_Costos"];
                string Nivel_Salarial = Request.Form["Nivel_Salarial"];
                string EstadoTrabajador = Request.Form["EstadoTrabajador"];
                string Tipo_Contrato = Request.Form["Tipo_Contrato"];
                string Tipo_Cese = Request.Form["Tipo_Cese"];
                string EstadoCivil = Request.Form["EstadoCivil"];
                string TipodeComision = Request.Form["TipodeComision"];
                string FechaNacimiento = Request.Form["FechaNacimiento"];
                string FechaIngreso = Request.Form["FechaIngreso"];
                string FechaCese = Request.Form["FechaCese"];
                string PeriododeVacaciones = Request.Form["PeriododeVacaciones"];
                string FechaReingreso = Request.Form["FechaReingreso"];
                string Fecha_Ult_Actualizacion = Request.Form["Fecha_Ult_Actualizacion"];
                string EsReingreso = Request.Form["EsReingreso"];
                string Tipo_Cuenta = Request.Form["Tipo_Cuenta"];
                string FormaCalculo13ro = Request.Form["FormaCalculo13ro"];
                string FormaCalculo14to = Request.Form["FormaCalculo14to"];
                string BoniComplementaria = Request.Form["BoniComplementaria"];
                string BoniEspecial = Request.Form["BoniEspecial"];
                string Remuneracion_Minima = Request.Form["Remuneracion_Minima"];
                string Fondo_Reserva = Request.Form["Fondo_Reserva"];
                string Mensaje = Request.Form["Mensaje"];

                var url = "http://apiservicios.ecuasolmovsa.com:3009/api/Varios/TrabajadorSelect?sucursal=" + COMP_Codigo;
                var data = GetJsonData(url);

                // Buscar código de categoría ocupacional
                url = "http://apiservicios.ecuasolmovsa.com:3009/api/Varios/CategoriaOcupacional";
                var dataCategoriaOcupacional = GetJsonData(url);

                foreach (var item in dataCategoriaOcupacional)
                {
                    if (item["Descripcion"].ToString() == Codigo_Categoria_Ocupacion)
                    {
                        Codigo_Categoria_Ocupacion = item["Codigo"].ToString();
                        break;
                    }
                }

                // Buscar código de ocupación
                url = "http://apiservicios.ecuasolmovsa.com:3009/api/Varios/Ocupaciones";
                var dataOcupaciones = GetJsonData(url);

                foreach (var item in dataOcupaciones)
                {
                    if (item["Descripcion"].ToString() == Ocupacion)
                    {
                        Ocupacion = item["Codigo"].ToString();
                        break;
                    }
                }

                // Buscar código de centro de costos
                url = "http://apiservicios.ecuasolmovsa.com:3009/api/Varios/CentroCostosSelect";
                var dataCentroCostos = GetJsonData(url);

                foreach (var item in dataCentroCostos)
                {
                    if (item["Descripcion"].ToString() == Centro_Costos)
                    {
                        Centro_Costos = item["Codigo"].ToString();
                        break;
                    }
                }

                // Buscar forma de cálculo 13
                url = "http://apiservicios.ecuasolmovsa.com:3009/api/Varios/DecimoTerceroDecimoCuarto";
                var dataDecimoTerceroDecimoCuarto = GetJsonData(url);

                foreach (var item in dataDecimoTerceroDecimoCuarto)
                {
                    if (item["Descripcion"].ToString() == FormaCalculo13ro)
                    {
                        FormaCalculo13ro = item["Codigo"].ToString();
                        break;
                    }
                }

                // Buscar forma de cálculo 14
                foreach (var item in dataDecimoTerceroDecimoCuarto)
                {
                    if (item["Descripcion"].ToString() == FormaCalculo14to)
                    {
                        FormaCalculo14to = item["Codigo"].ToString();
                        break;
                    }
                }

                url = "http://apiservicios.ecuasolmovsa.com:3009/api/Varios/TrabajadorInsert?COMP_Codigo=" + COMP_Codigo +
                      "&Tipo_trabajador=" + Tipo_trabajador + "&Apellido_Paterno=" + Apellido_Paterno + "&Apellido_Materno=" +
                      Apellido_Materno + "&Nombres=" + Nombres + "&Identificacion=" + Identificacion + "&Entidad_Bancaria=" +
                      Entidad_Bancaria + "&CarnetIESS=" + CarnetIESS + "&Direccion=" + Direccion + "&Telefono_Fijo=" +
                      Telefono_Fijo + "&Telefono_Movil=" + Telefono_Movil + "&Genero=" + Genero +
                      "&Nro_Cuenta_Bancaria=" + Nro_Cuenta_Bancaria + "&Codigo_Categoria_Ocupacion=" +
                      Codigo_Categoria_Ocupacion + "&Ocupacion=" + Ocupacion + "&Centro_Costos=" + Centro_Costos +
                      "&Nivel_Salarial=" + Nivel_Salarial + "&EstadoTrabajador=" + EstadoTrabajador + "&Tipo_Contrato=" + Tipo_Contrato +
                      "&Tipo_Cese=" + Tipo_Cese + "&EstadoCivil=" + EstadoCivil + "&TipodeComision=" + TipodeComision + "&FechaNacimiento=" + FechaNacimiento +
                      "&FechaIngreso=" + FechaIngreso + "&FechaCese=" + FechaCese + "&PeriododeVacaciones=" +
                      PeriododeVacaciones + "&FechaReingreso=" + FechaReingreso + "&Fecha_Ult_Actualizacion=" +
                      Fecha_Ult_Actualizacion + "&EsReingreso=" + EsReingreso + "&Tipo_Cuenta=" + Tipo_Cuenta +
                      "&FormaCalculo13ro=" + FormaCalculo13ro + "&FormaCalculo14ro=" + FormaCalculo14to +
                      "&BoniComplementaria=" + BoniComplementaria + "&BoniEspecial=" + BoniEspecial +
                      "&Remuneracion_Minima=" + Remuneracion_Minima + "&Fondo_Reserva=" + Fondo_Reserva +
                      "&Mensaje=" + Mensaje;

                GetJsonData(url);

                return View("IndexInfoTrabajador", new
                {
                    data = data,
                    id = id
                });
            }
            else
            {
                var url = "http://apiservicios.ecuasolmovsa.com:3009/api/Varios/TrabajadorSelect?sucursal=" + id;
                var data = GetJsonData(url);
                JObject data2 = new JObject();

                foreach (JObject item in data)
                {
                    if (item["Id_Trabajador"].ToString() == id.ToString())
                    {
                        data2 = item;
                        break;
                    }
                }


                url = "http://apiservicios.ecuasolmovsa.com:3009/api/Varios/GetEmisor";
                var dataSucursal = GetJsonData(url);
                url = "http://apiservicios.ecuasolmovsa.com:3009/api/Varios/TipoTrabajador";
                var dataTipoTrabajador = GetJsonData(url);
                url = "http://apiservicios.ecuasolmovsa.com:3009/api/Varios/Genero";
                var dataGenero = GetJsonData(url);
                url = "http://apiservicios.ecuasolmovsa.com:3009/api/Varios/CategoriaOcupacional";
                var dataCategoriaOcupacional = GetJsonData(url);
                url = "http://apiservicios.ecuasolmovsa.com:3009/api/Varios/NivelSalarial";
                var dataNivelSalarial = GetJsonData(url);
                url = "http://apiservicios.ecuasolmovsa.com:3009/api/Varios/TipoContrato";
                var dataTipoContrato = GetJsonData(url);
                url = "http://apiservicios.ecuasolmovsa.com:3009/api/Varios/TipoCese";
                var dataTipoCese = GetJsonData(url);
                url = "http://apiservicios.ecuasolmovsa.com:3009/api/Varios/EstadoCivil";
                var dataEstadoCivil = GetJsonData(url);
                url = "http://apiservicios.ecuasolmovsa.com:3009/api/Varios/TipoComision";
                var dataTipoComision = GetJsonData(url);
                url = "http://apiservicios.ecuasolmovsa.com:3009/api/Varios/PeriodoVacaciones";
                var dataPeriodoVacaciones = GetJsonData(url);
                url = "http://apiservicios.ecuasolmovsa.com:3009/api/Varios/EsReingreso";
                var dataEsReingreso = GetJsonData(url);
                url = "http://apiservicios.ecuasolmovsa.com:3009/api/Varios/TipoCuenta";
                var dataTipoCuenta = GetJsonData(url);
                url = "http://apiservicios.ecuasolmovsa.com:3009/api/Varios/DecimoTerceroDecimoCuarto";
                var dataDecimoTerceroDecimoCuarto = GetJsonData(url);

                return View("IndexModificarTrabajador", new
                {
                    data2 = data2,
                    dataSucursal = dataSucursal,
                    dataTipoTrabajador = dataTipoTrabajador,
                    dataGenero = dataGenero,
                    dataCategoriaOcupacional = dataCategoriaOcupacional,
                    dataNivelSalarial = dataNivelSalarial,
                    dataTipoContrato = dataTipoContrato,
                    dataTipoCese = dataTipoCese,
                    dataEstadoCivil = dataEstadoCivil,
                    dataTipoComision = dataTipoComision,
                    dataPeriodoVacaciones = dataPeriodoVacaciones,
                    dataEsReingreso = dataEsReingreso,
                    dataTipoCuenta = dataTipoCuenta,
                    dataDecimoTerceroDecimoCuarto = dataDecimoTerceroDecimoCuarto,
                    id = id,
                    id2 = id
                });
            }
        }

        private JArray GetJsonData(string url)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "GET";

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                return JArray.Parse(result);
            }
        }

        public ActionResult TrabajadoresDelete(HttpRequest request, int id, string id2)
        {
            int id2Int = int.Parse(id2);
            if (request.Method == "POST")
            {
                using (HttpClient client = new HttpClient())
                {
                    client.GetAsync("http://apiservicios.ecuasolmovsa.com:3009/api/Varios/TrabajadorDelete?sucursal=" + id + "&codigoempleado=1" + id2Int);
                }
            }

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = client.GetAsync("http://apiservicios.ecuasolmovsa.com:3009/api/Varios/TrabajadorSelect?sucursal=" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    return View("IndexInfoTrabajador", new
                    {
                        data = data,
                        id = id
                    });
                }
            }

            return null;
        }
        public ActionResult BuscarTrabajador(int id)
        {
            var context = HttpContext;
            if (context.Request.Method == "POST")
            {
                string codigo = context.Request.Form["Codigo"];

                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = client.GetAsync("http://apiservicios.ecuasolmovsa.com:3009/api/Varios/TrabajadorSelect?sucursal=" + 1).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var data = response.Content.ReadAsStringAsync().Result;
                        if (!string.IsNullOrEmpty(data))
                        {
                            try
                            {
                                var dataArray = JArray.Parse(data);
                                foreach (var item in dataArray)
                                {
                                    if (item["Id_Trabajador"].Value<int>() == int.Parse(codigo))
                                    {
                                        return View("IndexSearchTrabajador", new
                                        {
                                            data2 = item,
                                            id = id
                                        });
                                    }
                                }
                            }
                            catch (JsonReaderException ex)
                            {
                                // Manejar la excepción, puede imprimir o registrar el error para obtener más detalles
                                Console.WriteLine("Error al analizar el JSON: " + ex.Message);
                            }
                        }
                    }
                }
            }

            // Redirige a otra acción o devuelve una vista de error
            return RedirectToAction("Error", "Shared");
        }



        public ActionResult PagTipoTrabajador()
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = client.GetAsync("http://apiservicios.ecuasolmovsa.com:3009/api/Varios/TipoTrabajador").Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    return View("IndexTipoTrabajador", new
                    {
                        data = data
                    });
                }
            }

            return null;
        }

        public ActionResult PagNivelSalarial()
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = client.GetAsync("http://apiservicios.ecuasolmovsa.com:3009/api/Varios/NivelSalarial").Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    return View("IndexNivelSalarial", new
                    {
                        data = data
                    });
                }
            }

            return null;
        }

        public ActionResult PagCategoriaOcupacional()
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = client.GetAsync("http://apiservicios.ecuasolmovsa.com:3009/api/Varios/CategoriaOcupacional").Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    return View("IndexCategoriaOcupacional", new
                    {
                        data = data
                    });
                }
            }

            return null;
        }

        public ActionResult PagTipoCese()
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = client.GetAsync("http://apiservicios.ecuasolmovsa.com:3009/api/Varios/TipoCese").Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    return View("IndexTipoCese", new
                    {
                        data = data
                    });
                }
            }

            return null;
        }

        public ActionResult PagTipoContrato()
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = client.GetAsync("http://apiservicios.ecuasolmovsa.com:3009/api/Varios/TipoContrato").Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    return View("IndexTipoContrato", new
                    {
                        data = data
                    });
                }
            }

            return null;
        }

        public ActionResult PagEstadoTrabajador()
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = client.GetAsync("http://apiservicios.ecuasolmovsa.com:3009/api/Varios/EstadoTrabajador").Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    return View("IndexEstadotrabajador", new
                    {
                        data = data
                    });
                }
            }

            return null;
        }

    }
}

