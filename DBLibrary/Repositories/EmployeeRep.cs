using DBLibrary.Entities;
using DBLibrary.Interfaces;
using MySqlConnector;
using System.Data;

namespace DBLibrary;

public class EmployeeRep : IEmployeeRepository
{
    private readonly MySqlConnection _connection;

    public EmployeeRep(DbContext context)
    {
        _connection = context.GetConnection();
    }

    public List<Employee> GetAll(int take, int skip)
    {
        var employees = new List<Employee>();
        var commandString = "SELECT * FROM employees WHERE isDeleted = 0 LIMIT @skip, @take";
        var command = new MySqlCommand(commandString, _connection);
        command.Parameters.Add("@skip", MySqlDbType.Int32).Value = skip;
        command.Parameters.Add("@take", MySqlDbType.Int32).Value = take;
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {            
            employees.Add(Record(reader));           
        }                
        return employees;
    }

    public Employee? GetByID(uint id)
    {               
        var commandStr = "SELECT * FROM employees WHERE id = @id AND isDeleted = 0";
        var command = new MySqlCommand(commandStr, _connection);
        command.Parameters.Add("@id", MySqlDbType.Int32).Value = id;
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            return Record(reader);
        }        
        return null;
    }

    public void Create(Employee entity)
    {
        var commandStr = "INSERT INTO employees (name, position, phone, login, password)" +
            " VALUES(@name, @position, @phone, @login, @password)";
        var command = new MySqlCommand(commandStr, _connection);        
        AddParameters(command, entity);
        command.ExecuteNonQuery();
    }

    public void Update(Employee entity)
    {       
        var commandStr = "UPDATE employees SET name = @name, position = @position, phone = @phone WHERE id = @id";
        var command = new MySqlCommand(commandStr, _connection);
        AddParameters(command, entity);
        command.ExecuteNonQuery();        
    }   

    private static Employee Record(IDataRecord record)
    {
        var log = record["login"];
        var pass = record["password"];
        return new Employee
        {
            ID = (uint)record.GetInt32(0),
            Name = (string)record["name"],
            Phone = (string)record["phone"],
            Position = (PositionEnum)record["position"],
            Login = log == DBNull.Value ? String.Empty : (string)log,
            Password = pass == DBNull.Value ? String.Empty : (string)pass,
            IsDeleted = (bool)record["isDeleted"]
        };
    }

    public void Delete(uint id)
    {        
        var commandStr = "UPDATE employees, computers SET employees.isDeleted = 1," +
            " computers.isDeleted = 1 WHERE employees.id = @id AND computers.employeeID = @id";
        var command = new MySqlCommand(commandStr, _connection);
        command.Parameters.Add("@id", MySqlDbType.Int32).Value = id;
        command.ExecuteNonQuery();        
    }

    private static void AddParameters(MySqlCommand command, Employee entity)
    {
        command.Parameters.Add("@id", MySqlDbType.Int32).Value = entity.ID;
        command.Parameters.Add("@name", MySqlDbType.VarChar).Value = entity.Name;
        command.Parameters.Add("@position", MySqlDbType.Int32).Value = (int)entity.Position;
        command.Parameters.Add("@phone", MySqlDbType.VarChar).Value = entity.Phone;
        command.Parameters.Add("@login", MySqlDbType.VarChar).Value = entity.Login;
        command.Parameters.Add("@password", MySqlDbType.VarChar).Value = entity.Password;
    }

    public Employee? GetByLogin(string login)
    {                 
        var commandString = "SELECT * FROM employees WHERE isDeleted = 0 AND login = @login";
        var command = new MySqlCommand(commandString, _connection);
        command.Parameters.Add("@login", MySqlDbType.VarChar).Value = login;
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            return Record(reader);
        }        
        return null;
    }
    
    public List<Employee> GetEmployees(string name, string phone, string? login = null)
    {
        var employees = new List<Employee>();
        var commandString = "SELECT * FROM employees WHERE isDeleted = 0" +
            " AND (phone = @phone  OR name = @name ";
        var command = new MySqlCommand(commandString, _connection);
        command.Parameters.Add("@name", MySqlDbType.VarChar).Value = name;
        command.Parameters.Add("@phone", MySqlDbType.VarChar).Value = phone;
        if (login != null)
        {
            command.CommandText += "OR login = @login";
            command.Parameters.Add("@login", MySqlDbType.VarChar).Value = login;
        }
        command.CommandText += ") LIMIT 2";
        using var reader = command.ExecuteReader();       
        while (reader.Read())
        {
            employees.Add(Record(reader));
        }
        return employees;
    }
}

