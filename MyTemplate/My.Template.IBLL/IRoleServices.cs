using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using My.Template.Model;

namespace My.Template.IBLL
{
    public partial interface  IRoleServices
    {
        List<Model.Action> GetActionsWithRole(Role role);
    }
}
