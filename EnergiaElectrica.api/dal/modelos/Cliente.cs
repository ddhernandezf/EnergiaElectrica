using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace EnergiaElectrica.api.dal.modelos
{
    public partial class Cliente
    {
        public Cliente()
        {
            Factura = new HashSet<Factura>();
            Medicion = new HashSet<Medicion>();
        }

        public long Id { get; set; }
        public byte Tipo { get; set; }
        public byte Medidor { get; set; }
        public string NombreCompleto { get; set; }
        public string Correo { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public bool EnviarFactura { get; set; }
        public string NumeroContador { get; set; }

        public virtual TipoMedidor MedidorNavigation { get; set; }
        public virtual TipoCliente TipoNavigation { get; set; }
        public virtual ICollection<Factura> Factura { get; set; }
        public virtual ICollection<Medicion> Medicion { get; set; }
    }
}
