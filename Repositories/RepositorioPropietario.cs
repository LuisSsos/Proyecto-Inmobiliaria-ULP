using MVC.Models;
using MySqlConnector;

namespace MVC.Repositories;

public class RepositorioPropietario : RepositorioBase, IRepositorioPropietario
{
    public RepositorioPropietario(IConfiguration configuration) : base(configuration) { }

    public int Alta(Propietario p)
    {
        using var connection = new MySqlConnection(connectionString);
        string sql = @"INSERT INTO propietario (nombre, dni_cuit, email, telefono)
                       VALUES (@nombre, @dni_cuit, @email, @telefono);
                       SELECT LAST_INSERT_ID();";
        using var command = new MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("@nombre", p.Nombre);
        command.Parameters.AddWithValue("@dni_cuit", p.DniCuit);
        command.Parameters.AddWithValue("@email", (object?)p.Email ?? DBNull.Value);
        command.Parameters.AddWithValue("@telefono", (object?)p.Telefono ?? DBNull.Value);
        connection.Open();
        int id = Convert.ToInt32(command.ExecuteScalar());
        p.IdPropietario = id;
        return id;
    }

    public int Baja(int id)
    {
        using var connection = new MySqlConnection(connectionString);
        string sql = "DELETE FROM propietario WHERE id = @id";
        using var command = new MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("@id", id);
        connection.Open();
        return command.ExecuteNonQuery();
    }

    public int Modificacion(Propietario p)
    {
        using var connection = new MySqlConnection(connectionString);
        string sql = @"UPDATE propietario 
                       SET nombre = @nombre, dni_cuit = @dni_cuit, 
                           email = @email, telefono = @telefono
                       WHERE id = @id";
        using var command = new MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("@id", p.IdPropietario);
        command.Parameters.AddWithValue("@nombre", p.Nombre);
        command.Parameters.AddWithValue("@dni_cuit", p.DniCuit);
        command.Parameters.AddWithValue("@email", (object?)p.Email ?? DBNull.Value);
        command.Parameters.AddWithValue("@telefono", (object?)p.Telefono ?? DBNull.Value);
        connection.Open();
        return command.ExecuteNonQuery();
    }

    // verifica si ya existe un propietario con ese DNI/CUIT
    public bool ExisteDniCuit(string dniCuit, int idExcluir = 0)
    {
        using var connection = new MySqlConnection(connectionString);
        connection.Open();

        var sql = "SELECT COUNT(*) FROM propietario WHERE dni_cuit = @dniCuit AND id != @idExcluir";
        using var command = new MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("@dniCuit", dniCuit);
        command.Parameters.AddWithValue("@idExcluir", idExcluir);

        var count = Convert.ToInt32(command.ExecuteScalar());
        return count > 0;
    }

    public IList<Propietario> GetAll()
    {
        var lista = new List<Propietario>();
        using var connection = new MySqlConnection(connectionString);
        string sql = "SELECT id, nombre, dni_cuit, email, telefono FROM propietario";
        using var command = new MySqlCommand(sql, connection);
        connection.Open();
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            lista.Add(new Propietario
            {
                IdPropietario = reader.GetInt32("id"),
                Nombre = reader.IsDBNull(reader.GetOrdinal("nombre")) ? "" : reader.GetString("nombre"),
                DniCuit = reader.IsDBNull(reader.GetOrdinal("dni_cuit")) ? "" : reader.GetString("dni_cuit"),
                Email = reader.IsDBNull(reader.GetOrdinal("email")) ? "" : reader.GetString("email"),
                Telefono = reader.IsDBNull(reader.GetOrdinal("telefono")) ? "" : reader.GetString("telefono")
            });
        }
        return lista;
    }
}