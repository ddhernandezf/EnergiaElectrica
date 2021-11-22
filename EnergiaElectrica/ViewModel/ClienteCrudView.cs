using System.Collections.Generic;

namespace EnergiaElectrica.ViewModel
{
    public class ClienteCrudView
    {
        public List<TipoCliente> tipoCliente { get; set; }

        public List<TipoMedidor> tipoMedidor { get; set; }

        public ClienteModel formulario { get; set; }
    }
}
