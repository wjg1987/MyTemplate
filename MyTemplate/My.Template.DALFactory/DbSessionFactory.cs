using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using My.Template.IDAL;

namespace My.Template.DALFactory
{
    public class DbSessionFactory
    {
        public static IDbSession GetCurDbSession()
        {
            IDbSession dbSession = CallContext.GetData("curDbSession") as DbSession;
            if (dbSession == null)
            {
                dbSession = new DbSession();
                CallContext.SetData("curDbSession",dbSession);
            }
            return dbSession;
        }
    }
}
