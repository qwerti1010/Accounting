
using DBLibrary;
using DBLibrary.Entities;
using Services.Services;

namespace Accounting;

public partial class LoginForm : Form
{
    private readonly EmployeeService _loginService;
    private readonly DbContext _context;
    
    public LoginForm()
    {
        InitializeComponent();
        _context = new DbContext();
        _loginService = new EmployeeService(_context);
    }

    private void SignUp_Click(object sender, EventArgs e)
    {
        var status = _loginService.Login(loginTextBox.Text, passTextBox.Text);
        if (!status.IsSuccess)
        {
            MessageBox.Show(status.Message);
            return;
        }
        var form = new MainForm(status.Employee!, _context);
        form.ShowDialog();
        Hide();
    }

    private void Registration_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        var form = new RegistrationForm(_context);
        form.ShowDialog();
    }

    private void LoginForm_Load(object sender, EventArgs e)
    {

    }
}
