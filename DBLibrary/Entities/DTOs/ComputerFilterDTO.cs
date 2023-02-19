namespace DBLibrary.Entities.DTOs;

public class ComputerFilterDTO
{
    public string? NameFilter { get; set; }
    public decimal PriceFilter { get; set; }
    public string? StatusFilter { get; set; }
    public uint EmplID { get; set; }

}
