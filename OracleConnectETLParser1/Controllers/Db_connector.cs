using System;
using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace OracleConnectETLParser1.Controllers
{
    public class DbConnector
    {
<<<<<<< HEAD:OracleConnectETLParser1/db_operation/Db_connector.cs
        public OracleConnection OraConnection = new OracleConnection(@"Data Source=localhost:1521/xe; User ID=liesko; Password=trileptal");
        public string DbOwner = "LIESKO";
=======
        public OracleConnection OraConnection = new OracleConnection(@"Data Source=localhost:1521/xe; User ID=ivka; Password=11591951");
        public string DbOwner = "IVKA";
>>>>>>> cac8aa3d456abbec905cc8a0a097da1db75f1ee0:OracleConnectETLParser1/Controllers/Db_connector.cs

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
