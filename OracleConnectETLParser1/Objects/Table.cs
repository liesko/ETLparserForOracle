using System;
using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;
using OracleConnectETLParser1.Controllers;
using OracleConnectETLParser1.Settings;

namespace OracleConnectETLParser1.Objects
{
    public class Table : DbObject
    {
        public string Comment { get; private set; }
        public Table(string name, string owner, DbConnector db, string objectId, string creationTime, string lastChangedTime, DbObjectType DbObjectTypeName) : base(name, owner, db, objectId, creationTime, lastChangedTime, DbObjectTypeName)
        {
            Columns = new List<Column>();
            AddColumns(db);
            AddCardinality(db);
            Comment = SetTableComment(db, owner, name);
        }
 
        public void AddColumns(DbConnector db)
        {
            OracleCommand oraCmd = new OracleCommand
            {
                Connection = db.OraConnection,
                CommandText = Settings.Selects.SelectColumnsForTable + Name + "'"
            };

            OracleDataReader dr = oraCmd.ExecuteReader();

            while (dr.Read())
            {
                var isNullable = dr.GetString(2) == "Y" ? true : false;
                var comment = SetColumnComment(db, Owner, dr.GetString(0), Name);
                var newColumn = new Column(dr.GetString(0), dr.GetString(1), isNullable, comment);
                Columns.Add(newColumn);
            }
        }

        private void AddCardinality(DbConnector db)
        {
            int value = 0;
            OracleCommand oraCmd = new OracleCommand
            {
                Connection = db.OraConnection,
                CommandText = Selects.SelectCountFrom + Name
            };
            OracleDataReader dr = oraCmd.ExecuteReader();
            while (dr.Read())
            {
                value = dr.GetInt32(0);
            }
            Cardinality = value;
        }

        private string SetColumnComment(DbConnector db, string owner, string column_name, string table_name)
        {
            string value="no comment";
            OracleCommand oraCmd = new OracleCommand
            {
                Connection = db.OraConnection,
                CommandText = Selects.SelectColumnsComment(owner,table_name,column_name)
            };
            OracleDataReader dr = oraCmd.ExecuteReader();
            try
            {
                while (dr.Read())
                {
                    value = dr.GetString(0);
                }
            }
            catch (Exception)
            {
                // sorry for this kind of code...
            }
            return value;
        }

        private string SetTableComment(DbConnector db, string owner, string table_name)
        {
            string value = "no comment";
            OracleCommand oraCmd = new OracleCommand
            {
                Connection = db.OraConnection,
                CommandText = Selects.SelectTableOrViewComment(owner,table_name)
            };
            OracleDataReader dr = oraCmd.ExecuteReader();
            try
            {
                while (dr.Read())
                {
                    value = dr.GetString(0);
                }
            }
            catch (Exception)
            {
                // sorry for this kind of code...
            }
            return value;
        }
    }
}
