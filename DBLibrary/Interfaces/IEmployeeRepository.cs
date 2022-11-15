using DBLibrary.Entities;

namespace DBLibrary.Interfaces;

public interface IEmployeeRepository : IRepository<Employee>
{
    public Employee? GetByLogin(string login);   
    public List<Employee> GetEmployees(string name, string? phone = null, string? login = null);
    public List<Employee> GetAll(int take, int skip);
}