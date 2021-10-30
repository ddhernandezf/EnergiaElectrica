using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using EnergiaElectrica.api.dal;
using EnergiaElectrica.api.dal.modelos;

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
                List<TipoCliente> datos = db.TipoCliente.ToList();

                return Ok(datos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
