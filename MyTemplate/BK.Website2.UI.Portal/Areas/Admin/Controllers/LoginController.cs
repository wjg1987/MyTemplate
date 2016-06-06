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
using My.Template.Model.AdminDataModel;
using Newtonsoft.Json;

namespace My.Template.UI.Portal.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        IUserServices UserServicesEntity { get; set; }

        public ActionResult Index()
        {
            CheckCooiesInfo();
            return View(new User());
        }

        [HttpPost]
        public ActionResult Index(User user, string validatecode, int hidenremberme = 0)
        {
            var model = new User() { Account = user.Account };
            if (ModelState.IsValid)
            {
                #region 验证验证码
                var sessionCode = Session["AdminValidateCode"];
                if (string.IsNullOrEmpty(validatecode)
                    || sessionCode == null
                    || !string.Equals(validatecode, sessionCode.ToString()))
                {
                    ModelState.AddModelError("ValidateCode", "验证码错误.");
                    return View(model);
                }
                #endregion

                #region 验证用户名和密码 以及角色的有效性
               
                var dbUser = UserServicesEntity.LoadEntitys(
                    u => string.Equals(u.Account, user.Account)
                         && u.IsDelete == false
                    ).FirstOrDefault();

                if (dbUser == null)
                {
                    ModelState.AddModelError("ValidateCode", "用户名不存在.");
                    return View(model);
                }

                if (!string.Equals(dbUser.Pwd, Common.Encryption.MD5(user.Pwd, true)))
                {
                    ModelState.AddModelError("ValidateCode", "密码错误.");
                    return View(model);
                }

                if (dbUser.IsFrozen)
                {
                    ModelState.AddModelError("ValidateCode", "该账号已冻结.");
                    return View(model);
                }

                #region 不用再此处校验是否是后台管理员，在base里已经做了权限校验此处可以不用校验。没有权限登陆不了后台...
                //bool flag = false;
                //foreach (var role in dbUser.User_Role)
                //{
                //    if (role.Role.ID == 1)//后台管理员
                //    {
                //        flag = true;
                //    }
                //}

                //if (!flag)//没有后台管理员权限
                //{
                //    ModelState.AddModelError("ValidateCode", "当前账号没有管理员权限.");
                //    return View(model);
                //}
                #endregion
               

                #region 登陆成功后 需要用 全局缓存保存 当期登陆用户的信息（图像和账号  后台需要显示）
                var loginAdminInfo =  new LoginAdminInfo()
                {
                    LogingUser = dbUser,
                    Userid = dbUser.ID,
                    Account = dbUser.Account,
                    HeadImg = dbUser.UserInfo.HeadPic
                };

                string sessionId = Guid.NewGuid().ToString();
                //使用memcache代替session解决不同web服务器之间共享的问题
                Common.MemcacheHelper.Set(sessionId, Common.SerializerHelper.SerializeToString(loginAdminInfo), DateTime.Now.AddMinutes(20));
                //将memcache的key以cookie的形式返回浏览器端的内存中。当用户再次请求其他页面时，请求报文会将cookie中的sessionId再次发送到服务端
                Response.Cookies["sessionId"].Value = sessionId;

                #endregion


                #region 如果登陆成功并且选择了记住我
                if (hidenremberme == 1)
                {
                    HttpCookie cookie1 = new HttpCookie("cp1",dbUser.Account);
                    HttpCookie cookie2 = new HttpCookie("cp2",Common.Encryption.EncryptAES(dbUser.Pwd));
                    cookie1.Expires = DateTime.Now.AddDays(3);
                    cookie2.Expires = DateTime.Now.AddDays(3);
                    Response.Cookies.Add(cookie1);
                    Response.Cookies.Add(cookie2);
                }
                #endregion

                return Redirect("/Admin/Home/index");

                #endregion
            }

            return View(model);
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

            #region MyRegion
            //string vcodesessionId = Guid.NewGuid().ToString() + "vcode";
            ////使用memcache代替session解决不同web服务器之间共享的问题
            //Common.MemcacheHelper.Set(vcodesessionId, vcode, DateTime.Now.AddMinutes(20));
            ////将memcache的key以cookie的形式返回浏览器端的内存中。当用户再次请求其他页面时，请求报文会将cookie中的sessionId再次发送到服务端
            //Response.Cookies["vcodesessionId"].Value = vcode;
            #endregion
           
            Session["AdminValidateCode"] = vcode;
            var byteData = validateCode.CreateValidateGraphic(vcode);
            return File(byteData, "image/jpeg");
        }


        #region 判断cookie信息 用户是否选择了记住我功能
        private void CheckCooiesInfo()
        {
            if (Request.Cookies["cp1"] != null && Request.Cookies["cp2"] != null)
            {
                string account = Request.Cookies["cp1"].Value;
                string pwd = Request.Cookies["cp2"].Value;

                //判断cookie中存储的用户名和密码是否正确
                var dbuser = UserServicesEntity.LoadEntitys(u => u.Account == account).FirstOrDefault();
                if (dbuser != null)
                {
                    //比较密码 数据库中的密码和 cookie中的存储的密码都是加密过的
                    if (dbuser.Pwd == Common.Encryption.DecryptAES(pwd))
                    {
                        #region 存储用户登陆状态

                        var loginAdminInfo = new LoginAdminInfo()
                            {
                                LogingUser = dbuser,
                                Userid = dbuser.ID,
                                Account = dbuser.Account,
                                HeadImg = dbuser.UserInfo.HeadPic
                            };
                        string sessionId = Guid.NewGuid().ToString();
                        //使用memcache代替session解决不同web服务器之间共享的问题
                        Common.MemcacheHelper.Set(sessionId, Common.SerializerHelper.SerializeToString(loginAdminInfo),
                                                  DateTime.Now.AddMinutes(20));
                        //将memcache的key以cookie的形式返回浏览器端的内存中。当用户再次请求其他页面时，请求报文会将cookie中的sessionId再次发送到服务端
                        Response.Cookies["sessionId"].Value = sessionId;

                        #endregion

                        Response.Redirect("/Admin/Home/index");
                    }

                }
                
                //没有找到用户 说明没有该账号 cookie过期 里面的密码比对失败也让cookie过期
                Response.Cookies["cp1"].Expires = DateTime.Now.AddDays(-1);
                Response.Cookies["cp2"].Expires = DateTime.Now.AddDays(-1);
            }
        }
        #endregion
    }
}
