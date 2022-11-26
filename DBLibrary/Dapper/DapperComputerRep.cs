

using Dapper;
using DBLibrary.Entities;
using DBLibrary.Interfaces;
using MySqlConnector;

namespace DBLibrary.Dapper;

public class DapperComputerRep : IComputerRepository
{
    private readonly MySqlConnection _connection;

    public DapperComputerRep(DbContext context)
    {
        _connection = context.GetConnection();
    }

    public void Create(Computer entity)
    {
        var commandStr = "INSERT INTO computers (name, registrationDate," +
            " price, status, employeeID, exploitationStart)" +
            " VALUES(@name, @registrationDate, @price, @status, @employeeID, @exploitationStart)";
        _connection.Execute(commandStr, entity);
    }

    public void Delete(uint id)
    {
        var commandStr = "UPDATE computers SET isDeleted = 1 WHERE id = @id";
        _connection.Execute(commandStr, new {id});
    }

    public List<Computer> Filter(int skip, int take, string? name = null, decimal price = 0, int status = 0, uint employeeID = 0)
    {
        var commandString = "SELECT * FROM computers WHERE isDeleted = 0";
        var command = new MySqlCommand(commandString, _connection);
        if (name != null)
        {
            commandString += " AND name = @name";
        }

        if (price != 0)
        {
            commandString += " AND price = @price";
        }

        if (status != 0)
        {
            commandString += " AND status = @status";
        }

        if (employeeID != 0)
        {
            commandString += " AND employeeID = @employeeID";
        }

        commandString += " LIMIT @skip, @take";
        return _connection.Query<Computer>(commandString, new { name, price, status, employeeID, skip, take }).ToList();
    }

    public Computer? GetByID(uint id)
    {
        var commandStr = "SELECT * FROM computers WHERE id = @id AND isDeleted = 0";
        return _connection.Query<Computer>(commandStr, new { id }).FirstOrDefault();
    }

    public void Update(Computer entity)
    {
        var commandStr = "UPDATE computers SET name = @name, registrationDate = @registrationDate," +
            " price = @price, status = @status, employeeID = @employeeID," +
            " exploitationStart = @exploitationStart WHERE id = @id";
        _connection.Execute(commandStr, entity);
    }
}
