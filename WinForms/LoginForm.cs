
using DBLibrary;
using DBLibrary.Entities;
using Services;

namespace Accounting;

public partial class LoginForm : Form
{
    private readonly LoginService _loginService;
    private readonly DbContext _context;
    
    public LoginForm()
    {
        InitializeComponent();
        _context = new DbContext();
        _loginService = new LoginService(_context);
    }

    private void SignUp_Click(object sender, EventArgs e)
    {        
        
        if (!_loginService.IsEmployeeExist(loginTextBox.Text))
        {
            MessageBox.Show("Неверный логин");
            return;
        }
        else if (!_loginService.IsPasswordValid(passTextBox.Text))
        {
            MessageBox.Show("Неверный пароль");
            return;
        }

        _loginService.AddVisit();
        
        var form = new MainForm(_loginService.Employee!, _context);
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
