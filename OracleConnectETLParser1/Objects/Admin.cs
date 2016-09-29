using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OracleConnectETLParser1.Controllers;

namespace OracleConnectETLParser1.Objects
{
    public class Admin
    {
        public List<Manager> ListOfManagers = new List<Manager>();

        public Admin()
        {
            
            ListOfManagers.Add(new Manager(NewConnector("Oracle", "DOD_DB", "DOD_DB", "", "potter", "1521", "xe", "", "", "localhost", "", "", "")));
            ListOfManagers.Add(new Manager(NewConnector("Oracle", "LIESKO", "LIESKO", "", "potter", "1521", "xe", "", "", "localhost", "", "", "")));
      
        }

        public DbConnector NewConnector(string databaseType = "xxx",
                                        string dbowner = "xxx",
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
            return new DbConnector(databaseType,dbowner,connectionName,userName,password,port,sid,serviceName,jdbc_url,hostName,connectionName,databaseType,initialCatalog,integratedSucurity);
        }
    }
}
