using DBLibrary.Entities;
using DBLibrary.Interfaces;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

namespace DBLibrary.Repositories.EF;

public class EfPropertyRep : DbContext, IPropertyRepository
{
    private readonly MySqlConnection _connection;
    private DbSet<Property> Properties => Set<Property>();

    public EfPropertyRep(DbConnect context)
    {
        _connection = context.GetConnection();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySQL(_connection);
    }

    public void Create(Property entity)
    {
        Properties.Add(entity);
        SaveChanges();
    }

    public void Delete(uint id)
    {
        var prop = GetByID(id);
        if (prop != null)
        {
            prop.IsDeleted = true;
            Update(prop);
            SaveChanges();
        }
    }

    public IList<Property> GetByComputerID(uint id)
    {
        return Properties.Where(p => p.ComputerID == id && !p.IsDeleted).ToList() ?? new List<Property>(); 
    }

    public Property? GetByID(uint id) => Properties.FirstOrDefault(p => p.ID == id && !p.IsDeleted);

    public void Update(Property entity)
    {
        Properties.Update(entity);
        SaveChanges();
    }

    public int Count() => Properties.Where(p => !p.IsDeleted).Count();
}
