using MySqlConnector;
using System.Data;

namespace DBLibrary;

public class ComputerRep : IRepository<Computer>
{
    private readonly MySqlConnection _connection;

    public ComputerRep(DbContext context)
    {
        _connection = context.GetConnection();
    }

    public List<Computer> GetAll()
    {
        var computers = new List<Computer>();
        var commandString = "SELECT * FROM computers WHERE isDeleted = 0";        
        var command = new MySqlCommand(commandString, _connection);
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            computers.Add(Record(reader));
        }                
        return computers;
    }

    public Computer GetById(uint id)
    {
        var computer = new Computer(); 
        var commandStr = "SELECT * FROM computers WHERE computerid = @id AND isDeleted = @id";
        var command = new MySqlCommand(commandStr, _connection);
        command.Parameters.Add("@id", MySqlDbType.Int32).Value = id;
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            computer = Record(reader);
        }       
        return computer;
    }

    public void Create(Computer entity)
    {
        var commandStr = "INSERT INTO computers (name, registerNumber, registrationDate," +
            " price, producer, cpu, numberOfCores, ram, graphicsCard, status, employee," +
            " location, bodySize, explutationStart, amortisationPeriod, memory)" +
            " VALUES(@name, @registerNumber, @registrationDate, @price, @producer, @cpu," +
            " @numberOfCores, @ram, @graphicsCard, @status, @employee, @location, @bodySize," +
            " @explutationStart, @amortisationPeriod, @memory)";

        var command = new MySqlCommand(commandStr, _connection);        
        AddParameters(command, entity);
        command.ExecuteNonQuery();        
    }

    public void Update(Computer entity)
    {
        var commandStr = "UPDATE computers SET name = @name, registerNumber = @registerNumber," +
            " registrationDate = @registrationDate, price = @price, producer = @producer," +
            " cpu = @cpu, numberOfCores = @numberOfCores, ram = @ram, graphicsCard = @graphicsCard," +
            " status = @status, employee = @employee, location = @location, bodySize = @bodySize," +
            " explutationStart = @explutationStart, amortisationPeriod = @amortisationPeriod, memory = @memory" +
            " WHERE computerid = @id";

        var command = new MySqlCommand(commandStr, _connection);        
        AddParameters(command, entity);
        command.ExecuteNonQuery();        
    }

    public Computer Record(IDataRecord record)
    {
        return new Computer
        {
            ID = (uint)record.GetInt32(0),
            Name = record.GetString(1),
            RegNumber = record.GetString(2),
            RegDate = record.GetDateTime(3),            
            Price = record.GetDecimal(4),
            Producer = record.GetString(5),
            Processor = record.GetString(6),
            CoresCount = record.GetInt32(7),
            RAM = record.GetInt32(8),
            GraphicsCard = record.GetString(9),
            Status = record.GetString(10),
            Employee = record.GetString(11),
            Location = record.GetString(12),
            BodySize = record.GetDouble(13),
            ExplDate = record.GetDateTime(14),
            AmortPeriod = record.GetInt32(15),
            Memory = record.GetDouble(16),
            IsDeleted = record.GetInt32(17)
        };
    }

    public void Delete(uint id)
    {        
        var commandStr = "UPDATE computers SET isDeleted = 1 WHERE computerid = @id";
        var command = new MySqlCommand(commandStr, _connection);
        command.Parameters.Add("@id", MySqlDbType.Int32).Value = id;
        command.ExecuteNonQuery();
    }

    private void AddParameters(MySqlCommand command, Computer entity)
    {
        command.Parameters.Add("@id", MySqlDbType.UInt32).Value = entity.ID;
        command.Parameters.Add("@name", MySqlDbType.VarChar).Value = entity.Name;
        command.Parameters.Add("@registerNumber", MySqlDbType.VarChar).Value = entity.RegNumber;
        command.Parameters.Add("@registrationDate", MySqlDbType.DateTime).Value = entity.RegDate;
        command.Parameters.Add("@price", MySqlDbType.Decimal).Value = entity.Price;
        command.Parameters.Add("@producer", MySqlDbType.VarChar).Value = entity.Producer;
        command.Parameters.Add("@cpu", MySqlDbType.VarChar).Value = entity.Processor;
        command.Parameters.Add("@numberOfCores", MySqlDbType.Int32).Value = entity.CoresCount;
        command.Parameters.Add("@ram", MySqlDbType.Int32).Value = entity.RAM;
        command.Parameters.Add("@graphicsCard", MySqlDbType.VarChar).Value = entity.GraphicsCard;
        command.Parameters.Add("@status", MySqlDbType.VarChar).Value = entity.Status;
        command.Parameters.Add("@employee", MySqlDbType.VarChar).Value = entity.Employee;
        command.Parameters.Add("@location", MySqlDbType.VarChar).Value = entity.Location;
        command.Parameters.Add("@bodySize", MySqlDbType.Double).Value = entity.BodySize;
        command.Parameters.Add("@explutationStart", MySqlDbType.DateTime).Value = entity.ExplDate;
        command.Parameters.Add("@amortisationPeriod", MySqlDbType.Int32).Value = entity.AmortPeriod;
        command.Parameters.Add("@memory", MySqlDbType.Double).Value = entity.Memory;
    }
}
