using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;
using My.Template.BLL;
using My.Template.Common;
using My.Template.IBLL;
using My.Template.Model;
using Newtonsoft.Json;

namespace My.Template.UI.Portal.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        
        
        public ActionResult Index()
        {
            #region 测试 后台 不登陆使用
            Common.Common.redisClient.SetValue("curLoginAdminID", "1");
            Common.Common.redisClient.SetValue("curLoginAdminAccount", "admin");
            Common.Common.redisClient.SetValue("curLoginAdminHeadImg", "/areas/admin/images/th.jpg");
            //Session["curLoginAdminID"] = 1;
            //Session["curLoginAdminAccount"] = "admin";
            //Session["curLoginAdminHeadImg"] = "/areas/admin/images/th.jpg";
            return Redirect("/Admin/Home/index");
            #endregion
           
            return View();
        }

        [HttpPost]
        public ActionResult Index(User user, string validatecode)
        {
            if (ModelState.IsValid)
            {
                #region 验证验证码
                var sessionCode = Session["AdminValidateCode"];
                if (string.IsNullOrEmpty(validatecode)
                    || sessionCode == null
                    || !string.Equals(validatecode, sessionCode.ToString()))
                {
                    ModelState.AddModelError("validatecode", "验证码错误.");
                    return View();
                }
                #endregion

                #region 验证用户名和密码 以及角色的有效性
                IUserServices userServices = new UserServices();
                var dbUser = userServices.LoadEntitys(
                    u => string.Equals(u.Account, user.Account)
                         && string.Equals(u.Pwd, Common.Encryption.MD5(user.Pwd, true))
                         && u.IsDelete == false
                    ).FirstOrDefault();

                if (dbUser == null)
                {
                    ModelState.AddModelError("validatecode", "用户名或者密码错误.");
                    return View();
                }

                if (dbUser.IsFrozen)
                {
                    ModelState.AddModelError("validatecode", "该账号已冻结.");
                    return View();
                }

                bool flag = false;
                foreach (var role in dbUser.User_Role)
                {
                    if (role.Role.ID == 1)//后台管理员
                    {
                        flag = true;
                    }
                }

                if (!flag)//没有后台管理员权限
                {
                    ModelState.AddModelError("validatecode", "当前账号没有管理员权限.");
                    return View();
                }


         
                #region 登陆成功后 需要用 session或全局缓存保存 当期登陆用户的信息（图像和账号  后台需要显示）

                //将登录用户所有权限ID 存入session中


                Session["curLoginAdminID"] = dbUser.ID;
                Session["curLoginAdminAccount"] = dbUser.Account;
                Session["curLoginAdminHeadImg"] = dbUser.UserInfo.HeadPic;
                #endregion

                return Redirect("/Admin/Home/index");

                #endregion
            }

            return View();
        }


        public ActionResult RecoverPwd(string email)
        {
            //todo:找回密码逻辑（验证邮箱 并发送重置密码邮件）
            return View();
        }

        /// <summary>
        /// 获得验证码
        /// </summary>
        /// <returns></returns>
        public ActionResult ShowValidateCode()
        {
            Common.ValidateCode validateCode = new ValidateCode();
            string vcode = validateCode.CreateValidateCode(4);
            Session["AdminValidateCode"] = vcode;

            var byteData = validateCode.CreateValidateGraphic(vcode);
            return File(byteData, "image/jpeg");
        }
    }
}
