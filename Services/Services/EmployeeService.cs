using DBLibrary.Interfaces;
using DBLibrary;
using DBLibrary.Entities;
using System.Text.RegularExpressions;
using Services.Responses;
using DBLibrary.Repositories.EF;
using System.Security.Cryptography;
using System.Text;

namespace Services.Services;

public class EmployeeService
{
    private readonly IEmployeeRepository _employeeRep;
    private readonly IComputerRepository _computerRep;
    private readonly IVisitRepository _visitRep;
    private readonly IPropertyRepository _propRep;
    private readonly DbConnect _context;

    public EmployeeService(DbConnect context)
    {
        _context = context;
        _employeeRep = new EfEmployeeRep(_context);
        _visitRep = new EfVisitRep(_context);
        _computerRep = new EfComputerRep(_context);
        _propRep = new EfPropertyRep(_context);
    }
    #region Работа с db
    public EmployeeResponse Login(string login, string password)
    {
        password = HashPassword(password);
        _context.Open();
        var employee = _employeeRep.GetEmployees(1, 0, null, null, login).FirstOrDefault();
        _context.Close();
        if (employee == null)
        {
            return new EmployeeResponse
            {
                IsSuccess = false,
                Message = "Пользователя с таким логином не существует"
            };
        }
        if (employee.Password != password && employee.Password != null)
        {
            return new EmployeeResponse
            {
                IsSuccess = false,
                Message = "Неверный пароль"
            };
        }       
        _context.Open();
        _visitRep.Create(new Visit { EmployeeID = employee.ID, VisitTime = DateTime.UtcNow });
        _context.Close();
        return new EmployeeResponse
        {
            Employee = employee,
            IsSuccess = true
        };
    }

    public EmployeeResponse Registration(Employee employee)
    {
        if (string.IsNullOrWhiteSpace(employee.Phone)
            || string.IsNullOrWhiteSpace(employee.Name)
            || string.IsNullOrWhiteSpace(employee.Login))
        {
            return new EmployeeResponse
            {
                IsSuccess = false,
                Message = "Форма не заполнена"
            };
        }

        if (!new Regex(@"^[+]79\d{9}$").IsMatch(employee.Phone))
        {
            return new EmployeeResponse
            {
                IsSuccess = false,
                Message = "Неверный формат номера телефона"
            };
        }

        if (employee.Password != null)
        {
            employee.Password = HashPassword(employee.Password);
        }

        _context.Open();
        var filtered = _employeeRep.GetEmployees(2, 0, employee.Name).ToList();
        filtered.AddRange(_employeeRep.GetEmployees(2, 0, phone: employee.Phone));
        filtered.AddRange(_employeeRep.GetEmployees(2, 0, login: employee.Login));

        if (filtered.Count != 0)
        {
            _context.Close();
            return new EmployeeResponse
            {
                IsSuccess = false,
                Message = "Такие данные уже существуют"
            };
        }
        _employeeRep.Create(employee);
        _context.Close();

        return new EmployeeResponse
        {
            IsSuccess = true,
            Message = "Регистрация успешно завершена",
            Employee = employee
        };
    }    

    public IList<Employee> GetEmployees(int take, int skip)
    {
        _context.Open();
        var dt = _employeeRep.GetEmployees(take, skip);
        _context.Close();
        return dt;
    }

    public Employee GetByID(uint id)
    {
        _context.Open();
        var employee = _employeeRep.GetByID(id)!;
        _context.Close();
        return employee;
    }

    public EmployeeResponse Delete(uint id)
    {
        _context.Open();                
        var result = _employeeRep.GetByID(id);
       
        
        if (result == null)
        {
            _context.Close();
            return new EmployeeResponse
            {
                Message = $"Пользователь с id:{id} не найден"
            };            
        }

        _employeeRep.Delete(id);
        var computers = _computerRep.GetByEmpID(id);
        foreach (var computer in computers)
        {
            _computerRep.Delete(computer.ID);
            _propRep.Delete(computer.ID);
        }
        _context.Close();
        return new EmployeeResponse
        {
            IsSuccess = true,
            Message = $"Пользователь с id:{id} успешно удален"
        };        
    }

    public EmployeeResponse Update(Employee employee)
    {
        if (string.IsNullOrWhiteSpace(employee.Phone)
            || string.IsNullOrWhiteSpace(employee.Name) 
            || string.IsNullOrWhiteSpace(employee.Login))
        {
            return new EmployeeResponse
            {
                IsSuccess = false,
                Message = "Поля не могут быть пустыми"
            };
        }

        if (!new Regex(@"^[+]79\d{9}$").IsMatch(employee.Phone))
        {
            return new EmployeeResponse
            {
                IsSuccess = false,
                Message = "Неверный формат номера телефона"                
            };
        }
        _context.Open();
        var filters = _employeeRep.GetEmployees(2, 0, employee.Name).ToList();
        filters.AddRange( _employeeRep.GetEmployees(2, 0, phone: employee.Phone));
        filters.AddRange( _employeeRep.GetEmployees(2, 0, login: employee.Login));

        if (filters.Any(e => e.ID != employee.ID))
        {
            _context.Close();
            return new EmployeeResponse
            {
                IsSuccess = false,
                Message = "Такие данные уже есть в базе"
            };
        }

        _employeeRep.Update(employee);
        _context.Close();
        return new EmployeeResponse
        {
            IsSuccess = true,
            Message = "Данные успешно обновлены"
        };
    }

    public int Count()
    {
        _context.Open();
        var result = _employeeRep.Count();
        _context.Close();
        return result;
    }
    #endregion
    public static string HashPassword(string password)
    {
        var md = MD5.Create();
        var bytes = Encoding.ASCII.GetBytes(password);
        var hash = md.ComputeHash(bytes);
        var sb = new StringBuilder();
        foreach (var item in hash)
        {
            sb.Append(item.ToString("X2"));
        }
        return sb.ToString();
    }
}