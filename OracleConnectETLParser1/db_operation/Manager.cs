using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common.CommandTrees;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OracleConnectETLParser1.db_operation
{
    public class Manager
    {
        public List<DbObject> ListOfObjects;                   // MAIN list of DbObjects
        public List<DbObject> RelateDbObjects;                 // list of related objects - this list is filled by listOfRelatedObjects method
        public void print_TableNameList()
        {
            DbConnector databaza = new DbConnector();
            databaza.Open();
            OracleCommand oraCmd = new OracleCommand();
            oraCmd.Connection = databaza.OraConnection;
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
            DbConnector db = new DbConnector();
            db.Open();
            OracleCommand oraCmd = new OracleCommand();
            oraCmd.Connection = db.OraConnection;
            oraCmd.CommandText = "select object_name, object_type from ALL_OBJECTS WHERE Owner ='"+db.DbOwner+"'";
            OracleDataReader dr = oraCmd.ExecuteReader();
            this.ListOfObjects = new List<DbObject>();
            while (dr.Read())
            {
                ListOfObjects.Add(new DbObject(dr.GetString(0), dr.GetString(1)));
            }
            // some cycle will be necessary here - later
            SetNextLevel(2);
            SetNextLevel(3);
            // - END cycle
            setListOfRelatedObjects();
            db.Close();
        }
        /*
         * Quite complicated method for DbObjects level setting.
         */
        private void SetNextLevel(int newLevel)                        
        {
            for (int i = 0; i < ListOfObjects.Count; i++)                                   // all objects
            {
                if (ListOfObjects[i].TableLevel==-1)                                        // ==-1 (only unleveled DbObjects expected)
                {
                    for (int j = 0; j < ListOfObjects[i].ReferencedObjectNames.Count; j++)       // reference objects for previous object
                    {
                    if (GetDbObjectLevel(ListOfObjects[i].ReferencedObjectNames[j])<newLevel)    // checking if referenced object had less level than main object
                        {
                            ListOfObjects[i].TableLevel = newLevel;
                        }
                    else
                        {
                            ListOfObjects[i].TableLevel = -1;
                        }
                    }
                }
            }
        }

        /*
         * Get of DBObject level.
         * return: int - level
         * param: pname - DbObjectName
         */
        private int GetDbObjectLevel(string pname)                  // when I need return DB_object.level matched by pname
        {
           //DB_Object someoDbObject= new DB_Object();
            for (int i = 0; i < ListOfObjects.Count; i++)
            {
                if (ListOfObjects[i].Name==pname)
                {
                    return ListOfObjects[i].TableLevel;
                }
            }
            return 999;
        }
        /*
         * Method return DbObject from List<> ListOfObjects based on pname - name ob object
         */
        private DbObject GetDbObjectFromListOfObjects(string pname)
        {
            for (int i = 0; i < ListOfObjects.Count; i++)
            {
                if (ListOfObjects[i].Name == pname)
                {
                    return ListOfObjects[i];
                }
            }
            return null;
        }

        /*
         * Method will set list of all related objects for selected Object in param
         * - this methot should rewrite atribute in object from DB_object class
        */

        private void setListOfRelatedObjects()                           
        {
            // prejdem vsetky objekty v liste X - main list
            // pre vsetky objekty mena listu X.related urob
            // do X.ReferencedDbObjects=GetDbObjectFromListOfObjects(x.related[actual])
            for (int i = 0; i < ListOfObjects.Count; i++)
            {
                for (int j = 0; j < ListOfObjects[i].ReferencedObjectNames.Count; j++)
                {
                    ListOfObjects[i].ReferencedDbObjects.Add(GetDbObjectFromListOfObjects(ListOfObjects[i].ReferencedObjectNames[j]));
                }
            }
        }

    }
}
