using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using My.Template.BLL;
using My.Template.IBLL;
using My.Template.Model;

namespace My.Template.UI.Portal.Areas.Admin.Controllers
{
    public class BannerController : BaseController
    {

        IBannerServices BannerServicesEntity { get; set; }
        IBannerTypeServices BannerTypeServicesEntity { get; set; }
        
        public ActionResult Index(int pageSize = 10, int pageIndex = 1, string searchWords = "")
        {
            //返回前台页数和每页条数
            ViewBag.pageIndex = pageIndex;
            ViewBag.pageSize = pageSize;
            ViewBag.searchWords = searchWords;

            //过滤条件后的数据总条数 
            int totalCount;

            Func<Banner,bool> whereFunc = (b) =>
            {
                if (!string.IsNullOrEmpty(searchWords))
                {
                    return b.IsDelete == false && (b.Title.Contains(searchWords) || b.Title.Contains(searchWords.ToLower()));
                }
                else
                {
                    return b.IsDelete == false;
                }
            };


            Func<Banner, int> orderFunc = (b) => { return b.Sequence; };

            var tmp = BannerServicesEntity.LoadPageEntitys(pageIndex, pageSize, out totalCount, whereFunc,orderFunc,true).ToList();

            //分页字符串
            ViewBag.pageStr = Common.Tools.ShowPageNavigate(pageSize, pageIndex, totalCount, "&searchWords=" + searchWords);

            return View(tmp);
        }

   
        public ActionResult Add()
        {
            //取消返回页面
            ViewBag.returnUrl = Request.UrlReferrer == null ? Url.Action("Index", "Banner") : Request.UrlReferrer.ToString();

            #region 获取BannerType类型列表
            var bannertypeList = BannerTypeServicesEntity.LoadEntitys(b=>true).ToList();
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (var item in bannertypeList)
            {
                list.Add(new SelectListItem { Text = item.TName, Value = item.ID.ToString() });
            }
            ViewBag.BannerTypeID = list;
            #endregion
             
            Banner model = new Banner();
            model.Sequence = 1000;//默认排序数字1000
            model.LinkUrl = "javascript:;";
            return View(model);
        }

        //
        // POST: /Admin/Banner/Create

        [HttpPost]
        public ActionResult Add(Banner model,string returnUrl)
        {
             //取消添加 返回页面
            ViewBag.returnUrl = returnUrl;

            #region 获取BannerType类型列表
            var bannertypeList = BannerTypeServicesEntity.LoadEntitys(b => true).ToList();
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (var item in bannertypeList)
            {
                list.Add(new SelectListItem { Text = item.TName, Value = item.ID.ToString() });
            }
            ViewBag.BannerTypeID = list;
            #endregion

            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(model.ImgPic) && model.ImgPic.IndexOf("/UserUpload/") == 0)
                {
                    model.ImgPic = Common.Common.WebSiteUrl + model.ImgPic;
                }
                model.AddTime = DateTime.Now;
                model.IsDelete = false;

                model = BannerServicesEntity.Add(model);
                if (model.ID > 0)
                {
                    return Redirect(returnUrl);
                }
            }

            return View(model);
        }

        //
        // GET: /Admin/Banner/Edit/5

        public ActionResult Edit(int id)
        {
            //取消返回页面
            ViewBag.returnUrl = Request.UrlReferrer == null ?Url.Action("Index","Banner") : Request.UrlReferrer.ToString();

            #region 获取BannerType类型列表
            var bannertypeList = BannerTypeServicesEntity.LoadEntitys(b => true).ToList();
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (var item in bannertypeList)
            {
                list.Add(new SelectListItem { Text = item.TName, Value = item.ID.ToString() });
            }
            ViewBag.BannerTypeID = list;
            #endregion

            var model =  BannerServicesEntity.LoadEntitys(b => b.ID == id).FirstOrDefault();
            return View(model);
        }

        //
        // POST: /Admin/Banner/Edit/5

        [HttpPost]
        public ActionResult Edit(Banner model,string returnUrl)
        {
            //取消添加 返回页面
            ViewBag.returnUrl = returnUrl;

            #region 获取BannerType类型列表
            var bannertypeList = BannerTypeServicesEntity.LoadEntitys(b => true).ToList();
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (var item in bannertypeList)
            {
                list.Add(new SelectListItem { Text = item.TName, Value = item.ID.ToString() });
            }
            ViewBag.BannerTypeID = list;
            #endregion


            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(model.ImgPic) && model.ImgPic.IndexOf("/UserUpload/") == 0)
                {
                    model.ImgPic = Common.Common.WebSiteUrl + model.ImgPic;
                }

                if (BannerServicesEntity.Update(model))
                {
                    return Redirect(returnUrl);
                }
            }
            return View(model);
        }

        //
        // GET: /Admin/Banner/Delete/5

        public int Delete(int id)
        {
          
            var model = BannerServicesEntity.LoadEntitys(b => b.ID == id).FirstOrDefault();
            if (model == null)
            {
                return 0;
            }
            model.IsDelete = true;
            if (BannerServicesEntity.Update(model))
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