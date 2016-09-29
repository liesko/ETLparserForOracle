using System;
using System.Data;
using System.Data.SqlClient;
using Oracle.ManagedDataAccess.Client;

namespace OracleConnectETLParser1.Controllers
{
    public class DbConnector
    {

        /*
         * Oracle
         * MSSQL
         */

        //public OracleConnection OraConnection = new OracleConnection(@"Data Source=localhost:1521/xe; User ID=turok; Password=turok");
        //public OracleConnection OraConnection = new OracleConnection(@"Data Source=localhost:1521/xe; User ID=liesko; Password=potter");
        //public OracleConnection OraConnection = new OracleConnection(@"Data Source=localhost:1521/xe; User ID=DOD_DB; Password=potter");
        public OracleConnection OraConnection;
        public SqlConnection SqlConnection= new SqlConnection(@"Data Source = SK1A991C; Initial Catalog = master; Integrated Security = True");
        //public string DbOwner = "TUROK";
        //------------------------------------------
        public string DatabaseType { get; set; }
        public string DbOwner { get; set; }
        public string ConnectionName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Port { get; set; }
        public string SID { get; set; }
        public string ServiceName { get; set; }
        public string JDBC_URL { get; set; }
        public string HostName { get; set; }
        public string ConnectionLink { get; set; }
        public string DataSource { get; set; }
        public string InitialCatalog { get; set; }
        public string IntegratedSucurity { get; set; }
        

        public DbConnector( string databaseType = "xxx",
                            string dbowner="xxx",
                            string connectionName = "xxx",
                            string userName = "xxx",
                            string password = "xxx",
                            string port = "xxx",
                            string sid = "xxx",
                            string serviceName = "xxx",
                            string jdbc_url = "xxx",
                            string hostName = "xxx",
                            string connectionLink = "xxx",
                            string dataSource = "xxx",
                            string initialCatalog = "xxx",
                            string integratedSucurity = "xxx"
                           )
        {
            DatabaseType = databaseType;
            DbOwner = dbowner;
            ConnectionName = connectionName;
            UserName = userName;
            Password = password;
            Port = port;
            SID = sid;
            ServiceName = serviceName;
            JDBC_URL = jdbc_url;
            HostName = hostName;
            ConnectionLink = connectionLink;
            DataSource = dataSource;
            InitialCatalog = initialCatalog;
            IntegratedSucurity = integratedSucurity;
            OraConnection= new OracleConnection(@"Data Source="+this.HostName+":"+this.Port+"/"+this.SID+"; " +
                                                "User ID="+ this.DbOwner + "; Password="+this.Password);
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
                        SqlConnection.Close();
                    }
                    break;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
