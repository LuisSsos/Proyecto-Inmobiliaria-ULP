using System.ComponentModel.DataAnnotations;

namespace MVC.Models
{
    public class Inquilino
    {
        public int id_inquilino { get; set; }

        [Required(ErrorMessage = "El DNI es obligatorio")]
        [RegularExpression(@"^\d{7,8}$", ErrorMessage = "El DNI debe tener 7 u 8 números, sin letras ni puntos")]
        public string dni { get; set; } = string.Empty;

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(150, ErrorMessage = "El nombre no puede superar los 150 caracteres")]
        public string nombre_completo { get; set; } = string.Empty;

        [StringLength(150)]
        [RegularExpression(@"^$|^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "El formato de email no es válido")]
        public string? email { get; set; }

        [StringLength(15)]
        [RegularExpression(@"^$|^\d{6,15}$", ErrorMessage = "El teléfono debe contener solo números")]
        public string? telefono { get; set; }
    }
}