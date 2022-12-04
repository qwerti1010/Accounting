using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBLibrary.Entities;

[Table("visit_history")]
public class Visit
{    
    public uint ID { get; set; }
    public uint EmployeeID { get; set; }
    public DateTime VisitTime { get; set; }
    public bool IsDeleted { get; set; }
}
