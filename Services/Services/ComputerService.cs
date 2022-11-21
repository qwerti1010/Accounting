using DBLibrary;
using DBLibrary.Entities;

namespace Services.Services;

public class ComputerService
{
    private readonly DbContext _context;
    private readonly ComputerRep _computerRep;

    public ComputerService(DbContext context)
    {
        _context = context;
        _computerRep = new ComputerRep(_context);
    }

    public List<Computer> GetComputers(int take, int skip, string? nameFilter = null,
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
        _context.Close();
        return computers;
    }

    public Computer GetByID(uint id)
    {
        _context.Open();
        var computer = _computerRep.GetByID(id);
        _context.Close();
        return computer!;
    }

    public void Delete(uint id)
    {
        _context.Open();
        _computerRep.Delete(id);
        _context.Close();
    }

    public void Update(Computer computer)
    {
        _context.Open();
        _computerRep.Update(computer);
        _context.Close();
    }

    public void Create(Computer computer)
    {
        _context.Open();
        _computerRep.Create(computer);
        _context.Close();
    }
}
