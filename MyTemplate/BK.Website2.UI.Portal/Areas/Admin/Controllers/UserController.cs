using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using My.Template.BLL;
using My.Template.IBLL;
using My.Template.Model;

namespace My.Template.UI.Portal.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        //
        // GET: /Admin/User/
        IUserServices userServices = new UserServices();



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
                                   b.Account.Contains(searchWords)
                            );
                }
                else
                {
                    return b.IsDelete == false && b.IsFrozen == false;
                }
            };

            Func<User, int> orderFunc = (b) => { return b.ID; };

            var tmp = userServices.LoadPageEntitys(pageIndex, pageSize, out totalCount, whereFunc, orderFunc, true).ToList();



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
                User dbUser = userServices.LoadEntitys(u => u.Account == model.Account).FirstOrDefault();
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
                uinfo.User = model;
                uinfo.NickName = NickName;

   
              

                model.Pwd = Common.Encryption.MD5(model.Pwd, true);
                model.IsDelete = false;
                model.UserInfo = uinfo;
                model.CreateTime = DateTime.Now;
                

                model = userServices.Add(model);
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

            var model = userServices.LoadEntitys(b => b.ID == id).FirstOrDefault();

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

                User dbUser = userServices.LoadEntitys(u => u.ID == model.ID).FirstOrDefault();
                if (!string.IsNullOrEmpty(HeadPic) && HeadPic.IndexOf("/UserUpload/") == 0)
                {
                    dbUser.UserInfo.HeadPic  = Common.Common.WebSiteUrl + HeadPic;
                }
                dbUser.UserInfo.NickName = NickName;
                dbUser.IsFrozen = model.IsFrozen;
              
                if (userServices.Update(dbUser))
                {
                    return Redirect(returnUrl);
                }
            }


            return View(model);
        }

        [HttpGet]
        public ActionResult SetUserRole(int userID)
        {
            //取消返回页面
            ViewBag.returnUrl = Request.UrlReferrer == null ? Url.Action("Index", "User") : Request.UrlReferrer.ToString();

            User dbUser = userServices.LoadEntitys(u => u.ID == userID).FirstOrDefault();

            List<int> rolInts = new List<int>();
            foreach (var ur in dbUser.User_Role)
            {
                rolInts.Add(ur.RoleID);
            }
            ViewBag.rolInts = rolInts;

            IRoleServices ser = new RoleServices();
            var list = ser.LoadEntitys(u=>u.IsFrozen==false && u.IsDelete == false).ToList();
            ViewBag.RoleIDs = list;
            return View(dbUser);
        }

        [HttpPost]
        public int SetUserRole(int roleID,int userID,bool setOrDelete)
        {
            IUser_RoleServices urserServices = new User_RoleServices();
            var dbur = urserServices.LoadEntitys(u => u.RoleID == roleID && u.UserID == userID).FirstOrDefault();

            if (setOrDelete) //如果是设置
            {
                if (dbur != null)
                {
                    return -1; //已经设置过该角色
                }
                else
                {
                    Model.User_Role ur = new User_Role();
                    ur.RoleID = roleID;
                    ur.UserID = userID;
                    if (urserServices.Add(ur) != null)
                    {
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
            else//删除角色
            {
              
                if (urserServices.Delete(dbur))
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }

            return 0;
        }

        public int Delete(int id)
        {

            var model = userServices.LoadEntitys(b => b.ID == id).FirstOrDefault();
            if (model == null)
            {
                return 0;
            }
            model.IsDelete = true;
            if (userServices.Update(model))
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }


        [HttpGet]
        public ActionResult SetUserPwd(int userID)
        {
            //取消返回页面
            ViewBag.returnUrl = Request.UrlReferrer == null ? Url.Action("Index", "User") : Request.UrlReferrer.ToString();

            User dbUser = userServices.LoadEntitys(u => u.ID == userID).FirstOrDefault();

            return View(dbUser);
        }

        [HttpPost]
        public ActionResult SetUserPwd(User user, string returnUrl)
        {
            //取消返回页面
            ViewBag.returnUrl = Request.UrlReferrer == null ? Url.Action("Index", "User") : Request.UrlReferrer.ToString();

            User dbUser = userServices.LoadEntitys(u => u.ID == user.ID).FirstOrDefault();
            dbUser.Pwd = Common.Encryption.MD5(user.Pwd, true);

            if (userServices.Update(dbUser))
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View(user);
            }
        }
    }
}
