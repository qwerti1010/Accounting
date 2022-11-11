using DBLibrary.Entities;
using DBLibrary.Interfaces;
using MySqlConnector;
using System.Data;

namespace DBLibrary;

public class PropertyRep : IPropertyRepository
{
    private readonly MySqlConnection _connection;
    public PropertyRep(DbContext context)
    {
        _connection = context.GetConnection();
    }
    public void Create(Property entity)
    {
        var commandStr = "INSERT INTO properties (computerID, typeID, value)" +
            " VALUES (@computerID, @typeID, @value)";
        var command = new MySqlCommand(commandStr, _connection);
        command.Parameters.Add("@computerID", MySqlDbType.UInt32).Value = entity.ComputerID;
        command.Parameters.Add("@typeID", MySqlDbType.Int32).Value = (int)entity.TypeID;
        command.Parameters.Add("@value", MySqlDbType.VarChar).Value = entity.Value;
        command.ExecuteNonQuery();
    }

    public void Delete(uint id)
    {
        var commandStr = "UPDATE properties SET isDeleted = 1" +
            " WHERE id = @id";
        var command = new MySqlCommand(commandStr, _connection);
        command.Parameters.Add("@id", MySqlDbType.UInt32).Value = id;
        command.ExecuteNonQuery();
    }

    public List<Property> GetByComputerID(uint id)
    {
        var properties = new List<Property>();
        var commandStr = "SELECT * FROM properties" +
            " WHERE isDeleted = 0 AND computerID = @computerID";
        var command = new MySqlCommand(commandStr, _connection);
        command.Parameters.Add("@computerId", MySqlDbType.UInt32).Value = id;
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            properties.Add(Record(reader));
        }
        return properties;
    }

    private Property Record(IDataRecord record)
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

    public List<Property> GetAll()
    {
        throw new NotImplementedException();
    }

    public Property? GetById(uint id)
    {
        throw new NotImplementedException();
    }

    public void Update(Property entity)
    {
        throw new NotImplementedException();
    }
}
