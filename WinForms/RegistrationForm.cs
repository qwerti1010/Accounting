using DBLibrary;
using DBLibrary.Entities;
using System.Text.RegularExpressions;

namespace Accounting;

public partial class RegistrationForm : Form
{
    private readonly DbContext _context;
    private readonly EmployeeRep _employeeRep;
    public RegistrationForm(DbContext context)
    {
        _context = context;
        _employeeRep = new EmployeeRep(_context);
        InitializeComponent();
    }

    private void Close_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void Send_Click(object sender, EventArgs e)
    {        
        if(!TextBoxesArentEmpty())
        {
            MessageBox.Show("Форма не заполнена");
            return;
        }

        var regex = new Regex(@"^[+]7\d{10}$").IsMatch(phone.Text);        
        if (!regex)
        {
            MessageBox.Show("Неверный формат номера телефона");
            return;
        }

        if (password.Text != confirmation.Text)
        {
            MessageBox.Show("Неверный пароль");
            return;
        }

        _context.Open();
        var employees = _employeeRep.GetEmployees(1,0, name.Text, phone.Text, login.Text);
        if (employees.Count > 0)
        {
            MessageBox.Show("Неверные данные");
            _context.Close();
            return;
        }

        _employeeRep.Create(new Employee
        {
            Name = name.Text,
            Phone = phone.Text,
            Login = login.Text,
            Position = (PositionEnum)position.SelectedIndex,
            Password = password.Text
        });
        MessageBox.Show("Регистрация успешно завершена");
        _context.Close();
        Close();
    }

    private bool TextBoxesArentEmpty()
    {
        return !(string.IsNullOrWhiteSpace(phone.Text) || string.IsNullOrWhiteSpace(name.Text)
            || string.IsNullOrWhiteSpace(position.Text) || string.IsNullOrWhiteSpace(login.Text)
            || string.IsNullOrWhiteSpace(password.Text));
    }

    private void RegistrationForm_Load(object sender, EventArgs e)
    {
        position.Items.AddRange(Enum.GetValues<PositionEnum>().Cast<object>().ToArray());
    }
}
