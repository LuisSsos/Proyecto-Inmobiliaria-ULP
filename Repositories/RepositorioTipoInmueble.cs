using MVC.Models;
using MySqlConnector;
namespace MVC.Repositories;

public class RepositorioTipoInmueble : RepositorioBase, IRepositorioTipoInmueble
{
    public RepositorioTipoInmueble(IConfiguration config) : base(config)
    {

    }

    public int Alta(TipoInmueble tipo)
    {
        using var conexion = new MySqlConnection(connectionString);
        string sql = @"INSERT INTO tipo_inmueble(nombre, activo)
                        VALUES(@nombre, 1)
                        SELECT LAST_INSERT_ID();";
        using var command = new MySqlCommand(sql, conexion);
        command.Parameters.AddWithValue("@nombre", tipo.Nombre);
        conexion.Open();
        int id = Convert.ToInt32(command.ExecuteScalar());
        tipo.IdTipoInmueble = id;
        return id;
    }
    public int Modificacion(TipoInmueble tipo)
    {
        using var conexion = new MySqlConnection(connectionString);
        string sql = @"UPDATE tipo_inmueble
                        SET nombre=@nombre,
                         activo = @activo   
                         WHERE id=@id";
        using var command = new MySqlCommand(sql, conexion);
        command.Parameters.AddWithValue("@id", tipo.IdTipoInmueble);
        command.Parameters.AddWithValue("@nombre", tipo.Nombre);
        command.Parameters.AddWithValue("@activo", tipo.Activo);
        conexion.Open();
        return command.ExecuteNonQuery();

    }
    public int Baja(int id)
    {
        using var conexion = new MySqlConnection(connectionString);
        string sql = "UPDATE tipo_inmueble SET activo = 0 WHERE id = @id";
        using var command = new MySqlCommand(sql, conexion);
        command.Parameters.AddWithValue("@id", id);
        conexion.Open();
        return command.ExecuteNonQuery();
    }
    public int BajaFisica(int id)
    {
        using var conexion = new MySqlConnection(connectionString);
        string sql = @"DELETE FROM tipo_inmueble WHERE id=@id";
        using var command = new MySqlCommand(sql, conexion);
        command.Parameters.AddWithValue("@id", id);
        conexion.Open();
        return command.ExecuteNonQuery();
    }
    public IList<TipoInmueble> GetAll()
    {
        var lista = new List<TipoInmueble>();
        using var conexion = new MySqlConnection(connectionString);
        string sql = @"SELECT id, nombre, activo
                       FROM tipo_inmueble
                       WHERE activo = 1;";
        using var command = new MySqlCommand(sql, conexion);
        conexion.Open();
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            lista.Add(new TipoInmueble
            {
                IdTipoInmueble = reader.GetInt32("id"),
                Nombre = reader.GetString("nombre"),
                Activo = reader.GetBoolean("activo")
            });
        }
        return lista;
    }
    public TipoInmueble? GetById(int id)
    {
        using var conexion = new MySqlConnection(connectionString);
        string sql = @"SELECT id, nombre, activo
                       FROM tipo_inmueble
                       WHERE id = @id AND activo = 1;";
        using var command = new MySqlCommand(sql, conexion);
        command.Parameters.AddWithValue("@id", id);
        conexion.Open();
        using var reader = command.ExecuteReader();
        if (reader.Read())
        {
            return new TipoInmueble
            {
                IdTipoInmueble = reader.GetInt32("id"),
                Nombre = reader.GetString("nombre"),
                Activo = reader.GetBoolean("activo")
            };
        }
        return null;
    }

    public IList<TipoInmueble> GetAllIncludingInactive()
    {
       var lista = new List<TipoInmueble>();
        using var conexion = new MySqlConnection(connectionString);
        string sql = @"SELECT id, nombre, activo
                       FROM tipo_inmueble;";
        using var command = new MySqlCommand(sql, conexion);
        conexion.Open();
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            lista.Add(new TipoInmueble
            {
                IdTipoInmueble = reader.GetInt32("id"),
                Nombre = reader.GetString("nombre"),
                Activo = reader.GetBoolean("activo")
            });
        }
        return lista;
    }
}