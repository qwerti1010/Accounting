using System.ComponentModel.DataAnnotations.Schema;

namespace DBLibrary.Entities;

[Table("computers")]
public class Computer
{
    public uint ID { get; set; }
    public string? Name { get; set; }
    public DateTime RegistrationDate { get; set; }    
    public decimal Price { get; set; }
    public Status Status { get; set; }
    public uint EmployeeID { get; set; }    
    public DateTime ExploitationStart { get; set; }
    public bool IsDeleted { get; set; }
    public IList<Property>? Properties { get; set; }    
}
