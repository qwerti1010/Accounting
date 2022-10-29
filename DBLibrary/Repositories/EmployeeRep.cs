using MySqlConnector;
using System.Data;

namespace DBLibrary;

public class EmployeeRep : IRepository<Employee>
{
    private readonly MySqlConnection _connection;

    public EmployeeRep(DbContext context)
    {
        _connection = context.GetConnection();
    }

    public List<Employee> GetAll()
    {
        var employees = new List<Employee>();
        var commandString = "SELECT * FROM employees WHERE isDeleted = 0";
        var command = new MySqlCommand(commandString, _connection);
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            var employee = Record(reader);
            employees.Add(employee);           
        }                
        return employees;
    }

    public Employee GetById(uint id)
    {
        var employee = new Employee();        
        var commandStr = "SELECT * FROM employees WHERE id = @id AND isDeleted = 0";
        var command = new MySqlCommand(commandStr, _connection);
        command.Parameters.Add("@id", MySqlDbType.Int32).Value = id;
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            employee = Record(reader);
        }        
        return employee;
    }

    public void Create(Employee entity)
    {
        var commandStr = "INSERT INTO employees (name, position, phone) VALUES(@name, @position, @phone)";
        var command = new MySqlCommand(commandStr, _connection);        
        AddParameters(command, entity);
        command.ExecuteNonQuery();
    }

    public void Update(Employee entity)
    {       
        var commandStr = "UPDATE employees SET name = @name, position = @position, phone = @phone WHERE employeeid = @id";
        var command = new MySqlCommand(commandStr, _connection);        
        AddParameters(command, entity);
        command.ExecuteNonQuery();        
    }   

    public Employee Record(IDataRecord record)
    {
        var log = record["login"];
        var pass = record["password"];
        return new Employee
        {
            ID = (uint)record.GetInt32(0),
            Name = (string)record["name"],
            Phone = (string)record["phone"],
            Position = (string)record["position"],
            Login = log == DBNull.Value ? String.Empty : (string)log,
            Password = pass == DBNull.Value ? String.Empty : (string)pass,
            IsDeleted = (int)record["isDeleted"]
        };
    }

    public void Delete(uint id)
    {        
        var commandStr = "UPDATE employees SET isDeleted = 1 WHERE employeeid = @id";
        var command = new MySqlCommand(commandStr, _connection);
        command.Parameters.Add("@id", MySqlDbType.Int32).Value = id;
        command.ExecuteNonQuery();        
    }

    private void AddParameters(MySqlCommand command, Employee entity)
    {
        command.Parameters.Add("@id", MySqlDbType.Int32).Value = entity.ID;
        command.Parameters.Add("@name", MySqlDbType.VarChar).Value = entity.Name;
        command.Parameters.Add("@position", MySqlDbType.VarChar).Value = entity.Position;
        command.Parameters.Add("@phone", MySqlDbType.VarChar).Value = entity.Phone;
    }

    public Employee GetByLogin(string login)
    {
        var employee = new Employee();         
        var commandString = "SELECT * FROM employees WHERE login = @login";
        var command = new MySqlCommand(commandString, _connection);
        command.Parameters.Add("@login", MySqlDbType.VarChar).Value = login;
        var reader = command.ExecuteReader();
        while (reader.Read())
        {
            employee = Record(reader);
        }
        reader.Close();        
        return employee;
    }
 }

