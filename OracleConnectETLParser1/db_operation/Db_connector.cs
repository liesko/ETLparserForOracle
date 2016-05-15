using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;

namespace OracleConnectETLParser1.db_operation
{
    public class DbConnector
    {
        public OracleConnection OraConnection = new OracleConnection(@"Data Source=localhost:1521/xe; User ID=liesko; Password=trileptal");
        public string DbOwner = "LIESKO";

        public void Open()
        {
            if (OraConnection.State != ConnectionState.Open)
            {
                OraConnection.Open();
            }
        }
        public void Close()
        {
            if (OraConnection.State == ConnectionState.Open)
            {
                OraConnection.Close();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
