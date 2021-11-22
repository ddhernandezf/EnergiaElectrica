using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using EnergiaElectrica.api.dal;
using EnergiaElectrica.api.dal.modelos;
using EnergiaElectrica.ViewModel;
using Microsoft.EntityFrameworkCore;
using ClienteDalModel = EnergiaElectrica.api.dal.modelos.Cliente;
using ClienteViewModel = EnergiaElectrica.ViewModel.Cliente;

namespace EnergiaElectrica.api.Controllers
{
    [Route("cliente")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        [HttpGet]
        public IActionResult Buscar()
        {
            try
            {
                using Database db = new Database();
                IQueryable<ClienteDalModel> consulta = db.Cliente
                    .Include(x => x.TipoNavigation)
                    .Include(x => x.MedidorNavigation);

                IList<ClienteViewModel> resultado = consulta
                    .Select(x => new ClienteViewModel()
                    {
                        id = x.Id,
                        medidor = x.MedidorNavigation.Id,
                        tipo = x.TipoNavigation.Id,
                        medidorNombre = x.MedidorNavigation.Nombre,
                        tipoNombre = x.TipoNavigation.Nombre,
                        nombre = x.NombreCompleto,
                        direccion = x.Direccion,
                        telefono = x.Telefono,
                        correo = x.Correo,
                        numeroContador = x.NumeroContador
                    })
                    .ToList();

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult Buscar(long id)
        {
            try
            {
                using Database db = new Database();
                IQueryable<ClienteDalModel> consulta = db.Cliente
                    .Include(x => x.TipoNavigation)
                    .Include(x => x.MedidorNavigation)
                    .Where(x => x.Id.Equals(id));

                IList<ClienteViewModel> resultado = consulta
                    .Select(x => new ClienteViewModel()
                    {
                        id = x.Id,
                        medidor = x.MedidorNavigation.Id,
                        tipo = x.TipoNavigation.Id,
                        medidorNombre = x.MedidorNavigation.Nombre,
                        tipoNombre = x.TipoNavigation.Nombre,
                        nombre = x.NombreCompleto,
                        direccion = x.Direccion,
                        telefono = x.Telefono,
                        correo = x.Correo,
                        numeroContador = x.NumeroContador
                    })
                    .ToList();

                return Ok(resultado.First());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Nuevo([FromBody] ClienteModel modelo)
        {
            try
            {
                using Database db = new Database();

                ClienteDalModel cliente = new ClienteDalModel()
                {
                    Tipo = modelo.tipo,
                    Medidor = modelo.medidor,
                    NombreCompleto = modelo.nombre,
                    Correo = modelo.correo,
                    Direccion = modelo.direccion,
                    Telefono = modelo.telefono,
                    EnviarFactura = true,
                    NumeroContador = modelo.numeroContador
                };
                db.Cliente.Add(cliente);
                db.SaveChanges();

                db.Medicion.Add(new Medicion()
                {
                    Anio = (short)DateTime.Today.Year,
                    Mes = (byte)(DateTime.Today.Month - 1),
                    Cliente = cliente.Id,
                    Lectura = 0,
                    Fecha = DateTime.Now,
                    Usuario = 1,
                    MontoCobrar = 0
                });
                db.SaveChanges();


                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Actualizar(long id, [FromBody] ClienteModel modelo)
        {
            try
            {
                using Database db = new Database();

                ClienteDalModel registro = db.Cliente.FirstOrDefault(x => x.Id.Equals(id));
                registro.Tipo = modelo.tipo;
                registro.Medidor = modelo.medidor;
                registro.NombreCompleto = modelo.nombre;
                registro.Correo = modelo.correo;
                registro.Direccion = modelo.direccion;
                registro.Telefono = modelo.telefono;
                registro.EnviarFactura = true;
                registro.NumeroContador = modelo.numeroContador;

                db.Cliente.Update(registro);
                db.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Eliminar(long id)
        {
            try
            {
                using Database db = new Database();
                ClienteDalModel registro = db.Cliente.FirstOrDefault(x => x.Id.Equals(id));

                db.Medicion.RemoveRange(db.Medicion.Where(x => x.Cliente.Equals(id)));
                db.Factura.RemoveRange(db.Factura.Where(x => x.Cliente.Equals(id)));
                db.Cliente.Remove(registro);
                db.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
