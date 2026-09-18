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
        string sql = @"INSERT INTO inmueble (propietario_id, tipo_inmueble_id, 
                        direccion, cupo, latitud, longitud, precio_por_dia, 
                        porcentaje_sena, estado, activo)
                        VALUES (@propietario_id, @tipo_inmueble_id, @direccion, 
                        @cupo, @latitud, @longitud, @precio_por_dia, @porcentaje_sena, @estado, 1);
                        SELECT LAST_INSERT_ID();";
        using var command = new MySqlCommand(sql, conexion);
        command.Parameters.AddWithValue("@propietario_id", i.PropietarioId);
        command.Parameters.AddWithValue("@tipo_inmueble_id", i.TipoInmuebleId);
        command.Parameters.AddWithValue("@direccion", i.Direccion);
        command.Parameters.AddWithValue("@cupo", i.Cupo);
        command.Parameters.AddWithValue("@latitud", i.Latitud);
        command.Parameters.AddWithValue("@longitud", i.Longitud);
        command.Parameters.AddWithValue("@precio_por_dia", i.PrecioPorDia);
        command.Parameters.AddWithValue("@porcentaje_sena", i.PorcentajeSeña);
        command.Parameters.AddWithValue("@estado", i.Estado);
        conexion.Open();
        int id = Convert.ToInt32(command.ExecuteScalar());
        i.IdInmueble = id;
        return id;
    }
    public int BajaFisica(int id)
    {
        using var conexion = new MySqlConnection(connectionString);
        string sql = @"DELETE FROM inmueble WHERE id=@id;";
        using var command = new MySqlCommand(sql, conexion);
        command.Parameters.AddWithValue("@id", id);
        conexion.Open();
        return command.ExecuteNonQuery();
    }
    public int Modificacion(Inmueble i)
    {
        using var conexion = new MySqlConnection(connectionString);

        string sql = @"UPDATE inmueble
                        SET propietario_id=@propietario_id, tipo_inmueble_id=@tipo_inmueble_id, 
                        direccion=@direccion, cupo=@cupo, latitud=@latitud, longitud=@longitud, 
                        precio_por_dia=@precio_por_dia, porcentaje_sena=@porcentaje_sena, estado=@estado, activo=@activo
                        WHERE id=@id;";
        using var command = new MySqlCommand(sql, conexion);
        command.Parameters.AddWithValue("@propietario_id", i.PropietarioId);
        command.Parameters.AddWithValue("@tipo_inmueble_id", i.TipoInmuebleId);
        command.Parameters.AddWithValue("@direccion", i.Direccion);
        command.Parameters.AddWithValue("@cupo", i.Cupo);
        command.Parameters.AddWithValue("@latitud", i.Latitud);
        command.Parameters.AddWithValue("@longitud", i.Longitud);
        command.Parameters.AddWithValue("@precio_por_dia", i.PrecioPorDia);
        command.Parameters.AddWithValue("@porcentaje_sena", i.PorcentajeSeña);
        command.Parameters.AddWithValue("@estado", i.Estado);
        command.Parameters.AddWithValue("@activo", i.Activo);
        command.Parameters.AddWithValue("@id", i.IdInmueble);
        conexion.Open();
        return command.ExecuteNonQuery();

    }
    public IList<Inmueble> GetAll()
    {
        var lista = new List<Inmueble>();
        using var conexion = new MySqlConnection(connectionString);
        string sql = @"SELECT i.id, i.propietario_id, i.tipo_inmueble_id, i.direccion, 
                        i.cupo, i.latitud, i.longitud, i.precio_por_dia, i.porcentaje_sena, i.estado,
                        p.nombre AS PropietarioNombre, p.dni_cuit, i.activo,
                        t.nombre AS TipoNombre
                        FROM inmueble i
                        INNER JOIN propietario p ON i.propietario_id = p.id
                        INNER JOIN tipo_inmueble t ON i.tipo_inmueble_id = t.id
                        WHERE i.activo=1;";
        using var command = new MySqlCommand(sql, conexion);
        conexion.Open();
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            lista.Add(new Inmueble
            {
                IdInmueble = reader.GetInt32("id"),
                PropietarioId = reader.GetInt32("propietario_id"),
                TipoInmuebleId = reader.GetInt32("tipo_inmueble_id"),
                Direccion = reader.GetString("direccion"),
                Cupo = reader.GetInt32("cupo"),
                Latitud = reader.GetDecimal("latitud"),
                Longitud = reader.GetDecimal("longitud"),
                PrecioPorDia = reader.GetDecimal("precio_por_dia"),
                PorcentajeSeña = reader.GetDecimal("porcentaje_sena"),
                Estado = reader.GetString("estado"),
                Activo = reader.GetBoolean("activo"),
                Titular = new Propietario
                {
                    IdPropietario = reader.GetInt32("propietario_id"),
                    Nombre = reader.GetString("PropietarioNombre"),
                    DniCuit = reader.IsDBNull(reader.GetOrdinal("dni_cuit")) ? "" : reader.GetString("dni_cuit")
                },
                Tipo = new TipoInmueble
                {
                    IdTipoInmueble = reader.GetInt32("tipo_inmueble_id"),
                    Nombre = reader.GetString("TipoNombre")
                }

            }
            );
        }
        return lista;
    }

    public int Baja(int id)
    {
        using var conexion = new MySqlConnection(connectionString);
        string sql = @"UPDATE inmueble SET activo = 0 WHERE id = @id;";
        using var command = new MySqlCommand(sql, conexion);
        command.Parameters.AddWithValue("@id", id);
        conexion.Open();
        return command.ExecuteNonQuery();
    }

    public Inmueble? GetById(int id)
    {
        using var conexion = new MySqlConnection(connectionString);
        string sql = @"SELECT i.id, i.propietario_id, i.tipo_inmueble_id, i.direccion, 
                    i.cupo, i.latitud, i.longitud, i.precio_por_dia, i.porcentaje_sena, i.estado,
                    p.nombre AS PropietarioNombre, p.dni_cuit, i.activo,
                    t.nombre AS TipoNombre
                    FROM inmueble i
                    INNER JOIN propietario p ON i.propietario_id = p.id
                    INNER JOIN tipo_inmueble t ON i.tipo_inmueble_id = t.id
                    WHERE i.id = @id;";
        using var command = new MySqlCommand(sql, conexion);
        command.Parameters.AddWithValue("@id", id);
        conexion.Open();
        using var reader = command.ExecuteReader();

        if (reader.Read())
        {
            return new Inmueble
            {
                IdInmueble = reader.GetInt32("id"),
                PropietarioId = reader.GetInt32("propietario_id"),
                TipoInmuebleId = reader.GetInt32("tipo_inmueble_id"),
                Direccion = reader.GetString("direccion"),
                Cupo = reader.GetInt32("cupo"),
                Latitud = reader.GetDecimal("latitud"),
                Longitud = reader.GetDecimal("longitud"),
                PrecioPorDia = reader.GetDecimal("precio_por_dia"),
                PorcentajeSeña = reader.GetDecimal("porcentaje_sena"),
                Estado = reader.GetString("estado"),
                Activo = reader.GetBoolean("activo"),
                Titular = new Propietario
                {
                    IdPropietario = reader.GetInt32("propietario_id"),
                    Nombre = reader.GetString("PropietarioNombre"),
                    DniCuit = reader.IsDBNull(reader.GetOrdinal("dni_cuit")) ? "" : reader.GetString("dni_cuit")
                },
                Tipo = new TipoInmueble
                {
                    IdTipoInmueble = reader.GetInt32("tipo_inmueble_id"),
                    Nombre = reader.GetString("TipoNombre")
                }
            };
        }

        return null;
    }

    public IList<Inmueble> GetAllIncludingInactive()
    {
        var lista = new List<Inmueble>();

        using var connection = new MySqlConnection(connectionString);
        var query = @"SELECT i.id, i.propietario_id, i.tipo_inmueble_id, i.direccion, 
                        i.cupo, i.latitud, i.longitud, i.precio_por_dia, i.porcentaje_sena, 
                        i.estado, i.activo,
                        p.nombre AS PropietarioNombre, p.dni_cuit,
                        t.nombre AS TipoNombre
                        FROM inmueble i
                        INNER JOIN propietario p ON i.propietario_id = p.id
                        INNER JOIN tipo_inmueble t ON i.tipo_inmueble_id = t.id;";

        using var command = new MySqlCommand(query, connection);
        connection.Open();
        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            var inmueble = new Inmueble
            {
                IdInmueble = reader.GetInt32("id"),
                PropietarioId = reader.GetInt32("propietario_id"),
                TipoInmuebleId = reader.GetInt32("tipo_inmueble_id"),
                Direccion = reader.GetString("direccion"),
                Cupo = reader.GetInt32("cupo"),
                Latitud = reader.IsDBNull(reader.GetOrdinal("latitud")) ? 0 : reader.GetDecimal("latitud"),
                Longitud = reader.IsDBNull(reader.GetOrdinal("longitud")) ? 0 : reader.GetDecimal("longitud"),
                PrecioPorDia = reader.GetDecimal("precio_por_dia"),
                PorcentajeSeña = reader.GetDecimal("porcentaje_sena"),
                Estado = reader.GetString("estado"),
                Activo = reader.GetBoolean("activo"),

                Titular = new Propietario
                {
                    IdPropietario = reader.GetInt32("propietario_id"),
                    Nombre = reader.GetString("PropietarioNombre"),
                    DniCuit = reader.IsDBNull(reader.GetOrdinal("dni_cuit")) ? "" : reader.GetString("dni_cuit")
                },
                Tipo = new TipoInmueble
                {
                    IdTipoInmueble = reader.GetInt32("tipo_inmueble_id"),
                    Nombre = reader.GetString("TipoNombre")
                }
            };
            lista.Add(inmueble);
        }

        return lista;
    }

    public int CambiarEstado(int id, string estado)
    {
        using var conexion = new MySqlConnection(connectionString);
        string sql = @"UPDATE inmueble SET estado = @estado WHERE id = @id;";
        using var command = new MySqlCommand(sql, conexion);
        command.Parameters.AddWithValue("@estado", estado);
        command.Parameters.AddWithValue("@id", id);
        conexion.Open();
        return command.ExecuteNonQuery();
    }
    public IList<Inmueble> ObtenerPaginado(
        int pagina,
        int cantidadPorPagina,
        string? estado,
        int? propietarioId)
    {
        var lista = new List<Inmueble>();

        int offset = (pagina - 1) * cantidadPorPagina;

        using var conexion = new MySqlConnection(connectionString);

        string sql = @"
        SELECT 
            i.id, i.propietario_id, i.tipo_inmueble_id, i.direccion, i.cupo, i.latitud, i.longitud,
            i.precio_por_dia, i.porcentaje_sena, i.estado, i.activo,

            p.nombre AS PropietarioNombre,
            p.dni_cuit,

            t.nombre AS TipoNombre

        FROM inmueble i

        INNER JOIN propietario p 
            ON i.propietario_id = p.id

        INNER JOIN tipo_inmueble t 
            ON i.tipo_inmueble_id = t.id

        WHERE i.activo = 1
    ";

        if (!string.IsNullOrEmpty(estado))
        {
            sql += " AND i.estado = @estado ";
        }

        if (propietarioId.HasValue)
        {
            sql += " AND i.propietario_id = @propietarioId ";
        }

        sql += " ORDER BY i.id LIMIT @cantidadPorPagina OFFSET @offset; ";

        using var command = new MySqlCommand(sql, conexion);

        if (!string.IsNullOrEmpty(estado))
        {
            command.Parameters.AddWithValue("@estado", estado);
        }

        if (propietarioId.HasValue)
        {
            command.Parameters.AddWithValue(
                "@propietarioId",
                propietarioId.Value
            );
        }

        command.Parameters.AddWithValue("@cantidadPorPagina", cantidadPorPagina);
        command.Parameters.AddWithValue("@offset", offset);

        conexion.Open();

        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            lista.Add(new Inmueble
            {
                IdInmueble = reader.GetInt32("id"),
                PropietarioId = reader.GetInt32("propietario_id"),
                TipoInmuebleId = reader.GetInt32("tipo_inmueble_id"),
                Direccion = reader.GetString("direccion"),
                Cupo = reader.GetInt32("cupo"),
                Latitud = reader.GetDecimal("latitud"),
                Longitud = reader.GetDecimal("longitud"),
                PrecioPorDia = reader.GetDecimal("precio_por_dia"),
                PorcentajeSeña = reader.GetDecimal("porcentaje_sena"),
                Estado = reader.GetString("estado"),
                Activo = reader.GetBoolean("activo"),

                Titular = new Propietario
                {
                    IdPropietario = reader.GetInt32("propietario_id"),
                    Nombre = reader.GetString("PropietarioNombre"),
                    DniCuit = reader.IsDBNull(reader.GetOrdinal("dni_cuit"))
                        ? ""
                        : reader.GetString("dni_cuit")
                },

                Tipo = new TipoInmueble
                {
                    IdTipoInmueble = reader.GetInt32("tipo_inmueble_id"),
                    Nombre = reader.GetString("TipoNombre")
                }
            });
        }

        return lista;
    }
    public int ContarInmuebles(string? estado, int? propietarioId)
    {
        using var conexion = new MySqlConnection(connectionString);

        string sql = "SELECT COUNT(*) FROM inmueble i WHERE i.activo = 1";

        if (!string.IsNullOrWhiteSpace(estado))
        {
            sql += " AND i.estado = @estado ";
        }

        if (propietarioId.HasValue)
        {
            sql += " AND i.propietario_id = @propietarioId ";
        }

        using var command = new MySqlCommand(sql, conexion);

        if (!string.IsNullOrWhiteSpace(estado))
        {
            command.Parameters.AddWithValue("@estado", estado);
        }

        if (propietarioId.HasValue)
        {
            command.Parameters.AddWithValue("@propietarioId", propietarioId.Value);
        }

        conexion.Open();

        return Convert.ToInt32(command.ExecuteScalar());
    }

    public IList<Inmueble> ObtenerMasReservados(
     int dias,
     int pagina,
     int cantidadPorPagina)
    {
        var lista = new List<Inmueble>();

        using var conexion = new MySqlConnection(connectionString);

        int offset = (pagina - 1) * cantidadPorPagina;

        string sql = @"SELECT 
            i.id, i.propietario_id, i.tipo_inmueble_id, i.direccion, i.cupo,
            i.latitud, i.longitud, i.precio_por_dia, i.porcentaje_sena, i.estado, i.activo,

            p.nombre AS PropietarioNombre,
            p.dni_cuit,

            t.nombre AS TipoNombre,

            COUNT(r.id) AS CantidadReservas

        FROM inmueble i

        INNER JOIN propietario p
            ON i.propietario_id = p.id

        INNER JOIN tipo_inmueble t
            ON i.tipo_inmueble_id = t.id

        INNER JOIN reserva r
            ON i.id = r.inmueble_id

        WHERE i.activo = 1
          AND r.fecha_desde >= DATE_SUB(
              CURDATE(),
              INTERVAL @dias DAY
          )

        GROUP BY
            i.id,
            i.propietario_id,
            i.tipo_inmueble_id,
            i.direccion,
            i.cupo,
            i.latitud,
            i.longitud,
            i.precio_por_dia,
            i.porcentaje_sena,
            i.estado,
            i.activo,
            p.nombre,
            p.dni_cuit,
            t.nombre

        ORDER BY CantidadReservas DESC

        LIMIT @cantidadPorPagina
        OFFSET @offset;
    ";

        using var command = new MySqlCommand(sql, conexion);

        command.Parameters.AddWithValue("@dias", dias);

        command.Parameters.AddWithValue( "@cantidadPorPagina", cantidadPorPagina);

        command.Parameters.AddWithValue( "@offset", offset);

        conexion.Open();

        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            lista.Add(new Inmueble
            {
                IdInmueble = reader.GetInt32("id"),
                PropietarioId = reader.GetInt32("propietario_id"),

                TipoInmuebleId = reader.GetInt32("tipo_inmueble_id"),

                Direccion = reader.GetString("direccion"),

                Cupo = reader.GetInt32("cupo"),

                Latitud = reader.GetDecimal("latitud"),

                Longitud = reader.GetDecimal("longitud"),

                PrecioPorDia = reader.GetDecimal("precio_por_dia"),

                PorcentajeSeña = reader.GetDecimal("porcentaje_sena"),

                Estado = reader.GetString("estado"),

                Activo = reader.GetBoolean("activo"),

                CantidadReservas = reader.GetInt32("CantidadReservas"),

                Titular = new Propietario
                {
                    IdPropietario = reader.GetInt32("propietario_id"),

                    Nombre = reader.GetString("PropietarioNombre"),

                    DniCuit = reader.IsDBNull( reader.GetOrdinal("dni_cuit"))? "" : reader.GetString("dni_cuit")
                },

                Tipo = new TipoInmueble
                {
                    IdTipoInmueble =
                        reader.GetInt32("tipo_inmueble_id"),

                    Nombre =
                        reader.GetString("TipoNombre")
                }
            });
        }

        return lista;
    }

    public int ContarMasReservados(int dias)
    {
        using var conexion = new MySqlConnection(connectionString);

        string sql = @"
        SELECT COUNT(DISTINCT i.id)

        FROM inmueble i

        INNER JOIN reserva r
            ON i.id = r.id

        WHERE i.activo = 1
          AND r.fecha_desde >= DATE_SUB(
              CURDATE(),
              INTERVAL @dias DAY
          );";

        using var command = new MySqlCommand(sql, conexion);

        command.Parameters.AddWithValue("@dias", dias);

        conexion.Open();

        return Convert.ToInt32(command.ExecuteScalar());
    }
   
    public IList<Inmueble> InmueblesEntreFechas( DateTime fechaDesde, DateTime fechaHasta)
    {
        var lista = new List<Inmueble>();

        using var conexion = new MySqlConnection(connectionString);

        String sql = @"SELECT i.id,
        i.propietario_id,
        i.tipo_inmueble_id,
        i.direccion,
        i.cupo,
        i.latitud,
        i.longitud,
        i.precio_por_dia,
        i.porcentaje_sena,
        i.estado,
        i.activo,
        p.nombre AS PropietarioNombre,
        p.dni_cuit,
        t.nombre AS TipoNombre
        FROM inmueble i
    INNER JOIN propietario p
        ON i.propietario_id = p.id
    INNER JOIN tipo_inmueble t
        ON i.tipo_inmueble_id = t.id
    WHERE i.activo = 1
      AND i.estado = 'Disponible'
      AND NOT EXISTS
      (
          SELECT 1
          FROM reserva r
          WHERE r.inmueble_id = i.id
            AND r.activo = 1
            AND r.estado != 'Cancelada'
            AND r.fecha_desde < @fechaHasta
            AND r.fecha_hasta > @fechaDesde);";

    using var command = new MySqlCommand(sql, conexion);

    command.Parameters.AddWithValue("@fechaDesde",fechaDesde);
    command.Parameters.AddWithValue("@fechaHasta",fechaHasta);
            
    conexion.Open();
    using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            lista.Add(new Inmueble
            {
                IdInmueble = reader.GetInt32("id"),
                PropietarioId = reader.GetInt32("propietario_id"),
                TipoInmuebleId = reader.GetInt32("tipo_inmueble_id"),
                Direccion = reader.GetString("direccion"),
                Cupo = reader.GetInt32("cupo"),
                Latitud = reader.GetDecimal("latitud"),
                Longitud = reader.GetDecimal("longitud"),
                PrecioPorDia = reader.GetDecimal("precio_por_dia"),
                PorcentajeSeña = reader.GetDecimal("porcentaje_sena"),
                Estado = reader.GetString("estado"),
                Activo = reader.GetBoolean("activo"),

                Titular = new Propietario
                {
                     IdPropietario = reader.GetInt32("propietario_id"),
            Nombre = reader.GetString("PropietarioNombre"),
            DniCuit = reader.IsDBNull(reader.GetOrdinal("dni_cuit"))
                ? ""
                : reader.GetString("dni_cuit")
       
                },
                Tipo = new TipoInmueble
                {
                    IdTipoInmueble = reader.GetInt32("tipo_inmueble_id"),
                    Nombre = reader.GetString("TipoNombre")
                } 
               });
               
        }        
        return lista;
    }
}