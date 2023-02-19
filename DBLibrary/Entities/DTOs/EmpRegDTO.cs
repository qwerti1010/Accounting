using System.ComponentModel.DataAnnotations;

namespace DBLibrary.Entities.DTOs;

public class EmpRegDTO
{
    [Required]
    public string Name { get; set; } = null!;

    [Required]
    public string Phone { get; set; } = null!;   

    [Required]
    public string Login { get; set; } = null!;

    [Required]
    public string Password { get; set; } = null!;
}
