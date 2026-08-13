public class Pago
{
    public int Id {get; set;}
    public int ReservaId {get; set;}
    public string? Concepto {get; set;}
    public DateTime FechaPago {get; set;}
    public decimal importe {get; set;}
    public string estado {get; set;} = string.Empty;
}