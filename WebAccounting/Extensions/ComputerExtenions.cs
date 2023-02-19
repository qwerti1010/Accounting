using DBLibrary;
using DBLibrary.Entities;
using DBLibrary.Entities.DTOs;
using Microsoft.OpenApi.Extensions;

namespace WebAccounting.Extensions;

public static class ComputerExtenions
{

    public static Computer ToComputer(this ComputerFilterDTO dto)
    {
        return new Computer 
        {
            Name = dto.NameFilter,
            Price = dto.PriceFilter,
            EmployeeID = dto.EmplID,
            Status = Enum.GetValues<Status>()
                         .FirstOrDefault(x => x.GetAttributeOfType<DescriptionAttribute>().Description == dto.StatusFilter)
        };
    }

    public static Computer? ToComputer(this ComputerCreateDTO dto)
    {
        Status status;
        try
        {
            status = Enum.GetValues<Status>()
                         .First(x => x.GetAttributeOfType<DescriptionAttribute>().Description == dto.Status);
        }
        catch
        {
            return null;
        }

        return new Computer
        {
            Name = dto.Name,
            RegistrationDate = dto.RegistrationDate,
            Price = dto.Price,
            Status = status,
            EmployeeID = dto.EmployeeID,
            ExploitationStart = dto.ExploitationStart,
            Properties = new PropList(ToProp(dto.Properties!))
        };
    }

    public static Computer? ToComputer(this ComputerDTO dto)
    {
        Status status;
        try
        {
            status = Enum.GetValues<Status>()
                         .First(x => x.GetAttributeOfType<DescriptionAttribute>().Description == dto.Status);
        }
        catch
        {
            return null;
        }

        return new Computer
        {
            ID = dto.ID,
            Name = dto.Name,
            RegistrationDate = dto.RegistrationDate,
            Price = dto.Price,
            Status = status,
            EmployeeID = dto.EmployeeID,
            ExploitationStart = dto.ExploitationStart,
            Properties = new PropList(ToProp(dto.Properties!))
        };
    }

    public static ComputerDTO ToDto(this Computer computer)
    {
        return new ComputerDTO
        {
            ID = computer.ID,
            Name = computer.Name!,
            Status = computer.Status.GetAttributeOfType<DescriptionAttribute>().Description,
            ExploitationStart = computer.ExploitationStart,
            EmployeeID = computer.EmployeeID,
            Price = computer.Price,
            RegistrationDate = computer.RegistrationDate,
            Properties = ToDto(computer.Properties!.Props)
        };
    }

    public static IList<ComputerDTO> ToDto(this IList<Computer> computers)
    {
        var result = new List<ComputerDTO>();
        foreach (var computer in computers)
        {
            result.Add(computer.ToDto());
        }
        return result;
    }

    private static PropertyDTO ToDto(Property property)
    {
        return new PropertyDTO
        {
            ID = property.ID,
            ComputerID = property.ComputerID,
            TypeID = property.TypeID.ToString(),
            Value = property.Value
        };
    }

    private static IList<PropertyDTO> ToDto(IList<Property> properties)
    {
        var result = new List<PropertyDTO>();
        foreach (var property in properties)
        {
            result.Add(ToDto(property));
        }
        return result;
    }

    private static IList<Property> ToProp(IList<PropertyDTO> dtos)
    {
        var result = new List<Property>();
        foreach (var dto in dtos)
        {
            result.Add(ToProp(dto));
        }
        return result;
    }

    private static Property ToProp(PropertyDTO dto)
    {
        var result = new Property {ID = dto.ID, Value = dto.Value, ComputerID = dto.ComputerID};
        try
        {
            result.TypeID = (PropType)Enum.Parse(typeof(PropType), dto.TypeID!);
        }
        catch
        {

            result.TypeID = PropType.None;
            result.Value = null;
        }
        return result;
    }
}
