using DBLibrary.Entities;

namespace DBLibrary.Interfaces;

public interface IEmployeeRepository : IRepository<Employee>
{
    public Employee? GetByLogin(string login);   
    public Employee? GetEmployee(string name, string phone, string login);
}