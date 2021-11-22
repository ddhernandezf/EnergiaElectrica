using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using EnergiaElectrica.api.dal;
using EnergiaElectrica.ViewModel;

namespace EnergiaElectrica.api.Controllers
{
    [Route("tipocliente")]
    [ApiController]
    public class TipoClienteController : ControllerBase
    {
        private Database db => new Database();

        [HttpGet]
        public IActionResult Buscar()
        {
            try
            {
                List<TipoCliente> datos = db.TipoCliente
                    .Select(x => new TipoCliente()
                    {
                        id = x.Id,
                        nombre = x.Nombre
                    })
                    .ToList();

                return Ok(datos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
