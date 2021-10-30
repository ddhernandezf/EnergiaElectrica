using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace EnergiaElectrica.api.dal.modelos
{
    public partial class Usuario
    {
        public Usuario()
        {
            Medicion = new HashSet<Medicion>();
        }

        public byte Id { get; set; }
        public string Usuario1 { get; set; }
        public string Password { get; set; }

        public virtual ICollection<Medicion> Medicion { get; set; }
    }
}
