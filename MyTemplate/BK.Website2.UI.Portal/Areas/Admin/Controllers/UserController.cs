using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using My.Template.BLL;
using My.Template.IBLL;
using My.Template.Model;
using Newtonsoft.Json.Linq;

namespace My.Template.UI.Portal.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {

        IUserServices UserServicesEntity { get; set; }
        IActionServices ActionServicesEntity { get; set; }
        IUser_ActionServices User_ActionServicesEntity { get; set; }
        IRoleServices RoleServicesEntity { get; set; }
        IUser_RoleServices UserRoleServicesEntity { get; set; }

        public ActionResult Index(int pageSize = 10, int pageIndex = 1, string searchWords = "")
        {
            //返回前台页数和每页条数
            ViewBag.pageIndex = pageIndex;
            ViewBag.pageSize = pageSize;
            ViewBag.searchWords = searchWords;

            //过滤条件后的数据总条数 
            int totalCount;

            Func<User, bool> whereFunc = (b) =>
            {
                if (!string.IsNullOrEmpty(searchWords))
                {
                    return
                            (
                                b.IsDelete == false && b.IsFrozen == false
                            )
                            &&
                            (
                                   b.Account.Contains(searchWords) || b.Account.Contains(searchWords.ToLower())
                            );
                }
                else
                {
                    return b.IsDelete == false && b.IsFrozen == false;
                }
            };

            Func<User, int> orderFunc = (b) => { return b.ID; };

            var tmp = UserServicesEntity.LoadPageEntitys(pageIndex, pageSize, out totalCount, whereFunc, orderFunc, true).ToList();



            //分页字符串
            ViewBag.pageStr = Common.Tools.ShowPageNavigate(pageSize, pageIndex, totalCount, "&searchWords=" + searchWords);

            return View(tmp);
        }


        public ActionResult Add()
        {
            //取消返回页面
            ViewBag.returnUrl = Request.UrlReferrer == null ? Url.Action("Index", "User") : Request.UrlReferrer.ToString();

            User model = new User();
            model.IsDelete = false;


            return View(model);
        }


        [HttpPost]
        [ValidateInput(false)]//添加带有html标签的数据的时候必须关闭此验证
        public ActionResult Add(User model, string HeadPic, string returnUrl,  string NickName = "")
        {
            //取消添加 返回页面
            ViewBag.returnUrl = returnUrl;

            if (ModelState.IsValid)
            {
                User dbUser = UserServicesEntity.LoadEntitys(u => u.Account == model.Account).FirstOrDefault();
                if (dbUser != null)
                {
                    ModelState.AddModelError("Account", "用户名已存在。");
                    return View(model);
                }


                if (string.IsNullOrEmpty(HeadPic))
                {
                    ModelState.AddModelError("HeadPic", "图像不能为空。");
                    return View(model);
                }

                UserInfo uinfo = new UserInfo();
                if (!string.IsNullOrEmpty(HeadPic) && HeadPic.IndexOf("/UserUpload/") == 0)
                {
                    uinfo.HeadPic = Common.Common.WebSiteUrl + HeadPic;
                }
                uinfo.NickName = NickName;

                model.Pwd = Common.Encryption.MD5(model.Pwd, true);
                model.IsDelete = false;
                model.UserInfo = uinfo;
                model.CreateTime = DateTime.Now;
          
                model = UserServicesEntity.Add(model);
                if (model.ID > 0)
                {
                    return Redirect(returnUrl);
                }
            }

            return View(model);
        }


        public ActionResult Edit(int id)
        {
            //取消返回页面
            ViewBag.returnUrl = Request.UrlReferrer == null ? Url.Action("Index", "User") : Request.UrlReferrer.ToString();

            var model = UserServicesEntity.LoadEntitys(b => b.ID == id).FirstOrDefault();

       
            return View(model);
        }


