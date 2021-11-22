using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using EnergiaElectrica.ui.Filters;
using EnergiaElectrica.ViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using RestSharp;
using Newtonsoft.Json;
using Cliente = EnergiaElectrica.ViewModel.Cliente;
using Controller = Microsoft.AspNetCore.Mvc.Controller;

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

        public IActionResult Nuevo()
        {
            ClienteCrudView modelo = new ClienteCrudView()
            {
                tipoCliente = TipoCliente(),
                tipoMedidor = TipoMedidor(),
                formulario = new ClienteModel()
            };

            return View(modelo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Nuevo(ClienteCrudView modelo)
        {
            if (ModelState.IsValid)
            {
                Api api = new Api("http://localhost:52508");
                IRestResponse respuesta = api.Process(Method.POST, $"cliente", modelo.formulario);
                if (respuesta.StatusCode != HttpStatusCode.OK)
                    ModelState.AddModelError("error", respuesta.Content);
                else
                    return RedirectToAction("Index", "Cliente");
            }

            modelo.tipoCliente = TipoCliente();
            modelo.tipoMedidor = TipoMedidor();
            return View(modelo);
        }

        [HttpGet("actualizar/{id}")]
        public IActionResult Actualizar(long id)
        {
            Cliente model = new Cliente();
            Api api = new Api("http://localhost:52508");
            IRestResponse respuesta = api.Process(Method.GET, $"cliente/{id}");
            if (respuesta.StatusCode != HttpStatusCode.OK)
                ModelState.AddModelError("error", respuesta.Content);
            else
                model = JsonConvert.DeserializeObject<Cliente>(respuesta.Content);

            ClienteCrudView modelo = new ClienteCrudView()
            {
                tipoCliente = TipoCliente(),
                tipoMedidor = TipoMedidor(),
                formulario = new ClienteModel()
                {
                    id = model.id,
                    nombre = model.nombre,
                    direccion = model.direccion,
                    correo = model.correo,
                    telefono = model.telefono,
                    numeroContador = model.numeroContador,
                    medidor = model.medidor,
                    tipo = model.tipo
                }
            };

            return View(modelo);
        }

        [HttpPost("actualizar/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Actualizar(long id, ClienteCrudView modelo)
        {
            if (ModelState.IsValid)
            {
                Api api = new Api("http://localhost:52508");
                IRestResponse respuesta = api.Process(Method.PUT, $"cliente/{id}", modelo.formulario);
                if (respuesta.StatusCode != HttpStatusCode.OK)
                    ModelState.AddModelError("error", respuesta.Content);
                else
                    return RedirectToAction("Index", "Cliente");
            }

            modelo.tipoCliente = TipoCliente();
            modelo.tipoMedidor = TipoMedidor();
            return View(modelo);
        }

        [HttpGet("eliminar/{id}")]
        public IActionResult Eliminar(long id)
        {
            Cliente model = new Cliente();
            Api api = new Api("http://localhost:52508");
            IRestResponse respuesta = api.Process(Method.DELETE, $"cliente/{id}");


            return RedirectToAction("Index", "Cliente");
        }

        private List<TipoCliente> TipoCliente()
        {
            Api api = new Api("http://localhost:52508");
            IRestResponse respuesta = api.Process(Method.GET, $"tipocliente");
            if (respuesta.StatusCode != HttpStatusCode.OK)
                throw new Exception(respuesta.Content);

            return JsonConvert.DeserializeObject<List<TipoCliente>>(respuesta.Content);
        }

        private List<TipoMedidor> TipoMedidor()
        {
            Api api = new Api("http://localhost:52508");
            IRestResponse respuesta = api.Process(Method.GET, $"tipomedidor");
            if (respuesta.StatusCode != HttpStatusCode.OK)
                throw new Exception(respuesta.Content);

            return JsonConvert.DeserializeObject<List<TipoMedidor>>(respuesta.Content);
        }
    }
}
