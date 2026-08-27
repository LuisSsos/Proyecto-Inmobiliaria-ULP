
using MVC.Models;

public class Inmueble{
    public int IdInmueble {get; set;}
    public int PropietarioId {get; set;}
    public int TipoInmuebleId {get; set;}
    public string Direccion {get; set;} = string.Empty;
    public int Cupo {get; set;}
    public decimal Latitud {get; set;}
    public decimal Longitud{get; set;}
    public decimal PrecioPorDia {get; set;}
    public decimal PorcentajeSeña{get; set;}
    public string Estado {get; set;} = string.Empty;


    public Propietario? Titular{get; set;}
    public TipoInmueble? Tipo{get; set;}

}