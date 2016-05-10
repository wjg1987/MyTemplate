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
    public class RoleController : BaseController
    {
        //
        // GET: /Admin/Role/
        IRoleServices roleServices = new RoleServices();

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
                                b.IsDelete == false && b.IsFrozen == false
                            )
                            &&
                            (
                                   b.RCNName.Contains(searchWords) || b.REnName.Contains(searchWords)
                            );
                }
                else
                {
                    return b.IsDelete == false && b.IsFrozen == false;
                }
            };

            Func<Role, int> orderFunc = (b) => { return b.ID; };

            var tmp = roleServices.LoadPageEntitys(pageIndex, pageSize, out totalCount, whereFunc, orderFunc, true).ToList();



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
                Role dbRole = roleServices.LoadEntitys(u => u.RCNName == model.RCNName).FirstOrDefault();
                if (dbRole != null)
                {
                    ModelState.AddModelError("RCNName", "角色名已存在。");
                    return View(model);
                }

                model.CreateTime = DateTime.Now;
                model.IsDelete = false;

                model = roleServices.Add(model);
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

            var model = roleServices.LoadEntitys(b => b.ID == id).FirstOrDefault();
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
                if (roleServices.Update(model))
                {
                    return Redirect(returnUrl);
                }
            }
            return View(model);
        }


        public int Delete(int id)
        {

            var model = roleServices.LoadEntitys(b => b.ID == id).FirstOrDefault();
            if (model == null)
            {
                return 0;
            }
            model.IsDelete = true;
            if (roleServices.Update(model))
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
}
