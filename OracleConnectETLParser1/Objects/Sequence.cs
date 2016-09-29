using OracleConnectETLParser1.Controllers;

namespace OracleConnectETLParser1.Objects
{
    public class Sequence : DbObject
    {
        public Sequence(string name, string owner, DbConnector db, string objectId, string creationTime, string lastChangedTime, DbObjectType DbObjectTypeName) : base(name, owner, db, objectId, creationTime, lastChangedTime, DbObjectTypeName)
        {
        }
    }
}
