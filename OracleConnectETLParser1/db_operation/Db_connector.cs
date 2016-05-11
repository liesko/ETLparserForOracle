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
    public class Db_connector
    {
        public OracleConnection oraConnection = new OracleConnection(@"");
        public void Open()
        {
            if (oraConnection.State != ConnectionState.Open)
            {
                oraConnection.Open();
            }
        }
        public void Close()
        {
            if (oraConnection.State == ConnectionState.Open)
            {
                oraConnection.Close();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
