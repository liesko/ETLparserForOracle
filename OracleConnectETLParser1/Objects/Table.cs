using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;
using OracleConnectETLParser1.Controllers;
using OracleConnectETLParser1.Settings;

namespace OracleConnectETLParser1.Objects
{
    public class Table : DbObject
    {
        public Table(string name, string owner, DbConnector db): base(name, owner, db)
        {
            Columns = new List<Column>();
            AddColumns(db);
            AddCardinality(db);
        }
 
        public void AddColumns(DbConnector db)
        {
           // db.Open();
            OracleCommand oraCmd = new OracleCommand
            {
                Connection = db.OraConnection,
                CommandText = Settings.Selects.SelectColumnsForTable + Name + "'"
            };

            OracleDataReader dr = oraCmd.ExecuteReader();

            while (dr.Read())
            {
                var isNullable = dr.GetString(2) == "Y" ? true : false;
                var newColumn = new Column(dr.GetString(0), dr.GetString(1), isNullable);
                Columns.Add(newColumn);
            }
            //db.Close();
        }

        private void AddCardinality(DbConnector db)
        {
            int value = 0;
           // db.Open();
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
          //  db.Close();
            Cardinality = value;
        }
    }
}
