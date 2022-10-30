
namespace DBLibrary;

public interface IEmployeeRepository : IRepository<Employee>
{
    public Employee? GetByLogin(string login);
}