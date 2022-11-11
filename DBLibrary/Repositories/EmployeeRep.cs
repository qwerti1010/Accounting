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

    public List<Employee> GetAll()
    {
        var employees = new List<Employee>();
        var commandString = "SELECT * FROM employees WHERE isDeleted = 0 LIMIT 10";
        var command = new MySqlCommand(commandString, _connection);
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {            
            employees.Add(Record(reader));           
        }                
        return employees;
    }

    public Employee? GetById(uint id)
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

    private Employee Record(IDataRecord record)
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
        var commandStr = "UPDATE employees SET isDeleted = 1 WHERE id = @id";
        var command = new MySqlCommand(commandStr, _connection);
        command.Parameters.Add("@id", MySqlDbType.Int32).Value = id;
        command.ExecuteNonQuery();        
    }

    private void AddParameters(MySqlCommand command, Employee entity)
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
    
    public Employee? GetEmployee(string name, string phone, string login)
    {
        var commandString = "SELECT * FROM employees WHERE isDeleted = 0" +
            " AND (phone = @phone OR login = @login OR name = @name) LIMIT 1";
        var command = new MySqlCommand(commandString, _connection);
        command.Parameters.Add("@name", MySqlDbType.VarChar).Value = name;
        command.Parameters.Add("@phone", MySqlDbType.VarChar).Value = phone;
        command.Parameters.Add("@login", MySqlDbType.VarChar).Value = login;
        using var reader = command.ExecuteReader();       
        while (reader.Read())
        {
            return Record(reader);
        }
        return null;
    }
}

