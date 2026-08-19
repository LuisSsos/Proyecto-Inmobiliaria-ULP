namespace MVC.Models
{
    public class Propietario
    {
        public int IdPropietario { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string DniCuit { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? Telefono { get; set; }

    }
}