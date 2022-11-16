using DBLibrary.Interfaces;
using DBLibrary;
using DBLibrary.Entities;

namespace Services;

public class LoginService
{
    private readonly IEmployeeRepository _employeeRep;
    private readonly IVisitRepository _visitRep;
    private readonly DbContext _context;
    public Employee? Employee { get; private set; }
    public LoginService()
    {
        _context = new DbContext();
        _employeeRep = new EmployeeRep(_context);
        _visitRep = new VisitRep(_context);
    }

    public LoginService(DbContext context)
    {
        _context = context;
        _employeeRep = new EmployeeRep(_context);
        _visitRep = new VisitRep(_context);
    }

    public bool IsEmployeeExist(string login)
    {
        _context.Open();
        Employee = _employeeRep.GetEmployees(1, 0, null, null, login).FirstOrDefault();
        _context.Close();            
        return Employee != null;
    }

    public bool IsPasswordValid(string password) => Employee!.Password == password;

    public void AddVisit()
    {
        _context.Open();
        _visitRep.Create(new Visit { EmployeeID = Employee!.ID, VisitTime = DateTime.UtcNow});
        _context.Close();
    }
}