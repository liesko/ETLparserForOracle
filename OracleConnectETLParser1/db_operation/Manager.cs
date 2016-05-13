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
        public List<DB_Object> ListOfObjects;
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
            oraCmd.CommandText = "select object_name, object_type from ALL_OBJECTS WHERE Owner ='"+databaza.DB_owner+"'";
            OracleDataReader dr = oraCmd.ExecuteReader();
            this.ListOfObjects = new List<DB_Object>();
            while (dr.Read())
            {
                ListOfObjects.Add(new DB_Object(dr.GetString(0), dr.GetString(1)));
            }
            setNextLevel(2);
            setNextLevel(3);
            databaza.Close();
        }
        private void setNextLevel(int newLevel)                         // setting of others levels
        {
            for (int i = 0; i < ListOfObjects.Count; i++)               // all objects
            {
                if (ListOfObjects[i].tableLevel==-1)                    // except level!=-1
                {
                    for (int j = 0; j < ListOfObjects[i].referencedObject.Count; j++)
                    {
                    if (getDbObjectLevel(ListOfObjects[i].referencedObject[j])<newLevel)
                        {
                            ListOfObjects[i].tableLevel = newLevel;
                        }
                    else
                        {
                            ListOfObjects[i].tableLevel = -1;
                        }
                    }
                }
            }
        }

        private int getDbObjectLevel(string pname)                  // when I need return DB_object.level matched by pname
        {
           //DB_Object someoDbObject= new DB_Object();
            for (int i = 0; i < ListOfObjects.Count; i++)
            {
                if (ListOfObjects[i].name==pname)
                {
                    return ListOfObjects[i].tableLevel;
                }
            }
            return 999;
        }
        
         /*
          * Method will return list of all related objects for selected Object in param
          * - it should return List<Db_Object>...
         */
        private void listOfRelatedObjects(string objectName)                           
        {
            // code here
        }
    }
}
