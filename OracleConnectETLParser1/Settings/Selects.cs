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
        public const string SelectObjectsMSSQL = "with objects_cte as(" +
                                                 "select" +
                                                 "o.name," +
                                                 "case type_desc" +
                                                    "when 'SQL_STORED_PROCEDURE'		then    'PROCEDURE'" +
                                                    "when 'VIEW'						then    'VIEW'" +
                                                    "when 'EXTENDED_STORED_PROCEDURE'   then    'PROCEDURE'" +
                                                    "when 'SQL_SCALAR_FUNCTION'		    then    'FUNCTION'" +
                                                    "when 'USER_TABLE'				    then    'TABLE'" +
                                                    "when 'INTERNAL_TABLE'			    then    'TABLE" +
                                                    "when 'SQL_TRIGGER'			        then    'TRIGGER" +
                                                    "when 'SEQUENCE_OBJECT'			    then    'SEQUENCE" +
                                                    "else type_desc" +
                                                 "end as type_desc," +
                                                 "case" +
                                                    "when o.principal_id is null then s.principal_id" +
                                                    "else o.principal_id" +
                                                 "end as principal_id" +
                                                 "from sys.objects o" +
                                                    "inner join sys.schemas s" +
                                                    "on o.schema_id = s.schema_id" +
                                                 "where o.is_ms_shipped = 0" +
                                                         "and o.type in ('U', 'FN', 'FS', 'FT', 'IF', 'P', 'PC', 'TA', 'TF', 'TR', 'V'))" +
                                                 "select" +
                                                    "cte.name as			OBJECT_NAME," +
                                                    "cte.type_desc as	    OBJECT_TYPE," +
                                                    "dp.name as			    OWNER" +
                                                 "from objects_cte cte" +
                                                 "inner join sys.database_principals dp" +
                                                    "on cte.principal_id = dp.principal_id" +
                                                    "where dp.name = '"; //dbo

        // select columns for selected table
        public const string SelectColumnsForTable = "SELECT COLUMN_NAME, DATA_TYPE, NULLABLE FROM user_tab_columns where table_name='";        
        public const string SelectColumnsForTableMSSQL = "SELECT COLUMN_NAME, DATA_TYPE, case IS_NULLABLE when 'YES' then 'Y' when 'NO' then 'N' end NULLABLE FROM information_schema.columns where TABLE_NAME = '";

        // select columns for selected view
        public const string SelectColumnsForViews = "SELECT COLUMN_NAME, DATA_TYPE, NULLABLE FROM user_tab_columns where table_name='";
        public const string SelectColumnsForViewsMSSQL = "SELECT COLUMN_NAME, DATA_TYPE, case IS_NULLABLE when 'YES' then 'Y' when 'NO' then 'N' end NULLABLE FROM information_schema.columns where TABLE_NAME = '";


        // select cardinality of table
        public const string SelectCountFrom = "SELECT COUNT(*) from ";
        public const string SelectCountFromMSSQL = "SELECT COUNT(*) from ";

        // select for referenced objects 
        public const string SelectReferenced = "SELECT REFERENCED_NAME from ALL_DEPENDENCIES where 1 = 1 and ";
        public const string SelectReferencedMSSQL = "SELECT a.referenced_entity_name as name " +
                                                    "FROM sys.sql_expression_dependencies a join sys.all_objects b on(a.referencing_id=b.object_id) " +
                                                    "where b.name='";
    }
}
