using MySqlConnector;

namespace DBLibrary;

public class DbConnect : IDisposable
{
    private readonly MySqlConnection _connection;
    public const string DbName = "accountingdb";

    public DbConnect()
    {
        _connection = new MySqlConnection($"server=localhost;port=3307;user=root;database={DbName};password=root;");        
    }

    public MySqlConnection GetConnection() => _connection;

    public void Open() => _connection.Open();

    public void Close() => _connection.Close();

    public void Dispose()
    {
        _connection.Close();
        _connection.Dispose();
    }     
}
