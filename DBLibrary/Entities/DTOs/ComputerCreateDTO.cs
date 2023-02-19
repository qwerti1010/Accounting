using System.ComponentModel.DataAnnotations;

namespace DBLibrary.Entities.DTOs;

public class ComputerCreateDTO
{
    [Required]
    public string? Name { get; set; }

    [Required]
    public DateTime RegistrationDate { get; set; }

    [Required]
    public decimal Price { get; set; }
    public string? Status { get; set; }

    [Required]
    public uint EmployeeID { get; set; }

    [Required]
    public DateTime ExploitationStart { get; set; }

    [Required]
    public IList<PropertyDTO>? Properties { get; set; }
}
