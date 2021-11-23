using System.Net;
using EnergiaElectrica.ui.Filters;
using EnergiaElectrica.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using LecturaCobro = EnergiaElectrica.Model.Lectura;

namespace EnergiaElectrica.ui.Controllers
{
    [ValidarAutenticacion]
    public class LecturaController : Controller
    {
        public IActionResult Index()
        {
            LecturaVista modelo = new LecturaVista();
            modelo.lectura = 0;

            return View(modelo);
        }

        public IActionResult Factura([FromQuery(Name = "numero")] string factura)
        {
            ViewBag.factura = factura;

            LecturaVista modelo = new LecturaVista();
            modelo.lectura = 0;
            return View(modelo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(LecturaVista data, string button)
        {
            switch (button)
            {
                case "lectura":
                    if (ModelState.IsValid)
                    {
                        Api api = new Api("http://localhost:52508");
                        IRestResponse respuesta = api.Process(Method.GET, $"cobro/contador/{data.contador}/lectura/{data.lectura}");
                        if (respuesta.StatusCode != HttpStatusCode.OK)
                        {
                            if (respuesta.StatusCode == HttpStatusCode.NoContent)
                                ModelState.AddModelError("error", "Contador no encontrado");
                            else
                                ModelState.AddModelError("error", respuesta.Content);
                        }
                        else
                        {
                            data.datos = JsonConvert.DeserializeObject<LecturaCobro>(respuesta.Content);
                            data.montoCobrar = data.datos.montoCobrar;
                        }
                    }
                    
                    break;
                case "cobro":
                    Api apiCobro = new Api("http://localhost:52508");
                    IRestResponse respuestaCobro = apiCobro.Process(Method.POST, $"cobro", data);
                    if (respuestaCobro.StatusCode != HttpStatusCode.OK)
                        ModelState.AddModelError("error", respuestaCobro.Content);
                    else
                        return RedirectToAction("Factura", "Lectura", new { numero = respuestaCobro.Content.Replace("\"", "") });

                    break;
            }

            return View(data);
        }

        [HttpGet]
        public IActionResult Descargar(string factura)
        {
            byte[] fileBytes = System.IO.File.ReadAllBytes($"c:\\reportes\\{factura}.pdf");
            string fileName = $"{factura}.pdf";
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }
    }
}
