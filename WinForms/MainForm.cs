using DBLibrary;
using DBLibrary.Entities;
using Services.Services;

namespace Accounting;

public partial class MainForm : Form
{
    private readonly Employee _employee;
    private Employee? _selectedEmployee;
    private Computer _computer = null!;   
    private readonly EmployeeService _employeeService;
    private readonly ComputerService _computerService;
    
    public MainForm(Employee employee, DbConnect context)
    {
        _employee = employee;        
        InitializeComponent();
        _employeeService = new EmployeeService(context);
        _computerService = new ComputerService(context);
        mainFormTabPage.SelectTab(nameof(tabPage2));       
    }

    private void MainForm_Load(object sender, EventArgs e)
    {
        Text = "Привет, " + _employee.Name;
        var emp = _employeeService.GetEmployees(10, 0);
        dgv.DataSource = emp;
        status.Items.AddRange(Enum.GetValues<Status>().Cast<object>().ToArray());
        position.Items.AddRange(Enum.GetValues<PositionEnum>().Cast<object>().ToArray());
        ProvidePositionAccess();
    }    

    private void ClearTextBoxes()
    {
        nameTextBox.Clear();
        phoneTextBox.Clear();
        login.Clear();
    }

    private void MainForm_Closed(object sender, FormClosedEventArgs e)
    {
        Application.Exit();
    }
    
    private void UpdateEmployee_Click(object sender, EventArgs e)
    {
        if(_selectedEmployee != null)
        {
            _selectedEmployee.Name = nameTextBox.Text;
            _selectedEmployee.Phone = phoneTextBox.Text;
            _selectedEmployee.Position = (PositionEnum)position.SelectedIndex;
            var status = _employeeService.Update(_selectedEmployee);
            MessageBox.Show(status.Message);
            dgv.DataSource = _employeeService.GetEmployees(10, 0);
        }
    }

    private void DeleteEmployee_Click(object sender, EventArgs e)
    {
        ClearTextBoxes();  
        if(_selectedEmployee != null)
        {
            _employeeService.Delete(_selectedEmployee.ID);
        }
        deleteEmployee.Enabled = false;
        updateEmployee.Enabled = false;
        dgv.DataSource = _employeeService.GetEmployees(10, 0);
    }

    private void CreateEmployee_Click(object sender, EventArgs e)
    {
        var employee = new Employee
        {
            Name = nameTextBox.Text,
            Phone = phoneTextBox.Text,
            Position = (PositionEnum)position.SelectedIndex,
            Login = login.Text
        };
        
        var responce = _employeeService.Registration(employee);       
        MessageBox.Show(responce.Message);
        if (responce.IsSuccess)
        {
            if (employee.Position == PositionEnum.Admin)
            {
                var form = new PasswordForm(employee, _employeeService);
                form.ShowDialog();
            }
            ClearTextBoxes();
        }
        dgv.DataSource = _employeeService.GetEmployees(10, 0);
    }    

    private void TabPage_Selecting(object sender, TabControlCancelEventArgs e)
    {
        switch (e.TabPageIndex)
        {
            case 0:
                {
                    dgv.DataSource = _computerService.GetComputers(10, 0);
                    computersCount.Text = _computerService.Count();
                    break;
                }
            case 1:
                {
                    dgv.DataSource = _employeeService.GetEmployees(10, 0);                    
                    break;
                }
            case 2:
                {
                    dgv.DataSource = null;
                    break;
                }
            case 3:
                {
                    break;
                }
        }
    }

    private void DataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0) return;

        switch (mainFormTabPage.SelectedIndex)
        {
            case (int)DataGridViewCondition.EmployeeTab:
                {
                    var row = dgv.Rows[e.RowIndex];
                    _selectedEmployee = _employeeService.GetByID((uint)row.Cells[0].Value)!;
                    nameTextBox.Text = _selectedEmployee.Name;
                    phoneTextBox.Text = _selectedEmployee.Phone;
                    position.Text = _selectedEmployee.Position.ToString();
                    login.Text = _selectedEmployee.Login;
                    updateEmployee.Enabled = true;
                    deleteEmployee.Enabled = true;
                    break;
                }
            case (int)DataGridViewCondition.DeviceTab:
                {
                    var row = dgv.Rows[e.RowIndex];
                    _computer = _computerService.GetByID((uint)row.Cells[0].Value);
                    getComputer.Enabled = true;
                    deleteComputer.Enabled = true;
                    break;
                }
        }
    }

    private void GetComputer_Click(object sender, EventArgs e)
    {        
        var form = new ComputerForm(_computerService, _employeeService, _computer);
        form.ShowDialog();
    }

    private void DeleteComputer_Click(object sender, EventArgs e)
    {       
        _computerService.Delete(_computer.ID);
        deleteComputer.Enabled = false;
        getComputer.Enabled = false;
        dgv.DataSource = _computerService.GetComputers(10, 0);
    }

    private void CreateComputer_Click(object sender, EventArgs e)
    {
        var form = new ComputerForm(_computerService, _employeeService);
        form.ShowDialog();
    }

    private void ApplyFilters_Click(object sender, EventArgs e)
    {        
        dgv.DataSource = _computerService.GetComputers(10, 0,
            computerName.Text, price.Text, status.SelectedIndex, employeeID.Text);
    }

    private void UpdateDb_Click(object sender, EventArgs e)
    {
        dgv.DataSource = _computerService.GetComputers(10, 0);
    }

    public void ProvidePositionAccess()
    {
        switch (_employee.Position)
        {
            case PositionEnum.User:
                {
                    updateEmployee.Visible = false;
                    deleteEmployee.Visible = false;
                    addEmployee.Visible = false;
                    addComputer.Visible = false;
                    deleteComputer.Visible = false;
                    break;
                }
            case PositionEnum.Moderator:
                {
                    deleteEmployee.Visible = false;
                    addEmployee.Visible = false;
                    position.Enabled = false;
                    addComputer.Visible = false;
                    deleteComputer.Visible = false;
                    break;
                }
        } 
    }   
}
