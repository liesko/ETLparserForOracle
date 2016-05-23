using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OracleConnectETLParser1.Settings
{
    public static class Selects
    {
        /*
         * Queries in order:
         *      1. Oracle
         *      2. MS-SQL
         *      3. POSTGRES
         *      4. others......
         */

        // select all objects
        public const string SelectObjects = "select object_name, object_type, owner from ALL_OBJECTS WHERE Owner ='";


        // select columns for selected table
        public const string SelectColumnsForTable = "SELECT COLUMN_NAME, DATA_TYPE, NULLABLE FROM user_tab_columns where table_name='";        
        public const string SelectColumnsForTableMSSQL = "SELECT COLUMN_NAME, DATA_TYPE, IS_NULLABLE FROM information_schema.columns where TABLE_NAME ='";

        // select columns for selected view
        public const string SelectColumnsForViews = "SELECT COLUMN_NAME, DATA_TYPE, NULLABLE FROM user_tab_columns where table_name='";

        // select cardinality of table
        public const string SelectCountFrom = "SELECT COUNT(*) from ";
        
        // select for referenced objects 
        public const string SelectReferenced = "SELECT REFERENCED_NAME from ALL_DEPENDENCIES where 1 = 1 and ";
    }
}
