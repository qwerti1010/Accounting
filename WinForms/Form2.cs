using Accounting.WinForms;
using MySqlConnector;
using System.Data;

namespace Accounting
{
    public partial class Form2 : Form
    {
        int empID;
        string login;
        List<(string, string)> empColumns;
        List<(string, string)> computerColumns;
        string commandToEmp = "SELECT employeeId, name, position, phone FROM employee WHERE isDeleted = 0";
        string commandToComputers = "SELECT * FROM computers WHERE isDeleted = 0";
        Computer computer = new Computer();

        public Form2(string login)
        {
            InitializeComponent();
            tabPage.SelectTab(nameof(tabPage2));
            empColumns = new List<(string, string)>
            {
                ("employeeId", "Id"),
                ("name", "Имя"),
                ("position", "Должность"),
                ("phone","Номер телефона")
            };
            computerColumns = new List<(string, string)>
            {
                ("computerId", "Id"),
                ("name", "Наименование"),
                ("registerNumber", "Инвертарный номер"),
                ("registrationDate", "Дата постановки на учет"),
                ("price", "Цена"),
                ("producer", "Производитель"),
                ("cpu", "Процессор"),
                ("numberOfCores", "Количество ядер"),
                ("ram", "Объем оперативной памяти"),
                ("graphicsCard", "Видеокарта"),
                ("status", "Статус"),
                ("employee","Сотрудник"),
                ("location","Место"),
                ("bodySize","Размер корпуса"),
                ("explutationStart","Начало эксплуатации"),
                ("amortisationPeriod","Период амортизации"),
                ("memory","Память")
            };
            this.login = login;
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            SetHeadText();
            CreateColumns(empColumns);
            RefreshDataGrid(commandToEmp);
        }

        //
        public void CreateColumns(List<(string, string)> columnsNames)
        {
            dataGridView1.Columns.Clear();
            foreach (var name in columnsNames )
                dataGridView1.Columns.Add(name.Item1, name.Item2);                       
        }

        public void ReadSingleRow(IDataRecord record)
        {
            if (dataGridView1.ColumnCount == 4)
                dataGridView1.Rows.Add(record.GetInt32(0), record.GetString(1), record.GetString(2), record.GetString(3));
            else
                dataGridView1.Rows.Add(record.GetInt32(0), record.GetString(1),
                    record.GetString(2), record.GetDateTime(3), record.GetDecimal(4), record.GetString(5),
                    record.GetString(6), record.GetInt32(7), record.GetInt32(8), record.GetString(9),
                    record.GetString(10), record.GetString(11), record.GetString(12), record.GetDouble(13),
                    record.GetDateTime(14), record.GetInt32(15), record.GetDouble(16));
        } 

        public void RefreshDataGrid(string comandStr)
        {
            dataGridView1.Rows.Clear();            
            using(var db = new DbContext().GetConnection())
            {
                db.Open();
                var command = new MySqlCommand(comandStr, db);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ReadSingleRow(reader);
                }
                reader.Close();
            }  
        }    
        
        public void ClearTextBoxes()
        {
            nameTextBox.Clear();
            positionTextBox.Clear();
            phoneTextBox.Clear();
        }        

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void updateEmployee_Click(object sender, EventArgs e)
        {
            if (empID <= 0 || string.IsNullOrWhiteSpace(nameTextBox.Text) 
                || string.IsNullOrWhiteSpace(positionTextBox.Text) || string.IsNullOrWhiteSpace(phoneTextBox.Text))
            {
                MessageBox.Show("Поля не могут быть пустыми");
                return;
            }
                
            var commandStr = "UPDATE employee SET name = @n, position = @pos, phone = @ph WHERE employeeId = @id";

            using(var db = new DbContext().GetConnection())
            {
                db.Open();
                var command = new MySqlCommand(commandStr, db);
                command.Parameters.Add("@id", MySqlDbType.Int32).Value = empID;
                command.Parameters.Add("@n", MySqlDbType.VarChar).Value = nameTextBox.Text;
                command.Parameters.Add("@pos", MySqlDbType.VarChar).Value = positionTextBox.Text;
                command.Parameters.Add("@ph", MySqlDbType.VarChar).Value = phoneTextBox.Text;
                command.ExecuteNonQuery();
            }
            empID = 0;
            ClearTextBoxes();
            RefreshDataGrid(commandToEmp);
        }

