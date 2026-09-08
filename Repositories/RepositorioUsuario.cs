using MVC.Models;
using MySqlConnector;

namespace MVC.Repositories;

public class RepositorioUsuario : RepositorioBase, IRepositorioUsuario
{
    public RepositorioUsuario(IConfiguration configuration) : base(configuration) { }

    public List<Usuario> ObtenerTodos()
    {
        var lista = new List<Usuario>();

        using var connection = new MySqlConnection(connectionString);
        connection.Open();

        var query = "SELECT id, nombre, apellido, email, rol, avatar FROM usuario";
        using var command = new MySqlCommand(query, connection);
        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            lista.Add(new Usuario
            {
                id_usuario = reader.GetInt32("id"),
                nombre = reader.IsDBNull(reader.GetOrdinal("nombre")) ? "" : reader.GetString("nombre"),
                apellido = reader.IsDBNull(reader.GetOrdinal("apellido")) ? "" : reader.GetString("apellido"),
                email = reader.IsDBNull(reader.GetOrdinal("email")) ? "" : reader.GetString("email"),
                rol = reader.IsDBNull(reader.GetOrdinal("rol")) ? "" : reader.GetString("rol"),
                avatar = reader.IsDBNull(reader.GetOrdinal("avatar")) ? null : reader.GetString("avatar")
            });
        }

        return lista;
    }

    public Usuario? ObtenerPorId(int id)
    {
        using var connection = new MySqlConnection(connectionString);
        connection.Open();

        var query = "SELECT id, nombre, apellido, email, rol, avatar FROM usuario WHERE id = @id";
        using var command = new MySqlCommand(query, connection);
        command.Parameters.AddWithValue("@id", id);

        using var reader = command.ExecuteReader();

        if (reader.Read())
        {
            return new Usuario
            {
                id_usuario = reader.GetInt32("id"),
                nombre = reader.IsDBNull(reader.GetOrdinal("nombre")) ? "" : reader.GetString("nombre"),
                apellido = reader.IsDBNull(reader.GetOrdinal("apellido")) ? "" : reader.GetString("apellido"),
                email = reader.IsDBNull(reader.GetOrdinal("email")) ? "" : reader.GetString("email"),
                rol = reader.IsDBNull(reader.GetOrdinal("rol")) ? "" : reader.GetString("rol"),
                avatar = reader.IsDBNull(reader.GetOrdinal("avatar")) ? null : reader.GetString("avatar")
            };
        }

        return null;
    }

    public bool ExisteEmail(string email, int idExcluir = 0)
    {
        using var connection = new MySqlConnection(connectionString);
        connection.Open();

        var query = "SELECT COUNT(*) FROM usuario WHERE email = @email AND id != @idExcluir";
        using var command = new MySqlCommand(query, connection);
        command.Parameters.AddWithValue("@email", email);
        command.Parameters.AddWithValue("@idExcluir", idExcluir);

        var count = Convert.ToInt32(command.ExecuteScalar());
        return count > 0;
    }

    public void Crear(Usuario usuario)
    {
        using var connection = new MySqlConnection(connectionString);
        connection.Open();

       var query = "INSERT INTO usuario (nombre, apellido, email, rol, avatar, password_hash) VALUES (@nombre, @apellido, @email, @rol, @avatar, @contrasena)";
        using var command = new MySqlCommand(query, connection);

        command.Parameters.AddWithValue("@nombre", usuario.nombre);
        command.Parameters.AddWithValue("@apellido", usuario.apellido);
        command.Parameters.AddWithValue("@email", usuario.email);
        command.Parameters.AddWithValue("@rol", usuario.rol);
        command.Parameters.AddWithValue("@avatar", (object?)usuario.avatar ?? DBNull.Value);
        command.Parameters.AddWithValue("@contrasena", usuario.contrasena);

        command.ExecuteNonQuery();
    }

    public void Modificar(Usuario usuario)
    {
        using var connection = new MySqlConnection(connectionString);
        connection.Open();

        var query = "UPDATE usuario SET nombre = @nombre, apellido = @apellido, email = @email, rol = @rol, avatar = @avatar, password_hash = @contrasena WHERE id = @id";
        using var command = new MySqlCommand(query, connection);

        command.Parameters.AddWithValue("@id", usuario.id_usuario);
        command.Parameters.AddWithValue("@nombre", usuario.nombre);
        command.Parameters.AddWithValue("@apellido", usuario.apellido);
        command.Parameters.AddWithValue("@email", usuario.email);
        command.Parameters.AddWithValue("@rol", usuario.rol);
        command.Parameters.AddWithValue("@avatar", (object?)usuario.avatar ?? DBNull.Value);
        command.Parameters.AddWithValue("@contrasena", usuario.contrasena);

        command.ExecuteNonQuery();
    }

    public void Eliminar(int id)
    {
        using var connection = new MySqlConnection(connectionString);
        connection.Open();

        var query = "DELETE FROM usuario WHERE id = @id";
        using var command = new MySqlCommand(query, connection);
        command.Parameters.AddWithValue("@id", id);

        command.ExecuteNonQuery();
    }

    public Usuario? ValidarCredenciales(string email, string contrasena)
{
    using var connection = new MySqlConnection(connectionString);
    connection.Open();
    var query = "SELECT id, nombre, apellido, email, rol, avatar FROM usuario WHERE email = @email AND password_hash = @contrasena";
    using var command = new MySqlCommand(query, connection);
    command.Parameters.AddWithValue("@email", email);
    command.Parameters.AddWithValue("@contrasena", contrasena);

    using var reader = command.ExecuteReader();

    if (reader.Read())
    {
        return new Usuario
        {
            id_usuario = reader.GetInt32("id"),
            nombre = reader.IsDBNull(reader.GetOrdinal("nombre")) ? "" : reader.GetString("nombre"),
            apellido = reader.IsDBNull(reader.GetOrdinal("apellido")) ? "" : reader.GetString("apellido"),
            email = reader.IsDBNull(reader.GetOrdinal("email")) ? "" : reader.GetString("email"),
            rol = reader.IsDBNull(reader.GetOrdinal("rol")) ? "" : reader.GetString("rol"),
            avatar = reader.IsDBNull(reader.GetOrdinal("avatar")) ? null : reader.GetString("avatar")
        };
    }

    return null;
}
}