using OracleConnectETLParser1.Controllers;

namespace OracleConnectETLParser1.Objects
{
    class Procedure : DbObject
    {
        public Procedure(string name, string owner, DbConnector db, DbObjectType DbObjectTypeName) : base(name, owner, db,  DbObjectTypeName)
        {
        }
    }
}
