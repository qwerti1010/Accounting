using DBLibrary;
using DBLibrary.Entities;
using DBLibrary.Interfaces;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Accounting;

public partial class MainForm : Form
{
    private Employee _employee;
    private Computer? _computer;
    private List<Employee> _employees = null!;
    private List<Computer> _computers = null!;
    private List<Computer> _filredComputers;
    private readonly IEmployeeRepository _employeeRep;
    private readonly IComputerRepository _computerRep;
    private readonly List<(string, string)> _empColumns;
    private readonly List<(string, string)> _computerColumns;
    private readonly DbContext _context;    
    
    public MainForm(Employee employee, DbContext context)
    {
        _context = context; 
        _employee = employee;
        _employeeRep = new EmployeeRep(_context);
        _computerRep = new ComputerRep(_context);
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
        _employees = _employeeRep.GetAll(10,0);
        _computers = _computerRep.Filter(0,10);
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
        dgv.Rows.Add(computer.ID, computer.Name, computer.RegDate, computer.Price,
            computer.Properties.ContainsKey(PropType.CPU) ? computer.Properties[PropType.CPU].Value : PropType.None,
            computer.Properties.ContainsKey(PropType.RAM) ? computer.Properties[PropType.RAM].Value : PropType.None,
            computer.Properties.ContainsKey(PropType.GraphicsCard) ? computer.Properties[PropType.GraphicsCard].Value : PropType.None,
            computer.Status, _employeeRep.GetByID(computer.EmployeeID)?.Name,
            computer.Properties.ContainsKey(PropType.Case) ? computer.Properties[PropType.Case].Value : PropType.None,
            computer.ExplDate,
            computer.Properties.ContainsKey(PropType.Memory) ? computer.Properties[PropType.Memory].Value : PropType.None);      
    }

    private void RefreshDataGrid(DataGridViewCondition condition)
    {
        dgv.Rows.Clear();
        _context.Open();
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
        _context.Close();
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
        var employees = _employeeRep.GetEmployees(nameTextBox.Text, phoneTextBox.Text);
        if (employees.Count > 1 || employees[0].ID != _employee.ID)
        {
            MessageBox.Show("Эти данные уже существуют");
            _context.Close();
            return;
        }
        _employee.Name = nameTextBox.Text;
        _employee.Phone = phoneTextBox.Text;
        _employee.Position = (PositionEnum)position.SelectedIndex;
        _employeeRep.Update(_employee);
        _context.Close();
        ClearTextBoxes();
        deleteEmployee.Enabled = false;
        updateEmployee.Enabled = false;
        _employees = _employeeRep.GetAll(10, 0);
        RefreshDataGrid(DataGridViewCondition.EmployeeTab);        
    }

    private void DeleteEmployee_Click(object sender, EventArgs e)
    {
        ClearTextBoxes();        
        _context.Open();
        _employeeRep.Delete(_employee.ID);
        _employees = _employeeRep.GetAll(10, 0);
        _computers = _computerRep.Filter(0,10);
        _context.Close();
        deleteEmployee.Enabled = false;
        updateEmployee.Enabled = false;
        RefreshDataGrid(DataGridViewCondition.EmployeeTab);
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
        var employee = _employeeRep.GetEmployees(nameTextBox.Text, phoneTextBox.Text);
        if (employee.Count > 0)
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
        _employees = _employeeRep.GetAll(10, 0);
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
        if (mainFormTabPage.SelectedIndex == (int)DataGridViewCondition.EmployeeTab && e.RowIndex >= 0)
        {
            var row = dgv.Rows[e.RowIndex];            
            _employee = _employeeRep.GetByID((uint)row.Cells[0].Value)!;
            nameTextBox.Text = _employee?.Name;
            phoneTextBox.Text = _employee?.Phone;
            position.Text = _employee?.Position.ToString();
            updateEmployee.Enabled = true;
            deleteEmployee.Enabled = true;
        }
        else if (mainFormTabPage.SelectedIndex == (int)DataGridViewCondition.DeviceTab && e.RowIndex >= 0)
        {
            var row = dgv.Rows[e.RowIndex];
            _computer = _computerRep.GetByID((uint)row.Cells[0].Value);
            getComputer.Enabled = true;
            deleteComputer.Enabled = true;
        }
        _context.Close();
    }

    private void GetComputer_Click(object sender, EventArgs e)
    {
        
        var form = new ComputerForm(_context, _computer);
        form.ShowDialog();
    }

    private void DeleteComputer_Click(object sender, EventArgs e)
    {
        _context.Open();
        _computerRep.Delete(_computer!.ID);
        deleteComputer.Enabled = false;
        getComputer.Enabled = false;
        _computers = _computerRep.Filter(0, 10);
        _context.Close();
        RefreshDataGrid(DataGridViewCondition.DeviceTab);
    }

    private void CreateComputer_Click(object sender, EventArgs e)
    {
        var form = new ComputerForm(_context);
        form.ShowDialog();
    }

    private void RefreshDbState_Click(object sender, EventArgs e)
    {
        _context.Open();
        _computers = _computerRep.Filter(0, 10);
        _context.Close();
        RefreshDataGrid(DataGridViewCondition.DeviceTab);
    }

    private void ApplyFilters_Click(object sender, EventArgs e)
    {
        _context.Open();
        string? nameFilter = null;
        var statusFilter = status.SelectedIndex;
        if (!string.IsNullOrWhiteSpace(computerName.Text))
        {
            nameFilter = computerName.Text;
        }                   
        if (statusFilter < 0)
        {
            statusFilter = 0;
        }
        decimal.TryParse(price.Text, out decimal priceFilter);
        uint.TryParse(employeeID.Text, out uint empIDFilter);        
        _filredComputers = _computerRep.Filter(0,10, nameFilter, priceFilter, statusFilter, empIDFilter);
        _context.Close();
        RefreshDataGrid(DataGridViewCondition.FilterTab);
    }  
}
