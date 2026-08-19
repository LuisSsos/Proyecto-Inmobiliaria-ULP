namespace MVC.Models
{
    public class Propietario
    {
        public int id_propietario { get; set; }
        public string nombre_completo { get; set; } = string.Empty;
        public string dniCuit { get; set; } = string.Empty;
        public string? email { get; set; }
        public string? telefono { get; set; }

    }
}