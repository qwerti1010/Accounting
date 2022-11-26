using DBLibrary.Entities;

namespace Services.Responses;

public class EmployeeResponse
{
    public bool IsSuccess { get; set; }
    public string? Message { get; set; }
    public Employee? Employee { get; set; }
}

