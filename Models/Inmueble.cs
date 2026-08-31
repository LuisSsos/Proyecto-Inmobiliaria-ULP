
using System.ComponentModel.DataAnnotations;
using MVC.Models;

public class Inmueble{
    public int IdInmueble {get; set;}
    
    [Required(ErrorMessage = "Se debe asignar un propietario al inmueble")]
    public int PropietarioId {get; set;}

    [Required(ErrorMessage = "Se debe elegir el tipo de inmueble")]
    public int TipoInmuebleId {get; set;}

    [Required(ErrorMessage = "La dirección del inmueble es obligatoria")]
    [StringLength(50, ErrorMessage = "La dirección no puede superar los 50 caracteres")]
    public string Direccion {get; set;} = string.Empty;
    
    [Required(ErrorMessage = "El cupo de personas es obligatorio")]
    [Range(1, 20, ErrorMessage = "El cupo de inquilinos debe ser de al menos 1 persona")]
    public int Cupo {get; set;}

    [Required(ErrorMessage = "La latitud geográfica es obligatoria")]
    [Range(-90.0, 90.0, ErrorMessage = "La latitud debe estar entre -90 y 90 grados")]
    public decimal Latitud {get; set;}

    [Required(ErrorMessage = "La longitud geográfica es obligatoria")]
    [Range(-180.0, 180.0, ErrorMessage = "La longitud debe estar entre -180 y 180 grados")]
    public decimal Longitud{get; set;}

    [Required(ErrorMessage = "El precio por día es obligatorio")]
    [Range(0.01, 99999999.99, ErrorMessage = "El precio por día debe ser un monto mayor a cero")]
    public decimal PrecioPorDia {get; set;}

    [Required(ErrorMessage = "El porcentaje de seña es obligatorio")]
    [Range(0.0, 100.0, ErrorMessage = "El porcentaje de seña debe estar entre 0 y 100")]
    public decimal PorcentajeSeña{get; set;}

    [Required(ErrorMessage = "El estado del inmueble es obligatorio")]
    [StringLength(50, ErrorMessage = "El estado no puede superar los 50 caracteres")]
    public string Estado {get; set;} = string.Empty;


    public Propietario? Titular{get; set;}
    public TipoInmueble? Tipo{get; set;}

}