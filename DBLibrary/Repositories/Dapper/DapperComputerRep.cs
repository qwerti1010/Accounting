using Dapper;
using DBLibrary.Entities;
using DBLibrary.Interfaces;
using MySqlConnector;

namespace DBLibrary.Dapper;

public class DapperComputerRep : IComputerRepository
{
    private readonly MySqlConnection _connection;

    public DapperComputerRep(DbConnect context)
    {
        _connection = context.GetConnection();
    }

    public int Count()
    {        
        return _connection.ExecuteScalar<int>("SELECT COUNT(id) FROM computers WHERE isDeleted = 0");
    }

    public void Create(Computer entity)
    {
        var commandStr = "INSERT INTO computers (name, registrationDate," +
            " price, status, employeeID, exploitationStart)" +
            " VALUES(@name, @registrationDate, @price, @status, @employeeID, @exploitationStart)";
        var param = new DynamicParameters();
        param.Add("@name", entity.Name);
        param.Add("@registrationDate", entity.RegistrationDate);
        param.Add("@price", entity.Price);
        param.Add("@status", entity.Status);
        param.Add("@employeeID", entity.EmployeeID);
        param.Add("@exploitationStart", entity.ExploitationStart);
        _connection.Execute(commandStr, param);
        entity.ID = _connection.Query<uint>("SELECT LAST_INSERT_ID()").FirstOrDefault();
    }

    public void Delete(uint id)
    {
        var commandStr = "UPDATE computers SET isDeleted = 1 WHERE id = @id";
        _connection.Execute(commandStr, new {id});
    }

    public IList<Computer> Filter(int skip, int take, string? name = null, decimal price = 0, int status = 0, uint employeeID = 0)
    {
        var commandString = "SELECT * FROM computers WHERE isDeleted = 0";
        var param = new DynamicParameters();
        if (name != null)
        {
            commandString += " AND name = @name";
            param.Add("@name", name);
        }

        if (price != 0)
        {
            commandString += " AND price = @price";
            param.Add("@price", price);
        }

        if (status != 0)
        {
            commandString += " AND status = @status";
            param.Add("@status", status);
        }

        if (employeeID != 0)
        {
            commandString += " AND employeeID = @employeeID";
            param.Add("@employeeID", employeeID);
        }
        commandString += " LIMIT @skip, @take";
        param.Add("@skip", skip);
        param.Add("@take", take);
        var x = _connection.Query<Computer>(commandString, param).ToList();
        return x;
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
        var param = new DynamicParameters();
        param.Add("@name", entity.Name);
        param.Add("@registrationDate", entity.RegistrationDate);
        param.Add("@price", entity.Price);
        param.Add("@status", entity.Status);
        param.Add("@employeeID", entity.EmployeeID);
        param.Add("@exploitationStart", entity.ExploitationStart);
        param.Add("@id", entity.ID);
        _connection.Execute(commandStr, param);
    }
}
