using DBLibrary;
using DBLibrary.Entities.DTOs;
using DesktopClientServices;
using System.Reflection;

namespace Accounting;

public partial class ComputerForm : Form
{
    private readonly ComputerDTO? _computer;
    private readonly CompService _compService;
    private readonly IList<EmployeeDTO> _employees;

    public ComputerForm(CompService computerService, IList<EmployeeDTO> employees, ComputerDTO? computer = null)
    {
        InitializeComponent();
        _computer = computer;
        _compService = computerService;
        _computer = computer;
        _employees = employees;
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
        status.Text = _computer.Status!.ToString();
        regDate.Value = _computer.RegistrationDate;
        price.Text = _computer.Price.ToString();
        explStart.Value = _computer.ExploitationStart;
        employees.Text = _employees.FirstOrDefault(emp => emp.ID == _computer.EmployeeID)!.Name;
        if (_computer.Properties == null) return;
        foreach (var property in _computer.Properties)
        {
            switch (property.TypeID)
            {
                case "CPU":
                    {
                        cpu.Text = property.Value;
                        break;
                    }
                case "MotherBoard":
                    {
                        motherBoard.Text = property.Value;
                        break;
                    }
                case "Case":
                    {
                        caseBox.Text = property.Value;
                        break;
                    }
                case "GraphicsCard":
                    {
                        graphicsCard.Text = property.Value;
                        break;
                    }
                case "Memory":
                    {
                        memory.Text = property.Value;
                        break;
                    }
                case "RAM":
                    {
                        ram.Text = property.Value;
                        break;
                    }
                case "PowerSupply":
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

    private async void Update_Click(object sender, EventArgs e)
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
                case "CPU":
                    {
                        property.Value = cpu.Text;
                        break;
                    }
                case "MotherBoard":
                    {
                        property.Value = motherBoard.Text;
                        break;
                    }
                case "Case":
                    {
                        property.Value = caseBox.Text ;
                        break;
                    }
                case "GraphicsCard":
                    {
                        property.Value = graphicsCard.Text;
                        break;
                    }
                case "Memory":
                    {
                        property.Value = memory.Text;
                        break;
                    }
                case "RAM":
                    {
                        property.Value = ram.Text;
                        break;
                    }
                case "PowerSupply":
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
        _computer.Status = status.Text.ToString();
        _computer.EmployeeID = _employees.First(emp => emp.Name == employees.Text).ID;
        var response = await _compService.UpdateAsync(_computer);
        MessageBox.Show(response.Message);
        if (response.IsSuccess)
        {
            Close();
        }
    }

    private async void Create_ClickAsync(object sender, EventArgs e)
    {
        var id = _employees.First(e => e.Name == employees.Text).ID;
        var prop = new List<PropertyDTO>
        {
            new PropertyDTO { TypeID = "CPU", Value = cpu.Text },
            new PropertyDTO { TypeID = "MotherBoard", Value = motherBoard.Text },
            new PropertyDTO { TypeID = "Case", Value = caseBox.Text },
            new PropertyDTO { TypeID = "GraphicsCard", Value = graphicsCard.Text },
            new PropertyDTO { TypeID = "Memory", Value = memory.Text },
            new PropertyDTO { TypeID = "RAM", Value = ram.Text },
            new PropertyDTO { TypeID = "PowerSupply", Value = powerSupply.Text }
        };
        var response = await _compService.CreateAsync(name.Text, regDate.Value, price.Text, status.Text, id, explStart.Value, prop);
        MessageBox.Show(response.Message);
        if (response.IsSuccess)
        {
            Close();
        }
    }

    private void AddItemsToComboBoxes()
    {
        employees.Items.AddRange(_employees.Select(e => e.Name).ToArray());
        GetAttributes(typeof(Status), status);
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


