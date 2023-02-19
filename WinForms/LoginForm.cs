using DBLibrary;
using DesktopClientServices;

namespace Accounting;

public partial class LoginForm : Form
{
    private readonly HttpClient _httpClient;
    private EmpService _service;
    public LoginForm(HttpClient client)
    {
        InitializeComponent();
        _httpClient = client;
        _service = new EmpService(_httpClient);        
    }    

    private void Registration_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        var form = new RegistrationForm(_service);
        form.ShowDialog();
    }

    private async void CreateDB_Click(object sender, EventArgs e)
    {
        var result = await _service.CreateDb();
        MessageBox.Show(result);
    }
    private void LoginForm_Load(object sender, EventArgs e)
    {
       
    }

    private async void SignUp_Click(object sender, EventArgs e)
    {
        var response = await _service.LoginAsync(loginTextBox.Text, passTextBox.Text);

        if (!response.IsSuccess)
        {
            MessageBox.Show(response.Message);
            return;
        }

        MessageBox.Show(response.Message);
        var form = new MainForm(response.Value!, _httpClient, _service);
        form.Show();
        Hide();
    }
}
