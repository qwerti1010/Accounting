namespace DBLibrary;

public class Computer
{
    public uint ID { get; set; }
    public string Name { get; set; } = null!;
    public string RegNumber { get; set; } = null!;
    public DateTime RegDate { get; set; }
    public string Status { get; set; } = null!;
    public string Employee { get; set; } = null!;
    public string Location { get; set; } = null!;
    public decimal Price { get; set; }
    public string Producer { get; set; } = null!;
    public int CoresCount { get; set; }
    public string Processor { get; set; } = null!;
    public int RAM { get; set; }
    public double Memory { get; set; }
    public string GraphicsCard { get; set; } = null!;
    public double BodySize { get; set; }
    public DateTime ExplDate { get; set; }
    public int AmortPeriod { get; set; }
    public int IsDeleted { get; set; }
}
