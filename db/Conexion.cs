using MySqlConnector;

namespace MVC.db;

public class Conexion
{
    private readonly string connectionString;

    public Conexion(IConfiguration configuration)
    {
        connectionString = configuration.GetConnectionString("DefaultConnection")!;
    }

    public MySqlConnection GetConnection()
    {
        return new MySqlConnection(connectionString);
    }
}