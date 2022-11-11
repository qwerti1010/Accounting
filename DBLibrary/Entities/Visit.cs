namespace DBLibrary.Entities;

public class Visit
{
    public uint ID;
    public uint EmployeeID { get; set; }
    public DateTime VisitTime { get; set; }
    public bool IsDeleted { get; set; }
}
