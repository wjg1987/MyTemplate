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
    
    public partial class GoodsSkuPrice : IBaseEntity
    {
        public GoodsSkuPrice()
        {
            this.GoodsSkuPrice_R_GoodsProDetails = new HashSet<GoodsSkuPrice_R_GoodsProDetails>();
        }
    
        public int ID { get; set; }
        public string SKUNumber { get; set; }
        public int OriginalPrice { get; set; }
        public int SalPrice { get; set; }
        public int RepertoryCount { get; set; }
        public int GoodsInfoID { get; set; }
        public bool IsDelete { get; set; }
    
    	[JsonIgnore]
        public virtual GoodsInfo GoodsInfo { get; set; }
    	[JsonIgnore]
        public virtual ICollection<GoodsSkuPrice_R_GoodsProDetails> GoodsSkuPrice_R_GoodsProDetails { get; set; }
    }
}
