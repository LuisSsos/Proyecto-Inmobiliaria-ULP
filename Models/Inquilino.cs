using System.ComponentModel.DataAnnotations;
using MVC.Models.Validaciones;

namespace MVC.Models
{
    [AlMenosUnContacto(nameof(email), nameof(telefono))]
    public class Inquilino
    {
        public int id_inquilino { get; set; }

        [Required(ErrorMessage = "El DNI es obligatorio")]
        [RegularExpression(
            @"^\d{7,8}$",
            ErrorMessage = "El DNI debe tener 7 u 8 números, sin letras ni puntos"
        )]
        public string dni { get; set; } = string.Empty;

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "El nombre debe tener entre 2 y 50 caracteres")]
        [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑüÜ\s]+$",
         ErrorMessage = "El nombre solo puede contener letras y espacios")]
        public string nombre_completo { get; set; } = string.Empty;

        [StringLength(150)]
        [RegularExpression(
            @"^$|^[^@\s]+@[^@\s]+\.[^@\s]+$",
            ErrorMessage = "El formato de email no es válido"
        )]
        public string? email { get; set; }

        [RegularExpression(@"^[0-9\s\-\+\(\)]{8,20}$",
            ErrorMessage = "El teléfono contiene caracteres no válidos")]
        public string? telefono { get; set; }
    }
}