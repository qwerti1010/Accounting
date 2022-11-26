using DBLibrary.Entities;

namespace DBLibrary.Interfaces;

public interface IPropertyRepository : IRepository<Property>
{
    public Dictionary<PropType, Property> GetByComputerID(uint id);
}
