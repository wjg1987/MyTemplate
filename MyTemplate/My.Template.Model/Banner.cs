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
    
    public partial class Banner
    {
        public Banner()
        {
            this.IsView = false;
            this.IsDelete = false;
        }
    
        public int ID { get; set; }
        public string Title { get; set; }
        public string ImgPic { get; set; }
        public string LinkUrl { get; set; }
        public bool IsView { get; set; }
        public bool IsDelete { get; set; }
        public System.DateTime AddTime { get; set; }
        public int Sequence { get; set; }
        public bool IsUse4Phone { get; set; }
        public int BannerTypeID { get; set; }
    
    	[JsonIgnore]
        public virtual BannerType BannerType { get; set; }
    }
}
