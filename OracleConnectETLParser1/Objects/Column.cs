namespace OracleConnectETLParser1
{
    public class Column
    {
        public string ColumnName { get; private set; }
        public string ColumnCommment { get; private set; }
        public string DataType { get; private set; }
        public bool IsNullable { get; private set; }
        public string Comment { get; private set; }

        public Column(string colName, string dataType, bool nullable, string comment)
        {
            ColumnName = colName;
            DataType = dataType;
            IsNullable = nullable;
            Comment = comment;
        }
    }
}