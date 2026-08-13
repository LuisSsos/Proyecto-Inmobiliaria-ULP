public class Pago
{
    public int id_pago {get; set;}
    public int ReservaId {get; set;}
    public string? concepto {get; set;}
    public DateTime fecha_pago {get; set;}
    public decimal importe {get; set;}
    public string estado {get; set;} = string.Empty;
}