        [HttpPost]
        [ValidateInput(false)]//添加带有html标签的数据的时候必须关闭此验证
        public ActionResult Edit(User model, string HeadPic,  string returnUrl,string NickName="")
        {
            //取消添加 返回页面
            ViewBag.returnUrl = returnUrl;
           


            if (ModelState.IsValid)
            {
              
                if (string.IsNullOrEmpty(HeadPic))
                {
                    ModelState.AddModelError("HeadPic", "图像不能为空。");
                    return View(model);
                }


                User dbUser = UserServicesEntity.LoadEntitys(u => u.ID == model.ID).FirstOrDefault();
                if (!string.IsNullOrEmpty(HeadPic) && HeadPic.IndexOf("/UserUpload/") == 0)
                {
                    dbUser.UserInfo.HeadPic  = Common.Common.WebSiteUrl + HeadPic;
                }
                dbUser.UserInfo.NickName = NickName;
                dbUser.IsFrozen = model.IsFrozen;

                if (UserServicesEntity.Update(dbUser))
                {
                    return Redirect(returnUrl);
                }
            }


            return View(model);
        }

      
     


        public int Delete(int id)
        {

            var model = UserServicesEntity.LoadEntitys(b => b.ID == id).FirstOrDefault();
            if (model == null)
            {
                return 0;
            }
            model.IsDelete = true;
            if (UserServicesEntity.Update(model))
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
       
        
        #region 设置用户角色
        public ActionResult SetUserRole(int userID, int pageSize = 10, int pageIndex = 1, string searchWords = "", string returnUrl="")
        {
            //取消返回页面
            returnUrl = returnUrl == "" ? Request.UrlReferrer.ToString() : returnUrl;
            ViewBag.returnUrl = returnUrl;

            //返回前台页数和每页条数
            ViewBag.pageIndex = pageIndex;
            ViewBag.pageSize = pageSize;
            ViewBag.searchWords = searchWords;
           

            var user = UserServicesEntity.LoadEntitys(u => u.ID == userID && !u.IsDelete).FirstOrDefault();
            if (user == null)
            {
                return Redirect("/error.html");
            }

            ViewBag.user = user;
            ViewBag.userRoles = UserServicesEntity.GetAllRoles(user);

            //过滤条件后的数据总条数 
            int totalCount;

            Func<Model.Role, bool> whereFunc = (b) =>
            {
                if (!string.IsNullOrEmpty(searchWords))
                {
                    return
                            (
                                b.IsDelete == false && b.IsFrozen == false
                            )
                            &&
                            (
                                   b.RCNName.Contains(searchWords) || b.REnName.Contains(searchWords.ToLower())
                            );
                }
                else
                {
                    return b.IsDelete == false && b.IsFrozen == false;
                }
            };

            Func<Model.Role, int> orderFunc = (b) => { return b.ID; };

            var tmp = RoleServicesEntity.LoadPageEntitys(pageIndex, pageSize, out totalCount, whereFunc, orderFunc, true).ToList();



            //分页字符串
            ViewBag.pageStr = Common.Tools.ShowPageNavigate(pageSize, pageIndex, totalCount, "&searchWords=" + searchWords + "&userID=" + userID + "&returnUrl=" + Url.Encode(returnUrl));

            return View(tmp);
        }



        public JsonResult SetUserRoleAjax(int userid, int roleid)
        {
            JObject json = new JObject();

            //1.判断角色列表中，是否有该角色
            var dbrole = RoleServicesEntity.LoadEntitys(u => u.ID == roleid && u.IsDelete == false).FirstOrDefault();
            if (dbrole == null)
            {
                json.Add(new JProperty("res", "-1"));
                json.Add(new JProperty("msg", "角色不存在."));
                return this.Json(json.ToString());
            }

            //2.判断用户是否存在
            var user = UserServicesEntity.LoadEntitys(u => u.ID == userid && u.IsDelete == false).FirstOrDefault();
            if (user == null)
            {
                json.Add(new JProperty("res", "-3"));
                json.Add(new JProperty("msg", "用户不存在."));
                return this.Json(json.ToString());
            }

            //3.判断 用户和角色 关联表中是否已经设置过该角色
            var dbur= UserRoleServicesEntity.LoadEntitys(u => u.RoleID == roleid && u.UserID == userid).FirstOrDefault();
            if (dbur != null)
            {
                json.Add(new JProperty("res", "-2"));
                json.Add(new JProperty("msg", "已经设置过该角色."));
                return this.Json(json.ToString());
            }


            User_Role ua = new User_Role() { RoleID = roleid, UserID = userid };
            if (UserRoleServicesEntity.Add(ua).ID > 0)
            {
                json.Add(new JProperty("res", "1"));
                json.Add(new JProperty("msg", "设置成功."));
                return this.Json(json.ToString());
            }
            else
            {
                json.Add(new JProperty("res", "0"));
                json.Add(new JProperty("msg", "设置失败."));
                return this.Json(json.ToString());
            }
        }



        public JsonResult CancelUserRoleAjax(int userid, int roleid)
        {
            JObject json = new JObject();

            //1.判断角色列表中，是否有该角色
            var dbrole = RoleServicesEntity.LoadEntitys(u => u.ID == roleid && u.IsDelete == false).FirstOrDefault();
            if (dbrole == null)
            {
                json.Add(new JProperty("res", "-1"));
                json.Add(new JProperty("msg", "角色不存在."));
                return this.Json(json.ToString());
            }

            //2.判断用户是否存在
            var user = UserServicesEntity.LoadEntitys(u => u.ID == userid && u.IsDelete == false).FirstOrDefault();
            if (user == null)
            {
                json.Add(new JProperty("res", "-3"));
                json.Add(new JProperty("msg", "用户不存在."));
                return this.Json(json.ToString());
            }

            //3.判断 用户和角色 关联表中是否已经设置过该角色
            var dbur = UserRoleServicesEntity.LoadEntitys(u => u.RoleID == roleid && u.UserID == userid).FirstOrDefault();
            if (dbur == null)
            {
                json.Add(new JProperty("res", "-2"));
                json.Add(new JProperty("msg", "没有设置过该角色."));
                return this.Json(json.ToString());
            }

            if (UserRoleServicesEntity.Delete(dbur))
            {
                json.Add(new JProperty("res", "1"));
                json.Add(new JProperty("msg", "取消成功."));
                return this.Json(json.ToString());
            }
            else
            {
                json.Add(new JProperty("res", "0"));
                json.Add(new JProperty("msg", "取消失败."));
                return this.Json(json.ToString());
            }
        }
        #endregion

        #region 重置用户密码
        [HttpGet]
        public ActionResult SetUserPwd(int userID)
        {
            //取消返回页面
            ViewBag.returnUrl = Request.UrlReferrer == null ? Url.Action("Index", "User") : Request.UrlReferrer.ToString();

            User dbUser = UserServicesEntity.LoadEntitys(u => u.ID == userID).FirstOrDefault();

            return View(dbUser);
        }

        [HttpPost]
        public ActionResult SetUserPwd(User user, string returnUrl)
        {
            //取消返回页面
            ViewBag.returnUrl = Request.UrlReferrer == null ? Url.Action("Index", "User") : Request.UrlReferrer.ToString();

            User dbUser = UserServicesEntity.LoadEntitys(u => u.ID == user.ID).FirstOrDefault();
            dbUser.Pwd = Common.Encryption.MD5(user.Pwd, true);

            if (UserServicesEntity.Update(dbUser))
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View(user);
            }
        }

