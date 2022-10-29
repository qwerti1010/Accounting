using DBLibrary;

namespace Accounting;

public partial class LoginForm : Form
{
    private EmployeeRep _employeeRep;
    private DbContext _dbContext;
    public LoginForm()
    {
        _dbContext = new DbContext();
        _employeeRep = new EmployeeRep(_dbContext);
        InitializeComponent();
    }

    private void SignUp_Click(object sender, EventArgs e)
    {
        _dbContext.Open();
        var employee = _employeeRep.GetByLogin(loginTextBox.Text);
        _dbContext.Close();
        if (employee.ID == 0)
        {
            MessageBox.Show("Неверный логин");
            return;
        }

        if (employee.Password != passTextBox.Text)
        {
            MessageBox.Show("Неверный пароль");
            return;
        }
        var form = new MainForm(employee);
        form.ShowDialog();
        Hide();
    }    
}
