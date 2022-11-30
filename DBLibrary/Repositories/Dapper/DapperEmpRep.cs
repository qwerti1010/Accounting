
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
        var param = new DynamicParameters();
        param.Add("@name", entity.Name);
        param.Add("@position", entity.Position);
        param.Add("@phone", entity.Phone);
        param.Add("@login", entity.Login);
        param.Add("@password", entity.Password);
        _connection.Execute(commandStr, param);
        entity.ID = _connection.Query<uint>("SELECT LAST_INSERT_ID()").FirstOrDefault();
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

    public IList<Employee> GetEmployees(int take, int skip, string? name = null, string? phone = null, string? login = null)
    {
        var commandString = "SELECT * FROM employees WHERE isDeleted = 0";        
        var param = new DynamicParameters();
        if (name != null)
        {
            commandString += " AND name = @name";
            param.Add("name", name);
        }
        
        if (phone != null)
        {
            commandString += " AND phone = @phone";
            param.Add("@phone", phone);
        }

        if (login != null)
        {
            commandString += " AND login = @login";
            param.Add("@login", login);
        }

        commandString += " LIMIT @skip, @take";
        param.Add("@skip", skip);
        param.Add("@take", take);
        return _connection.Query<Employee>(commandString, param).ToList();
    }

    public void Update(Employee entity)
    {
        var commandStr = "UPDATE employees SET name = @name, position = @position, phone = @phone, password = @password WHERE id = @id";
        var param = new DynamicParameters();
        param.Add("@name", entity.Name);
        param.Add("@position", entity.Position);
        param.Add("@phone", entity.Phone);
        param.Add("@id", entity.ID);
        param.Add("@password", entity.Password);
        _connection.Execute(commandStr, param);
    }
}