        #endregion

        #region 设置用户权限
        public ActionResult SetUserAction(int userID, int pageSize = 10, int pageIndex = 1, string searchWords = "", string returnUrl="")
        {
            //取消返回页面
            returnUrl = returnUrl == "" ? Request.UrlReferrer.ToString() : returnUrl;
            ViewBag.returnUrl = returnUrl;

            //返回前台页数和每页条数
            ViewBag.pageIndex = pageIndex;
            ViewBag.pageSize = pageSize;
            ViewBag.searchWords = searchWords;

            var user = UserServicesEntity.LoadEntitys(u => u.ID == userID && !u.IsDelete).FirstOrDefault();
            if (user == null)
            {
                return Redirect("/error.html");
            }

            ViewBag.user = user;
            ViewBag.userActions = UserServicesEntity.GetAllActions(user);

            //过滤条件后的数据总条数 
            int totalCount;

            Func<Model.Action, bool> whereFunc = (b) =>
            {
                if (!string.IsNullOrEmpty(searchWords))
                {
                    return
                            (
                                b.IsDelete == false && b.IsFrozen == false
                            )
                            &&
                            (
                                   b.AName.Contains(searchWords) || b.AUrl.Contains(searchWords.ToLower())
                            );
                }
                else
                {
                    return b.IsDelete == false && b.IsFrozen == false;
                }
            };

            Func<Model.Action, int> orderFunc = (b) => { return b.ID; };

            var tmp = ActionServicesEntity.LoadPageEntitys(pageIndex, pageSize, out totalCount, whereFunc, orderFunc, true).ToList();



            //分页字符串
            ViewBag.pageStr = Common.Tools.ShowPageNavigate(pageSize, pageIndex, totalCount, "&searchWords=" + searchWords + "&userID=" + userID + "&returnUrl=" + Url.Encode(returnUrl));

            return View(tmp);
        }


