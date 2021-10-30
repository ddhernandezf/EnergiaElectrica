using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace EnergiaElectrica.api.dal.modelos
{
    public partial class Factura
    {
        public long Id { get; set; }
        public Guid Codigo { get; set; }
        public DateTime Fecha { get; set; }
        public long Cliente { get; set; }
        public long Medicion { get; set; }

        public virtual Cliente ClienteNavigation { get; set; }
        public virtual Medicion MedicionNavigation { get; set; }
    }
}
