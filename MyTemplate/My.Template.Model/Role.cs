//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

using Newtonsoft.Json;


namespace My.Template.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Role : IBaseEntity
    {
        public Role()
        {
            this.IsDelete = false;
            this.IsFrozen = false;
            this.User_Role = new HashSet<User_Role>();
            this.Role_Action = new HashSet<Role_Action>();
        }
    
        public int ID { get; set; }
        public string RCNName { get; set; }
        public string REnName { get; set; }
        public string RRemark { get; set; }
        public bool IsDelete { get; set; }
        public bool IsFrozen { get; set; }
        public System.DateTime CreateTime { get; set; }
    
    	[JsonIgnore]
        public virtual ICollection<User_Role> User_Role { get; set; }
    	[JsonIgnore]
        public virtual ICollection<Role_Action> Role_Action { get; set; }
    }
}
