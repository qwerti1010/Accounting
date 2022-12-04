using DBLibrary.Entities;
using DBLibrary.Interfaces;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

namespace DBLibrary.Repositories.EF;

public class EfEmployeeRep : DbContext, IEmployeeRepository
{
    private readonly MySqlConnection _connection;
    private DbSet<Employee> Employees => Set<Employee>();

    public EfEmployeeRep(DbConnect context)
    {
        _connection = context.GetConnection();
    }
 
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySQL(_connection);
    }

    public void Create(Employee entity)
    {
        Employees.Add(entity);
        SaveChanges();
    }

    public void Delete(uint id)
    {
        var emp = GetByID(id);
        if (emp != null)
        {
            emp.IsDeleted = true;
            Update(emp);
        }        
    }

    public Employee? GetByID(uint id) => Employees.FirstOrDefault(e => e.ID == id && e.IsDeleted == false);
    
    public IList<Employee> GetEmployees(int take, int skip,
        string? name = null, string? phone = null, string? login = null)
    {        
        return Employees.Where(e => (name == null || e.Name == name)
            && (phone == null || e.Phone == phone)
            && (login == null || login == e.Login) 
            && e.IsDeleted == false).Skip(skip).Take(take).ToList() ?? new List<Employee>();
    }

    public void Update(Employee entity)
    {
        Employees.Update(entity);
        SaveChanges();
    }
}
