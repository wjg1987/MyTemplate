using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using My.Template.IDAL;
using My.Template.Model;

namespace My.Template.EFDAL
{
    public partial  class ActionDal:BaseDal<Model.Action>,IActionDal
    {
        public int GetLoginAdminAllActionCount(int loginAdminId,int actionInfoID)
        {
            var count = (from A in
                            (from ur in db.Set<User_Role>()
                             join a in db.Set<Model.Role_Action>()
                                 on ur.RoleID equals a.RoleID
                             where ur.UserID == loginAdminId
                             where a.ActionID == actionInfoID
                             select a)
                        join c in db.Set<Role>()
                            on A.RoleID equals c.ID
                            where c.IsFrozen == false
                            where c.IsDelete == false
                        select A ).Count();
            return count;
        }
    }
}
