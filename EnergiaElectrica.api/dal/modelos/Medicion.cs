using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace EnergiaElectrica.api.dal.modelos
{
    public partial class Medicion
    {
        public Medicion()
        {
            Factura = new HashSet<Factura>();
        }

        public long Id { get; set; }
        public byte Anio { get; set; }
        public byte Mes { get; set; }
        public long Cliente { get; set; }
        public long Lectura { get; set; }
        public DateTime Fecha { get; set; }
        public byte Usuario { get; set; }
        public decimal MontoCobrar { get; set; }

        public virtual Cliente ClienteNavigation { get; set; }
        public virtual Usuario UsuarioNavigation { get; set; }
        public virtual ICollection<Factura> Factura { get; set; }
    }
}