        /// <summary>
        /// 异步设置用户权限
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="actionid"></param>
        /// <returns></returns>
        public JsonResult SetUserActionAjax(int userid, int actionid)
        {
            JObject json = new JObject();

            //1.判断权限列表中，是否有该权限
            var dbaction = ActionServicesEntity.LoadEntitys(u => u.ID == actionid && u.IsDelete == false).FirstOrDefault();
            if (dbaction == null)
            {
                json.Add(new JProperty("res", "-1"));
                json.Add(new JProperty("msg", "权限不存在."));
                return this.Json(json.ToString());//权限不存在
            }


            //2.判断用户是否存在
            var user = UserServicesEntity.LoadEntitys(u => u.ID == userid && u.IsDelete == false).FirstOrDefault();
            if (user == null)
            {
                json.Add(new JProperty("res", "-3"));
                json.Add(new JProperty("msg", "用户不存在."));
                return this.Json(json.ToString());
            }

            //3.判断 用户和权限 关联表中是否已经设置过该权限
            var dbua = User_ActionServicesEntity.LoadEntitys(u => u.ActionID == actionid && u.UserID == userid).FirstOrDefault();
            if (dbua != null)
            {
                json.Add(new JProperty("res", "-2"));
                json.Add(new JProperty("msg", "已经设置过该权限."));
                return this.Json(json.ToString());
            }


            User_Action ua = new User_Action() { ActionID = actionid, UserID = userid };
            if (User_ActionServicesEntity.Add(ua).ID > 0)
            {
                json.Add(new JProperty("res", "1"));
                json.Add(new JProperty("msg", "设置成功."));
                return this.Json(json.ToString());
            }
            else
            {
                json.Add(new JProperty("res", "0"));
                json.Add(new JProperty("msg", "设置失败."));
                return this.Json(json.ToString());
            }
        }



        public JsonResult CancelUserActionAjax(int userid, int actionid)
        {
            JObject json = new JObject();

            //1.判断权限列表中，是否有该权限
            var dbaction = ActionServicesEntity.LoadEntitys(u => u.ID == actionid && u.IsDelete == false).FirstOrDefault();
            if (dbaction == null)
            {
                json.Add(new JProperty("res", "-1"));
                json.Add(new JProperty("msg", "权限不存在."));
                return this.Json(json.ToString());//权限不存在
            }

            //2.判断用户是否存在
            var user = UserServicesEntity.LoadEntitys(u => u.ID == userid && u.IsDelete == false).FirstOrDefault();
            if (user == null)
            {
                json.Add(new JProperty("res", "-3"));
                json.Add(new JProperty("msg", "用户不存在."));
                return this.Json(json.ToString());
            }

            //3.判断 用户和权限 关联表中是否已经设置过该权限
            var dbua = User_ActionServicesEntity.LoadEntitys(u => u.ActionID == actionid && u.UserID == userid).FirstOrDefault();
            if (dbua == null)
            {
                json.Add(new JProperty("res", "-2"));
                json.Add(new JProperty("msg", "没有设置过该权限或该权限在其拥有的角色中(要删除角色中的权限请去角色->设置权限)."));
                return this.Json(json.ToString());
            }



            if (User_ActionServicesEntity.Delete(dbua))
            {
                json.Add(new JProperty("res", "1"));
                json.Add(new JProperty("msg", "取消成功."));
                return this.Json(json.ToString());
            }
            else
            {
                json.Add(new JProperty("res", "0"));
                json.Add(new JProperty("msg", "取消失败."));
                return this.Json(json.ToString());
            }
        }
        #endregion
  
    }
}
