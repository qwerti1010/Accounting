using System.ComponentModel.DataAnnotations;

namespace DBLibrary.Entities.DTOs;

public class EmpLoginDTO
{
    [Required]
    public string? Login { get; set; }

    [Required]
    public string? Password { get; set; }
}
