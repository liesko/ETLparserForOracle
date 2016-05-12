using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Printing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;
using OracleConnectETLParser1.db_operation;

namespace OracleConnectETLParser1
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
           // Application.Run(new Form1());
            Console.WriteLine("xxxxxxxxxxxxxxxxxxxxxxxxxx");           
            Manager test=new Manager();
            test.createDB_Objects();
            Console.ReadKey();
        }
    }
}
