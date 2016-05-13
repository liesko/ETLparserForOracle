namespace OracleConnectETLParser1
{
    public class Column
    {
        public string ColumnName { get; private set; }
        public string DataType { get; private set; }
        public bool IsNullable { get; private set; }

        public Column(string colName, string dataType, bool nullable)
        {
            this.ColumnName = colName;
            this.DataType = dataType;
            this.IsNullable = nullable;
        }
    }
}