using System.ComponentModel.DataAnnotations;
using EnergiaElectrica.Model;

namespace EnergiaElectrica.ViewModel
{
    public class LecturaVista
    {
        [Required(ErrorMessage = "*")]
        public string contador { get; set; }

        [Required(ErrorMessage = "*")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Solo números")]
        public long lectura { get; set; }

        public decimal montoCobrar { get; set; }

        public Lectura datos { get; set; }
    }
}
