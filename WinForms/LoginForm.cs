
using DBLibrary;
using Services.Services;

namespace Accounting;

public partial class LoginForm : Form
{
    private readonly EmployeeService _employeeService;
    private readonly DbConnect _context;
    private readonly DBService _dBService;
    
    public LoginForm()
    {
        InitializeComponent();
        _context = new DbConnect();
        _employeeService = new EmployeeService(_context);
        _dBService = new DBService(_context);
    }

    private void SignUp_Click(object sender, EventArgs e)
    {
        var pass = EmployeeService.HashPassword(passTextBox.Text);
        var status = _employeeService.Login(loginTextBox.Text, pass);
        if (!status.IsSuccess || status.Employee == null)
        {
            MessageBox.Show(status.Message);
            return;
        }
        if (status.Employee.Password == null)
        {
            var passForm = new PasswordForm(status.Employee, _employeeService);
            passForm.ShowDialog();
            return;
        }
        var form = new MainForm(status.Employee, _context);
        form.ShowDialog();
        Hide();
    }

    private void Registration_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        var form = new RegistrationForm(_context);
        form.ShowDialog();
    }

    private void CreateDB_Click(object sender, EventArgs e)
    {
        MessageBox.Show(_dBService.Create());
    }
    private void LoginForm_Load(object sender, EventArgs e)
    {

    }
}
