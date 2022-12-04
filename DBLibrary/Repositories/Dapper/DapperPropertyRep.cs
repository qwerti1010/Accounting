using Dapper;
using DBLibrary.Entities;
using DBLibrary.Interfaces;
using MySqlConnector;

namespace DBLibrary.Dapper;

public class DapperPropertyRep : IPropertyRepository
{
    private readonly MySqlConnection _connection;

    public DapperPropertyRep(DbConnect context)
    {
        _connection = context.GetConnection();
    }

    public void Create(Property entity)
    {
        var commandString = "INSERT INTO properties (computerID, typeID, value)" +
            " VALUES(@computerID, @typeID, @value)";
        var param = new DynamicParameters();
        param.Add("@computerID", entity.ComputerID);
        param.Add("@typeID", entity.TypeID);
        param.Add("@value", entity.Value);
        _connection.Execute(commandString, param);
        entity.ID = _connection.Query<uint>("SELECT LAST_INSERT_ID()").FirstOrDefault();
    }

    public void Delete(uint id)
    {
        var commandStr = "UPDATE properties SET isDeleted = 1 WHERE computerID = @id";
        _connection.Execute(commandStr, new { id });
    }

    public IList<Property> GetByComputerID(uint id)
    {
        var param = new DynamicParameters();
        var commandStr = "SELECT * FROM properties" +
            " WHERE isDeleted = 0 AND computerID = @computerID";
        param.Add("@computerID", id);
        return _connection.Query<Property>(commandStr, param).ToList();
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
        var param = new DynamicParameters();
        param.Add("@value", entity.Value);
        param.Add("@id", entity.ID);
        _connection.Execute(commandStr, param);
    }
}
