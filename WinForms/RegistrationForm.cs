using DBLibrary;
using DBLibrary.Entities;
using Services.Services;

namespace Accounting;

public partial class RegistrationForm : Form
{
    private readonly EmployeeService _employeeService;
    
    public RegistrationForm(DbConnect context)
    {
        _employeeService = new EmployeeService(context);
        InitializeComponent();
    }

    private void Close_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void Send_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(password.Text))
        {
            MessageBox.Show("Пустой пароль");
            return;
        }

        var employee = new Employee
        {
            Name = name.Text,
            Phone = phone.Text,
            Login = login.Text,
            Password = password.Text,
            Position = PositionEnum.User
        };
        var status = _employeeService.Registration(employee);
        MessageBox.Show(status.Message);
        if (status.IsSuccess)
        {
            Close();
        }
    }
}
