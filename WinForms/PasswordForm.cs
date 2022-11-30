using DBLibrary.Entities;
using Services.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Accounting
{
    public partial class PasswordForm : Form
    {
        private readonly EmployeeService _employeeService;
        private readonly Employee _employee;
        public PasswordForm(Employee employee, EmployeeService employeeService)
        {
            _employee = employee;
            _employeeService = employeeService;
            InitializeComponent();
        }

        private void Send_Click(object sender, EventArgs e)
        {
            _employee.Password = password.Text;
            var status = _employeeService.Update(_employee);
            MessageBox.Show(status.Message);
            if (status.IsSuccess)
            {
                Close();
            }

        }
    }
}
