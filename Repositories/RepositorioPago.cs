using MVC.Models;
using MySqlConnector;

namespace MVC.Repositories;

public class RepositorioPago : RepositorioBase, IRepositorioPago
{
    public RepositorioPago(IConfiguration configuration)
        : base(configuration)
    {
    }

    public List<Pago> ObtenerTodos()
    {
        var pagos = new List<Pago>();

        using var connection = new MySqlConnection(connectionString);
        connection.Open();

        var query = @"SELECT id,reserva_id,concepto,fecha_pago,importe,estado,usuario_creador_id,usuario_anulador_id,activo FROM pago WHERE activo = 1";

        using var command = new MySqlCommand(query, connection);
        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            pagos.Add(new Pago
            {
                id_pago = reader.GetInt32("id"),
                ReservaId = reader.GetInt32("reserva_id"),
                concepto = reader.IsDBNull(reader.GetOrdinal("concepto"))
                    ? null
                    : reader.GetString("concepto"),
                fecha_pago = reader.GetDateTime("fecha_pago"),
                importe = reader.GetDecimal("importe"),
                estado = reader.GetString("estado"),
                Activo = reader.GetBoolean("activo"),

                usuario_creador_id =
                    reader.IsDBNull(reader.GetOrdinal("usuario_creador_id"))
                    ? null
                    : reader.GetInt32("usuario_creador_id"),

                usuario_anulador_id =
                    reader.IsDBNull(reader.GetOrdinal("usuario_anulador_id"))
                    ? null
                    : reader.GetInt32("usuario_anulador_id")
            });
        }

        return pagos;
    }

    public Pago? ObtenerPorId(int id)
    {
        using var connection = new MySqlConnection(connectionString);
        connection.Open();
        var query = @"SELECT id,reserva_id,concepto,fecha_pago,importe,estado,usuario_creador_id,usuario_anulador_id,activo FROM pago WHERE id = @id";

        using var command = new MySqlCommand(query, connection);

        command.Parameters.AddWithValue("@id", id);

        using var reader = command.ExecuteReader();

        if (!reader.Read())
            return null;

        return new Pago
        {
            id_pago = reader.GetInt32("id"),
            ReservaId = reader.GetInt32("reserva_id"),

            concepto = reader.IsDBNull(reader.GetOrdinal("concepto"))
                ? null
                : reader.GetString("concepto"),

            fecha_pago = reader.GetDateTime("fecha_pago"),
            importe = reader.GetDecimal("importe"),
            estado = reader.GetString("estado"),
            Activo = reader.GetBoolean("activo"),

            usuario_creador_id =
                reader.IsDBNull(reader.GetOrdinal("usuario_creador_id"))
                ? null
                : reader.GetInt32("usuario_creador_id"),

            usuario_anulador_id =
                reader.IsDBNull(reader.GetOrdinal("usuario_anulador_id"))
                ? null
                : reader.GetInt32("usuario_anulador_id")
        };
    }

    public void Crear(Pago pago)
    {
        var query = @"INSERT INTO pago
                    (reserva_id,concepto,fecha_pago,importe,estado,usuario_creador_id,activo) VALUES(@reserva_id,@concepto,@fecha_pago,@importe,@estado,@usuario_creador_id,1)";

        using var connection = new MySqlConnection(connectionString);
        connection.Open();
        using var command = new MySqlCommand(query, connection);

        command.Parameters.AddWithValue("@reserva_id", pago.ReservaId);
        command.Parameters.AddWithValue("@concepto", pago.concepto);
        command.Parameters.AddWithValue("@fecha_pago", pago.fecha_pago);
        command.Parameters.AddWithValue("@importe", pago.importe);
        command.Parameters.AddWithValue("@estado", pago.estado);

        command.Parameters.AddWithValue(
            "@usuario_creador_id",
            (object?)pago.usuario_creador_id ?? DBNull.Value
        );

        command.ExecuteNonQuery();
    }

    public void Modificar(Pago pago)
    {
        var query = @"UPDATE pago
                      SET reserva_id = @reserva_id,concepto = @concepto,fecha_pago = @fecha_pago,importe = @importe,estado = @estado,usuario_anulador_id = @usuario_anulador_id,activo = @activo
                      WHERE id = @id";

        using var connection = new MySqlConnection(connectionString);
        connection.Open();
        using var command = new MySqlCommand(query, connection);

        command.Parameters.AddWithValue("@id", pago.id_pago);
        command.Parameters.AddWithValue("@reserva_id", pago.ReservaId);
        command.Parameters.AddWithValue("@concepto", pago.concepto);
        command.Parameters.AddWithValue("@fecha_pago", pago.fecha_pago);
        command.Parameters.AddWithValue("@importe", pago.importe);
        command.Parameters.AddWithValue("@estado", pago.estado);
        command.Parameters.AddWithValue("@activo", pago.Activo);

        command.Parameters.AddWithValue(
            "@usuario_anulador_id",
            (object?)pago.usuario_anulador_id ?? DBNull.Value
        );

        command.ExecuteNonQuery();
    }
}