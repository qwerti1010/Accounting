using DBLibrary.Entities;
using Services.Services;

namespace Accounting;

public partial class PasswordForm : Form
{
    private readonly EmployeeService _employeeService;
    private readonly Employee _employee;
    public PasswordForm(Employee employee, EmployeeService employeeService)
    {
        _employee = employee;
        _employeeService = employeeService;
        InitializeComponent();
    }

    private void Send_Click(object sender, EventArgs e)
    {
        _employee.Password = EmployeeService.HashPassword(password.Text);
        var status = _employeeService.Update(_employee);
        MessageBox.Show(status.Message);
        if (status.IsSuccess)
        {
            Close();
        }

    }

    private void PasswordForm_Load(object sender, EventArgs e)
    {

    }
}
