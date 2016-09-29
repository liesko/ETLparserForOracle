using OracleConnectETLParser1.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using OracleConnectETLParser1.Objects;

namespace Tester
{
    class Program
    {
        static void Main(string[] args)
        {            
            DbConnector db = new DbConnector("Oracle");
            //DbConnector db = new DbConnector("MSSQL");
            Admin test_admin=new Admin();
            test_admin.NewConnector();
            
            /*
            Manager test = new Manager();
            tet.CreateDB_Objects(db);
            */
            Console.WriteLine("xxxxxxxxxxxxxxxxxxxxxxxxxx");
            Console.ReadKey();
        }
    }
}
