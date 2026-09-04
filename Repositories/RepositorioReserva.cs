using MVC.Models;
using MySqlConnector;

namespace MVC.Repositories;

public class RepositorioReserva : RepositorioBase, IRepositorioReserva
{
    public RepositorioReserva(IConfiguration configuration) : base(configuration) { }

    // Listar todas las reservas
    public List<Reserva> ObtenerTodos()
    {
        var lista = new List<Reserva>();

        using var connection = new MySqlConnection(connectionString);
        connection.Open();

        var query = "SELECT id, inquilino_id, inmueble_id, fecha_desde, fecha_hasta, fecha_fin_real, monto_por_dia, multa, estado FROM reserva ";

        using var command = new MySqlCommand(query, connection);
        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            lista.Add(new Reserva
            {
                id_reserva = reader.GetInt32("id"),
                inquilino_id = reader.GetInt32("inquilino_id"),
                inmueble_id = reader.GetInt32("inmueble_id"),

                fecha_desde = reader.GetDateTime("fecha_desde"),
                fecha_hasta = reader.GetDateTime("fecha_hasta"),

                fecha_fin_real = reader.IsDBNull(reader.GetOrdinal("fecha_fin_real")) ? null : reader.GetDateTime("fecha_fin_real"),

                monto_por_dia = reader.GetDecimal("monto_por_dia"),
                multa = reader.GetDecimal("multa"),
                estado = reader.GetString("estado"),


            });
        }

        return lista;
    }


    // Obtener reserva por ID
    public Reserva? ObtenerPorId(int id)
    {
        using var connection = new MySqlConnection(connectionString);
        connection.Open();

        var query = "SELECT id, inquilino_id, inmueble_id, fecha_desde, fecha_hasta, fecha_fin_real, monto_por_dia, multa, estado FROM reserva WHERE id = @id";

        using var command = new MySqlCommand(query, connection);
        command.Parameters.AddWithValue("@id", id);

        using var reader = command.ExecuteReader();

        if (reader.Read())
        {
            return new Reserva
            {
                id_reserva = reader.GetInt32("id"),
                inquilino_id = reader.GetInt32("inquilino_id"),
                inmueble_id = reader.GetInt32("inmueble_id"),

                fecha_desde = reader.GetDateTime("fecha_desde"),
                fecha_hasta = reader.GetDateTime("fecha_hasta"),

                fecha_fin_real = reader.IsDBNull(reader.GetOrdinal("fecha_fin_real")) ? null : reader.GetDateTime("fecha_fin_real"),

                monto_por_dia = reader.GetDecimal("monto_por_dia"),
                multa = reader.GetDecimal("multa"),
                estado = reader.GetString("estado")
            };
        }

        return null;
    }


    // Alta
    // Alta
    public void Crear(Reserva reserva)
    {
        using var connection = new MySqlConnection(connectionString);
        connection.Open();

        var query = @"INSERT INTO reserva (inquilino_id, inmueble_id, fecha_desde, fecha_hasta, fecha_fin_real, monto_por_dia, multa, estado) 
                VALUES ( @inquilino_id, @inmueble_id, @fecha_desde, @fecha_hasta, @fecha_fin_real, @monto_por_dia, @multa, @estado)";

        using var command = new MySqlCommand(query, connection);

        command.Parameters.AddWithValue("@inquilino_id", reserva.inquilino_id);
        command.Parameters.AddWithValue("@inmueble_id", reserva.inmueble_id);
        command.Parameters.AddWithValue("@fecha_desde", reserva.fecha_desde);
        command.Parameters.AddWithValue("@fecha_hasta", reserva.fecha_hasta);

        command.Parameters.AddWithValue(
            "@fecha_fin_real",
            (object?)reserva.fecha_fin_real ?? DBNull.Value
        );

        command.Parameters.AddWithValue("@monto_por_dia", reserva.monto_por_dia);
        command.Parameters.AddWithValue("@multa", reserva.multa);
        command.Parameters.AddWithValue("@estado", reserva.estado);
        command.ExecuteNonQuery();
    }


    // Modificar
    public void Modificar(Reserva reserva)
    {
        using var connection = new MySqlConnection(connectionString);
        connection.Open();

        var query = " UPDATE reserva SET inquilino_id = @inquilino_id, inmueble_id = @inmueble_id, fecha_desde = @fecha_desde, fecha_hasta = @fecha_hasta, fecha_fin_real = @fecha_fin_real, monto_por_dia = @monto_por_dia, multa = @multa, estado = @estado WHERE id = @id";

        using var command = new MySqlCommand(query, connection);

        command.Parameters.AddWithValue("@id", reserva.id_reserva);
        command.Parameters.AddWithValue("@inquilino_id", reserva.inquilino_id);
        command.Parameters.AddWithValue("@inmueble_id", reserva.inmueble_id);
        command.Parameters.AddWithValue("@fecha_desde", reserva.fecha_desde);
        command.Parameters.AddWithValue("@fecha_hasta", reserva.fecha_hasta);

        command.Parameters.AddWithValue(
            "@fecha_fin_real",
            (object?)reserva.fecha_fin_real ?? DBNull.Value
        );

        command.Parameters.AddWithValue("@monto_por_dia", reserva.monto_por_dia);
        command.Parameters.AddWithValue("@multa", reserva.multa);
        command.Parameters.AddWithValue("@estado", reserva.estado);
        command.ExecuteNonQuery();
    }


    // Baja
    public void Eliminar(int id)
    {
        using var connection = new MySqlConnection(connectionString);
        connection.Open();

        var query = "DELETE FROM reserva WHERE id = @id";

        using var command = new MySqlCommand(query, connection);
        command.Parameters.AddWithValue("@id", id);

        command.ExecuteNonQuery();
    }

    // Verifica si hay otra reserva para el mismo inmueble 
    public bool ExisteSolapamiento(int inmuebleId, DateTime fechaDesde, DateTime fechaHasta, int idExcluir = 0)
    {
        using var connection = new MySqlConnection(connectionString);
        connection.Open();

        var query = @"SELECT COUNT(*) FROM reserva 
                  WHERE inmueble_id = @inmuebleId 
                  AND id != @idExcluir
                  AND estado != 'Cancelada'
                  AND fecha_desde < @fechaHasta 
                  AND fecha_hasta > @fechaDesde";

        using var command = new MySqlCommand(query, connection);
        command.Parameters.AddWithValue("@inmuebleId", inmuebleId);
        command.Parameters.AddWithValue("@idExcluir", idExcluir);
        command.Parameters.AddWithValue("@fechaDesde", fechaDesde);
        command.Parameters.AddWithValue("@fechaHasta", fechaHasta);

        var count = Convert.ToInt32(command.ExecuteScalar());
        return count > 0;
    }
}