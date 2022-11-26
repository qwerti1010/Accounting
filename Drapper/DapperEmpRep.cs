using DBLibrary.Entities;
using DBLibrary.Interfaces;

namespace Drapper;

public class DapperEmpRep : IEmployeeRepository
{
    public void Create(Employee entity)
    {
        
    }

    public void Delete(uint id)
    {
        throw new NotImplementedException();
    }

    public Employee? GetByID(uint id)
    {
        throw new NotImplementedException();
    }

    public List<Employee> GetEmployees(int take, int skip, string? name = null, string? phone = null, string? login = null)
    {
        throw new NotImplementedException();
    }

    public void Update(Employee entity)
    {
        throw new NotImplementedException();
    }
}