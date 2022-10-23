using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.WinForms
{
    public class Computer
    {
        public int Id { get; set; }
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

        public void SetProperties(DataGridViewRow row)
        {
            Id = (int)row.Cells[0].Value;
            Name = row.Cells[1].Value.ToString()!;
            RegNumber = row.Cells[2].Value.ToString()!;
            RegDate = (DateTime)row.Cells[3].Value;
            Price = (decimal)row.Cells[4].Value;
            Producer = row.Cells[5].Value.ToString()!;
            Processor = row.Cells[6].Value.ToString()!;
            CoresCount = (int)row.Cells[7].Value;
            RAM = (int)row.Cells[8].Value;
            GraphicsCard = row.Cells[9].Value.ToString()!;
            Status = row.Cells[10].Value.ToString()!;
            Employee = row.Cells[11].Value.ToString()!;
            Location = row.Cells[12].Value.ToString()!;
            BodySize = (double)row.Cells[13].Value;
            ExplDate = (DateTime)row.Cells[14].Value;
            AmortPeriod = (int)row.Cells[15].Value;
            Memory = (double)row.Cells[16].Value;
        }
    }
}
