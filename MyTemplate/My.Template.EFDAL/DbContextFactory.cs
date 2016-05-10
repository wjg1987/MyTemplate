using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using My.Template.Model;

namespace My.Template.EFDAL
{
   /// <summary>
   /// 为了保证 dbcontext 在线程内唯一 
   /// </summary>
    public class DbContextFactory
    {
       public static DbContext GetCurDbContext()
       {
           DbContext db = CallContext.GetData("CurDbContext") as DbContext;
           if (db == null)
           {
               db  = new DataModelContainer();//此处如果有另外的 子类  那么用父类接收 在basedal中不需要改变
               //在EF5.0修改实体的时候，出现“对一个或多个实体的验证失败。有关详细信息，请参见“EntityValidationErrors”属性这个错误   -------------修改：SaveChanges前先关闭验证实体有效性（ValidateOnSaveEnabled）这个开关
               db.Configuration.ValidateOnSaveEnabled = false;  
               CallContext.SetData("CurDbContext", db);
           }
           return db;
       }
    }
}
