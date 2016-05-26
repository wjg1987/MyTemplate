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
    public class RoleController : BaseController
    {
         IRoleServices RoleServicesEntity { get; set; }
         IActionServices ActionServicesEntity { get; set; }
         IUserServices UserServicesEntity { get; set; }
         IRole_ActionServices RoleActionServicesEntity { get; set; }

        public ActionResult Index(int pageSize = 10, int pageIndex = 1, string searchWords = "")
        {
            //返回前台页数和每页条数
            ViewBag.pageIndex = pageIndex;
            ViewBag.pageSize = pageSize;
            ViewBag.searchWords = searchWords;

            //过滤条件后的数据总条数 
            int totalCount;

            Func<Role, bool> whereFunc = (b) =>
            {
                if (!string.IsNullOrEmpty(searchWords))
                {
                    return
                            (
                                b.IsDelete == false
                            )
                            &&
                            (
                                   b.RCNName.Contains(searchWords) || b.RCNName.Contains(searchWords.ToLower()) || b.REnName.Contains(searchWords)
                            );
                }
                else
                {
                    return b.IsDelete == false;
                }
            };

            Func<Role, int> orderFunc = (b) => { return b.ID; };

            var tmp = RoleServicesEntity.LoadPageEntitys(pageIndex, pageSize, out totalCount, whereFunc, orderFunc, true).ToList();



            //分页字符串
            ViewBag.pageStr = Common.Tools.ShowPageNavigate(pageSize, pageIndex, totalCount, "&searchWords=" + searchWords);

            return View(tmp);
        }


        public ActionResult Add()
        {
            //取消返回页面
            ViewBag.returnUrl = Request.UrlReferrer == null ? Url.Action("Index", "Role") : Request.UrlReferrer.ToString();

            Role model = new Role();
            model.IsDelete = false;
            return View(model);
        }


        [HttpPost]
        [ValidateInput(false)]//添加带有html标签的数据的时候必须关闭此验证
        public ActionResult Add(Role model, string returnUrl)
        {
            //取消添加 返回页面
            ViewBag.returnUrl = returnUrl;

            if (ModelState.IsValid)
            {
                Role dbRole = RoleServicesEntity.LoadEntitys(u => u.RCNName == model.RCNName).FirstOrDefault();
                if (dbRole != null)
                {
                    ModelState.AddModelError("RCNName", "角色名已存在。");
                    return View(model);
                }

                model.CreateTime = DateTime.Now;
                model.IsDelete = false;

                model = RoleServicesEntity.Add(model);
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
            ViewBag.returnUrl = Request.UrlReferrer == null ? Url.Action("Index", "Role") : Request.UrlReferrer.ToString();

            var model = RoleServicesEntity.LoadEntitys(b => b.ID == id).FirstOrDefault();
            return View(model);
        }


        [HttpPost]
        [ValidateInput(false)]//添加带有html标签的数据的时候必须关闭此验证
        public ActionResult Edit(Role model, string returnUrl)
        {
            //取消添加 返回页面
            ViewBag.returnUrl = returnUrl;
            if (ModelState.IsValid)
            {
                if (RoleServicesEntity.Update(model))
                {
                    return Redirect(returnUrl);
                }
            }
            return View(model);
        }


        public int Delete(int id)
        {

            var model = RoleServicesEntity.LoadEntitys(b => b.ID == id).FirstOrDefault();
            if (model == null)
            {
                return 0;
            }
            model.IsDelete = true;
            if (RoleServicesEntity.Update(model))
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }


     

        public ActionResult SetRoleAction(int roleID, int pageSize = 10, int pageIndex = 1, string searchWords = "")
        {
            //返回前台页数和每页条数
            ViewBag.pageIndex = pageIndex;
            ViewBag.pageSize = pageSize;
            ViewBag.searchWords = searchWords;

            var role = RoleServicesEntity.LoadEntitys(u => u.ID == roleID && !u.IsDelete).FirstOrDefault();
            if (role == null)
            {
                return Redirect("/error.html");
            }

            ViewBag.role = role;
            ViewBag.roleActions = RoleServicesEntity.GetActionsWithRole(role);

            //过滤条件后的数据总条数 
            int totalCount;

            Func<Model.Action, bool> whereFunc = (b) =>
            {
                if (!string.IsNullOrEmpty(searchWords))
                {
                    return
                            (
                                b.IsDelete == false
                            )
                            &&
                            (
                                   b.AName.Contains(searchWords) || b.AUrl.Contains(searchWords.ToLower())
                            );
                }
                else
                {
                    return b.IsDelete == false;
                }
            };

            Func<Model.Action, int> orderFunc = (b) => { return b.ID; };

            var tmp = ActionServicesEntity.LoadPageEntitys(pageIndex, pageSize, out totalCount, whereFunc, orderFunc, true).ToList();



            //分页字符串
            ViewBag.pageStr = Common.Tools.ShowPageNavigate(pageSize, pageIndex, totalCount, "&searchWords=" + searchWords + "&roleID=" + roleID);

            return View(tmp);
        }


        /// <summary>
        /// 异步设置用户权限
        /// </summary>
        /// <param name="roleid"></param>
        /// <param name="actionid"></param>
        /// <returns></returns>
        public JsonResult SetRoleActionAjax(int roleid, int actionid)
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


            //1.判断角色列表中，是否有该角色
            var dbrole = RoleServicesEntity.LoadEntitys(u => u.ID == roleid && u.IsDelete == false).FirstOrDefault();
            if (dbrole == null)
            {
                json.Add(new JProperty("res", "-3"));
                json.Add(new JProperty("msg", "角色不存在."));
                return this.Json(json.ToString());
            }

            //2.判断 角色和权限 关联表中是否已经设置过该权限
            var dbua = RoleActionServicesEntity.LoadEntitys(u => u.RoleID == roleid && u.ActionID == actionid).FirstOrDefault();
            if (dbua != null)
            {
                json.Add(new JProperty("res", "-2"));
                json.Add(new JProperty("msg", "已经设置过该权限."));
                return this.Json(json.ToString());
            }


            Role_Action ua = new Role_Action() { ActionID = actionid, RoleID = roleid };
            if (RoleActionServicesEntity.Add(ua).ID > 0)
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



        public JsonResult CancelRoleActionAjax(int roleid, int actionid)
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


            //1.判断角色列表中，是否有该角色
            var dbrole = RoleServicesEntity.LoadEntitys(u => u.ID == roleid && u.IsDelete == false).FirstOrDefault();
            if (dbrole == null)
            {
                json.Add(new JProperty("res", "-3"));
                json.Add(new JProperty("msg", "角色不存在."));
                return this.Json(json.ToString());
            }

            //2.判断 角色和权限 关联表中是否已经设置过该权限
            var dbua = RoleActionServicesEntity.LoadEntitys(u => u.RoleID == roleid && u.ActionID == actionid).FirstOrDefault();
            if (dbua == null)
            {
                json.Add(new JProperty("res", "-2"));
                json.Add(new JProperty("msg", "没有设置过该权限."));
                return this.Json(json.ToString());
            }


            if (RoleActionServicesEntity.Delete(dbua))
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
    }
}
