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
    
    public partial class GoodsTypeFilterProperty : IBaseEntity
    {
        public GoodsTypeFilterProperty()
        {
            this.GoodsTypeFilterPropertyDetail = new HashSet<GoodsTypeFilterPropertyDetail>();
        }
    
        public int ID { get; set; }
        public int GoodsTypeID { get; set; }
        public string FilterWords { get; set; }
        public bool IsDelete { get; set; }
        public int Sequence { get; set; }
        public int GoodsTypeID1 { get; set; }
    
    	[JsonIgnore]
        public virtual ICollection<GoodsTypeFilterPropertyDetail> GoodsTypeFilterPropertyDetail { get; set; }
    	[JsonIgnore]
        public virtual GoodsType GoodsType { get; set; }
    }
}
