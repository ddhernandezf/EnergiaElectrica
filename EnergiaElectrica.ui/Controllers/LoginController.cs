using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using EnergiaElectrica.ui.Models;
using Microsoft.AspNetCore.Http;
using RestSharp;

namespace EnergiaElectrica.ui.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CerrarSesion()
        {
            HttpContext.Session.Clear();
            
            return Redirect("/");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(ModeloCredenciales data)
        {
            if (ModelState.IsValid)
            {
                Api api = new Api("http://localhost:52508");
                IRestResponse respuesta = api.Process(Method.GET, $"seguridad/login/usuario/{data.nombreUsuario}/password/{data.password}");
                if (respuesta.StatusCode != HttpStatusCode.OK)
                {
                    ModelState.AddModelError("error", "Credenciales incorrectas");
                    return View(data);
                }

                HttpContext.Session.SetString("USUARIO", data.nombreUsuario);
                ViewData["USUARIO"] = data.nombreUsuario;
                return Redirect("/");
            }

            return View(data);
        }
    }
}
