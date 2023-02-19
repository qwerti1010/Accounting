using System.ComponentModel.DataAnnotations;

namespace DBLibrary.Entities.DTOs;

public class PropertyDTO
{
    [Required]
    public uint ID { get; set; }

    [Required]
    public uint ComputerID { get; set; }

    [Required]
    public string? TypeID { get; set; }

    [Required]
    public string? Value { get; set; }
}
