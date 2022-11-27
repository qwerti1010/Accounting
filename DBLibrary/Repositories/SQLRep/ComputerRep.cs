using DBLibrary.Entities;
using DBLibrary.Interfaces;
using MySqlConnector;
using System.Data;

namespace DBLibrary.Repositories.SQLRep;

public class ComputerRep : IComputerRepository
{
    private readonly MySqlConnection _connection;

    public ComputerRep(DbContext context)
    {
        _connection = context.GetConnection();
    }

    public IList<Computer> Filter(int skip, int take, string? name = null, decimal price = 0, int status = 0, uint employeeID = 0)
    {
        var computers = new List<Computer>();
        var commandString = "SELECT * FROM computers WHERE isDeleted = 0";
        var command = new MySqlCommand(commandString, _connection);
        if (name != null)
        {
            command.CommandText += " AND name = @name";
            command.Parameters.Add("@name", MySqlDbType.VarChar).Value = name;
        }
        if (price != 0)
        {
            command.CommandText += " AND price = @price";
            command.Parameters.Add("@price", MySqlDbType.Decimal).Value = price;
        }
        if (status != 0)
        {
            command.CommandText += " AND status = @status";
            command.Parameters.Add("@status", MySqlDbType.Int32).Value = status;
        }
        if (employeeID != 0)
        {
            command.CommandText += " AND employeeID = @employeeID";
            command.Parameters.Add("@employeeID", MySqlDbType.UInt32).Value = employeeID;
        }
        command.CommandText += " LIMIT @skip, @take";
        command.Parameters.Add("@skip", MySqlDbType.Int32).Value = skip;
        command.Parameters.Add("@take", MySqlDbType.Int32).Value = take;
        var reader = command.ExecuteReader();
        while (reader.Read())
        {
            computers.Add(Record(reader));
        }
        reader.Close();
        return computers;
    }

    public Computer? GetByID(uint id)
    {
        var commandStr = "SELECT * FROM computers WHERE id = @id AND isDeleted = 0";
        var command = new MySqlCommand(commandStr, _connection);
        command.Parameters.Add("@id", MySqlDbType.Int32).Value = id;
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            return Record(reader);
        }
        return null;
    }

    public void Create(Computer entity)
    {
        var commandStr = "INSERT INTO computers (name, registrationDate," +
            " price, status, employeeID, exploitationStart)" +
            " VALUES(@name, @registrationDate, @price, @status, @employeeID, @exploitationStart)";
        var command = new MySqlCommand(commandStr, _connection);
        AddParameters(command, entity);
        command.ExecuteNonQuery();
    }

    public void Update(Computer entity)
    {
        var commandStr = "UPDATE computers SET name = @name, registrationDate = @registrationDate," +
            " price = @price, status = @status, employeeID = @employeeID," +
            " exploitationStart = @exploitationStart WHERE id = @id";
        var command = new MySqlCommand(commandStr, _connection);
        AddParameters(command, entity);
        command.ExecuteNonQuery();
    }

    private static Computer Record(IDataRecord record)
    {
        return new Computer
        {
            ID = (uint)record["ID"],
            Name = (string)record["name"],
            RegistrationDate = (DateTime)record["registrationDate"],
            Price = (decimal)record["price"],
            Status = (Status)record["status"],
            EmployeeID = (uint)record["employeeID"],
            ExploitationStart = (DateTime)record["exploitationStart"],
            IsDeleted = (bool)record["isDeleted"]
        };
    }

    public void Delete(uint id)
    {
        var commandStr = "UPDATE computers SET isDeleted = 1 WHERE id = @id";
        var command = new MySqlCommand(commandStr, _connection);
        command.Parameters.Add("@id", MySqlDbType.Int32).Value = id;
        command.ExecuteNonQuery();
    }

    private static void AddParameters(MySqlCommand command, Computer entity)
    {
        command.Parameters.Add("@id", MySqlDbType.UInt32).Value = entity.ID;
        command.Parameters.Add("@name", MySqlDbType.VarChar).Value = entity.Name;
        command.Parameters.Add("@registrationDate", MySqlDbType.DateTime).Value = entity.RegistrationDate;
        command.Parameters.Add("@price", MySqlDbType.Decimal).Value = entity.Price;
        command.Parameters.Add("@status", MySqlDbType.Int32).Value = (int)entity.Status;
        command.Parameters.Add("@employeeID", MySqlDbType.UInt32).Value = entity.EmployeeID;
        command.Parameters.Add("@exploitationStart", MySqlDbType.DateTime).Value = entity.ExploitationStart;
    }

    public int Count()
    {
        var commandString = "SELECT COUNT(id) FROM computers WHERE isDeleted = 0";
        var command = new MySqlCommand(commandString, _connection);
        var count = command.ExecuteScalar()?.ToString() ?? "0";
        return int.Parse(count);
    }
}
