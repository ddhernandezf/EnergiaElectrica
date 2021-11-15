using System;
using System.Collections.Generic;

namespace EnergiaElectrica.Model
{
    public class Lectura
    {
        public decimal precio { get; set; }
        public decimal montoCobrar => precio * (lecturaActual - lecturaAnterior);
        public byte mes { get; set; }
        public short anio { get; set; }
        public DateTime fecha { get; set; }
        public long lecturaActual { get; set; }
        public long lecturaAnterior { get; set; }
        public long consumido => lecturaActual - lecturaAnterior;
        public Cliente cliente { get; set; }
        public List<HistorialLectura> historial { get; set; }
    }
}
