using DBLibrary;
using DBLibrary.Entities;
using DBLibrary.Interfaces;
using System.Text.RegularExpressions;

namespace Accounting;

public partial class MainForm : Form
{
    private Employee _employee;
    private Computer _computer;
    private List<Employee> _employees;
    private List<Computer> _computers;
    private List<Computer> _filredComputers;
    private IEmployeeRepository _employeeRep;
    private IComputerRepository _computerRep;
    private IPropertyRepository _propertyRep;
    private List<(string, string)> _empColumns;
    private List<(string, string)> _computerColumns;
    private DbContext _context;    
    
    public MainForm(Employee employee, DbContext context)
    {
        _context = context; 
        _employee = employee;
        _employeeRep = new EmployeeRep(_context);
        _computerRep = new ComputerRep(_context);
        _propertyRep = new PropertyRep(_context);
        _filredComputers = new List<Computer>();
        InitializeComponent();
        mainFormTabPage.SelectTab(nameof(tabPage2));
        _empColumns = new List<(string, string)>
            {
                ("employeeId", "Id"),
                ("name", "Имя"),
                ("position", "Должность"),
                ("phone","Номер телефона")
            };
        _computerColumns = new List<(string, string)>
            {
                ("computerId", "Id"),
                ("name", "Наименование"),
                ("registrationDate", "Дата постановки на учет"),
                ("price", "Цена"),
                ("cpu", "Процессор"),
                ("ram", "Объем оперативной памяти"),
                ("graphicsCard", "Видеокарта"),
                ("status", "Статус"),
                ("employeeID","ID сотрудника"),
                ("case","Корпус"),
                ("explutationStart","Начало эксплуатации"),
                ("memory","Память")
            };
    }

    private void MainForm_Load(object sender, EventArgs e)
    {
        Text = "Привет, " + _employee.Name;
        _context.Open();
        _employees = _employeeRep.GetAll().ToList();
        _computers = _computerRep.GetAll().ToList();
        _context.Close();
        CreateColumns(_empColumns);
        RefreshDataGrid(DataGridViewCondition.EmployeeTab);
        status.Items.AddRange(Enum.GetValues<Status>().Cast<object>().ToArray());
        position.Items.AddRange(Enum.GetValues<PositionEnum>().Cast<object>().ToArray());
    }

    private void CreateColumns(List<(string, string)> columnsNames)
    {
        dgv.Columns.Clear();
        foreach (var name in columnsNames)
        {
            dgv.Columns.Add(name.Item1, name.Item2);
        }
    }

    private void ReadSingleRow(Employee employee)
    {
        dgv.Rows.Add(employee.ID, employee.Name, employee.Position, employee.Phone);
    }

    private void ReadSingleRow(Computer computer)
    {
        _context.Open();
        var properties = _propertyRep.GetByComputerID(computer.ID);
        _context.Close();
        if (properties.Count == 0)
        {
            properties.Add(new Property { TypeID = PropType.None });
        }
        dgv.Rows.Add(computer.ID, computer.Name, computer.RegDate, computer.Price,
            properties.FirstOrDefault(p => p.TypeID == PropType.CPU)?.Value,
            properties.FirstOrDefault(p => p.TypeID == PropType.RAM)?.Value,
            properties.FirstOrDefault(p => p.TypeID == PropType.GraphicsCard)?.Value,
            computer.Status, computer.EmployeeID,
            properties.FirstOrDefault(p => p.TypeID == PropType.Case)?.Value, computer.ExplDate,
            properties.FirstOrDefault(p => p.TypeID == PropType.Memory)?.Value);
    }

    private void RefreshDataGrid(DataGridViewCondition condition)
    {
        dgv.Rows.Clear();
        switch (condition)
        {
            case DataGridViewCondition.DeviceTab:
                {
                    foreach (var computer in _computers)
                    {
                        ReadSingleRow(computer);
                    }
                    break;
                }
            case DataGridViewCondition.EmployeeTab:
                {
                    foreach (var employee in _employees)
                    {
                        ReadSingleRow(employee);
                    }
                    break;
                }
            case DataGridViewCondition.FilterTab:
                {
                    foreach (var computer in _filredComputers)
                    {
                        ReadSingleRow(computer);
                    }
                    break;
                }
        }        
    }

    private void ClearTextBoxes()
    {
        nameTextBox.Clear();
        phoneTextBox.Clear();
    }

    private void MainForm_Closed(object sender, FormClosedEventArgs e)
    {
        Application.Exit();
    }
    
    private void UpdateEmployee_Click(object sender, EventArgs e)
    {        
        if (String.IsNullOrWhiteSpace(nameTextBox.Text) || String.IsNullOrWhiteSpace(phoneTextBox.Text))
        {
            MessageBox.Show("Поля не могут быть пустыми");            
            return;
        }

        var regex = new Regex(@"^[+]7\d{10}$").IsMatch(phoneTextBox.Text);
        if (!regex)
        {
            MessageBox.Show("Неверный формат номера телефона");
            return;
        }

        _context.Open();
        var employee = _employeeRep.GetEmployee(nameTextBox.Text, phoneTextBox.Text, null);
        if (employee != null)
        {
            MessageBox.Show("Эти данные уже существуют");
            _context.Close();
            return;
        }
        
        _employee.Name = nameTextBox.Text;
        _employee.Phone = phoneTextBox.Text;
        _employee.Position = (PositionEnum)position.SelectedIndex;
        _employeeRep.Update(_employee);
        ClearTextBoxes();
        deleteEmployee.Enabled = false;
        updateEmployee.Enabled = false;
        _employees = _employeeRep.GetAll();
        RefreshDataGrid(DataGridViewCondition.EmployeeTab);
        _context.Close();
    }

