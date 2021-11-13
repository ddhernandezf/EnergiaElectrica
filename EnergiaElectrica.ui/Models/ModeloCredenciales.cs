using System.ComponentModel.DataAnnotations;

namespace EnergiaElectrica.ui.Models
{
    public class ModeloCredenciales
    {
        [Required(ErrorMessage = "*")]
        public string nombreUsuario { get; set; }

        [Required(ErrorMessage = "*")]
        public string password { get; set; }
    }
}
