using System.ComponentModel.DataAnnotations;

namespace MVC.Models
{
    public class Propietario
    {
        public int IdPropietario { get; set; }

        [Required(ErrorMessage = "El nombre del propietario es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede superar los 100 caracteres.")]
        [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$", ErrorMessage = "El nombre solo puede contener letras y espacios.")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El DNI/CUIT es obligatorio")]
        [RegularExpression(@"^\d{7,11}$", ErrorMessage = "Ingrese un DNI o CUIT válido (solo números)")]
        public string DniCuit { get; set; } = string.Empty;

        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [EmailAddress(ErrorMessage = "Debe ingresar un correo electrónico con formato válido (ejemplo@dominio.com).")]
        [StringLength(100, ErrorMessage = "El email no puede superar los 100 caracteres.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "El teléfono de contacto es obligatorio.")]
        [StringLength(18, MinimumLength = 6, ErrorMessage = "El teléfono no puede ser menor a 6 dígitos ni superar los 18 dígitos")]
        [RegularExpression(@"^\+?[0-9]+[\s\-\(\)0-9]*$", ErrorMessage = "El teléfono solo permite números, espacios, guiones, paréntesis y prefijo '+'.")]
        public string? Telefono { get; set; }
    }
}