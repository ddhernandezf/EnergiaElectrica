using System;

namespace EnergiaElectrica.Model
{
    public class Factura
    {
        public Guid numero { get; set; }
        public DateTime fecha { get; set; }
        public string periodo { get; set; }
        public long lectura { get; set; }
        public decimal monto { get; set; }
        public string nombreCliente { get; set; }
        public string direccionCliente { get; set; }
        public string contadorCliente { get; set; }
    }
}
