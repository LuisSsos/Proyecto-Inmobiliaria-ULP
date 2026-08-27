using System.ComponentModel.DataAnnotations;

namespace MVC.Models
{
    public class Propietario
    {   
        [Required(ErrorMessage = "Debe seleccionar un propietario")]
        public int IdPropietario { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string DniCuit { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;

    }
}