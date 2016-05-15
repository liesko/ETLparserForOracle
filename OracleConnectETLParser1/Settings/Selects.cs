using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OracleConnectETLParser1.Settings
{
    public static class Selects
    {
        public const string SelectObjects = "";

        public const string SelectColumnsForTable = "SELECT COLUMN_NAME, DATA_TYPE, NULLABLE FROM user_tab_columns where table_name='";

        public const string SelectColumnsForViews = "SELECT COLUMN_NAME, DATA_TYPE, NULLABLE FROM user_tab_columns where table_name='";

        public const string SelectCountFrom = "SELECT COUNT(*) from ";

        public const string SelectReferenced = "SELECT REFERENCED_NAME from ALL_DEPENDENCIES where 1 = 1 and ";
    }
}
