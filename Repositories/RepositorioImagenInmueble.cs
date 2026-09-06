using MVC.Models;
using MySqlConnector;

namespace MVC.Repositories;

public class RepositorioImagenInmueble : RepositorioBase, IRepositorioImagenInmueble
{
    public RepositorioImagenInmueble(IConfiguration configuration) : base(configuration) { }

    public List<ImagenInmueble> ObtenerPorInmueble(int inmuebleId)
    {
        var lista = new List<ImagenInmueble>();

        using var connection = new MySqlConnection(connectionString);
        connection.Open();

        var query = "SELECT id, inmueble_id, url, es_portada FROM imagen_inmueble WHERE inmueble_id = @inmuebleId";
        using var command = new MySqlCommand(query, connection);
        command.Parameters.AddWithValue("@inmuebleId", inmuebleId);

        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            lista.Add(new ImagenInmueble
            {
                id_imagenInmueble = reader.GetInt32("id"),
                inmueble_id = reader.GetInt32("inmueble_id"),
                url = reader.IsDBNull(reader.GetOrdinal("url")) ? null : reader.GetString("url"),
                esPortada = !reader.IsDBNull(reader.GetOrdinal("es_portada")) && reader.GetBoolean("es_portada")
            });
        }

        return lista;
    }

    public ImagenInmueble? ObtenerPorId(int id)
    {
        using var connection = new MySqlConnection(connectionString);
        connection.Open();

        var query = "SELECT id, inmueble_id, url, es_portada FROM imagen_inmueble WHERE id = @id";
        using var command = new MySqlCommand(query, connection);
        command.Parameters.AddWithValue("@id", id);

        using var reader = command.ExecuteReader();

        if (reader.Read())
        {
            return new ImagenInmueble
            {
                id_imagenInmueble = reader.GetInt32("id"),
                inmueble_id = reader.GetInt32("inmueble_id"),
                url = reader.IsDBNull(reader.GetOrdinal("url")) ? null : reader.GetString("url"),
                esPortada = !reader.IsDBNull(reader.GetOrdinal("es_portada")) && reader.GetBoolean("es_portada")
            };
        }

        return null;
    }

    public void Crear(ImagenInmueble imagen)
    {
        using var connection = new MySqlConnection(connectionString);
        connection.Open();

        var query = "INSERT INTO imagen_inmueble (inmueble_id, url, es_portada) VALUES (@inmuebleId, @url, @esPortada)";
        using var command = new MySqlCommand(query, connection);

        command.Parameters.AddWithValue("@inmuebleId", imagen.inmueble_id);
        command.Parameters.AddWithValue("@url", imagen.url);
        command.Parameters.AddWithValue("@esPortada", imagen.esPortada);

        command.ExecuteNonQuery();
    }

    public void Eliminar(int id)
    {
        using var connection = new MySqlConnection(connectionString);
        connection.Open();

        var query = "DELETE FROM imagen_inmueble WHERE id = @id";
        using var command = new MySqlCommand(query, connection);
        command.Parameters.AddWithValue("@id", id);

        command.ExecuteNonQuery();
    }
}