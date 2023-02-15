
namespace DBLibrary;

public enum Status
{
    [Description("Отсутствует")]
    None,
    [Description("Эксплуатируется")]
    Exploitation = 1,
    [Description("В ремонте")]
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
    [Description("Пользователь")]
    User,
    [Description("Модератор")]
    Moderator = 1,
    [Description("Администратор")]
    Admin = 2
}

//все процессоры
public enum CPUs
{
    [Description("Отсутсвует")]
    None,
    [Description("Intel core i3")]
    Intel_core_i3,
    [Description("Intel core i5")]
    Intel_core_i5,
    [Description("Intel core i7")]
    Intel_core_i7
}

public enum RAMs
{
    [Description("Отсутсвует")]
    None,
    [Description("DDR4 4 GB")]
    DDR4_4,
    [Description("DDR4 8 GB")]
    DDR4_8,
    [Description("DDR4 16 GB")]
    DDR4_16,
}

public enum Cases
{
    [Description("Отсутсвует")]
    None,
    [Description("Корпус 1")]
    FirstCase,
    [Description("Корпус 2")]
    SecondCase
}

public enum MotherBoards
{
    [Description("Отсутсвует")]
    None,
    [Description("Материнская плата 1")]
    FirstMotherBoard,
    [Description("Материнская плата 2")]
    SecondMotherBoard
}

public enum PowerSupply
{
    [Description("Отсутсвует")]
    None,
    [Description("Блок питания 1")]
    FirstPowerSupply,
    [Description("Блок питания 2")]
    SecondPowerSupply
}

public enum GraphicsCard
{
    [Description("Отсутсвует")]
    None,
    [Description("Видеокарта 1 4 GB")]
    FirstCard_4,
    [Description("Видеокарта 2 8 GB")]
    SecondCard_8
}

public enum Memory
{
    [Description("Отсутсвует")]
    None,
    [Description("Жесткий диск 1TB")]
    FirstHardDisc_1,
    [Description("Жесткий диск 2TB")]
    SecondHardDissc_2
}

