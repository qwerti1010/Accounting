using DBLibrary.Entities;

namespace DBLibrary.Interfaces;

public interface IPropertyRepository : IRepository<Property>
{
    public List<Property> GetByComputerID(uint id);
}
