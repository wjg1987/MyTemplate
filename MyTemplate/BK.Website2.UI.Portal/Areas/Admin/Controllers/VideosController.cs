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
    public class VideosController : BaseController
    {
        IVideosServices videosServices = new VideosServices();



        public ActionResult Index(int pageSize = 10, int pageIndex = 1, string searchWords = "")
        {
            //返回前台页数和每页条数
            ViewBag.pageIndex = pageIndex;
            ViewBag.pageSize = pageSize;
            ViewBag.searchWords = searchWords;

            //过滤条件后的数据总条数 
            int totalCount;

            Func<Videos, bool> whereFunc = (b) =>
            {
                if (!string.IsNullOrEmpty(searchWords))
                {
                    return !b.IsDelete && (b.VName.Contains(searchWords) || b.VName.Contains(searchWords.ToLower()));
                }
                else
                {
                    return !b.IsDelete;
                }
            };

            Func<Videos, int> orderFunc = (b) => { return b.ID; };

            var tmp = videosServices.LoadPageEntitys(pageIndex, pageSize, out totalCount, whereFunc, orderFunc, true).ToList();



            //分页字符串
            ViewBag.pageStr = Common.Tools.ShowPageNavigate(pageSize, pageIndex, totalCount, "&searchWords=" + searchWords);

            return View(tmp);
        }


        public ActionResult Add()
        {
            //取消返回页面
            ViewBag.returnUrl = Request.UrlReferrer == null ? Url.Action("Index", "Videos") : Request.UrlReferrer.ToString();

            Videos model = new Videos();
            model.IsDelete = false;
            model.Sequence = 1000;//默认排序数字1000
            model.IsView = true;
            model.AddTime = DateTime.Now;
            return View(model);
        }


        [HttpPost]
        [ValidateInput(false)]//添加带有html标签的数据的时候必须关闭此验证
        public ActionResult Add(Videos model, string returnUrl)
        {
            //取消添加 返回页面
            ViewBag.returnUrl = returnUrl;

            if (ModelState.IsValid)
            {

                if (!string.IsNullOrEmpty(model.ThemPic) && model.ThemPic.IndexOf("/UserUpload/") == 0)
                {
                    model.ThemPic = Common.Common.WebSiteUrl + model.ThemPic;
                }
                if (!string.IsNullOrEmpty(model.MPEG4Url) && model.MPEG4Url.IndexOf("/UserUpload/") == 0)
                {
                    model.MPEG4Url = Common.Common.WebSiteUrl + model.MPEG4Url;
                }
                if (!string.IsNullOrEmpty(model.OggUrl) && model.OggUrl.IndexOf("/UserUpload/") == 0)
                {
                    model.OggUrl = Common.Common.WebSiteUrl + model.OggUrl;
                }
                if (!string.IsNullOrEmpty(model.WebMUrl) && model.WebMUrl.IndexOf("/UserUpload/") == 0)
                {
                    model.WebMUrl = Common.Common.WebSiteUrl + model.WebMUrl;
                }
                model.AddTime = DateTime.Now;
                model = videosServices.Add(model);
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
            ViewBag.returnUrl = Request.UrlReferrer == null ? Url.Action("Index", "Videos") : Request.UrlReferrer.ToString();

            var model = videosServices.LoadEntitys(b => b.ID == id).FirstOrDefault();
            return View(model);
        }


        [HttpPost]
        [ValidateInput(false)]//添加带有html标签的数据的时候必须关闭此验证
        public ActionResult Edit(Videos model, string returnUrl)
        {
            //取消添加 返回页面
            ViewBag.returnUrl = returnUrl;
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(model.ThemPic) && model.ThemPic.IndexOf("/UserUpload/") == 0)
                {
                    model.ThemPic = Common.Common.WebSiteUrl + model.ThemPic;
                }
                if (!string.IsNullOrEmpty(model.MPEG4Url) && model.MPEG4Url.IndexOf("/UserUpload/") == 0)
                {
                    model.MPEG4Url = Common.Common.WebSiteUrl + model.MPEG4Url;
                }
                if (!string.IsNullOrEmpty(model.OggUrl) && model.OggUrl.IndexOf("/UserUpload/") == 0)
                {
                    model.OggUrl = Common.Common.WebSiteUrl + model.OggUrl;
                }
                if (!string.IsNullOrEmpty(model.WebMUrl) && model.WebMUrl.IndexOf("/UserUpload/") == 0)
                {
                    model.WebMUrl = Common.Common.WebSiteUrl + model.WebMUrl;
                }
                if (videosServices.Update(model))
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(model);
                }
            }
            return View(model);
        }


        public int Delete(int id)
        {

            var model = videosServices.LoadEntitys(b => b.ID == id).FirstOrDefault();
            if (model == null)
            {
                return 0;
            }
            model.IsDelete = true;
            if (videosServices.Update(model))
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
