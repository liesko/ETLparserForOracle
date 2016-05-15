using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;
using OracleConnectETLParser1.Controllers;
using OracleConnectETLParser1.Settings;

namespace OracleConnectETLParser1.Objects
{
    public class View : DbObject
    {
        private bool IsMaterialized;
        public View(string name) : base(name)
        {
            Columns = new List<Column>();
            AddColumns();
            AddCardinality();
        }

        public View(string name, bool isMaterialized) : base(name)
        {
            Columns = new List<Column>();
            AddColumns();
            AddCardinality();
            IsMaterialized = isMaterialized;
        }

        public void AddColumns()
        {
            DbConnector db = new DbConnector();
            db.Open();

            OracleCommand oraCmd = new OracleCommand
            {
                Connection = db.OraConnection,
                CommandText = Settings.Selects.SelectColumnsForViews + Name + "'"
            };

            OracleDataReader dr = oraCmd.ExecuteReader();

            while (dr.Read())
            {
                var isNullable = dr.GetString(2) == "Y" ? true : false;
                var newColumn = new Column(dr.GetString(0), dr.GetString(1), isNullable);
                Columns.Add(newColumn);
            }
            db.Close();
        }

        private void AddCardinality()
        {
            int value = 0;
            DbConnector db = new DbConnector();
            db.Open();
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
            db.Close();
            Cardinality = value;
        }
    }
}
