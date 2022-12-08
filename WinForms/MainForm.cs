using DBLibrary;
using DBLibrary.Entities;
using Services.Services;
using System.Windows.Forms;

namespace Accounting;

public partial class MainForm : Form
{
    private readonly Employee _employee;
    private Employee _selectedEmployee = null!;
    private Computer _computer = null!;   
    private readonly EmployeeService _employeeService;
    private readonly ComputerService _computerService;
    private int _computersPage;
    private int _employeesPage;
    
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
        EmployeesToDataGridView(10, 0);
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

    private void TabPage_Selecting(object sender, TabControlCancelEventArgs e)
    {
        switch (e.TabPageIndex)
        {
            case 0:
                {
                    dgv.DataSource = _computerService.GetComputers(10, 0);
                    break;
                }
            case 1:
                {
                    EmployeesToDataGridView(10, _employeesPage * 10);                    
                    break;
                }
            case 2:
                {
                    ComputerToDataGridView();
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
                    _selectedEmployee = GetEmpOfDataGridView(e.RowIndex);
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
                    _computer = GetComputerOfDataGridView(e.RowIndex);
                    getComputer.Enabled = true;
                    deleteComputer.Enabled = true;
                    break;
                }
        }
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

    #region ComputerRegion
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

    private Computer GetComputerOfDataGridView(int rowIndex)
    {
        var c = new Computer();
        var x = dgv["Properties", rowIndex].Value;
        return new Computer
        {
            Properties = new List<Property>()
        };
    }

    private void ComputerToDataGridView()
    {
        dgv.DataSource = _computerService.GetComputers(10, 0);
        dgv.Columns["isDeleted"].Visible = false;
        dgv.Columns["Properties"].Visible = false;
    }

    private void Next_Click(object sender, EventArgs e)
    {
        var count = _computerService.Count();
        if (_computersPage * 10 > count - 10) return;
        dgv.DataSource = _computerService.GetComputers(10, ++_computersPage * 10);

    }

    private void Previous_Click(object sender, EventArgs e)
    {
        if (_computersPage == 0) return;
        dgv.DataSource = _computerService.GetComputers(10, --_computersPage * 10);
    }
    #endregion


    #region EmployeeRegion
    private void UpdateEmployee_Click(object sender, EventArgs e)
    {
        if (_selectedEmployee != null)
        {
            _selectedEmployee.Name = nameTextBox.Text;
            _selectedEmployee.Phone = phoneTextBox.Text;
            _selectedEmployee.Position = (PositionEnum)position.SelectedIndex;
            _selectedEmployee.Login = login.Text;
            var status = _employeeService.Update(_selectedEmployee);
            MessageBox.Show(status.Message);
            EmployeesToDataGridView(10, 0);
        }
    }

    private void DeleteEmployee_Click(object sender, EventArgs e)
    {
        ClearTextBoxes();
        if (_selectedEmployee != null)
        {
            _employeeService.Delete(_selectedEmployee.ID);
        }
        deleteEmployee.Enabled = false;
        updateEmployee.Enabled = false;
        EmployeesToDataGridView(10,0);
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
        EmployeesToDataGridView(10, 0);
    }

    private Employee GetEmpOfDataGridView(int rowIndex)
    {
        return new Employee
        {
            ID = (uint)dgv["ID", rowIndex].Value,
            Name = dgv["Name", rowIndex].Value.ToString(),
            Phone = dgv["Phone", rowIndex].Value.ToString(),
            Position = (PositionEnum)dgv["Position", rowIndex].Value,
            Login = dgv["Login", rowIndex].Value.ToString(),
            Password = dgv["Password", rowIndex]?.Value?.ToString(),
            IsDeleted = (bool)dgv["IsDeleted", rowIndex].Value
        };
    }

    private void PreviousEmp_Click(object sender, EventArgs e)
    {
        if (_employeesPage == 0) return;
        dgv.DataSource = _employeeService.GetEmployees(10, --_employeesPage * 10);
    }

    private void NextEmp_Click(object sender, EventArgs e)
    {
        var count = _employeeService.Count();
        if (_employeesPage * 10 > count - 10) return;
        dgv.DataSource = _employeeService.GetEmployees(10, ++_employeesPage * 10);
    }

    public void EmployeesToDataGridView(int take, int skip)
    {
        dgv.DataSource = _employeeService.GetEmployees(take, skip);
        dgv.Columns["Password"].Visible = false;
        dgv.Columns["isDeleted"].Visible = false;
        _employeesPage = 0;
    }
    #endregion
}
