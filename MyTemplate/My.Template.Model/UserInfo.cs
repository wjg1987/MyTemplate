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
    
    public partial class UserInfo
    {
        public UserInfo()
        {
            this.Age = 0;
        }
    
        public int ID { get; set; }
        public string FullName { get; set; }
        public string NickName { get; set; }
        public string HeadPic { get; set; }
        public string WXHeadPic { get; set; }
        public string QQHeadPic { get; set; }
        public string SinaWBHeadPic { get; set; }
        public int Age { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string QQNum { get; set; }
        public string Email { get; set; }
        public Nullable<bool> Sex { get; set; }
        public int UserID { get; set; }
    
    	[JsonIgnore]
        public virtual User User { get; set; }
    }
}
