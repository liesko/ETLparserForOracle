using OracleConnectETLParser1.Controllers;

namespace OracleConnectETLParser1.Objects
{
    class Procedure : DbObject
    {
        public Procedure(string name, string owner, DbConnector db, string objectId, string creationTime, string lastChangedTime, DbObjectType DbObjectTypeName) : base(name, owner, db, objectId, creationTime, lastChangedTime, DbObjectTypeName)
        {
        }
    }
}
