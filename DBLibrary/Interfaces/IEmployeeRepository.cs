using DBLibrary.Entities;

namespace DBLibrary.Interfaces;

public interface IEmployeeRepository : IRepository<Employee>
{
    public List<Employee> GetEmployees(int take, int skip, 
        string? name = null, string? phone = null, string? login = null);
}