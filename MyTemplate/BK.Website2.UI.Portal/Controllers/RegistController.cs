using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using My.Template.BLL;
using My.Template.IBLL;
using My.Template.Model;

namespace My.Template.UI.Portal.Controllers
{
    public class RegistController : Controller
    {

        UserServices userServices = new UserServices();
  
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Index(User user, string vilidateCode)
        {
            #region 测试使用 固定的验证码
            //Session.Add("18503083235regist", "123456");
            #endregion
            if (string.IsNullOrEmpty(vilidateCode) || Session[user.Account + "regist"] == null)
            {
                ViewBag.ErrorInfo = "验证码无效";
                ViewBag.res = 1;
                return View(user);
            }
            else
            {
                if (vilidateCode != Session[user.Account + "regist"].ToString())
                {  
                    ViewBag.ErrorInfo = "验证码错误";
                    ViewBag.res = 1;
                    return View(user);
                }
            }

            User dbUser = userServices.LoadEntitys(u => u.Account == user.Account).FirstOrDefault();
            if (dbUser != null)
            {
                ViewBag.ErrorInfo = "用户名已存在";
                return View(user);
            }
            else
            {
                //创建用户详细信息对象
                UserInfo uinfo = new UserInfo();
                Random r = new Random();

                uinfo.User = user;
                uinfo.ID = user.ID;
                uinfo.NickName = "";
                uinfo.Age =  28;
                uinfo.HeadPic = Common.Common.WebSiteUrl + "/images/images/person.png";
                user.UserInfo = uinfo;

                user.Pwd = Common.Encryption.MD5(user.Pwd, true);
                user.IsDelete = false;

                user.CreateTime = DateTime.Now;
                user.IsFrozen = false;
                user.CreateTime = DateTime.Now;
                user.Balance = 0;
                user.FrozenBlance = 0;


                //ShoppingCart sc = new ShoppingCart();
                //sc.User = user;
                //user.ShoppingCart = sc;

                //int dbuserid = userServices.AddUser(user, uinfo, sc);
                int dbuserid = 1;
                if (dbuserid > 0)
                {
                    Session["CurLoginUserID"] = dbuserid;
                    return Redirect("/Regist/RegistSccess");
                }
                else
                {
                    return View(user);
                }
            }

        }

        //注册成功页面
        public ActionResult RegistSccess()
        {
            //var user = new My.Template.UI.Portal.CommClass.Comms().IsVipLogin();
            //if (user != null)
            //{
            //    DisCardTypeServices DisCardTypeServices = new DisCardTypeServices();
            //    DisCardType dc = DisCardTypeServices.LoadEntitys(d => d.IsUsed4Regist).FirstOrDefault();
            //    if (dc == null)
            //    {
            //        return Redirect("/My/MyBalance"); 
            //    }
            //    else
            //    {
            //        return View(dc);
            //    }
            //}
            //else
            //{
            //    return Redirect("/Login/Index");

            //}
            return View();
        }

       


    }
}
