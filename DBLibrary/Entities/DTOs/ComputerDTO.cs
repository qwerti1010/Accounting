using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBLibrary.Entities.DTOs;

public class ComputerDTO
{
    public uint ID { get; set; }
    [Required]    
    public string? Name { get; set; }
    public DateTime RegistrationDate { get; set; }
    public decimal Price { get; set; }
    public string? Status { get; set; }
    public uint EmployeeID { get; set; }
    public DateTime ExploitationStart { get; set; }
    public IList<PropertyDTO>? Properties { get; set; }
}
