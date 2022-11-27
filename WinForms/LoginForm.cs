
using DBLibrary;
using Services.Services;

namespace Accounting;

public partial class LoginForm : Form
{
    private readonly EmployeeService _loginService;
    private readonly DbContext _context;
    private readonly DBService _dBService;
    
    public LoginForm()
    {
        InitializeComponent();
        _context = new DbContext();
        _loginService = new EmployeeService(_context);
        _dBService = new DBService(_context);
    }

    private void SignUp_Click(object sender, EventArgs e)
    {
        var status = _loginService.Login(loginTextBox.Text, passTextBox.Text);
        if (!status.IsSuccess || status.Employee == null)
        {
            MessageBox.Show(status.Message);
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
