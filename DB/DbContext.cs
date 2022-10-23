using MySqlConnector;

namespace DB;
public class DbContext
{
    private readonly MySqlConnection _connection;

    public DbContext()
    {
        _connection = new MySqlConnection("server=localhost;port=3307;user=root;database=accountingdb;password=root;");
    }

    public MySqlConnection GetConnection() => _connection;
}
