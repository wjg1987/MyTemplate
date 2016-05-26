using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using My.Template.BLL;
using My.Template.IBLL;
using My.Template.Model;
using My.Template.Model.AdminDataModel;
using Newtonsoft.Json.Linq;

namespace My.Template.UI.Portal.Areas.Admin.Controllers
{
    public class ActionController : BaseController
    {
        //
        // GET: /Admin/Role/
        IActionServices ActionServicesEntity { get; set; }

        public ActionResult Index(int pageSize = 10, int pageIndex = 1, string searchWords = "")
        {
            //返回前台页数和每页条数
            ViewBag.pageIndex = pageIndex;
            ViewBag.pageSize = pageSize;
            ViewBag.searchWords = searchWords;

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
                                   b.AName.Contains(searchWords) || b.AName.Contains(searchWords.ToLower())
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
            ViewBag.pageStr = Common.Tools.ShowPageNavigate(pageSize, pageIndex, totalCount, "&searchWords=" + searchWords);

            return View(tmp);
        }


        public ActionResult Add()
        {
            //取消返回页面
            ViewBag.returnUrl = Request.UrlReferrer == null ? Url.Action("Index", "Action") : Request.UrlReferrer.ToString();

            Model.Action model = new Model.Action();
            model.ASequence = 1000;
            model.IsDelete = false;
            model.IsFrozen = false;

            #region 权限类型
            List<SelectListItem> items = new List<SelectListItem>();

            IActionTypeServices atypeList = new ActionTypeServices();
            var typeList = atypeList.LoadEntitys(u => !u.IsDelete).ToList();
            foreach (var item in typeList)
            {
                items.Add(new SelectListItem() { Selected = false, Text = item.TName, Value = item.ID.ToString() });
            }
            ViewBag.ActionTypeID = items;
            #endregion

            #region 上级权限
            List<SelectListItem> items2 = new List<SelectListItem>();

            items2.Add(new SelectListItem() { Selected = false, Text = "请选择--", Value = "0" });
            ActionServices ser = new ActionServices();
            var aList = ser.LoadEntitys(u => u.ActionTypeID == Convert.ToInt32(ActionEnum.MenuAction) && u.ParentID == 0).ToList();

            int index = 0;
            foreach (var item in aList)
            {
                items2.Add(new SelectListItem() { Selected = false, Text = item.AName, Value = item.ID.ToString() });
                //ser.GetChildActions(item.ID, items2);
            }
            ViewBag.ParentID = items2;

            #endregion
          
            return View(model);
        }


