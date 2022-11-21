using DBLibrary.Entities;

namespace Services;

public class EmployeeResponce
{
    public bool IsSuccess { get; set; }
    public string? Message { get; set; }
    public Employee? Employee { get; set; }
}

