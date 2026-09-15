using System.ComponentModel.DataAnnotations;

namespace MVC.Models;

public class Pago
{
    public int id_pago { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar una reserva.")]
    public int ReservaId { get; set; }

    [Required(ErrorMessage = "Debe seleccionar un concepto.")]
    public string? concepto { get; set; }

    [Required(ErrorMessage = "Debe ingresar la fecha de pago.")]
    public DateTime fecha_pago { get; set; }

    [Range(20000, 999999, ErrorMessage = "El importe debe estar entre $20.000 y $999.999.")]
    public decimal importe { get; set; }

    public string estado { get; set; } = string.Empty;

    public int? usuario_creador_id { get; set; }

    public int? usuario_anulador_id { get; set; }

    public bool Activo { get; set; } = true;
}