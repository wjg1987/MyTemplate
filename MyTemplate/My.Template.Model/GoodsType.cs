//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace My.Template.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class GoodsType
    {
        public GoodsType()
        {
            this.GoodsInfo = new HashSet<GoodsInfo>();
        }
    
        public int ID { get; set; }
        public string ParentID { get; set; }
        public bool IsDelete { get; set; }
        public string GTName { get; set; }
        public int Sequence { get; set; }
        public bool IsView { get; set; }
        public string Remark { get; set; }
    
        public virtual ICollection<GoodsInfo> GoodsInfo { get; set; }
    }
}