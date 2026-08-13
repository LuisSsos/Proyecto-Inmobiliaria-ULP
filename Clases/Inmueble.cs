
public class Inmueble{
    public int Id {get; set;}
    public int PropietarioId {get; set;}
    public int TipoInmuebleId {get; set;}
    public string Direccion {get; set;} = string.Empty;
    public int Cupo {get; set;}
    public decimal PrecioPorDia {get; set;}
    public string Estado {get; set;} = string.Empty;
}