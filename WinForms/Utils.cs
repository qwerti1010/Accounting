using DB;
namespace Accounting;

public class Utils
{
    public static void SetProperties(Computer computer, DataGridViewRow row)
    {
        computer.Id = (int)row.Cells[0].Value;
        computer.Name = row.Cells[1].Value.ToString()!;
        computer.RegNumber = row.Cells[2].Value.ToString()!;
        computer.RegDate = (DateTime)row.Cells[3].Value;
        computer.Price = (decimal)row.Cells[4].Value;
        computer.Producer = row.Cells[5].Value.ToString()!;
        computer.Processor = row.Cells[6].Value.ToString()!;
        computer.CoresCount = (int)row.Cells[7].Value;
        computer.RAM = (int)row.Cells[8].Value;
        computer.GraphicsCard = row.Cells[9].Value.ToString()!;
        computer.Status = row.Cells[10].Value.ToString()!;
        computer.Employee = row.Cells[11].Value.ToString()!;
        computer.Location = row.Cells[12].Value.ToString()!;
        computer.BodySize = (double)row.Cells[13].Value;
        computer.ExplDate = (DateTime)row.Cells[14].Value;
        computer.AmortPeriod = (int)row.Cells[15].Value;
        computer.Memory = (double)row.Cells[16].Value;
    }
}