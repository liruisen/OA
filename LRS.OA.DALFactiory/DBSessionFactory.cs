using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace LRS.OA.DALFactory
{
   public  class DBSessionFactory
    {
       public static IDAL.IDBSession creatDBSession()
       {
           IDAL.IDBSession DbSession = (IDAL.IDBSession)CallContext.GetData("dbSession");
           if (DbSession==null)
           {
               DbSession = new DALFactiory.DBSession();
               CallContext.SetData("dbSession",DbSession);
           }
           return DbSession;
       }
    }
}