        [HttpPost]
        [ValidateInput(false)]//添加带有html标签的数据的时候必须关闭此验证
        public ActionResult Add(Model.Action model, string returnUrl)
        {
            //取消添加 返回页面
            ViewBag.returnUrl = returnUrl;


            #region 权限类型
            List<SelectListItem> items = new List<SelectListItem>();

            IActionTypeServices atypeList = new ActionTypeServices();
            var typeList = atypeList.LoadEntitys(u => !u.IsDelete).ToList();
            foreach (var item in typeList)
            {
                items.Add(new SelectListItem() { Selected = false, Text = item.TName, Value = item.ID.ToString() });
            }
            ViewBag.ActionTypeIDs = items;
            #endregion

            #region 上级权限
            List<SelectListItem> items2 = new List<SelectListItem>();

            items2.Add(new SelectListItem() { Selected = false, Text = "请选择--", Value = "0" });
            ActionServices ser = new ActionServices();
            var aList = ser.LoadEntitys(u => u.ActionTypeID == Convert.ToInt32(ActionEnum.MenuAction) && u.ParentID == 0).ToList();

            int index = 0;
            foreach (var item in aList)
            {
                items2.Add(new SelectListItem() { Selected = false, Text = item.AName, Value = item.ID.ToString() });
                //ser.GetChildActions(item.ID, items2);

            }
            ViewBag.ParentIDs = items2;
            #endregion

            if (ModelState.IsValid)
            {
                Model.Action dbAction = ActionServicesEntity.LoadEntitys(u => u.AUrl == model.AUrl).FirstOrDefault();
                if (dbAction != null)
                {
                    ModelState.AddModelError("AName", "权限路径已存在。");
                    return View(model);
                }

                model.IsDelete = false;

                model = ActionServicesEntity.Add(model);
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
            ViewBag.returnUrl = Request.UrlReferrer == null ? Url.Action("Index", "Action") : Request.UrlReferrer.ToString();

            var model = ActionServicesEntity.LoadEntitysAsNotracking(b => b.ID == id && !b.IsDelete).FirstOrDefault();


            List<SelectListItem> items = new List<SelectListItem>();

            IActionTypeServices atypeList = new ActionTypeServices();
            var typeList = atypeList.LoadEntitys(u => !u.IsDelete).ToList();
            foreach (var item in typeList)
            {
                if (item.ID == model.ActionTypeID)
                {
                    items.Add(new SelectListItem() { Selected = true, Text = item.TName, Value = item.ID.ToString() });
                }
                else
                {
                    items.Add(new SelectListItem() {  Text = item.TName, Value = item.ID.ToString() });
                }
               
            }
            ViewBag.ActionTypeID = items;


            #region 上级权限
            List<SelectListItem> items2 = new List<SelectListItem>();

            items2.Add(new SelectListItem() { Selected = false, Text = "请选择--", Value = "0" });
            ActionServices ser = new ActionServices();
            var aList = ser.LoadEntitys(u => u.ActionTypeID == Convert.ToInt32(ActionEnum.MenuAction) && u.ParentID == 0).ToList();

            int index = 0;
            foreach (var item in aList)
            {
                if (item.ID == model.ParentID)
                {
                    items2.Add(new SelectListItem() { Selected = true,Text = item.AName, Value = item.ID.ToString() });
              
                }
                else
                {
                    items2.Add(new SelectListItem() { Text = item.AName, Value = item.ID.ToString() });
              
                }
               
            }
            ViewBag.ParentID = items2;

            #endregion

            return View(model);
        }


        [HttpPost]
        [ValidateInput(false)]//添加带有html标签的数据的时候必须关闭此验证
        public ActionResult Edit(Model.Action model, string returnUrl)
        {
            //取消添加 返回页面
            ViewBag.returnUrl = returnUrl;
            if (ModelState.IsValid)
            {
                if (ActionServicesEntity.Update(model))
                {
                    return Redirect(returnUrl);
                }
            }

            List<SelectListItem> items = new List<SelectListItem>();

            IActionTypeServices atypeList = new ActionTypeServices();
            var typeList = atypeList.LoadEntitys(u => !u.IsDelete).ToList();
            foreach (var item in typeList)
            {
                if (item.ID == model.ActionTypeID)
                {
                    items.Add(new SelectListItem() { Selected = true, Text = item.TName, Value = item.ID.ToString() });
                }
                else
                {
                    items.Add(new SelectListItem() {  Text = item.TName, Value = item.ID.ToString() });
                }

            }
            ViewBag.ActionTypeID = items;


            #region 上级权限
            List<SelectListItem> items2 = new List<SelectListItem>();

            items2.Add(new SelectListItem() { Text = "请选择--", Value = "0" });
            ActionServices ser = new ActionServices();
            var aList = ser.LoadEntitys(u => u.ActionTypeID == Convert.ToInt32(ActionEnum.MenuAction) && u.ParentID == 0).ToList();

            int index = 0;
            foreach (var item in aList)
            {
                if (item.ID == model.ParentID)
                {
                    items2.Add(new SelectListItem() { Selected = true, Text = item.AName, Value = item.ID.ToString() });

                }
                else
                {
                    items2.Add(new SelectListItem() { Text = item.AName, Value = item.ID.ToString() });

                }
            }
            ViewBag.ParentID = items2;

            #endregion

            return View(model);
        }


        public int Delete(int id)
        {

            var model = ActionServicesEntity.LoadEntitys(b => b.ID == id).FirstOrDefault();
            if (model == null)
            {
                return 0;
            }
            model.IsDelete = true;
            if (ActionServicesEntity.Update(model))
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }


        public JsonResult GetParentAction(int lev)
        {
            JObject json = new JObject();

            return this.Json(json.ToString());
        }
    }
}
