using DBLibrary.Entities;

namespace DBLibrary.Interfaces;

public interface IComputerRepository : IRepository<Computer>
{
    public List<Computer> Filter(string name, decimal price, int status);
    //public void UpdateEmployeeID(uint ID);
}
