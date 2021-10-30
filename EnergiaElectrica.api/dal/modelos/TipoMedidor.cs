using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace EnergiaElectrica.api.dal.modelos
{
    public partial class TipoMedidor
    {
        public TipoMedidor()
        {
            Cliente = new HashSet<Cliente>();
        }

        public byte Id { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<Cliente> Cliente { get; set; }
    }
}
