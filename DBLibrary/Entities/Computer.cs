namespace DBLibrary.Entities;

public class Computer
{
    public uint ID { get; set; }
    public string? Name { get; set; }
    public DateTime RegDate { get; set; }
    public Status Status { get; set; }
    public uint EmployeeID { get; set; }
    public decimal Price { get; set; }
    public DateTime ExplDate { get; set; }
    public bool IsDeleted { get; set; }
}
