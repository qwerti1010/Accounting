using DBLibrary;
using DBLibrary.Entities;
using DBLibrary.Interfaces;
using System.Reflection;

namespace Accounting;

public partial class ComputerForm : Form
{
    private Computer _computer;
    private readonly IComputerRepository _computerRep;
    private readonly DbContext _context;
    private readonly IEmployeeRepository _employeeRep;

    public ComputerForm(Computer computer, DbContext context)
    {
        InitializeComponent();
        _computer = computer;
        _context = context;
        _computerRep = new ComputerRep(_context);
        _employeeRep = new EmployeeRep(_context);        
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
            update.Text = "Добавить";
            enableRedact.Visible = false;
            return;
        }

        Text = $"Просмотр информации устройства. Номер в базе - {_computer.ID}";
        ChangeState();
        name.Text = _computer.Name;
        _context.Open();
        status.Text = _computer.Status.ToString();
        employee.Text = _employeeRep.GetByID(_computer.EmployeeID).Name;
        _context.Close();
        regDate.Value = _computer.RegDate;
        price.Text = _computer.Price.ToString();
        cpu.Text = _computer.Properties.ContainsKey(PropType.CPU) ? _computer.Properties[PropType.CPU].Value : null;
        ram.Text = _computer.Properties.ContainsKey(PropType.RAM) ? _computer.Properties[PropType.RAM].Value : null;
        graphicsCard.Text = _computer.Properties.ContainsKey(PropType.GraphicsCard) ? _computer.Properties[PropType.GraphicsCard].Value : null; 
        memory.Text = _computer.Properties.ContainsKey(PropType.Memory) ? _computer.Properties[PropType.Memory].Value : null; 
        motherBoard.Text = _computer.Properties.ContainsKey(PropType.MotherBoard) ? _computer.Properties[PropType.MotherBoard].Value : null; 
        powerSupply.Text = _computer.Properties.ContainsKey(PropType.PowerSupply) ? _computer.Properties[PropType.PowerSupply].Value : null;        
        caseBox.Text = _computer.Properties.ContainsKey(PropType.Case) ? _computer.Properties[PropType.Case].Value : null;
        explStart.Value = _computer.ExplDate;
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
        if(!FieldsArentEmpty())
        {
            MessageBox.Show("Поля не могут быть пустыми");
            return;
        }

        _computer ??= new Computer
            {
                Properties = new Dictionary<PropType, Property>()
            };
        if (!_computer.Properties.ContainsKey(PropType.CPU))
        {
            _computer.Properties.Add(PropType.CPU, new Property());
        }
        _computer.Properties[PropType.CPU].Value = cpu.Text;

        if (!_computer.Properties.ContainsKey(PropType.MotherBoard))
        {
            _computer.Properties.Add(PropType.MotherBoard, new Property());
        }
        _computer.Properties[PropType.MotherBoard].Value = motherBoard.Text;

        if (!_computer.Properties.ContainsKey(PropType.Case))
        {
            _computer.Properties.Add(PropType.Case, new Property());
        }
        _computer.Properties[PropType.Case].Value = caseBox.Text;

        if (!_computer.Properties.ContainsKey(PropType.GraphicsCard))
        {
            _computer.Properties.Add(PropType.GraphicsCard, new Property());
        }
        _computer.Properties[PropType.GraphicsCard].Value = graphicsCard.Text;

        if (!_computer.Properties.ContainsKey(PropType.Memory))
        {
            _computer.Properties.Add(PropType.Memory, new Property());
        }
        _computer.Properties[PropType.Memory].Value = memory.Text;

        if (!_computer.Properties.ContainsKey(PropType.RAM))
        {
            _computer.Properties.Add(PropType.RAM, new Property());
        }
        _computer.Properties[PropType.RAM].Value = ram.Text;

        if (!_computer.Properties.ContainsKey(PropType.PowerSupply))
        {
            _computer.Properties.Add(PropType.PowerSupply, new Property());
        }       
        _computer.Properties[PropType.PowerSupply].Value = powerSupply.Text;
        _context.Open();
        _computer.Name = name.Text;
        _computer.RegDate = regDate.Value;
        decimal.TryParse(price.Text, out decimal p);
        _computer.Price = p;
        _computer.Status = (Status)status.SelectedIndex;
        _computer.EmployeeID = _employeeRep.GetEmployees(employee.Text, null, null)[0].ID;
        _computer.ExplDate = explStart.Value;
        if (_computer.ID == 0)
        {
            _computerRep.Create(_computer);            
            MessageBox.Show("устройство успешно дабавленно");
            _context.Close();
            Close();
        }
        else
        {
            _computerRep.Update(_computer);            
            MessageBox.Show("устройство успешно обнавленно");
            _context.Close();
            Close();
        }

        _context.Close();
    }

    private void AddItemsToComboBoxes()
    {
        _context.Open();        
        employee.Items.AddRange(_employeeRep.GetAll(10,0).Select(e => e.Name).ToArray());
        status.Items.AddRange(Enum.GetValues<Status>().Cast<object>().ToArray());
        _context.Close();
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

    private bool FieldsArentEmpty()
    {
        foreach(var control in Controls)
        {
            if (control is TextBox tb && string.IsNullOrWhiteSpace(tb.Text))
                return false;
            if (control is ComboBox cb && string.IsNullOrWhiteSpace(cb.Text))
                return false;
        }   
        return true;
    }
}


