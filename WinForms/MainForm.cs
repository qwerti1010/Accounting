using DBLibrary;

namespace Accounting;

public partial class MainForm : Form
{
    private Employee _employee;
    private Computer _computer;
    private List<Employee> _employees;
    private List<Computer> _computers;
    private EmployeeRep _employeeRep;
    private ComputerRep _computerRep;
    private List<(string, string)> _empColumns;
    private List<(string, string)> _computerColumns;
    private DbContext _dbContext;

    public MainForm(Employee employee)
    {
        _dbContext = new DbContext(); 
        _employee = employee;
        _employeeRep = new EmployeeRep(_dbContext);
        _computerRep = new ComputerRep(_dbContext);
        InitializeComponent();
        tabPage.SelectTab(nameof(tabPage2));
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
                ("registerNumber", "Инвертарный номер"),
                ("registrationDate", "Дата постановки на учет"),
                ("price", "Цена"),
                ("producer", "Производитель"),
                ("cpu", "Процессор"),
                ("numberOfCores", "Количество ядер"),
                ("ram", "Объем оперативной памяти"),
                ("graphicsCard", "Видеокарта"),
                ("status", "Статус"),
                ("employee","Сотрудник"),
                ("location","Место"),
                ("bodySize","Размер корпуса"),
                ("explutationStart","Начало эксплуатации"),
                ("amortisationPeriod","Период амортизации"),
                ("memory","Память")
            };
    }

    private void MainForm_Load(object sender, EventArgs e)
    {
        Text = "Привет, " + _employee.Name;
        _dbContext.Open();
        _employees = _employeeRep.GetAll();
        _computers = _computerRep.GetAll();
        _dbContext.Close();
        CreateColumns(_empColumns);
        RefreshDataGrid();
    }

    public void CreateColumns(List<(string, string)> columnsNames)
    {
        dgv.Columns.Clear();
        foreach (var name in columnsNames)
        {
            dgv.Columns.Add(name.Item1, name.Item2);
        }
    }

    public void ReadSingleRow(Employee employee)
    {
        dgv.Rows.Add(employee.ID, employee.Name, employee.Position, employee.Phone);
    }

    public void ReadSingleRow(Computer computer)
    {
        dgv.Rows.Add(computer.ID, computer.Name, computer.RegNumber, computer.RegDate,
            computer.Price, computer.Producer, computer.Processor, computer.CoresCount, computer.RAM,
            computer.GraphicsCard, computer.Status, computer.Employee, computer.Location, computer.BodySize,
            computer.ExplDate, computer.AmortPeriod, computer.Memory);
    }

    public void RefreshDataGrid()
    {
        dgv.Rows.Clear();
        if (dgv.ColumnCount == 4)
        {
            foreach (var e in _employees)
            {
                ReadSingleRow(e);
            }
        }
        else
        {
            foreach (var c in _computers)
            {
                ReadSingleRow(c);
            }
        }
    }

    public void ClearTextBoxes()
    {
        nameTextBox.Clear();
        positionTextBox.Clear();
        phoneTextBox.Clear();
    }

    private void MainForm_Closed(object sender, FormClosedEventArgs e)
    {
        Application.Exit();
    }
    
    private void UpdateEmployee_Click(object sender, EventArgs e)
    {
        _dbContext.Open();
        if (String.IsNullOrEmpty(nameTextBox.Text) || String.IsNullOrEmpty(phoneTextBox.Text)
            || String.IsNullOrEmpty(positionTextBox.Text))
        {
            MessageBox.Show("Поля не могут быть пустыми");
            _dbContext.Close();
            return;
        }
        try
        {            
            _employee.Name = nameTextBox.Text;
            _employee.Phone = phoneTextBox.Text;
            _employee.Position = positionTextBox.Text;
            _employeeRep.Update(_employee);            
        }
        catch
        {
            MessageBox.Show("Неверные данные");
        }
        ClearTextBoxes();
        deleteEmployee.Enabled = false;
        updateEmployee.Enabled = false;
        _employees = _employeeRep.GetAll();
        RefreshDataGrid();
        _dbContext.Close();
    }

    private void DeleteEmployee_Click(object sender, EventArgs e)
    {
        ClearTextBoxes();        
        _dbContext.Open();
        _employeeRep.Delete(_employee.ID);        
        deleteEmployee.Enabled = false;
        updateEmployee.Enabled = false;
        _employees = _employeeRep.GetAll();
        RefreshDataGrid();
        _dbContext.Close();
    }

    private void CreateEmployee_Click(object sender, EventArgs e)
    {
        _dbContext.Open();
        try
        {
            var employee = new Employee();
            employee.Name = nameTextBox.Text;
            employee.Position = positionTextBox.Text;
            employee.Phone = phoneTextBox.Text;
            _employeeRep.Create(employee);
        }
        catch
        {
            MessageBox.Show("Некоректные данные");
        }
        _employees = _employeeRep.GetAll();
        ClearTextBoxes();
        RefreshDataGrid();
        _dbContext.Close();
    }    

    private void TabPage_Selecting(object sender, TabControlCancelEventArgs e)
    {
        switch (e.TabPageIndex)
        {
            case 0:
                {
                    CreateColumns(_computerColumns);
                    RefreshDataGrid();
                    break;
                }
            case 1:
                {
                    CreateColumns(_empColumns);
                    RefreshDataGrid();
                    break;
                }
        }
    }

    private void DataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
    {
        
        if (tabPage.SelectedIndex == 1 && e.RowIndex >= 0)
        {
            var row = dgv.Rows[e.RowIndex];
            _employee = _employees.First(e => e.ID == (uint)row.Cells[0].Value);
            nameTextBox.Text = _employee.Name;
            phoneTextBox.Text = _employee.Phone;
            positionTextBox.Text = _employee.Position;
            updateEmployee.Enabled = true;
            deleteEmployee.Enabled = true;
        }
        if (tabPage.SelectedIndex == 0 && e.RowIndex >= 0)
        {
            var row = dgv.Rows[e.RowIndex];
            _computer = _computers.First(c => c.ID == (uint)row.Cells[0].Value);
            getComputer.Enabled = true;
            deleteComputer.Enabled = true;
        }
    }

    private void GetComputer_Click(object sender, EventArgs e)
    {
        var form = new ComputerForm(_computer, _computers);
        form.ShowDialog();
    }

    private void DeleteComputer_Click(object sender, EventArgs e)
    {
        _dbContext.Open();
        _computerRep.Delete(_computer.ID);
        deleteComputer.Enabled = false;
        getComputer.Enabled = false;
        _computers = _computerRep.GetAll();
        RefreshDataGrid();
        _dbContext.Close();
    }

    private void CreateComputer_Click(object sender, EventArgs e)
    {
        var form = new ComputerForm(_computers);
        form.ShowDialog();
    }

    private void RefreshDbState_Click(object sender, EventArgs e)
    {
        _dbContext.Open();
        _computers = _computerRep.GetAll();
        _dbContext.Close();
        RefreshDataGrid();
    }
}
