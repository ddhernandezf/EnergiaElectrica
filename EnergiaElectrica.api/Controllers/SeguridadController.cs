using System;
using System.Linq;
using EnergiaElectrica.api.dal;
using EnergiaElectrica.api.dal.modelos;
using EnergiaElectrica.api.filtros;
using Microsoft.AspNetCore.Mvc;

namespace EnergiaElectrica.api.Controllers
{
    [Route("seguridad")]
    [ApiController]
    public class SeguridadController : ControllerBase
    {
        private Database db => new Database();

        [HttpGet("login/usuario/{usuario}/password/{password}")]
        public IActionResult Login(string usuario, string password)
        {
            try
            {
                Usuario resultado = db.Usuario
                    .FirstOrDefault(x =>
                        x.Usuario1.Equals(usuario) &&
                        x.Password.Equals(password));

                if (resultado == null)
                    return Unauthorized();

                return Ok(new { usuario = resultado.Usuario1 });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
