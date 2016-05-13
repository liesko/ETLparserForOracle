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
        public int tableLevel;                  // level in ETL hierarchy (1-base level, -1-undefined level, other levels: 2,3,4)              
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
            setBaseLevel(this.name);            // this method set only tableLevel=1 for base table, for others table tableLevel=-1
            if (this.tableLevel==-1)
            {
                referencedObject=setReferenceObjects(this.name);
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
        /*
         * if DB object is from base level - this method set level=1 for base objects, for others objects level=-1
         */
        private int setBaseLevel(string name)
        {
            Db_connector databaza = new Db_connector();
            databaza.Open();
            OracleCommand oraCmd = new OracleCommand();
            oraCmd.Connection = databaza.oraConnection;
            oraCmd.CommandText = "select object_name from ALL_OBJECTS " +
                                 "WHERE 1 = 1 and " +
                                 "Owner = '"+databaza.DB_owner+"' and " +
                                 "OBJECT_NAME = '"+name+"' and " +
                                 "object_name not in (select name from ALL_DEPENDENCIES group by name)";
            OracleDataReader dr = oraCmd.ExecuteReader();
            while (dr.Read())
            {
                if (dr.GetString(0)==this.name)
                {
                    this.tableLevel = 1;
                }
            }
            if (this.tableLevel != 1)
            {
                this.tableLevel = -1;
            }
            databaza.Close();
            return 0;
        }

        /*
         * this method return list of reference objects for DB object in parameter
         */
        private List<string> setReferenceObjects(string name)
        {
            Db_connector databaza = new Db_connector();
            databaza.Open();
            OracleCommand oraCmd = new OracleCommand();
            oraCmd.Connection = databaza.oraConnection;
            oraCmd.CommandText = "select REFERENCED_NAME from ALL_DEPENDENCIES " +
                                 "where 1 = 1 and " +
                                 "OWNER = '"+databaza.DB_owner+"' and " +
                                 "REFERENCED_NAME != '"+name+"' and " +
                                 "name ='"+this.name+"' ";
            OracleDataReader dr = oraCmd.ExecuteReader();
            List<string> somelist = new List<string>();
            while (dr.Read())
            {
                somelist.Add(dr.GetString(0));
            }
            databaza.Close();
            return somelist;
        }
    }
}