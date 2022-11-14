using DBLibrary.Entities;

namespace DBLibrary.Interfaces;

public interface IComputerRepository : IRepository<Computer>
{
    public List<Computer> Filter(string? name = null, decimal price = 0, int status = 0, uint employeeID = 0);
    //public void UpdateEmployeeID(uint ID);
}
