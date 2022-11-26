

using Dapper;
using DBLibrary.Entities;
using DBLibrary.Interfaces;
using MySqlConnector;

namespace DBLibrary.Dapper;

public class DapperVisitRep : IVisitRepository
{
    private readonly MySqlConnection _connection;

    public DapperVisitRep(DbContext context)
    {
        _connection = context.GetConnection();
    }

    public void Create(Visit entity)
    {
        var commandString = "INSERT INTO visit_history (employeeID, visitTime)" +
            "VALUES (@employeeID, @visitTime)";
        _connection.Execute(commandString, entity);
    }

    public void Delete(uint id)
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
