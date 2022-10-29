using DBLibrary;

namespace Accounting;

public partial class ComputerForm : Form
{
    private Computer _computer;
    private List<Computer> _computers;
    private ComputerRep _computerRep;
    private DbContext _dbContext;
    public ComputerForm(List<Computer> computers)
    {
        InitializeComponent();
        _computers = computers;
        _dbContext = new DbContext();
        _computerRep = new ComputerRep(_dbContext);
    }

    public ComputerForm(Computer computer, List<Computer> computers)
    {        
        InitializeComponent();
        _computer = computer;
        _computers = computers;
        _dbContext = new DbContext();
        _computerRep = new ComputerRep(_dbContext);
    }

    private void Close_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void ComputerForm_Load(object sender, EventArgs e)
    {
          foreach (var c in _computers)
        {
            location.Items.Add(c.Location);
            status.Items.Add(c.Status);
            employee.Items.Add(c.Employee);
        }
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
        location.Text = _computer.Location;
        status.Text = _computer.Status;
        employee.Text = _computer.Employee;
        regNumber.Text = _computer.RegNumber;
        regDate.Text = _computer.RegDate.ToShortDateString();
        price.Text = _computer.Price.ToString();
        producer.Text = _computer.Producer.ToString();
        cpu.Text = _computer.Processor.ToString();
        coresCount.Text = _computer.CoresCount.ToString();
        ram.Text = _computer.RAM.ToString();
        graphicsCard.Text = _computer.GraphicsCard.ToString();
        memory.Text = _computer.Memory.ToString();
        bodySize.Text = _computer.BodySize.ToString();
        explStart.Text = _computer.RegDate.ToString();
        amortPeriod.Text = _computer.AmortPeriod.ToString();
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
        _dbContext.Open();
        if (_computer == null)
        {
            _computer = new Computer();
        }        
        try
        {
            _computer.Name = name.Text;
            _computer.RegNumber = regNumber.Text;
            _computer.RegDate = DateTime.Parse(regDate.Text);
            _computer.Price = decimal.Parse(price.Text);
            _computer.Producer = producer.Text;
            _computer.Processor = cpu.Text;
            _computer.CoresCount = int.Parse(coresCount.Text);
            _computer.RAM = int.Parse(ram.Text);
            _computer.GraphicsCard = graphicsCard.Text;
            _computer.Status = status.Text;
            _computer.Employee = employee.Text;
            _computer.Location = location.Text;
            _computer.BodySize = double.Parse(bodySize.Text);
            _computer.ExplDate = DateTime.Parse(explStart.Text);
            _computer.AmortPeriod = int.Parse(amortPeriod.Text);
            _computer.Memory = double.Parse(memory.Text);
            if (_computer.ID == 0)
            {
                _computerRep.Create(_computer);
                MessageBox.Show("Устройство успешно дабавленно");
                Close();
            }
            else
            {
                _computerRep.Update(_computer);
                MessageBox.Show("Устройство успешно обнавленно");
                Close();
            }
        }
        catch
        {
            MessageBox.Show("Некорректные данные");
        }
        _dbContext.Close();
    }
}
