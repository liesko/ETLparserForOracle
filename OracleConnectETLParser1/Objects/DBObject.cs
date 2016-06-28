using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;
using OracleConnectETLParser1.Controllers;
using OracleConnectETLParser1.Settings;

namespace OracleConnectETLParser1.Objects
{
    public enum DbObjectType
    {
        View,
        MaterializedView,
        Table,
        Trigger,
        Procedure,
        Function,
        Sequence,
        Unknown
    };
    public class DbObject
    {  
        public string Name { get; private set; }
        public List<string> ReferencedNames { get; set; }  
        public List<DbObject> ReferencedObjects { get; set; } 
        public List<Column> Columns { get; protected set; }     
        public int Level { get; set; } 
        public int Cardinality { get; protected set; }  
        public string Owner { get; private set; }
        public  DbObjectType DbObjectTypeName { get; private set; }        

        public DbObject(string name, string owner, DbConnector db, 
            DbObjectType paramDbObjectTypeName = DbObjectType.Unknown)
        {
            Name = name;
            Owner = owner;
            DbObjectTypeName = paramDbObjectTypeName;
            ReferencedNames = new List<string>();
            ReferencedObjects = new List<DbObject>();
            AddReferenceObjects(db);
            AddBaseLevel(db);
        }

        public void AddBaseLevel(DbConnector db)
        {
            //db.Open();
            OracleCommand oraCmd = new OracleCommand
            {
                Connection = db.OraConnection,
                CommandText = "select object_name from ALL_OBJECTS " +
                              "WHERE 1 = 1 and " +
                              "Owner = '" + db.DbOwner + "' and " +
                              "OBJECT_NAME = '" + Name + "' and " +
                              "object_name not in (select name from ALL_DEPENDENCIES group by name)"
            };
            OracleDataReader dr = oraCmd.ExecuteReader();
            while (dr.Read())
            {
                if (dr.GetString(0) == Name)
                {
                    Level = 1;
                }
            }
            if (Level != 1)
            {
                Level = -1;
            }
           // db.Close();
        }

        private void AddReferenceObjects(DbConnector db)
        {
           // db.Open();
            OracleCommand oraCmd = new OracleCommand
            {
                Connection = db.OraConnection,
                CommandText = Selects.SelectReferenced +
                              "OWNER = '" + db.DbOwner + "' and " +
                              "REFERENCED_NAME != '" + Name + "' and " +
                              "name ='" + Name + "' "
            };
            OracleDataReader dr = oraCmd.ExecuteReader();

            while (dr.Read())
            {
                ReferencedNames.Add(dr.GetString(0));
            }
          //  db.Close();
        }
    }
}