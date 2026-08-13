
public class Inmueble{
    public int id_inmueble {get; set;}
    public int propietarioId {get; set;}
    public int tipoInmuebleId {get; set;}
    public string direccion {get; set;} = string.Empty;
    public int cupo {get; set;}
    public decimal precioPorDia {get; set;}
    public string estado {get; set;} = string.Empty;
}