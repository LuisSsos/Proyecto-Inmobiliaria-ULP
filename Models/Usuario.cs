using System.ComponentModel.DataAnnotations;

namespace MVC.Models
{
    public class Usuario
    {
        public int id_usuario { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(50)]
        public string nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El apellido es obligatorio")]
        [StringLength(50)]
        public string apellido { get; set; } = string.Empty;

        [Required(ErrorMessage = "El email es obligatorio")]
        [StringLength(60)]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "El formato email no es válido")]
        public string email { get; set; } = string.Empty;

        [Required(ErrorMessage = "El rol es obligatorio")]
        public string rol { get; set; } = string.Empty;

        public string? avatar { get; set; }
    }
}