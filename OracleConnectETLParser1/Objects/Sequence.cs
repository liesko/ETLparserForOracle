using OracleConnectETLParser1.Controllers;

namespace OracleConnectETLParser1.Objects
{
    public class Sequence : DbObject
    {
        public Sequence(string name, string owner, DbConnector db, DbObjectType DbObjectTypeName) : base(name, owner, db,  DbObjectTypeName)
        {
        }
    }
}
