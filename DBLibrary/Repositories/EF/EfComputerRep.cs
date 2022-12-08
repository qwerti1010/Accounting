using DBLibrary.Entities;
using DBLibrary.Interfaces;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

namespace DBLibrary.Repositories.EF;

public class EfComputerRep : DbContext, IComputerRepository
{
    private readonly MySqlConnection _connection;
    private DbSet<Computer> Computers => Set<Computer>();
    
    public EfComputerRep(DbConnect context)
    {
        _connection = context.GetConnection();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySQL(_connection);
    }

    public int Count() => Computers.Where(c => !c.IsDeleted).Count();

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
            computer.IsDeleted = true;
            Update(computer);
            SaveChanges();
        }
    }

    public IList<Computer> Filter(int skip, int take, string? name = null, decimal price = 0, int status = 0, uint employeeID = 0)
    {
        
        return Computers.Where(c => (c.Name == name || name == null)
                                  && (c.Price == price  || price == 0)
                                  && ((int)c.Status == status || status == 0)
                                  && (c.EmployeeID == employeeID || employeeID == 0)
                                  && !c.IsDeleted).Skip(skip).Take(take).ToList();
    }

    public Computer? GetByID(uint id) => Computers.FirstOrDefault(c => c.ID == id && !c.IsDeleted);

    public void Update(Computer entity)
    {
        Computers.Update(entity);
        SaveChanges();
    }
}
