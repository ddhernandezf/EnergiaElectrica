using System.ComponentModel.DataAnnotations;

namespace EnergiaElectrica.ViewModel
{
    public class ClienteModel
    {
        public long id { get; set; }
        
        [Required]
        public byte tipo { get; set; }
        
        [Required]
        public byte medidor { get; set; }

        [Required]
        public string nombre { get; set; }

        [Required]
        public string correo { get; set; }

        [Required]
        public string direccion { get; set; }

        [Required]
        public string telefono { get; set; }

        [Required]
        public string numeroContador { get; set; }
    }
}
