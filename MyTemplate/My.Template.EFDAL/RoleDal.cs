using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using My.Template.Model;

namespace My.Template.EFDAL
{
    public partial class RoleDal
    {
        public List<Model.Action> GetActionsWithRole(Role role)
        {
            List<Model.Action> role_actions =
                          (from a in db.Set<Model.Action>()
                           join ra in db.Set<Role_Action>()
                           on a.ID equals ra.ActionID
                           where ra.RoleID == role.ID
                           select a).ToList();

            return role_actions;
        }
    }
}
