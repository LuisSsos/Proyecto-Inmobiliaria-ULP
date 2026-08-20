using MVC.Models;
using MVC.db;
using MySqlConnector;

namespace MVC.Repositories;

public class RepositorioInquilino
{
    private readonly Conexion conexion;

    public RepositorioInquilino(Conexion conexion)
    {
        this.conexion = conexion;
    }

    //listar todos los inquilinos
    public List<Inquilino> ObtenerTodos()
    {
        var lista = new List<Inquilino>();

        using var connection = conexion.GetConnection();
        connection.Open();

        var query = "SELECT id, nombre_completo, dni, email, telefono FROM inquilino";
        using var command = new MySqlCommand(query, connection);
        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            var inquilino = new Inquilino
            {
                id_inquilino = reader.GetInt32("id"),
                dni = reader.IsDBNull(reader.GetOrdinal("dni")) ? "" : reader.GetString("dni"),
                nombre_completo = reader.GetString("nombre_completo"),
                email = reader.IsDBNull(reader.GetOrdinal("email")) ? "" : reader.GetString("email"),
                telefono = reader.IsDBNull(reader.GetOrdinal("telefono")) ? "" : reader.GetString("telefono")
            };
            lista.Add(inquilino);
        }

        return lista;
    }

    //listar inquilino por id
    public Inquilino? ObtenerPorId(int id)
    {
        using var connection = conexion.GetConnection();
        connection.Open();

        var query = "SELECT id, nombre_completo, dni, email, telefono FROM inquilino WHERE id = @id";

        using var command = new MySqlCommand(query, connection);
        command.Parameters.AddWithValue("@id", id);

        using var reader = command.ExecuteReader();

        if (reader.Read())
        {
            return new Inquilino
            {
                id_inquilino = reader.GetInt32("id"),
                nombre_completo = reader.IsDBNull(reader.GetOrdinal("nombre_completo")) ? "" : reader.GetString("nombre_completo"),
                dni = reader.IsDBNull(reader.GetOrdinal("dni")) ? "" : reader.GetString("dni"),
                email = reader.IsDBNull(reader.GetOrdinal("email")) ? "" : reader.GetString("email"),
                telefono = reader.IsDBNull(reader.GetOrdinal("telefono")) ? "" : reader.GetString("telefono")
            };
        }

        return null;
    }

    //alta de inquilino
    public void Crear(Inquilino inquilino)
    {
        using var connection = conexion.GetConnection();
        connection.Open();

        var query = "INSERT INTO inquilino (nombre_completo, dni, email, telefono) VALUES (@nombre, @dni, @email, @telefono)";

        using var command = new MySqlCommand(query, connection);

        command.Parameters.AddWithValue("@nombre", inquilino.nombre_completo);
        command.Parameters.AddWithValue("@dni", inquilino.dni);
        command.Parameters.AddWithValue("@email", (object?)inquilino.email ?? DBNull.Value);
        command.Parameters.AddWithValue("@telefono", (object?)inquilino.telefono ?? DBNull.Value);

        command.ExecuteNonQuery();
    }

    // modificar inquilino
    public void Modificar(Inquilino inquilino)
    {
        using var connection = conexion.GetConnection();
        connection.Open();

        var query = "UPDATE inquilino SET nombre_completo = @nombre, dni = @dni, email = @email, telefono = @telefono WHERE id = @id";

        using var command = new MySqlCommand(query, connection);

        command.Parameters.AddWithValue("@id", inquilino.id_inquilino);
        command.Parameters.AddWithValue("@nombre", inquilino.nombre_completo);
        command.Parameters.AddWithValue("@dni", inquilino.dni);
        command.Parameters.AddWithValue("@email", (object?)inquilino.email ?? DBNull.Value);
        command.Parameters.AddWithValue("@telefono", (object?)inquilino.telefono ?? DBNull.Value);

        command.ExecuteNonQuery();
    }

    // baja de inquilino
    public void Eliminar(int id)
    {
        using var connection = conexion.GetConnection();
        connection.Open();

        var query = "DELETE FROM inquilino WHERE id = @id";

        using var command = new MySqlCommand(query, connection);
        command.Parameters.AddWithValue("@id", id);

        command.ExecuteNonQuery();
    }
}