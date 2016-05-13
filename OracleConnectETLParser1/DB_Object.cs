using System;
using Oracle.ManagedDataAccess.Client;
using OracleConnectETLParser1.db_operation;
using System.Collections.Generic;

namespace OracleConnectETLParser1
{
    public class DbObject
    {
        public string Name;                     // table name
        public string Type;                     // type of object e.g. PROCEDURE, TABLE, VIEW, TRIGGER, etc.
        public List<string> ReferencedObject;   // referenced objects
        public List<Column> ColumnsList;        // list of columns will be created only for tables and views, previous list will be deprecated
        public int TableLevel;                  // level in ETL hierarchy (1-base level, -1-undefined level, other levels: 2,3,4)    
        public int Cardinality;                 // cardinality value for tables and views          
        
         /*
         * Constructor
         */
        public DbObject(string name, string type)  
        {
            this.Name = name;
            this.Type = type;
            this.ReferencedObject= new List<string>();
            this.ColumnsList=new List<Column>();
            if (this.Type== "TABLE" || this.Type == "VIEW" || this.Type == "MATERIALIZED VIEW")
            {
               // this.columns = setColumns(this.name);
                this.ColumnsList=SetColumns(this.Name);
                this.Cardinality = SetCardinaliityValue(this.Name);
            }
            SetBaseLevel(this.Name);            // this method set only tableLevel=1 for base tables, for other tables tableLevel=-1
            if (this.TableLevel==-1)
            {
                ReferencedObject=SetReferenceObjects(this.Name);
            }
        }
        /*
         * Private method - creation list of columns for each: table, view
         */
        private List<Column> SetColumns(string tableName)
        {
            DbConnector db = new DbConnector();
            db.Open();
            OracleCommand oraCmd = new OracleCommand();
            oraCmd.Connection = db.OraConnection;
            oraCmd.CommandText = "SELECT COLUMN_NAME, DATA_TYPE, NULLABLE FROM user_tab_columns where table_name='" + tableName+"'";
            OracleDataReader dr = oraCmd.ExecuteReader();
            List<Column> somelist = new List<Column>();
            while (dr.Read())
            {
                bool isNullable = dr.GetString(2) == "Y" ? true : false;
                string coTamBolo = dr.GetString(2);
                Column newColumn = new Column(dr.GetString(0), dr.GetString(1), isNullable);
                somelist.Add(newColumn);
            }
            db.Close();
            return somelist;
        }
        /*
         * if DB object is from base level - this method set level=1 for base objects, for others objects level=-1
         */
        private int SetBaseLevel(string name)
        {
            DbConnector db = new DbConnector();
            db.Open();
            OracleCommand oraCmd = new OracleCommand();
            oraCmd.Connection = db.OraConnection;
            oraCmd.CommandText = "select object_name from ALL_OBJECTS " +
                                 "WHERE 1 = 1 and " +
                                 "Owner = '"+db.DbOwner+"' and " +
                                 "OBJECT_NAME = '"+name+"' and " +
                                 "object_name not in (select name from ALL_DEPENDENCIES group by name)";
            OracleDataReader dr = oraCmd.ExecuteReader();
            while (dr.Read())
            {
                if (dr.GetString(0)==this.Name)
                {
                    this.TableLevel = 1;
                }
            }
            if (this.TableLevel != 1)
            {
                this.TableLevel = -1;
            }
            db.Close();
            return 0;
        }

        /*
         * this method return list of reference objects for DB object in parameter
         */
        private List<string> SetReferenceObjects(string name)
        {
            DbConnector db = new DbConnector();
            db.Open();
            OracleCommand oraCmd = new OracleCommand();
            oraCmd.Connection = db.OraConnection;
            oraCmd.CommandText = "select REFERENCED_NAME from ALL_DEPENDENCIES " +
                                 "where 1 = 1 and " +
                                 "OWNER = '"+db.DbOwner+"' and " +
                                 "REFERENCED_NAME != '"+name+"' and " +
                                 "name ='"+this.Name+"' ";
            OracleDataReader dr = oraCmd.ExecuteReader();
            List<string> somelist = new List<string>();
            while (dr.Read())
            {
                somelist.Add(dr.GetString(0));
            }
            db.Close();
            return somelist;
        }

        /*
         * Method returns cardinality value for table/view name in the parameter
         */
        private int SetCardinaliityValue(string name)
        {
            int cardinalityvalue = 0;
            DbConnector db = new DbConnector();
            db.Open();
            OracleCommand oraCmd = new OracleCommand();
            oraCmd.Connection = db.OraConnection;
            oraCmd.CommandText = "SELECT COUNT(*) from " + name;
            OracleDataReader dr = oraCmd.ExecuteReader();
            while (dr.Read())
            {
                cardinalityvalue = dr.GetInt32(0);
            }
            db.Close();
            return cardinalityvalue;
        }
    }
}