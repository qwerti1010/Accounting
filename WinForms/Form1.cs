
using DB;

namespace Accounting
{
    public partial class Form1 : Form
    {
        private string? _login;
        public Form1()
        {
            InitializeComponent();
        }

        private void signUp_Click(object sender, EventArgs e)
        {
            

            var login = loginTextBox.Text;
            var pass = passTextBox.Text;

            var dbContext = new DbContext().GetConnection();
            var userRep = new EmployeeRep();
            var user = userRep.GetItems(login);

            //var command = new MySqlCommand("SELECT EmployeeId FROM employee " +
            //                               "WHERE Login = @log AND Password = @pass AND isDeleted = 0", db);
            //command.Parameters.Add("@log", MySqlDbType.VarChar).Value = login;
            //command.Parameters.Add("@pass", MySqlDbType.VarChar).Value = pass;

            var result = command.ExecuteScalar()?.ToString();
            if (result == null)
            {
                MessageBox.Show("Wrong login or password");
            }
            else
            {
                this._login = loginTextBox.Text;
                var form = new Form2(this._login);
                Hide();
                form.ShowDialog();                                        
            }
        }
    }
}