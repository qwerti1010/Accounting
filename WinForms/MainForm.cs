using DBLibrary;
using DBLibrary.Entities;
using DBLibrary.Entities.DTOs;
using DesktopClientServices;
using DesktopClientServices.Responses;
using Microsoft.OpenApi.Extensions;

namespace Accounting;

public partial class MainForm : Form
{
    private readonly EmployeeDTO _employee;
    private EmployeeDTO _selectedEmployee = null!;
    private ComputerDTO _computer = null!;
    private readonly EmpService _empService;
    private readonly CompService _compService;
    
    public MainForm(EmployeeDTO employee, HttpClient httpClient, EmpService empService)
    {
        _employee = employee;        
        InitializeComponent();
        _empService = empService;
        _compService = new CompService(httpClient);
        mainFormTabPage.SelectTab(nameof(tabPage2));
    }

    private async void MainForm_Load(object sender, EventArgs e)
    {
        Text = "Привет, " + _employee.Name;
        dgv.DataSource = await _empService.GetAllAsync();
        status.Items.AddRange(Enum.GetValues<Status>()
                                  .Select(st => st.GetAttributeOfType<DescriptionAttribute>().Description)                                  
                                  .ToArray());
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

    private async void TabPage_Selecting(object sender, TabControlCancelEventArgs e)
    {
        switch (e.TabPageIndex)
        {
            case 0:
                {
                    dgv.DataSource = await _compService.GetAllAsync();
                    break;
                }
            case 1:
                {
                    dgv.DataSource = await _empService.GetAllAsync();                    
                    break;
                }
            case 2:
                {
                    dgv.DataSource = await _compService.GetFilteredAsync(computerName.Text, price.Text, status.Text, employeeID.Text);
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
                    position.Text = _selectedEmployee.Position;
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
            case "User":
                {
                    updateEmployee.Visible = false;
                    deleteEmployee.Visible = false;
                    addEmployee.Visible = false;
                    addComputer.Visible = false;
                    deleteComputer.Visible = false;
                    break;
                }
            case "Moderator":
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
    private async void GetComputer_Click(object sender, EventArgs e)
    {
        var employees = await _empService.GetAllAsync();
        employees ??= new List<EmployeeDTO>();
        var form = new ComputerForm(_compService, employees, _computer);
        form.ShowDialog();
    }

    private async void DeleteComputer_Click(object sender, EventArgs e)
    {
        var response = await _compService.DeleteAsync(_computer.ID);
        MessageBox.Show(response);
        deleteComputer.Enabled = false;
        getComputer.Enabled = false;
        dgv.DataSource = await _compService.GetAllAsync();
    }

    private async void CreateComputer_Click(object sender, EventArgs e)
    {
        var employees = await _empService.GetAllAsync();
        employees ??= new List<EmployeeDTO>();
        var form = new ComputerForm(_compService, employees);
        form.ShowDialog();
    }

    private async void ApplyFilters_Click(object sender, EventArgs e)
    {
        dgv.DataSource = await _compService.GetFilteredAsync(computerName.Text, price.Text, status.Text, employeeID.Text);
    }

    private async void UpdateDb_ClickAsync(object sender, EventArgs e)
    {
        dgv.DataSource = await _compService.GetAllAsync();
    }


    //надо починить список свойств
    private ComputerDTO GetComputerOfDataGridView(int rowIndex)
    {
        return new ComputerDTO
        {
            ID = (uint)dgv["ID", rowIndex].Value,
            Name = dgv["Name", rowIndex].Value.ToString(),
            RegistrationDate = (DateTime)dgv["RegistrationDate", rowIndex].Value,
            Price = (decimal)dgv["Price", rowIndex].Value,
            Status = dgv["Status", rowIndex].Value.ToString(),
            EmployeeID = (uint)dgv["EmployeeID", rowIndex].Value,
            ExploitationStart = (DateTime)dgv["ExploitationStart", rowIndex].Value,
            Properties = (IList<PropertyDTO>)dgv["Properties", rowIndex].Value
        };
    }

    private async void Next_Click(object sender, EventArgs e)
    {
        dgv.DataSource = await _compService.GetNextAsync();
    }

    private async void Previous_Click(object sender, EventArgs e)
    {
        dgv.DataSource = await _compService.GetPreviousAsync();
    }
    #endregion


    #region EmployeeRegion
    private async void UpdateEmployee_Click(object sender, EventArgs e)
    {
        if (_selectedEmployee != null)
        {
            _selectedEmployee.Name = nameTextBox.Text;
            _selectedEmployee.Phone = phoneTextBox.Text;
            _selectedEmployee.Position = position.Text;
            _selectedEmployee.Login = login.Text;
            var response = await _empService.UpdateAsync(_selectedEmployee);
            MessageBox.Show(response.Message);
            if (response.IsSuccess)
            {
                dgv.DataSource = await _empService.GetAllAsync();
            }
        }
    }

    private async void DeleteEmployee_Click(object sender, EventArgs e)
    {
        ClearTextBoxes();
        if (_selectedEmployee != null)
        {
            var response = await _empService.DeleteAsync(_selectedEmployee.ID);
            MessageBox.Show(response);
        }
        deleteEmployee.Enabled = false;
        updateEmployee.Enabled = false;
        dgv.DataSource = await _empService.GetAllAsync();
    }

    
    private void CreateEmployee_Click(object sender, EventArgs e)
    {
        var form = new RegistrationForm(_empService);
        form.Show();        
    }

    private EmployeeDTO GetEmpOfDataGridView(int rowIndex)
    {
        return new EmployeeDTO
        {
            ID = (uint)dgv["ID", rowIndex].Value,
            Name = dgv["Name", rowIndex].Value.ToString(),
            Phone = dgv["Phone", rowIndex].Value.ToString(),
            Position = dgv["Position", rowIndex].Value.ToString(),
            Login = dgv["Login", rowIndex].Value.ToString(),
        };
    }

    private async void PreviousEmp_ClickAsync(object sender, EventArgs e)
    {
        dgv.DataSource = await _empService.GetPreviousAsync();
    }

    private async void NextEmp_ClickAsync(object sender, EventArgs e)
    {
        dgv.DataSource = await _empService.GetNextAsync();
    }    
    #endregion
}
