using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using EnergiaElectrica.api.dal;
using EnergiaElectrica.api.dal.modelos;
using EnergiaElectrica.Model;
using EnergiaElectrica.ViewModel;
using Microsoft.EntityFrameworkCore;
using ClienteModel = EnergiaElectrica.Model.Cliente;
using FacturaModel = EnergiaElectrica.Model.Factura;
using iTextSharp.text.pdf;
using iTextSharp.text;

namespace EnergiaElectrica.api.Controllers
{
    [Route("cobro")]
    [ApiController]
    public class CobroController : ControllerBase
    {
        [HttpGet("contador/{numeroContador}/lectura/{lectura}")]
        public IActionResult Lectura(string numeroContador, long lectura)
        {
            try
            {
                Lectura resultado = GetLectura(numeroContador, lectura);




                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Cobro([FromBody] /*Lectura modelo*/LecturaVista modelo)
        {
            try
            {
                Lectura resultado = GetLectura(modelo.contador, modelo.lectura);

                using Database db = new Database();

                Medicion medicion = new Medicion()
                {
                    Anio = resultado.anio,
                    Mes = resultado.mes,
                    Cliente = resultado.cliente.id,
                    Lectura = resultado.lecturaActual,
                    Fecha = DateTime.Now,
                    Usuario = 1,
                    MontoCobrar = resultado.montoCobrar
                };
                db.Medicion.Add(medicion);
                db.SaveChanges();

                Guid guid = Guid.NewGuid();
                db.Factura.Add(new dal.modelos.Factura()
                {
                    Codigo = guid,
                    Fecha = DateTime.Now,
                    Cliente = resultado.cliente.id,
                    Medicion = medicion.Id
                });
                db.SaveChanges();

                CrearDocumento(guid.ToString());

                return Ok(guid.ToString());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private Lectura GetLectura(string numeroContador, long lectura)
        {
            using Database db = new Database();
            short anio = (short)DateTime.Today.Year;
            byte mes = (byte)DateTime.Today.Month;
            byte mesAnterior = (byte)(mes - 1);

            Lectura resultado = db.Medicion
                    .Include(x => x.ClienteNavigation)
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

            if (resultado != null)
            {
                resultado.cliente = db.Cliente
                    .Include(x => x.TipoNavigation)
                    .Include(x => x.MedidorNavigation)
                    .Where(x => x.NumeroContador.Equals(numeroContador))
                    .Select(x => new ClienteModel()
                    {
                        id = x.Id,
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
            }

            return resultado;
        }

        private FacturaModel GetFactura(string numero)
        {
            using Database db = new Database();
            Guid guid = Guid.Parse(numero);

            return db.Factura
                .Include(x => x.MedicionNavigation)
                .Include(x => x.ClienteNavigation)
                .Where(x => x.Codigo.Equals(guid))
                .Select(x => new FacturaModel()
                {
                    numero = x.Codigo,
                    fecha = x.Fecha,
                    periodo = $"{x.MedicionNavigation.Anio}/{x.MedicionNavigation.Mes}",
                    lectura = x.MedicionNavigation.Lectura,
                    monto = x.MedicionNavigation.MontoCobrar,
                    nombreCliente = x.ClienteNavigation.NombreCompleto,
                    direccionCliente = x.ClienteNavigation.Direccion,
                    contadorCliente = x.ClienteNavigation.NumeroContador
                }).FirstOrDefault();
        }

        private void CrearDocumento(string numero)
        {
            FacturaModel factura = GetFactura(numero);
            FileStream fs = new FileStream($"c:\\reportes\\{numero}.pdf", FileMode.Create, FileAccess.Write, FileShare.None);
            Document archivo = new Document();
            PdfWriter writer = PdfWriter.GetInstance(archivo, fs);
            archivo.Open();

            archivo.Add(new Paragraph($"{factura.fecha:dd/MM/yyyy HH:mm:ss}"));
            archivo.Add(new Paragraph($"Nombre: {factura.nombreCliente}"));
            archivo.Add(new Paragraph($"Dirección: {factura.direccionCliente}"));
            archivo.Add(new Paragraph($"Contador: {factura.contadorCliente}"));
            archivo.Add(new Paragraph("  "));
            archivo.Add(new Paragraph("  "));
            archivo.Add(new Paragraph($"Lectura correspondiente al periodo {factura.periodo} por un total de {factura.lectura} KW"));
            archivo.Add(new Paragraph("  "));
            archivo.Add(new Paragraph($"TOTAL: {factura.monto:0:0.00}"));

            archivo.Close();
        }
    }
}
