using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using My.Template.BLL;
using My.Template.IBLL;
using My.Template.Model;
using Newtonsoft.Json;

namespace My.Template.UI.Portal.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/
        public ActionResult Index()
        {
            ViewBag.returnUrl = Request.UrlReferrer == null ? Url.Action("Index", "Login") : Request.UrlReferrer.ToString();
            User user = new User();
            return View(user);
        }

        [HttpPost]
        public ActionResult Index(User user,string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;

            string md5pwd = Common.Encryption.MD5(user.Pwd, true);

            IUserServices userServices = new UserServices();

                //查询当前登陆用户
                var dbuser = userServices.LoadEntitys(u => u.Account == user.Account && u.Pwd == md5pwd).FirstOrDefault();
                //var dbuser = userServices.LoadEntitys(u => u.ID == 2).FirstOrDefault();//测试时候使用的
                if (dbuser == null)
                {
                    ViewBag.ErrorInfo= "用户名或者密码错误。";
                    return View();
                }
                else
                {
                    Session.Add("CurLoginUserID", dbuser.ID);
                    Session.Add("CurLoginUserName", dbuser.Account);
                    if (!string.IsNullOrEmpty(returnUrl) && !returnUrl.ToLower().Contains("login") && !returnUrl.ToLower().Contains("regist"))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return Redirect("/my/mybalance");
                    }
                }
            }



        #region  找回密码
        //找回密码第一步 用户确定
        [HttpGet]
        public ActionResult FindPwd()
        {
            return View();
        }

        [HttpPost]
        public ActionResult FindPwd(string account, string validateCode)
        {
            UserServices userServices = new UserServices();
            User user = userServices.LoadEntitys(u => u.Account == account).FirstOrDefault();
            if (user == null)
            {
                ViewBag.ErrorInfo = "用户名或手机号不存在";
                ViewBag.res = 1;
                return View();
            }
            else if (validateCode == null)
            {
                ViewBag.ErrorInfo = "验证码不能为空";
                ViewBag.res = 2;
                return View();
            }
            else if (Session["AdminValidateCode"].ToString() != validateCode)
            {
                ViewBag.ErrorInfo = "验证码错误";
                ViewBag.res = 3;
                return View();
            }
            else
            {
                Session["findpwdmobile"] = account;
                Session.Timeout = 10;
                return Redirect("/Login/FindPwdStep2");
            }
        }
        //找回密码第二步 手机验证码验证
        [HttpGet]
        public ActionResult FindPwdStep2()
        {
            //todo:测试不验证 后面发布时候需要开放下面的验证
            if (Session["findpwdmobile"] == null)
            {
                return Redirect("/Login/FindPwd");
            }
            string Phone = Session["findpwdmobile"].ToString();

            Phone = Phone.Remove(3, 4);
            Phone = Phone.Insert(3, "****");
            ViewBag.account = Session["findpwdmobile"];
            ViewBag.mobile = Phone;
            return View();
        }

        [HttpPost]
        public ActionResult FindPwdStep2(string account, string validateCode)
        {
            //Session手机号为空返回找回密码页面
            if (Session["findpwdmobile"] == null)
            {
                return Redirect("/Login/FindPwd");
            }
            //手机号为空返回找回密码页面
            if (account == null)
            {
                ViewBag.ErrorInfo = "请重试";
                ViewBag.account = Session["findpwdmobile"];
                return View();
            }
            //验证手机号是否匹配
            if (account != Session["findpwdmobile"].ToString())
            {
                return Redirect("/Login/FindPwd");
            }
            //验证前台传手机验证码是否存在
            if (string.IsNullOrEmpty(validateCode))
            {
                ViewBag.account = account;
                ViewBag.res = 1;
                ViewBag.ErrorInfo = "验证码不能为空";
                return View();
            }
            //验证Session验证码是否存在
            if (Session[account + "code4pwd"] == null)
            {
                ViewBag.account = account;
                ViewBag.res = 1;
                ViewBag.ErrorInfo = "请先获取手机验证码";
                return View();
            }
            //验证验证码是否正确
            if (validateCode != Session[account + "code4pwd"].ToString())
            {
                ViewBag.account = account;
                ViewBag.res = 1;
                ViewBag.ErrorInfo = "验证码错误";
                return View();
            }
            //通过调整密码修改页面
            string url = "/Login/updatepwd?pwdcode=" + validateCode + "&account=" + account;
            return Redirect(url);
        }
        //找回密码第三步 密码修改
        [HttpGet]
        public ActionResult updatepwd(string pwdcode, string account = "")
        {
            if (Session[account + "code4pwd"] == null)
            {
                return Redirect("/Login/FindPwd");    
            }
            //验证短信验证码为空，或手机号为空返回找回密码页面
            if (string.IsNullOrEmpty(pwdcode) || string.IsNullOrEmpty(account))
            {
                return Redirect("/Login/FindPwd");             
            }
            else
            {
                //验证码不正确返回找回密码页面
                if (Session[account + "code4pwd"].ToString() != pwdcode)
                {
                    return Redirect("/Login/FindPwd");
                }
            }

            //设置验证码通过标识
            Session[account + "code4pwd"] = account[1] + "true";

            //todo:测试不验证 后面发布时候需要开放下面的验证
            if (Session["findpwdmobile"] == null)
            {
                return Redirect("/Login/FindPwd");
            }
            ViewBag.account = Session["findpwdmobile"].ToString();
            return View();
        }
        [HttpPost]
        public ActionResult updatepwd(string account, string pwd1, string pwd2)
        {
            if (Session[account + "code4pwd"] == null)
            {
                return Redirect("/Login/FindPwd");
            }
            //验证手机验证码通过标识
            if( Session[account + "code4pwd"].ToString() != account[1]+"true")
            {
                return Redirect("/Login/FindPwd");
            }
            //Session手机号为空返回找回密码页面
            if (Session["findpwdmobile"] == null)
            {
                return Redirect("/Login/FindPwd");
            }
            //手机号为空返回找回密码页面
            if (string.IsNullOrEmpty(account))
            {
                return Redirect("/Login/FindPwd");
            }
            //Session手机号和传过来的手机不匹配返回找回密码页面
            if (Session["findpwdmobile"].ToString() != account)
            {
                return Redirect("/Login/FindPwd");
            }

            if (string.IsNullOrEmpty(pwd1))
            {
                ViewBag.res = 0;
                ViewBag.account = account;
                ViewBag.ErrorInfo = "请输入密码";
                return View();
            }
            if (string.IsNullOrEmpty(pwd2))
            {
                ViewBag.res =0;
                ViewBag.account = account;
                ViewBag.ErrorInfo = "请输入确认密码";
                return View();
            }
            //验证两次密码是否对应
            if (pwd1 != pwd2)
            {
                ViewBag.res = 0;
                ViewBag.account = account;
                ViewBag.ErrorInfo = "输入密码不一致";
                return View();
            }

            //修改密码
            UserServices userser = new UserServices();
            var curuser = userser.LoadEntitys(u => u.Account == account).FirstOrDefault();
            curuser.Pwd = Common.Encryption.MD5(pwd1, true);
            if (userser.Update(curuser))
            {
                Session["findpwdmobile"] = null;
                Session["mobile"] = null;
                Session[account + "code4pwd"] = null;
                //修改密码成功返回登录页面
                return Redirect("/Login/Index");
            }
            else
            {
                ViewBag.res = 0;
                ViewBag.account = account;
                ViewBag.ErrorInfo = "修改失败，请重试";
                return View();
            }
           
        }

        //获取验证码
        public FileResult GetCheckCodeImg()
        {
            Common.ValidateCode validateCode = new Common.ValidateCode();
            string code = validateCode.CreateValidateCode(4);
            Session["AdminValidateCode"] = code;
            var bytedata = validateCode.CreateValidateGraphic(code);
            return File(bytedata, "image/jepg");
        }
        #endregion
    }
}
