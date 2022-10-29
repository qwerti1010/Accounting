namespace DBLibrary;

public class Employee
{
    public uint ID { get; set; }
    public string Name { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public string Position { get; set; } = null!;
    public string? Login { get; set; }
    public string? Password { get; set; }
    public int IsDeleted { get; set; }
}