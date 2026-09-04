using System.ComponentModel.DataAnnotations;

namespace MVC.Models
{
    public class TipoInmueble
    {
        public int IdTipoInmueble { get; set; }

        [Required(ErrorMessage = "Se debe asignar un nombre al nuevo tipo de inmueble")]
        [StringLength(20, ErrorMessage = "El tipo de inmueble no puede superar los 20 caracteres")]
        [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$", ErrorMessage = "El tipo de inmueble solo puede contener letras.")]
        public string Nombre { get; set; } = string.Empty;

    }
}
