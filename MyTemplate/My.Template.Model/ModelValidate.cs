using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Metadata.Edm;
using System.Linq;
using System.Text;

namespace My.Template.Model
{
    #region Action
    //此标签标示 Action 类 共享 ActionValidate 类中的元数据信息
    [MetadataType(typeof(ActionValidate))]
    public partial class Action
    {
    }

    public class ActionValidate
    {

    }
    #endregion


    //此标签标示 Role 类 共享 RoleValidate 类中的元数据信息
    [MetadataType(typeof(RoleValidate))]
    public partial class Role
    {
    }

    public class RoleValidate
    {
    }



    //此标签标示 Role_Action 类 共享 Role_ActionValidate 类中的元数据信息
    [MetadataType(typeof(Role_ActionValidate))]
    public partial class Role_Action
    {
    }

    public class Role_ActionValidate
    {
    }






    //此标签标示 User 类 共享 UserValidate 类中的元数据信息
    [MetadataType(typeof(UserValidate))]
    public partial class User
    {
    }

    public class UserValidate
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "*")]
        public string Account { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string WxOpenod { get; set; }
        public string SinaWBOpenid { get; set; }
        public string QQNum { get; set; }
        [Required(ErrorMessage = "*")]
        public string Pwd { get; set; }
        public bool IsDelete { get; set; }
        public bool IsFrozen { get; set; }
    }



    //此标签标示 User_Action 类 共享 User_ActionValidate 类中的元数据信息
    [MetadataType(typeof(User_ActionValidate))]
    public partial class User_Action
    {
    }

    public class User_ActionValidate
    {
    }



    //此标签标示 User_Role 类 共享 User_RoleValidate 类中的元数据信息
    [MetadataType(typeof(User_RoleValidate))]
    public partial class User_Role
    {
    }

    public class User_RoleValidate
    {
    }



    //此标签标示 UserInfo 类 共享 UserInfoValidate 类中的元数据信息
    [MetadataType(typeof(UserInfoValidate))]
    public partial class UserInfo
    {
    }

    public class UserInfoValidate
    {
    }

}