using DBLibrary;
using DBLibrary.Entities;
using DBLibrary.Interfaces;

namespace Accounting;

public partial class LoginForm : Form
{
    private IEmployeeRepository _employeeRep;
    private IVisitRepository _visitRep;
    private DbContext _context;

    public LoginForm()
    {
        _context = new DbContext();
        _employeeRep = new EmployeeRep(_context);
        _visitRep = new VisitRep(_context);
        InitializeComponent();
    }

    private void SignUp_Click(object sender, EventArgs e)
    {
        _context.Open();
        var employee = _employeeRep.GetByLogin(loginTextBox.Text);
        
        if (employee is null)
        {
            MessageBox.Show("Неверный логин");
            _context.Close();
            return;
        }

        if (employee.Password != passTextBox.Text)
        {
            MessageBox.Show("Неверный пароль");
            _context.Close();
            return;
        }

        _visitRep.Create(new Visit
        {
            EmployeeID = employee.ID,
            VisitTime = DateTime.UtcNow
        });
        _context.Close();
        var form = new MainForm(employee, _context);
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
