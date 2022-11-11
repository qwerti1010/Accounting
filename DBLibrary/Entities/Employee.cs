namespace DBLibrary.Entities;

public class Employee
{
    public uint ID { get; set; }
    public string? Name { get; set; }
    public string? Phone { get; set; }
    public PositionEnum Position { get; set; }
    public string? Login { get; set; }
    public string? Password { get; set; }
    public bool IsDeleted { get; set; }
}