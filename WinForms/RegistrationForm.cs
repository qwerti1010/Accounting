using DBLibrary;
using DBLibrary.Entities;
using DBLibrary.Entities.DTOs;
using DesktopClientServices;

namespace Accounting;

public partial class RegistrationForm : Form
{
    private readonly EmpService _service;

    public RegistrationForm(EmpService service)
    {
        _service = service;
        InitializeComponent();
    }

    private void Close_Click(object sender, EventArgs e)
    {
        Close();
    }

    private async void Send_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(password.Text))
        {
            MessageBox.Show("Пустой пароль");
            return;
        }

        var response = await _service.RegistrationAsync(name.Text, phone.Text, login.Text, password.Text);
        MessageBox.Show(response.Message);
        if (response.IsSuccess)
        {
            Close();
        } 
    }

    private void RegistrationForm_Load(object sender, EventArgs e)
    {

    }
}
