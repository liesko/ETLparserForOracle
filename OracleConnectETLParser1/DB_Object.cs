using Oracle.ManagedDataAccess.Client;
using OracleConnectETLParser1.db_operation;
using System.Collections.Generic;

namespace OracleConnectETLParser1
{
    public class DB_Object
    {
        public string name;                     // table name
        public string type;                     // type of object e.g. PROCEDURE, TABLE, VIEW, TRIGGER, etc.
        public List<string> referencedObject;   // referenced objects
        public List<string> columns;            // list of columns will be created only for tables and views
        public int tableLevel;                  // level in ETL hierarchy (0-base level, -1-undefined level, 1-first level)              
        public DB_Object(string name, string type)  // Constructor
        {
            this.name = name;
            this.type = type;
            this.referencedObject= new List<string>();
            this.columns = new List<string>();
            if (this.type== "TABLE" || this.type == "VIEW" || this.type == "MATERIALIZED VIEW")
            {
                this.columns = setColumns(this.name);
            }
        }
        /*
         * Private method - creation list of columns for each: table, view
         */
        private List<string> setColumns(string table_name)
        {
            Db_connector databaza = new Db_connector();
            databaza.Open();
            OracleCommand oraCmd = new OracleCommand();
            oraCmd.Connection = databaza.oraConnection;
            oraCmd.CommandText = "SELECT COLUMN_NAME from user_tab_columns where table_name='"+table_name+"'";
            OracleDataReader dr = oraCmd.ExecuteReader();
            List<string> somelist = new List<string>();
            while (dr.Read())
            {
                somelist.Add(dr.GetString(0));
            }
            databaza.Close();
            return somelist;
        }

        private int setBaseLevel(string table_name)
        {
            Db_connector databaza = new Db_connector();
            databaza.Open();
            OracleCommand oraCmd = new OracleCommand();
            oraCmd.Connection = databaza.oraConnection;
            oraCmd.CommandText = "SELECT COLUMN_NAME from user_tab_columns where table_name='" + table_name + "'";
            OracleDataReader dr = oraCmd.ExecuteReader();
            List<string> somelist = new List<string>();
            while (dr.Read())
            {
                somelist.Add(dr.GetString(0));
            }
            databaza.Close();
            return 0;
        }
    }
}