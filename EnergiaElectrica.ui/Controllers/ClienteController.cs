using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using EnergiaElectrica.ui.Filters;
using EnergiaElectrica.ViewModel;
using RestSharp;
using Newtonsoft.Json;

namespace EnergiaElectrica.ui.Controllers
{
    [ValidarAutenticacion]
    public class ClienteController : Controller
    {
        public IActionResult Index()
        {
            VistaCliente data = new VistaCliente();
            data.clientes = new List<Cliente>();

            Api api = new Api("http://localhost:52508");
            IRestResponse respuesta = api.Process(Method.GET, $"cliente");
            if (respuesta.StatusCode != HttpStatusCode.OK)
                ModelState.AddModelError("error", respuesta.Content);
            else
                data.clientes = JsonConvert.DeserializeObject<List<Cliente>>(respuesta.Content);

            return View(data);
        }
    }
}
