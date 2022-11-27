using DBLibrary.Interfaces;
using DBLibrary;
using DBLibrary.Entities;
using System.Text.RegularExpressions;
using Services.Responses;
using DBLibrary.Dapper;

namespace Services.Services;

public class EmployeeService
{
    private readonly IEmployeeRepository _employeeRep;
    private readonly IVisitRepository _visitRep;
    private readonly DbContext _context;

    public EmployeeService()
    {
        _context = new DbContext();
        _employeeRep = new DapperEmpRep(_context);
        _visitRep = new DapperVisitRep(_context);
    }

    public EmployeeService(DbContext context)
    {
        _context = context;
        _employeeRep = new DapperEmpRep(_context);
        _visitRep = new DapperVisitRep(_context);
    }

    public EmployeeResponse Login(string login, string password)
    {
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
        if (employee.Password != password)
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

    public EmployeeResponse Registration(Employee employee, string confirmation)
    {
        if (string.IsNullOrWhiteSpace(employee.Phone)
            || string.IsNullOrWhiteSpace(employee.Name)
            || employee.Position == PositionEnum.None
            || string.IsNullOrWhiteSpace(employee.Login)
            || string.IsNullOrWhiteSpace(employee.Password))
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

        if (employee.Password != confirmation)
        {
            return new EmployeeResponse
            {
                IsSuccess = false,
                Message = "Неверный пароль"
            };
        }

        if (IsEmployeeExist(employee.Name, employee.Phone, employee.Login))
        {
            return new EmployeeResponse
            {
                IsSuccess = false,
                Message = "Такие данные уже существуют"
            };
        }

        _context.Open();
        _employeeRep.Create(employee);
        _context.Close();

        return new EmployeeResponse
        {
            IsSuccess = true,
            Message = "Регистрация успешно завершена"
        };
    }

    private bool IsEmployeeExist(string name, string phone, string login)
    {
        _context.Open();
        var employee = _employeeRep.GetEmployees(1, 0, name, phone, login).FirstOrDefault();
        _context.Close();
        return employee != null;
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

    public void Delete(uint id)
    {
        _context.Open();
        _employeeRep.Delete(id);
        _context.Close();
    }

    public EmployeeResponse Update(Employee employee)
    {
        if (string.IsNullOrWhiteSpace(employee.Phone)
            || string.IsNullOrWhiteSpace(employee.Name)
            || employee.Position == PositionEnum.None)
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
        var namefilter = _employeeRep.GetEmployees(2, 0, employee.Name);
        var phoneFilter = _employeeRep.GetEmployees(2, 0, null, employee.Phone);
        if (namefilter.Count > 1 || phoneFilter.Count > 1 ||
            namefilter.Count == 1 && phoneFilter.Count == 1 && namefilter[0].ID != phoneFilter[0].ID)
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

    public EmployeeResponse GetByName(string name)
    {
        _context.Open();
        var employee = _employeeRep.GetEmployees(1, 0, name).FirstOrDefault();
        _context.Close();
        if (employee == null)
        {
            return new EmployeeResponse
            {
                IsSuccess = false,
                Message = "Сотрудник не найден",
                Employee = new Employee()             
            };
        }
        return new EmployeeResponse
        {
            IsSuccess = true,
            Message = "Сотрудник найден",
            Employee = employee
        };
    }
}