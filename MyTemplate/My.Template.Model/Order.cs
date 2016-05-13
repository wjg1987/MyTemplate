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
    
    public partial class Order
    {
        public Order()
        {
            this.OrderGoods = new HashSet<OrderGoods>();
        }
    
        public int ID { get; set; }
        public string OrderNum { get; set; }
        public int DueMoney { get; set; }
        public int ActualPay { get; set; }
        public string BalanceChange { get; set; }
        public int CouponValue { get; set; }
        public int OrderStatusID { get; set; }
        public int RecAddressID { get; set; }
        public bool IsDelete { get; set; }
    
    	[JsonIgnore]
        public virtual OrderStatus OrderStatus { get; set; }
    	[JsonIgnore]
        public virtual ICollection<OrderGoods> OrderGoods { get; set; }
    	[JsonIgnore]
        public virtual RecAddress RecAddress { get; set; }
    }
}
