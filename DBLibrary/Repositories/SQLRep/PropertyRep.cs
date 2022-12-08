using DBLibrary.Entities;
using DBLibrary.Interfaces;
using MySqlConnector;
using System.Data;

namespace DBLibrary.Repositories.SQLRep;

public class PropertyRep : IPropertyRepository
{
    private readonly MySqlConnection _connection;

    public PropertyRep(DbConnect context)
    {
        _connection = context.GetConnection();
    }

    public void Create(Property entity)
    {
        var command = new MySqlCommand
        {
            Connection = _connection,
            CommandText = "INSERT INTO properties (computerID, typeID, value)" +
            " VALUES(@computerID, @typeID, @value)"
        };
        command.Parameters.Add("@value", MySqlDbType.VarChar).Value = entity.Value;
        command.Parameters.Add("@computerID", MySqlDbType.UInt32).Value = entity.ComputerID;
        command.Parameters.Add("@typeID", MySqlDbType.Int32).Value = entity.TypeID;
        command.ExecuteNonQuery();
        command.CommandText = "SELECT LAST_INSERT_ID()";
        entity.ID = uint.Parse(command.ExecuteScalar()!.ToString()!);
    }

    public void Delete(uint id)
    {
        var commandStr = "UPDATE properties SET isDeleted = 1 WHERE computerID = @id";
        var command = new MySqlCommand(commandStr, _connection);
        command.Parameters.Add("@id", MySqlDbType.Int32).Value = id;
        command.ExecuteNonQuery();
    }

    public Property? GetByID(uint id)
    {
        var commandStr = "SELECT * FROM properties" +
            " WHERE isDeleted = 0 AND computerID = @computerID";
        var command = new MySqlCommand(commandStr, _connection);
        command.Parameters.Add("@computerId", MySqlDbType.UInt32).Value = id;
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            return PropRecord(reader);
        }
        return null;
    }

    public void Update(Property entity)
    {
        var commandStr = "UPDATE properties SET value = @value WHERE id = @id";
        var command = new MySqlCommand(commandStr, _connection);
        command.Parameters.Add("@value", MySqlDbType.VarChar).Value = entity.Value;
        command.Parameters.Add("@id", MySqlDbType.UInt32).Value = entity.ID;
        command.ExecuteNonQuery();
    }

    public IList<Property> GetByComputerID(uint id)
    {
        var properties = new List<Property>();
        var commandStr = "SELECT * FROM properties" +
            " WHERE isDeleted = 0 AND computerID = @computerID";
        var command = new MySqlCommand(commandStr, _connection);
        command.Parameters.Add("@computerId", MySqlDbType.UInt32).Value = id;
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            properties.Add(PropRecord(reader));
        }
        return properties;
    }

    public void Update(Dictionary<PropType, Property> properties)
    {
        var commandStr = "UPDATE properties SET value = @value WHERE computerID = @computerID AND typeID = @typeID";
        var command = new MySqlCommand(commandStr, _connection);
        foreach (var key in properties.Keys)
        {
            command.Parameters.Add("@value", MySqlDbType.VarChar).Value = properties[key].Value;
            command.Parameters.Add("@computerID", MySqlDbType.UInt32).Value = properties[key].ComputerID;
            command.Parameters.Add("@typeID", MySqlDbType.Int32).Value = (int)key;
            command.ExecuteNonQuery();
            command.Parameters.Clear();
        }
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

    public int Count()
    {
        var commandString = "SELECT COUNT(id) FROM properties WHERE isDeleted = 0";
        var command = new MySqlCommand(commandString, _connection);
        var count = command.ExecuteScalar()?.ToString() ?? "0";
        return int.Parse(count);
    }
}
