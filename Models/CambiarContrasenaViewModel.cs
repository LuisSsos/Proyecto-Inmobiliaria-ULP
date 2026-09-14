using System.ComponentModel.DataAnnotations;

namespace MVC.Models;

public class CambiarContrasenaViewModel
{
    [Required(ErrorMessage= "La contraseña es obligatoria")]
    [DataType(DataType.Password)]
    public string ContrasenaActual {get; set; } = string.Empty;

    [Required(ErrorMessage = "La nueva contraseña es obligatoria")]
    [DataType(DataType.Password)]
    public string NuevaContrasena {get;set;} = string.Empty;

    [Required(ErrorMessage = "Debes confirmar la nueva contraseña")]
    [DataType(DataType.Password)]
    [Compare(
        nameof(NuevaContrasena), 
        ErrorMessage = "Las contraseñas nuevas no coinciden")]
    public string ConfirmarContrasena{get; set; }= string.Empty;
}