using MVC.Models;
using MySqlConnector;
namespace MVC.Repositories;

public class RepositorioInmueble : RepositorioBase, IRepositorioInmueble
{
    public RepositorioInmueble(IConfiguration config) : base(config)
    {
        
    }


    public int Alta(Inmueble i)
    {
        using var conexion = new MySqlConnection(connectionString);
        string sql = @"INSERT INTO (inmueble propietario_id, tipo_inmueble_id, direccion, cupo, latitud, longitud, precio_por_dia, porcentaje_seña, estado)
        VALUES (@propietario_id, @tipo_inmueble_id, @direccion, @cupo, @latitud, @longitud, @precio_por_dia, @porcentaje_seña, @estado)
        SELECT LAST_INSERT_ID();";
        using var command =  new MySqlCommand(sql, conexion);
        command.Parameters.AddWithValue("@propietario_id", i.PropietarioId);
        command.Parameters.AddWithValue("@tipo_inmueble_id", i.TipoInmuebleId);
        command.Parameters.AddWithValue("@direccion", i.Direccion);
        command.Parameters.AddWithValue("@cupo", i.Cupo);        
        command.Parameters.AddWithValue("@latitud", i.Latitud);
        command.Parameters.AddWithValue("@longitud", i.Longitud);
        command.Parameters.AddWithValue("@precio_por_dia", i.PrecioPorDia);        
        command.Parameters.AddWithValue("@porcentaje_seña", i.PorcentajeSeña);
        command.Parameters.AddWithValue("@estado", i.Estado);
        conexion.Open();
        int id= Convert.ToInt32(command.ExecuteScalar());
        i.IdInmueble=id;
        return id;
    }
    public int Baja(int id)
    {return -1;}
    public int Modificacion(Inmueble i){return -1;}
    public IList<Inmueble> GetAll(){return null;}
}