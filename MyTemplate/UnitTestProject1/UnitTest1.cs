using System;
using My.Template.BLL;
using My.Template.Common;
using My.Template.EFDAL;
using My.Template.Model;
using My.Template.UI.Portal.Areas.mobile.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Collections.Generic;


namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        #region 添加单个商品到购物车
        [TestMethod]
        public void TestAddGoods2ShopCart()
        {
            GetDataController sms = new GetDataController();

            ////sms.AddGoodsShopCart(int goodsid = 0, string goodspros = "", int goodsCount = 1)
            //string str = "{\"goodspros\":[{\"proid\":2,\"prodeid\":3},{\"proid\":3,\"prodeid\":6}]}";
            ////String json = JsonConvert.SerializeObject(str);
            //sms.AddGoodsShopCart(2, str, 1);

            //Assert.Equals(res, "1");
        }
        #endregion
       


    }
}
