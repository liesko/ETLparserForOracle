using System;
using System.Data;
using System.Data.SqlClient;
using Oracle.ManagedDataAccess.Client;

namespace OracleConnectETLParser1.Controllers
{
    public class DbConnector
    {

        /*
         * Oracle, MSSQL
         */
        public string DatabaseType{ get; private set; }
        public OracleConnection OraConnection = new OracleConnection(@"Data Source=localhost:1521/xe; User ID=liesko; Password=potter");
        public SqlConnection SqlConnection= new SqlConnection(@"Data Source = SK1A991C; Initial Catalog = master; Integrated Security = True");
        public string DbOwner = "LIESKO";

        public DbConnector(string databaseType)
        {
            DatabaseType = databaseType;
        }

        public void Open()
        {
            switch (DatabaseType)
            {
                case "Oracle":
                    if (OraConnection.State != ConnectionState.Open)
                    {
                        OraConnection.Open();
                    }
                    break;
                case "MSSQL":
                    if (SqlConnection.State!=ConnectionState.Open)
                    {
                        SqlConnection.Open();
                    }
                    break;
            }
        }
        public void Close()
        {
            switch (DatabaseType)
            {
                case "Oracle":
                    if (OraConnection.State == ConnectionState.Open)
                    {
                        OraConnection.Close();
                    }
                    break;
                case "MSSQL":
                    if (SqlConnection.State == ConnectionState.Open)
                    {
                        SqlConnection.Open();
                    }
                    break;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
