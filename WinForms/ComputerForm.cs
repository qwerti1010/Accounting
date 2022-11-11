using DBLibrary;
using DBLibrary.Entities;
using DBLibrary.Interfaces;
using System.Reflection;

namespace Accounting;

public partial class ComputerForm : Form
{
    private Computer _computer;
    private IComputerRepository _computerRep;
    private DbContext _context;
    private IEmployeeRepository _employeeRep;
    private IPropertyRepository _propertyRep;
    private List<Property> _properties;

    public ComputerForm(Computer computer, DbContext context, List<Property> properties)
    {
        InitializeComponent();
        _computer = computer;
        _context = context;
        _computerRep = new ComputerRep(_context);
        _employeeRep = new EmployeeRep(_context);
        _propertyRep = new PropertyRep(_context);
        _properties = properties;
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
        employee.Text = _employeeRep.GetById(_computer.EmployeeID).Name;
        _context.Close();
        regDate.Value = _computer.RegDate;
        price.Text = _computer.Price.ToString();
        var cpu1 = _properties.FirstOrDefault(p => p.TypeID == PropType.CPU);
        var x = cpu1.Value.CompareTo("Intel core i5");
        cpu.Text = cpu1.Value;

        //ram.Text = _properties.First(p => p.TypeID == PropType.RAM).Value;
        //graphicsCard.Text = _properties.First(p => p.TypeID == PropType.GraphicsCard).Value;
        //memory.Text = _properties.First(p => p.TypeID == PropType.Memory).Value;      
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
        //_context.Open();
        //if (_computer is null)
        //{
        //    _computer = new Computer();
        //}
        //try
        //{
        //    _computer.Name = name.Text;
        //    _computer.RegDate = regDate.Value;
        //    _computer.Price = decimal.Parse(price.Text);
        //    _computer.Processor = cpu.Text;
        //    _computer.RAM = uint.Parse(ram.Text);
        //    _computer.GraphicsCard = graphicsCard.Text;
        //    _computer.Status = (Status)status.SelectedIndex;
        //    _computer.EmployeeID = uint.Parse(employee.Text);
            
        //    _computer.ExplDate = explStart.Value;
        //    _computer.Memory = double.Parse(memory.Text);
        //    if (_computer.ID == 0)
        //    {
        //        _computerRep.Create(_computer);
        //        MessageBox.Show("Устройство успешно дабавленно");
        //        Close();
        //    }
        //    else
        //    {
        //        _computerRep.Update(_computer);
        //        MessageBox.Show("Устройство успешно обнавленно");
        //        Close();
        //    }
        //}
        //catch
        //{
        //    MessageBox.Show("Некорректные данные");
        //}
        //_context.Close();
    }

    private void AddItemsToComboBoxes()
    {
        _context.Open();        
        employee.Items.AddRange(_employeeRep.GetAll().Select(e => e.Name).ToArray());
        status.Items.AddRange(Enum.GetValues<Status>().Cast<object>().ToArray());
        _context.Close();
        var fields = typeof(CPUs).GetFields();
        foreach (var field in fields)
        {
            var at = field?.GetCustomAttribute<DescriptionAttribute>();
            if (at != null)
            {
                cpu.Items.Add(at.Description);
            }                       
        }
    }
}


