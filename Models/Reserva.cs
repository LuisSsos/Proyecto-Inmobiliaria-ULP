using System.ComponentModel.DataAnnotations;
namespace MVC.Models

{
    public class Reserva : IValidatableObject
    {
        public int id_reserva { get; set; }
        public int inquilino_id { get; set; }
        public int inmueble_id { get; set; }
        public DateTime fecha_desde { get; set; }
        public DateTime fecha_hasta { get; set; }
        public DateTime? fecha_fin_real { get; set; }
        public decimal monto_por_dia { get; set; }
        public decimal multa { get; set; }
        public string estado { get; set; } = string.Empty;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (fecha_desde >= fecha_hasta)
            {
                yield return new ValidationResult(
                    "La fecha desde debe ser anterior a la fecha hasta.",
                    new[] { nameof(fecha_desde), nameof(fecha_hasta) }
                );
            }

            if (fecha_desde < DateTime.Today)
            {
                yield return new ValidationResult(
                    "La fecha desde no puede ser anterior a hoy.",
                    new[] { nameof(fecha_desde) }
                );
            }
        }
    }
}