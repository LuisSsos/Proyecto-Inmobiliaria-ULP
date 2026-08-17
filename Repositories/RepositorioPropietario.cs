using MVC.Models;
using MVC.db;
using MySqlConnector;

namespace MVC.Repositories;

public class RepositorioPropietario
{
    private readonly Conexion conexion;

    public RepositorioPropietario(Conexion conexion)
    {
        this.conexion = conexion;
    }

    public List<Propietario> GetAll()
    {
        var lista = new List<Propietario>();

        using var connection = conexion.GetConnection();
        connection.Open();

        var query = "SELECT id, nombre, dni_cuit, email FROM propietario";
        using var command = new MySqlCommand(query, connection);
        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            var propietario = new Propietario
            {
                id_propietario = reader.GetInt32("id"),
                nombre_completo = reader.GetString("nombre"),
                dniCuit = reader.IsDBNull(reader.GetOrdinal("dni_cuit")) ? "" : reader.GetString("dni_cuit"),
                email = reader.IsDBNull(reader.GetOrdinal("email")) ? null : reader.GetString("email")
            };
            lista.Add(propietario);
        }

        return lista;
    }
}