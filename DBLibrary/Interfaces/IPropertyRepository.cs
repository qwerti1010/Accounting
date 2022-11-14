using DBLibrary.Entities;

namespace DBLibrary.Interfaces;

public interface IPropertyRepository
{
    public List<Property> GetByComputerID(uint id);
    public void Update(List<Property> properties);
    public void Create(List<Property> properties);
}
