using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using EnergiaElectrica.api.dal;
using EnergiaElectrica.ViewModel;

namespace EnergiaElectrica.api.Controllers
{
    [Route("tipomedidor")]
    [ApiController]
    public class TipoMedidorController : ControllerBase
    {
        private Database db => new Database();

        [HttpGet]
        public IActionResult Buscar()
        {
            try
            {
                List<TipoMedidor> datos = db.TipoMedidor
                    .Select(x => new TipoMedidor()
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
