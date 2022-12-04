using DBLibrary.Entities;
using DBLibrary.Interfaces;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

namespace DBLibrary.Repositories.EF;

public class EfComputerRep : DbContext, IComputerRepository
{
    private readonly MySqlConnection _connection;
    //как правильно называть
    private DbSet<Computer> Computers => Set<Computer>();
    
    public EfComputerRep(DbConnect context)
    {
        _connection = context.GetConnection();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySQL(_connection);
    }

    public int Count() => Computers.Count();

    public void Create(Computer entity)
    {
        Computers.Add(entity);
        SaveChanges();
    }

    public void Delete(uint id)
    {
        var computer = GetByID(id);
        if (computer != null)
        {
            computer.IsDeleted = false;
            Update(computer);
        }
    }

    public IList<Computer> Filter(int skip, int take, string? name = null, decimal price = 0, int status = 0, uint employeeID = 0)
    {
        return Computers.Where(c => (name == null || name == c.Name)
                                  && (price == 0 || price == c.Price)
                                  && (status == 0 || status == (int)c.Status)
                                  && (employeeID == 0 || employeeID == c.EmployeeID)
                                  && c.IsDeleted == false).ToList();
    }

    public Computer? GetByID(uint id) => Computers.FirstOrDefault(c => c.ID == id && c.IsDeleted == false);

    public void Update(Computer entity)
    {
        Computers.Update(entity);
        SaveChanges();
    }
}
