using DBLibrary.Entities;

namespace DBLibrary.Interfaces;

public interface IComputerRepository : IRepository<Computer>
{
    public IList<Computer> Filter(int skip, int take, string? name = null, decimal price = 0, int status = 0, uint employeeID = 0);   
    public IList<Computer> GetByEmpID(uint id);
}
