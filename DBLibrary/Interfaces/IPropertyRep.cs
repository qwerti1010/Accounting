using DBLibrary.Entities;

namespace DBLibrary.Interfaces;

public interface IPropertyRepository : IRepository<Property>
{
    public IList<Property> GetByComputerID(uint id);
}
