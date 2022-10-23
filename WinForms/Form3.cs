using MySqlConnector;

namespace Accounting.WinForms
{
    public partial class Form3 : Form
    {
        Computer computer = new Computer();
        public Form3()
        {
            InitializeComponent();
        }

        public Form3(Computer computer)
        {            
            this.computer = computer;            
            InitializeComponent();
        }

        private void close_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            SetComboBoxItems("SELECT name FROM employee", employee);
            SetComboBoxItems("SELECT location FROM computers", location);
            SetComboBoxItems("SELECT status FROM computers", status);
            if (computer.Name == null)
            {
                Text = "Добавить новое устройство";
                update.Text = "Добавить";
                enableRedact.Visible = false;
                return;
            }

            Text = $"Просмотр информации устройства. Номер в базе - {computer.Id}";
            ChangeState();
            name.Text = computer.Name;
            location.Text = computer.Location;
            status.Text = computer.Status;
            employee.Text = computer.Employee;
            regNumber.Text = computer.RegNumber;
            regDate.Text = computer.RegDate.ToShortDateString();
            price.Text = computer.Price.ToString();
            producer.Text = computer.Producer.ToString();
            cpu.Text = computer.Processor.ToString();
            coresCount.Text = computer.CoresCount.ToString();
            ram.Text = computer.RAM.ToString();
            graphicsCard.Text = computer.GraphicsCard.ToString();
            memory.Text = computer.Memory.ToString();
            bodySize.Text = computer.BodySize.ToString();
            explStart.Text = computer.RegDate.ToString();
            amortPeriod.Text = computer.AmortPeriod.ToString();
        }

        private void enableRedact_CheckStateChanged(object sender, EventArgs e)
        {
            ChangeState();            
        }

        private void ChangeState()
        {
            foreach (var control in Controls)
            {
                switch (control)
                {
                    case TextBox tb:
                        tb.ReadOnly = !tb.ReadOnly;
                        break;
                    case ComboBox cb:
                        cb.Enabled = !cb.Enabled;
                        break;
                    case DateTimePicker dtp:
                        dtp.Enabled = !dtp.Enabled;
                        break;
                }
            }
            update.Visible = !update.Visible;
        }

        private void update_Click(object sender, EventArgs e)
        {
            try
            {
                var insertCommand = "INSERT INTO computers(name, registerNumber, registrationDate," +
                " price, producer, cpu, numberOfCores, ram, graphicsCard, status, employee, location," +
                " bodySize, explutationStart, amortisationPeriod, memory)" +
                " VALUES (@n,@rn,@rd,@p,@prod,@c,@numc,@ra,@gc,@s,@emp,@l,@bs,@exp,@am,@m)";
                var updateCommand = "UPDATE computers SET name=@n, registerNumber=@rn, registrationDate=@rd," +
                    " price=@p, producer=@prod, cpu=@c, numberOfCores=@numc, ram=@ra, graphicsCard=@gc," +
                    " status=@s, employee=@emp, location=@l, bodySize=@bs, explutationStart=@exp," +
                    " amortisationPeriod=@am, memory=@m WHERE computerId = @id";

                using (var db = new DbContext().GetConnection())
                {
                    db.Open();
                    var command = new MySqlCommand();
                    command.Parameters.Add("@n", MySqlDbType.VarChar).Value = name.Text;
                    command.Parameters.Add("@rn", MySqlDbType.VarChar).Value = regNumber.Text;
                    command.Parameters.Add("@rd", MySqlDbType.DateTime).Value = regDate.Text;
                    command.Parameters.Add("@p", MySqlDbType.Decimal).Value = price.Text;
                    command.Parameters.Add("@prod", MySqlDbType.VarChar).Value = producer.Text;
                    command.Parameters.Add("@c", MySqlDbType.VarChar).Value = cpu.Text;
                    command.Parameters.Add("@numc", MySqlDbType.Int32).Value = coresCount.Text;
                    command.Parameters.Add("@ra", MySqlDbType.Int32).Value = ram.Text;
                    command.Parameters.Add("@gc", MySqlDbType.VarChar).Value = graphicsCard.Text;
                    command.Parameters.Add("@s", MySqlDbType.VarChar).Value = status.Text;
                    command.Parameters.Add("@emp", MySqlDbType.VarChar).Value = employee.Text;
                    command.Parameters.Add("@l", MySqlDbType.VarChar).Value = location.Text;
                    command.Parameters.Add("@bs", MySqlDbType.Double).Value = bodySize.Text;
                    command.Parameters.Add("@exp", MySqlDbType.DateTime).Value = explStart.Text;
                    command.Parameters.Add("@am", MySqlDbType.Int32).Value = amortPeriod.Text;
                    command.Parameters.Add("@m", MySqlDbType.Double).Value = memory.Text;
                    //(@n,@rn,@rd,@p,@prod,@c,@numc,@ra,@gc,@s,@emp,@l,@bs,@exp,@am,@m)
                    command.Connection = db;
                    if (computer.Name == null)
                        command.CommandText = insertCommand;
                    else
                    {
                        command.Parameters.Add("@id", MySqlDbType.Int32).Value = computer.Id;
                        command.CommandText = updateCommand;
                    }
                    command.ExecuteNonQuery();
                    Close();
                }
            }
            catch
            {
                MessageBox.Show("Некорректные данные");
            }
        }

        private void SetComboBoxItems(string commandStr, ComboBox box)
        {
            commandStr += " WHERE isDeleted = 0";
            using(var db = new DbContext().GetConnection())
            {
                db.Open();
                var command = new MySqlCommand(commandStr, db);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    box.Items.Add(reader.GetString(0));
                }
            }
        }
    }
}
