using System.ComponentModel.DataAnnotations.Schema;

namespace DBLibrary.Entities;

[Table("properties")]
public class Property
{
    public uint ID { get; set; }
    public bool IsDeleted { get; set; }
    public uint ComputerID { get; set; }
    public PropType TypeID { get; set; }
    public string? Value { get; set; }
}
