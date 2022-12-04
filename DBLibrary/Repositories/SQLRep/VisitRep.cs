using DBLibrary.Entities;
using DBLibrary.Interfaces;
using MySqlConnector;

namespace DBLibrary.Repositories.SQLRep;

public class VisitRep : IVisitRepository
{
    private readonly MySqlConnection _connection;

    public VisitRep(DbConnect context)
    {
        _connection = context.GetConnection();
    }

    public void Create(Visit entity)
    {
        var commandString = "INSERT INTO visit_history (employeeID, visitTime)" +
            "VALUES (@employeeID, @visitTime)";
        var command = new MySqlCommand(commandString, _connection);
        command.Parameters.Add("@employeeID", MySqlDbType.UInt32).Value = entity.EmployeeID;
        command.Parameters.Add("@visitTime", MySqlDbType.DateTime).Value = entity.VisitTime;
        command.ExecuteNonQuery();
    }

    public void Delete(uint id)
    {
        throw new NotImplementedException();
    }

    public List<Visit> GetAll(int take, int skip)
    {
        throw new NotImplementedException();
    }

    public Visit? GetByID(uint id)
    {
        throw new NotImplementedException();
    }

    public void Update(Visit entity)
    {
        throw new NotImplementedException();
    }
}
