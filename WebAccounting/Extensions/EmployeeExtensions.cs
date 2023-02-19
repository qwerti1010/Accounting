using DBLibrary.Entities.DTOs;
using DBLibrary.Entities;
using DBLibrary;

namespace WebAccounting.Extensions;

public static class EmployeeExtensions
{
    public static EmployeeDTO ToDto(this Employee employee)
    {
        return new EmployeeDTO
        {
            ID = employee.ID,
            Name = employee.Name,
            Phone = employee.Phone,
            Position = employee.Position.ToString(),
            Login = employee.Login
        };
    }

    public static IList<EmployeeDTO> ToDto(this IList<Employee> employees)
    {
        var result = new List<EmployeeDTO>();
        foreach (var employee in employees)
        {
            result.Add(employee.ToDto());
        }
        return result;
    }

    public static Employee? ToEmp(this EmpCreateDTO dto)
    {
        if (dto.Position != "User" && dto.Position
            != "Moderator" && dto.Position != "Admin") return null;

        return new Employee
        {
            Name = dto.Name,
            Phone = dto.Phone,
            Login = dto.Login,
            Password = dto.Password,
            Position = (PositionEnum)Enum.Parse(typeof(PositionEnum), dto.Position)
        };
    }

    public static Employee? ToEmp(this EmployeeDTO dto)
    {
        if (dto.Position != "User" && dto.Position
            != "Moderator" && dto.Position != "Admin") return null;

        return new Employee
        {
            ID = dto.ID,
            Name = dto.Name,
            Phone = dto.Phone,
            Login = dto.Login,
            Position = (PositionEnum)Enum.Parse(typeof(PositionEnum), dto.Position)
        };
    }
}
