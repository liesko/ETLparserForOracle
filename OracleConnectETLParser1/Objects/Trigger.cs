using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OracleConnectETLParser1.Controllers;

namespace OracleConnectETLParser1.Objects
{
    public class Trigger : DbObject
    {
        public Trigger(string name, string owner, DbConnector db, string objectId, string creationTime, string lastChangedTime, DbObjectType DbObjectTypeName) : base(name, owner, db, objectId, creationTime, lastChangedTime, DbObjectTypeName)
        {
        }
    }
}
