
using Dapper;
using DBLibrary.Entities;
using DBLibrary.Interfaces;
using MySqlConnector;

namespace DBLibrary.Dapper;

public class DapperEmpRep : IEmployeeRepository
{
    private readonly MySqlConnection _connection;

    public DapperEmpRep(DbContext context)
    {
        _connection = context.GetConnection();
    }
    public void Create(Employee entity)
    {
        var commandStr = "INSERT INTO employees (name, position, phone, login, password)" +
            " VALUES(@name, @position, @phone, @login, @password)";
        _connection.Execute(commandStr, entity);
    }

    public void Delete(uint id)
    {
        var commandStr = "UPDATE employees SET isDeleted = 1 WHERE id = @id";
        _connection.Execute(commandStr, new { id });
    }

    public Employee? GetByID(uint id)
    {
        var commandStr = "SELECT * FROM employees WHERE id = @id AND isDeleted = 0";
        return _connection.Query<Employee>(commandStr, new { id }).FirstOrDefault();
    }

    public List<Employee> GetEmployees(int take, int skip, string? name = null, string? phone = null, string? login = null)
    {
        var commandString = "SELECT * FROM employees WHERE isDeleted = 0 ";
        if (name != null)
        {
            commandString += " AND name = @name";
        }

        if (phone != null)
        {
            commandString += " AND phone = @phone";
        }

        if (login != null)
        {
            commandString += " AND login = @login";
        }

        commandString += " LIMIT @skip, @take";
        return _connection.Query<Employee>(commandString, new { name, phone, login, skip, take }).ToList();
    }

    public void Update(Employee entity)
    {
        var commandStr = "UPDATE employees SET name = @name, position = @position, phone = @phone WHERE id = @id";
        _connection.Execute(commandStr, entity);
    }
}
