using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using My.Template.Model;

namespace My.Template.IBLL
{
    public partial interface IUserServices : IBaseServices<User>
    {
        int AddUser(User user, UserInfo userInfo);

        List<Model.Action> GetAllActions(User user);

        List<Model.Action> GetActionsWithoutRole(User user);

        List<Model.Role> GetAllRoles(User user);
    }
}
