using System.ComponentModel.DataAnnotations;

namespace DBLibrary.Entities.DTOs;

public class EmployeeDTO
{
    [Required]
    public uint ID { get; set; }

    [Required]
    public string? Name { get; set; }

    [Required]
    public string? Phone { get; set; }

    [Required]
    public string? Position { get; set; }

    [Required]
    public string? Login { get; set; }    
}    