    private void DeleteEmployee_Click(object sender, EventArgs e)
    {
        ClearTextBoxes();        
        _context.Open();
        _employeeRep.Delete(_employee.ID);        
        deleteEmployee.Enabled = false;
        updateEmployee.Enabled = false;
        _employees = _employeeRep.GetAll();
        RefreshDataGrid(DataGridViewCondition.EmployeeTab);
        _context.Close();
    }

    private void CreateEmployee_Click(object sender, EventArgs e)
    {
        if (String.IsNullOrWhiteSpace(nameTextBox.Text) || String.IsNullOrWhiteSpace(phoneTextBox.Text))
        {
            MessageBox.Show("Поля не могут быть пустыми");
            return;
        }

        var regex = new Regex(@"^[+]7\d{10}$").IsMatch(phoneTextBox.Text);
        if (!regex)
        {
            MessageBox.Show("Неверный формат номера телефона");
            return;
        }

        _context.Open();
        var employee = _employeeRep.GetEmployee(nameTextBox.Text, phoneTextBox.Text, null);
        if (employee != null)
        {
            MessageBox.Show("Эти данные уже существуют");
            _context.Close();
            return;
        }

        _employeeRep.Create(new Employee
        {
            Name = nameTextBox.Text,
            Phone = phoneTextBox.Text,
            Position = (PositionEnum)position.SelectedIndex
        });
        ClearTextBoxes();
        deleteEmployee.Enabled = false;
        updateEmployee.Enabled = false;
        _employees = _employeeRep.GetAll();
        RefreshDataGrid(DataGridViewCondition.EmployeeTab);
        _context.Close();
    }    

    private void TabPage_Selecting(object sender, TabControlCancelEventArgs e)
    {
        switch (e.TabPageIndex)
        {
            case 0:
                {
                    CreateColumns(_computerColumns);
                    RefreshDataGrid(DataGridViewCondition.DeviceTab);                    
                    break;
                }
            case 1:
                {
                    CreateColumns(_empColumns);
                    RefreshDataGrid(DataGridViewCondition.EmployeeTab);                    
                    break;
                }
            case 2:
                {
                    CreateColumns(_computerColumns);
                    RefreshDataGrid(DataGridViewCondition.FilterTab);
                    break;
                }
        }
    }

    private void DataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
    {
        _context.Open();
        if (mainFormTabPage.SelectedIndex == 1 && e.RowIndex >= 0)
        {
            var row = dgv.Rows[e.RowIndex];            
            _employee = _employeeRep.GetById((uint)row.Cells[0].Value);
            nameTextBox.Text = _employee.Name;
            phoneTextBox.Text = _employee.Phone;
            position.Text = _employee.Position.ToString();
            updateEmployee.Enabled = true;
            deleteEmployee.Enabled = true;
        }
        else if (mainFormTabPage.SelectedIndex == 0 && e.RowIndex >= 0)
        {
            var row = dgv.Rows[e.RowIndex];
            _computer = _computerRep.GetById((uint)row.Cells[0].Value);
            getComputer.Enabled = true;
            deleteComputer.Enabled = true;
        }
        _context.Close();
    }

    private void GetComputer_Click(object sender, EventArgs e)
    {
        _context.Open();
        var properties = _propertyRep.GetByComputerID(_computer.ID);
        _context.Close();
        var form = new ComputerForm(_computer, _context, properties);
        form.ShowDialog();
    }

    private void DeleteComputer_Click(object sender, EventArgs e)
    {
        _context.Open();
        _computerRep.Delete(_computer.ID);
        deleteComputer.Enabled = false;
        getComputer.Enabled = false;
        _computers = _computerRep.GetAll();
        RefreshDataGrid(DataGridViewCondition.DeviceTab);
        _context.Close();
    }

    private void CreateComputer_Click(object sender, EventArgs e)
    {
        var form = new ComputerForm(null, _context, null);
        form.ShowDialog();
    }

    private void RefreshDbState_Click(object sender, EventArgs e)
    {
        _context.Open();
        _computers = _computerRep.GetAll().Take(10).ToList();
        _context.Close();
        RefreshDataGrid(DataGridViewCondition.DeviceTab);
    }

    private void ApplyFilters_Click(object sender, EventArgs e)
    {
        _context.Open();
        string nameFilter = null;
        var priceFilter = 0M;
        var statusFilter = status.SelectedIndex;
        if (!string.IsNullOrWhiteSpace(computerName.Text))
        {
            nameFilter = computerName.Text;
        }
        if (!string.IsNullOrWhiteSpace(price.Text))
        {
            decimal.TryParse(price.Text, out priceFilter);
        }
        if (statusFilter < 0)
        {
            statusFilter = 0;
        }
        _filredComputers = _computerRep.Filter(nameFilter, priceFilter, statusFilter);
        _context.Close();
        RefreshDataGrid(DataGridViewCondition.FilterTab);
    }

    private void RegCheckBox_CheckedChanged(object sender, EventArgs e)
    {
        regDatePicker.Enabled = !regDatePicker.Enabled;
    }
}
