using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using My.Template.EFDAL;
using My.Template.Model;

namespace My.Template.BLL
{
    public partial class RoleServices
    {
        public List<Model.Action> GetActionsWithRole(Role role)
        {
            var dal = CurrentDal as RoleDal;
            return dal.GetActionsWithRole(role);
        }

    }
}
