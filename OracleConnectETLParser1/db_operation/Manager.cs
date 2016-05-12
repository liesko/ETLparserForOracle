using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OracleConnectETLParser1.db_operation
{
    public class Manager
    {
        public void print_TableNameList()
        {
            Db_connector databaza = new Db_connector();
            databaza.Open();
            OracleCommand oraCmd = new OracleCommand();
            oraCmd.Connection = databaza.oraConnection;
            oraCmd.CommandText = "select table_name, 'xxx' from tabs";
            OracleDataReader dr = oraCmd.ExecuteReader();
            while (dr.Read())
            {
                Console.WriteLine(dr.GetString(0)+dr.GetString(1));
            }
            databaza.Close();
        }
        /*
         * Creation of List<DB_object> - all DB objects transformed into Object: DB_Object
         * Objects are stored in List.
         */
        public void createDB_Objects()
        {
            Db_connector databaza = new Db_connector();
            databaza.Open();
            OracleCommand oraCmd = new OracleCommand();
            oraCmd.Connection = databaza.oraConnection;
            oraCmd.CommandText = "select object_name, object_type from ALL_OBJECTS WHERE Owner = 'LIESKO'";
            OracleDataReader dr = oraCmd.ExecuteReader();
            List<DB_Object> list = new List<DB_Object>();
            while (dr.Read())
            {
                list.Add(new DB_Object(dr.GetString(0), dr.GetString(1)));
            }
            databaza.Close();
        }
    }
}
