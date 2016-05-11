using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OracleConnectETLParser1.db_operation
{
    public class Selecter
    {
        public static void print_TableNameList()
        {
            Db_connector databaza = new Db_connector();
            databaza.Open();
            OracleCommand oraCmd = new OracleCommand();
            oraCmd.Connection = databaza.oraConnection;
            oraCmd.CommandText = "select table_name from tabs";
            oraCmd.CommandType = CommandType.Text;
            OracleDataReader dr = oraCmd.ExecuteReader();
            dr.Read();
            Console.WriteLine(dr.GetString(0));
            while (dr.Read())
            {
                Console.WriteLine(dr.GetString(0));
            }
            databaza.Close();
        }
    }
}
