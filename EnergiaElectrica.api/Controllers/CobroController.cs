using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using EnergiaElectrica.api.dal;
using EnergiaElectrica.Model;
using Microsoft.EntityFrameworkCore;
using ClienteModel = EnergiaElectrica.Model.Cliente;

namespace EnergiaElectrica.api.Controllers
{
    [Route("cobro")]
    [ApiController]
    public class CobroController : ControllerBase
    {
        private Database db => new Database();

        [HttpGet("contador/{numeroContador}/lectura/{lectura}")]
        public IActionResult Lectura(string numeroContador, long lectura)
        {
            try
            {
                short anio = (short)DateTime.Today.Year;
                byte mes = (byte)DateTime.Today.Month;
                byte mesAnterior = (byte)(mes - 1);

                Lectura resultado = db.Medicion
                    .Include(x=>x.ClienteNavigation)
                    .ThenInclude(x => x.TipoNavigation)
                    .Where(x =>
                        x.ClienteNavigation.NumeroContador.Equals(numeroContador) &&
                        x.Anio.Equals(anio) &&
                        x.Mes.Equals(mesAnterior))
                    .Select(x => new Lectura()
                    {
                        precio = x.ClienteNavigation.TipoNavigation.Precio,
                        lecturaActual = lectura,
                        lecturaAnterior = x.Lectura,
                        mes = mes,
                        anio = anio,
                        fecha = DateTime.Now,
                    })
                    .FirstOrDefault();

                resultado.cliente = db.Cliente
                    .Include(x => x.TipoNavigation)
                    .Include(x => x.MedidorNavigation)
                    .Where(x => x.NumeroContador.Equals(numeroContador))
                    .Select(x => new ClienteModel()
                    {
                        TipoCliente = x.TipoNavigation.Nombre,
                        TipoMedidor = x.MedidorNavigation.Nombre,
                        Nombre = x.NombreCompleto,
                        Direccion = x.Direccion,
                        Telefono = x.Telefono,
                        NumeroContador = x.NumeroContador
                    })
                    .FirstOrDefault();

                resultado.historial = db.Medicion
                    .Include(x => x.ClienteNavigation)
                    .Where(x =>
                        x.ClienteNavigation.NumeroContador.Equals(numeroContador) &&
                        x.Anio.Equals(anio))
                    .OrderByDescending(x => x.Mes)
                    .Select(x => new HistorialLectura()
                    {
                        fecha = x.Fecha,
                        mes = x.Mes,
                        anio = x.Anio,
                        lectura = x.Lectura,
                        monto = x.MontoCobrar
                    })
                    .ToList();

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
