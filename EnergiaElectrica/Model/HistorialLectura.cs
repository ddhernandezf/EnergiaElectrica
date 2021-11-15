using System;

namespace EnergiaElectrica.Model
{
    public class HistorialLectura
    {
        public DateTime fecha { get; set; }
        public short anio { get; set; }
        public byte mes { get; set; }
        public long lectura { get; set; }
        public decimal monto { get; set; }
    }
}
