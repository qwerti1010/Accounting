using Dapper;
using DBLibrary.Entities;
using DBLibrary.Interfaces;
using MySqlConnector;

namespace DBLibrary.Dapper;

public class DapperPropertyRep : IPropertyRepository
{
    private readonly MySqlConnection _connection;

    public DapperPropertyRep(DbContext context)
    {
        _connection = context.GetConnection();
    }

    public void Create(Property entity)
    {
        var commandString = "SELECT MAX(id) FROM computers";
        entity.ComputerID = _connection.Query<uint>(commandString).First();
        commandString = "INSERT INTO properties (computerID, typeID, value)" +
            " VALUES(@computerID, @typeID, @value)";
        _connection.Execute(commandString, entity);
    }

    public void Delete(uint id)
    {
        var commandStr = "UPDATE properties SET isDeleted = 1 WHERE computerID = @id";
        _connection.Execute(commandStr, new { id });
    }

    public Dictionary<PropType, Property> GetByComputerID(uint id)
    {
        var commandStr = "SELECT * FROM properties" +
            " WHERE isDeleted = 0 AND computerID = @computerID";
        return _connection.Query<Property>(commandStr, new {id}).ToDictionary(key => key.TypeID, prop => prop);
    }

    public Property? GetByID(uint id)
    {
        var commandStr = "SELECT * FROM properties" +
            " WHERE isDeleted = 0 AND computerID = @computerID";
        return _connection.Query<Property>(commandStr, new { id }).FirstOrDefault();
    }

    public void Update(Property entity)
    {
        var commandStr = "UPDATE properties SET value = @value WHERE id = @id";
        _connection.Execute(commandStr, entity);
    }
}
