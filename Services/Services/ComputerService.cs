using DBLibrary;
using DBLibrary.Dapper;
using DBLibrary.Entities;
using DBLibrary.Interfaces;
using DBLibrary.Repositories.EF;

namespace Services.Services;

public class ComputerService
{
    private readonly DbConnect _context;
    private readonly IComputerRepository _computerRep;
    private readonly IPropertyRepository _propertyRep;

    public ComputerService(DbConnect context)
    {
        _context = context;
        _computerRep = new EfComputerRep(_context);
        _propertyRep = new EfPropertyRep(_context);
    }

    public IList<Computer> GetComputers(int take, int skip, string? nameFilter = null,
        string? priceFilter = null, int statusFilter = 0, string? emplIDFilter = null)
    {
        if (string.IsNullOrWhiteSpace(nameFilter))
        {
            nameFilter = null;
        }
        if (statusFilter < 0)
        {
            statusFilter = 0;
        }
        decimal.TryParse(priceFilter, out decimal price);
        uint.TryParse(emplIDFilter, out uint empID);
        _context.Open();
        var computers = _computerRep.Filter(skip, take, nameFilter, price, statusFilter, empID);
        foreach (var computer in computers)
        {
            computer.Properties = _propertyRep.GetByComputerID(computer.ID);
        }
        _context.Close();
        return computers;
    }

    public Computer GetByID(uint id)
    {
        _context.Open();
        var computer = _computerRep.GetByID(id);
        computer!.Properties = _propertyRep.GetByComputerID(computer.ID);
        _context.Close();
        return computer!;
    }

    public void Delete(uint id)
    {
        _context.Open();
        _computerRep.Delete(id);
        _propertyRep.Delete(id);
        _context.Close();
    }

    public void Update(Computer computer)
    {
        _context.Open();
        _computerRep.Update(computer);
        if (computer.Properties == null)
        {
            _context.Close();
            return;
        }
        foreach (var prop in computer.Properties)
        {
            _propertyRep.Update(prop);
        }
        _context.Close();
    }

    public void Create(Computer computer)
    {
        _context.Open();
        _computerRep.Create(computer);
        if (computer.Properties == null)
        {
            _context.Close();
            return;
        }
        foreach (var prop in computer.Properties)
        {
            prop.ComputerID = computer.ID;
            _propertyRep.Create(prop);
        }
        _context.Close();
    }

    public int Count()
    {
        _context.Open();
        var result = _computerRep.Count();
        _context.Close();
        return result;
    }
}
