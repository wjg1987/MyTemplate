using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using My.Template.BLL;
using My.Template.Model;

namespace My.Template.UI.Portal.CommClass
{
    public class Comms
    {
        public User IsVipLogin()
        {
            User loginuser = null;
        
            //判断是否登录
            if (HttpContext.Current.Session["CurLoginUserID"] != null)
            {
                try
                {
                    int uid = Convert.ToInt32(HttpContext.Current.Session["CurLoginUserID"]);
                    UserServices userServices = new UserServices();
                    loginuser = userServices.LoadEntitys(u => u.ID == uid).FirstOrDefault();
                }
                catch (Exception)
                {
                    return loginuser;
                }
            }
            else
            {
                return loginuser;
            }

            return loginuser;
        }


        //public User IsVipLogin(out int shopCratThings)
        //{
        //    User loginuser = null;

        //    //判断是否登录
        //    if (HttpContext.Current.Session["CurLoginUserID"] != null)
        //    {
        //        try
        //        {
        //            int uid = Convert.ToInt32(HttpContext.Current.Session["CurLoginUserID"]);
        //            UserServices userServices = new UserServices();
        //            loginuser = userServices.LoadEntitys(u => u.ID == uid).FirstOrDefault();

        //            List<MGoods_ShopCart> model1 = loginuser.ShoppingCart.MGoods_ShopCart.ToList();
        //            for (int i = 0; i < model1.Count; i++)
        //            {
        //                model1[i].ShopCartType = 1;//1表示 商城单品 放入购物车中
        //                model1[i].ShopThingsTime = model1[i].AddTime;
        //            }

               
        //            shopCratThings = model1.Count;
        //        }
        //        catch (Exception)
        //        {
        //            shopCratThings = 0;
        //            return loginuser;
        //        }
        //    }
        //    else
        //    {
        //        shopCratThings = 0;
        //        return loginuser;
        //    }

        //    return loginuser;
        //}


        public User IsPhoneVipLogin()
        {
            User loginuser = null;

            //判断是否登录
            if (HttpContext.Current.Session["CurPhoneLoginUserID"] != null)
            {
                try
                {
                    int uid = Convert.ToInt32(HttpContext.Current.Session["CurPhoneLoginUserID"]);
                    UserServices userServices = new UserServices();
                    loginuser = userServices.LoadEntitys(u => u.ID == uid).FirstOrDefault();
                }
                catch (Exception)
                {

                    return loginuser;
                }
            }
            else
            {
                return loginuser;
            }

            return loginuser;
        }


        //public User IsPhoneVipLogin(out int shopCratThings)
        //{
        //    User loginuser = null;

        //    //判断是否登录
        //    if (HttpContext.Current.Session["CurPhoneLoginUserID"] != null)
        //    {
        //        try
        //        {
        //            int uid = Convert.ToInt32(HttpContext.Current.Session["CurPhoneLoginUserID"]);
        //            UserServices userServices = new UserServices();
        //            loginuser = userServices.LoadEntitys(u => u.ID == uid).FirstOrDefault();

        //            List<MGoods_ShopCart> model1 = loginuser.ShoppingCart.MGoods_ShopCart.ToList();
        //            for (int i = 0; i < model1.Count; i++)
        //            {
        //                model1[i].ShopCartType = 1;//1表示 商城单品 放入购物车中
        //                model1[i].ShopThingsTime = model1[i].AddTime;
        //            }

                  
        //            shopCratThings = model1.Count;
        //        }
        //        catch (Exception)
        //        {
        //            shopCratThings = 0;
        //            return loginuser;
        //        }
        //    }
        //    else
        //    {
        //        shopCratThings = 0;
        //        return loginuser;
        //    }

        //    return loginuser;
        //}


         
        public User IsAdminLogin()
        {
            User loginuser = null;

            //判断是否登录
            if (HttpContext.Current.Session["curLoginAdminID"] != null)
            {
                try
                {
                    int uid = Convert.ToInt32(HttpContext.Current.Session["curLoginAdminID"]);
                    UserServices userServices = new UserServices();
                    loginuser = userServices.LoadEntitys(u => u.ID == uid).FirstOrDefault();
                }
                catch (Exception)
                {
                    return loginuser;
                }
            }
            else
            {
                return loginuser;
            }

            return loginuser;
        }


    }


    public class Pro_ProDetailList
    {
        public List<Pro_ProDetail> goodspros { get; set; }
    }

    public class Pro_ProDetail
    {
        public int prodeid { get; set; }
        public int proid { get; set; }
    }


    public class ShopCartThingsList
    {
        public List<ShopCartThings> goodspros { get; set; }
    }

    public class ShopCartThings
    {
        public int stype { get; set; } //购物车中商品类型
        public int sid { get; set; } //购物车中商品id 根据不同类型去不同表中找对象数据
    }
}