namespace MVC.Models
{
    public class Reserva
    {
        public int id_reserva { get; set; }
        public int inquilino_id { get; set; }
        public int inmueble_id { get; set; }
        public DateTime fecha_desde { get; set; }
        public DateTime fecha_hasta { get; set; }
        public DateTime? fecha_fin_real { get; set; }
        public decimal monto_por_dia { get; set; }
        public decimal multa { get; set; }
        public string estado { get; set; } = string.Empty;
        public int usuario_creador_id { get; set; }
        public int? usuario_terminador_id { get; set; }
    }
}