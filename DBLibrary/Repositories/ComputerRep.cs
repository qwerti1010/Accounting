using DBLibrary.Entities;
using DBLibrary.Interfaces;
using MySqlConnector;
using System.Data;

namespace DBLibrary;

public class ComputerRep : IComputerRepository
{
    private readonly MySqlConnection _connection;

    public ComputerRep(DbContext context)
    {
        _connection = context.GetConnection();
    }

    public List<Computer> GetAll(int take, int skip)
    {
        var computers = new List<Computer>();
        var commandString = "SELECT * FROM computers WHERE isDeleted = 0 LIMIT 10";          
        var command = new MySqlCommand(commandString, _connection);
        var reader = command.ExecuteReader();
        while (reader.Read())
        {
            computers.Add(Record(reader));
        }
        reader.Close();
        foreach (var computer in computers)
        {
            computer.Properties = GetByComputerID(computer.ID);
        }
        return computers;
    }

    public List<Computer> Filter(string? name = null, decimal price = 0, int status = 0)
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
        var reader = command.ExecuteReader();
        while (reader.Read())
        {
            computers.Add(Record(reader));
        }
        reader.Close();
        foreach(var computer in computers)
        {
            computer.Properties = GetByComputerID(computer.ID);
        }
        return computers;
    }

    public Computer? GetByID(uint id)
    {
        Computer? computer = null;
        var commandStr = "SELECT * FROM computers WHERE id = @id AND isDeleted = 0";
        var command = new MySqlCommand(commandStr, _connection);
        command.Parameters.Add("@id", MySqlDbType.Int32).Value = id;
        var reader = command.ExecuteReader();        
        while (reader.Read())
        {       
            computer = Record(reader);            
        }
        reader.Close();
        if (computer != null)
        {
            computer.Properties = GetByComputerID(id);
        }       
        return computer;
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
            " price = @price, status = @status employeeID = @employee," +
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
            RegDate = (DateTime)record["registrationDate"],
            Price = (decimal)record["price"],          
            Status = (Status)record["status"],
            EmployeeID = (uint)record["employeeID"],
            ExplDate = (DateTime)record["exploitationStart"],
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

    private static void  AddParameters(MySqlCommand command, Computer entity)
    {
        command.Parameters.Add("@id", MySqlDbType.UInt32).Value = entity.ID;
        command.Parameters.Add("@name", MySqlDbType.VarChar).Value = entity.Name;
        command.Parameters.Add("@registrationDate", MySqlDbType.DateTime).Value = entity.RegDate;
        command.Parameters.Add("@price", MySqlDbType.Decimal).Value = entity.Price;
        command.Parameters.Add("@status", MySqlDbType.Int32).Value = (int)entity.Status;
        command.Parameters.Add("@employeeID", MySqlDbType.UInt32).Value = entity.EmployeeID;
        command.Parameters.Add("@exploitationStart", MySqlDbType.DateTime).Value = entity.ExplDate;
    }

    public Dictionary<PropType, Property> GetByComputerID(uint id)
    {
        var properties = new Dictionary<PropType, Property>();
        var commandStr = "SELECT * FROM properties" +
            " WHERE isDeleted = 0 AND computerID = @computerID";
        var command = new MySqlCommand(commandStr, _connection);
        command.Parameters.Add("@computerId", MySqlDbType.UInt32).Value = id;
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            var property = PropRecord(reader);
            properties[property.TypeID] = property;
        }
        return properties;
    }

    private static Property PropRecord(IDataRecord record)
    {
        return new Property
        {
            ID = (uint)record["ID"],
            ComputerID = (uint)record["computerID"],
            IsDeleted = (bool)record["isDeleted"],
            TypeID = (PropType)record["typeID"],
            Value = (string)record["value"]
        };
    }
}
