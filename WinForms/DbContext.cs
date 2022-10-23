
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting
{
    internal class DbContext
    {
        MySqlConnection connection;

        public DbContext()
        {
            connection = new MySqlConnection("server=localhost;port=3307;user=root;database=accountingdb;password=root;");
        }

        public MySqlConnection GetConnection() =>
            connection;
    }
}
