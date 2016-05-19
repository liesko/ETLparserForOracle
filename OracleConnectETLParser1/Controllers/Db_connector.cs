using System;
using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace OracleConnectETLParser1.Controllers
{
    public class DbConnector
    {

        public OracleConnection OraConnection = new OracleConnection(@"Data Source=localhost:1521/xe; User ID=liesko; Password=potter");
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
