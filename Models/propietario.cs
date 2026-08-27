using System.ComponentModel.DataAnnotations;

namespace MVC.Models
{
    public class Propietario
    {
        public int IdPropietario { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(150)]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El DNI/CUIT es obligatorio")]
        [RegularExpression(@"^\d{7,11}$", ErrorMessage = "Ingrese un DNI o CUIT válido (solo números)")]
        public string DniCuit { get; set; } = string.Empty;

        [StringLength(150)]
        [RegularExpression(@"^$|^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "El formato de email no es válido")]
        public string? Email { get; set; }

        [StringLength(15)]
        [RegularExpression(@"^$|^\d{6,15}$", ErrorMessage = "El teléfono debe contener solo números")]
        public string? Telefono { get; set; }
    }
}