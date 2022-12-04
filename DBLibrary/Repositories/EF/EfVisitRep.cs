
using DBLibrary.Entities;
using DBLibrary.Interfaces;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

namespace DBLibrary.Repositories.EF;

public class EfVisitRep : DbContext, IVisitRepository
{
    private readonly MySqlConnection _connection;
    private DbSet<Visit> Visits => Set<Visit>();   

    public EfVisitRep(DbConnect context)
    {
        _connection = context.GetConnection();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySQL(_connection);
    }

    public void Create(Visit entity)
    {
        Visits.Add(entity);
        SaveChanges();
    }

    public void Delete(uint id)
    {
        var visit = GetByID(id);
        if (visit != null)
        {
            visit.IsDeleted = true;
            Update(visit);
        }        
    }

    public Visit? GetByID(uint id) => Visits.FirstOrDefault(v => v.ID == id && v.IsDeleted == false);

    public void Update(Visit entity)
    {
        Visits.Update(entity);
        SaveChanges();
    }
}
