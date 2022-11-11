
namespace DBLibrary;

public enum Status
{
    None,
    Exploitation = 1,
    Repair = 2
}

public enum PropType
{
    None,
    CPU = 1,
    MotherBoard = 2,
    Case = 3,
    GraphicsCard = 4,
    Memory = 5,
    RAM = 6,
    PowerSupply = 7
}

public enum PositionEnum
{
    None,
    FirstPos = 1,
    SecondPos = 2,
    ThirdPos = 3
}

//все процессоры
public enum CPUs
{
    [Description("Intel core i3")]
    Intel_core_i3,
    [Description("Intel core i5")]
    Intel_core_i5,
    [Description("Intel core i7")]
    Intel_core_i7
}