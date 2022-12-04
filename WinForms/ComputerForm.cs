using DBLibrary;
using DBLibrary.Entities;
using Services.Services;
using System.Reflection;

namespace Accounting;

public partial class ComputerForm : Form
{
    private Computer? _computer;
    private readonly ComputerService _computerService;
    private readonly EmployeeService _employeeService;
    private List<Employee> _employees;

    public ComputerForm(ComputerService computerService, EmployeeService employeeService, Computer? computer = null)
    {
        InitializeComponent();
        _computerService = computerService;
        _employeeService = employeeService;
        _computer = computer;
        _employees = _employeeService.GetEmployees(10, 0).ToList();
    }

    private void Close_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void ComputerForm_Load(object sender, EventArgs e)
    {
        AddItemsToComboBoxes();
        if (_computer == null)
        {
            Text = "Добавить новое устройство";
            enableRedact.Visible = false;
            update.Visible = false;
            return;
        }
        create.Visible = false;
        Text = $"Просмотр информации устройства. Номер в базе - {_computer.ID}";
        ChangeState();
        name.Text = _computer.Name;        
        status.Text = _computer.Status.ToString();
        employee.Text = _employeeService.GetByID(_computer.EmployeeID)?.Name;
        regDate.Value = _computer.RegistrationDate;
        price.Text = _computer.Price.ToString();
        explStart.Value = _computer.ExploitationStart;
        if (_computer.Properties == null) return;
        foreach (var property in _computer.Properties)
        {
            switch (property.TypeID)
            {
                case PropType.CPU:
                    {
                        cpu.Text = property.Value;
                        break;
                    }
                case PropType.MotherBoard:
                    {
                        motherBoard.Text = property.Value;
                        break;
                    }
                case PropType.Case:
                    {
                        caseBox.Text = property.Value;
                        break;
                    }
                case PropType.GraphicsCard:
                    {
                        graphicsCard.Text = property.Value;
                        break;
                    }
                case PropType.Memory:
                    {
                        memory.Text = property.Value;
                        break;
                    }
                case PropType.RAM:
                    {
                        ram.Text = property.Value;
                        break;
                    }
                case PropType.PowerSupply:
                    {
                        powerSupply.Text = property.Value;
                        break;
                    }
            }
        }
    }

    private void EnableRedact_CheckStateChanged(object sender, EventArgs e)
    {
        ChangeState();
    }

    private void ChangeState()
    {
        foreach (var control in Controls)
        {
            switch (control)
            {
                case TextBox tb:
                    tb.ReadOnly = !tb.ReadOnly;
                    break;
                case ComboBox cb:
                    cb.Enabled = !cb.Enabled;
                    break;
                case DateTimePicker dtp:
                    dtp.Enabled = !dtp.Enabled;
                    break;
            }
        }
        update.Visible = !update.Visible;
    }

    private void Update_Click(object sender, EventArgs e)
    {
        if(FieldsAreEmpty())
        {
            MessageBox.Show("Поля не могут быть пустыми");
            return;
        }

        foreach (var property in _computer!.Properties!)
        {
            switch (property.TypeID)
            {
                case PropType.CPU:
                    {
                        property.Value = cpu.Text;
                        break;
                    }
                case PropType.MotherBoard:
                    {
                        property.Value = motherBoard.Text;
                        break;
                    }
                case PropType.Case:
                    {
                         property.Value = caseBox.Text ;
                        break;
                    }
                case PropType.GraphicsCard:
                    {
                        property.Value = graphicsCard.Text;
                        break;
                    }
                case PropType.Memory:
                    {
                        property.Value = memory.Text;
                        break;
                    }
                case PropType.RAM:
                    {
                        property.Value = ram.Text;
                        break;
                    }
                case PropType.PowerSupply:
                    {
                        property.Value = powerSupply.Text;
                        break;
                    }
            }            
        }

        _computer.Name = name.Text;
        _computer.Price = decimal.Parse(price.Text);
        _computer.RegistrationDate = regDate.Value;
        _computer.ExploitationStart = explStart.Value;
        _computer.EmployeeID = _employees.First(e => e.Name == employee.Text).ID;
        _computer.Status = (Status)status.SelectedIndex;

        _computerService.Update(_computer);
        MessageBox.Show("Данные обновлены");
        Close();
    }

    private void Create_Click(object sender, EventArgs e)
    {
        _computer = new Computer
        {
            Name = name.Text,
            Price = decimal.Parse(price.Text),
            RegistrationDate = regDate.Value,
            ExploitationStart = explStart.Value,
            EmployeeID = _employees.First(e => e.Name == employee.Text).ID,
            Status = (Status)status.SelectedIndex,

            Properties = new List<Property>
        {
            new Property{TypeID = PropType.CPU, Value = cpu.Text},
            new Property{TypeID = PropType.MotherBoard, Value = motherBoard.Text},
            new Property{TypeID = PropType.Case, Value = caseBox.Text},
            new Property{TypeID = PropType.GraphicsCard, Value = graphicsCard.Text},
            new Property{TypeID = PropType.Memory, Value = memory.Text},
            new Property{TypeID = PropType.RAM, Value = ram.Text},
            new Property{TypeID = PropType.PowerSupply, Value = powerSupply.Text}
        }
        };

        _computerService.Create(_computer);
        MessageBox.Show("Компьютер создан");
        Close();
    }

    private void AddItemsToComboBoxes()
    {
        employee.Items.AddRange(_employees.Select(e => e.Name).ToArray());
        status.Items.AddRange(Enum.GetValues<Status>().Cast<object>().ToArray());
        GetAttributes(typeof(CPUs), cpu);
        GetAttributes(typeof(RAMs), ram);
        GetAttributes(typeof(MotherBoards), motherBoard);
        GetAttributes(typeof(GraphicsCard), graphicsCard);
        GetAttributes(typeof(Cases), caseBox);
        GetAttributes(typeof(Memory), memory);
        GetAttributes(typeof(PowerSupply), powerSupply);        
    }

    private static void GetAttributes(Type type, ComboBox box)
    {        
        var fields = type.GetFields();
        foreach (var field in fields)
        {
            var at = field?.GetCustomAttribute<DescriptionAttribute>();
            if (at != null)
            {
                box.Items.Add(at.Description);
            }
        }
    }

    private bool FieldsAreEmpty()
    {
        foreach(var control in Controls)
        {
            if (control is TextBox tb && string.IsNullOrWhiteSpace(tb.Text))
                return true;
            if (control is ComboBox cb && string.IsNullOrWhiteSpace(cb.Text))
                return true;
        }
        return false;
    }    
}