        private void removeEmployee_Click(object sender, EventArgs e)
        {
            
            var commandStr = "UPDATE employee SET isDeleted = 1 WHERE name = @n AND phone = @ph";            
            using(var db = new DbContext().GetConnection())
            {
                db.Open();
                var command = new MySqlCommand(commandStr, db);
                command.Parameters.Add("@n", MySqlDbType.VarChar).Value = nameTextBox.Text;                
                command.Parameters.Add("@ph", MySqlDbType.VarChar).Value = phoneTextBox.Text;
                command.ExecuteNonQuery();
            }
            empID = 0;
            ClearTextBoxes();
            RefreshDataGrid(commandToEmp);
        }

        private void addEmployee_Click(object sender, EventArgs e)
        {            
            if(string.IsNullOrWhiteSpace(nameTextBox.Text) || string.IsNullOrWhiteSpace(positionTextBox.Text)
                || string.IsNullOrWhiteSpace(phoneTextBox.Text))
            {
                MessageBox.Show("Не получилось добавить сотрудника");
                return;
            }
            using (var db = new DbContext().GetConnection())
            {
                db.Open();
                var commStr = $"SELECT employeeId FROM employee WHERE name = @n OR phone = @ph";                
                var command = new MySqlCommand(commStr, db);
                command.Parameters.Add("@n", MySqlDbType.VarChar).Value = nameTextBox.Text;
                command.Parameters.Add("@ph", MySqlDbType.VarChar).Value = phoneTextBox.Text;
                var id = command.ExecuteScalar()?.ToString();
                if (id != null)
                {
                    MessageBox.Show("Такой сотрудник уже есть в базе");
                    return;
                }
            }
            var commandStr = "INSERT INTO employee (name, position, phone) VALUES(@n, @pos, @ph)";
            using (var db = new DbContext().GetConnection())
            {
                db.Open();
                var command = new MySqlCommand(commandStr, db);
                command.Parameters.Add("@n", MySqlDbType.VarChar).Value = nameTextBox.Text;
                command.Parameters.Add("@pos", MySqlDbType.VarChar).Value = positionTextBox.Text;
                command.Parameters.Add("@ph", MySqlDbType.VarChar).Value = phoneTextBox.Text;
                command.ExecuteNonQuery();
            }
            ClearTextBoxes();
            RefreshDataGrid(commandToEmp);
        }

        private void tabPage_Selecting(object sender, TabControlCancelEventArgs e)
        {
            switch (e.TabPageIndex)
            {
                case 0:                    
                    CreateColumns(computerColumns);
                    RefreshDataGrid(commandToComputers);
                    break;
                case 1:
                    ClearTextBoxes();
                    CreateColumns(empColumns);
                    RefreshDataGrid(commandToEmp);
                    break;
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(tabPage.SelectedIndex == 1 && e.RowIndex >= 0)
            {
                var row = dataGridView1.Rows[e.RowIndex];
                empID = (int)row.Cells[0].Value;
                nameTextBox.Text = row.Cells[1].Value.ToString();
                positionTextBox.Text = row.Cells[2].Value.ToString();
                phoneTextBox.Text = row.Cells[3].Value.ToString();
            }
            if (tabPage.SelectedIndex == 0 && e.RowIndex >= 0)
            {
                var row = dataGridView1.Rows[e.RowIndex];
                computer.SetProperties(row);
                getComputer.Enabled = true;
            }
        }

        private void getComputer_Click(object sender, EventArgs e)
        {
            var form = new Form3(computer);
            form.ShowDialog();
        }

        private void removeComputer_Click(object sender, EventArgs e)
        {
            var commandStr = "UPDATE computers SET isDeleted = 1 WHERE computerId = @id";
            using (var db = new DbContext().GetConnection())
            {
                db.Open();
                var command = new MySqlCommand(commandStr, db);
                command.Parameters.Add("@id", MySqlDbType.VarChar).Value = computer.Id;
                command.ExecuteNonQuery();                
            }
            RefreshDataGrid(commandToComputers);
        }

        private void SetHeadText()
        {
            var commandStr = "SELECT name FROM employee WHERE login = @log";
            using(var db = new DbContext().GetConnection())
            {
                db.Open();
                var command = new MySqlCommand(commandStr, db);
                command.Parameters.Add("@log", MySqlDbType.VarChar).Value = login;
                Text = command.ExecuteScalar()!.ToString();
            }
        }

        private void addComputer_Click(object sender, EventArgs e)
        {
            var form = new Form3();
            form.ShowDialog();
        }

        private void refreshDbState_Click(object sender, EventArgs e)
        {
            RefreshDataGrid(commandToComputers);
        }
    }
}
