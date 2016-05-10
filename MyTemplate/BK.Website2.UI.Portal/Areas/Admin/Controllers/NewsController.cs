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
    public class NewsController : BaseController
    {
        INewsServices newsServices = new NewsServices();



        public ActionResult Index(int pageSize = 10, int pageIndex = 1, string searchWords = "")
        {
            //返回前台页数和每页条数
            ViewBag.pageIndex = pageIndex;
            ViewBag.pageSize = pageSize;
            ViewBag.searchWords = searchWords;

            //过滤条件后的数据总条数 
            int totalCount;

            Func<News, bool> whereFunc = (b) =>
            {
                if (!string.IsNullOrEmpty(searchWords))
                {
                    return !b.IsDelete  && (b.Title.Contains(searchWords)||b.Content.Contains(searchWords));
                }
                else
                {
                    return !b.IsDelete;
                }
            };

            Func<News, int> orderFunc = (b) => { return b.ID; };

            var tmp = newsServices.LoadPageEntitys(pageIndex, pageSize, out totalCount, whereFunc, orderFunc, true).ToList();

          

            //分页字符串
            ViewBag.pageStr = Common.Tools.ShowPageNavigate(pageSize, pageIndex, totalCount, "&searchWords=" + searchWords);

            return View(tmp);
        }


        public ActionResult Add()
        {
            //取消返回页面
            ViewBag.returnUrl = Request.UrlReferrer == null ? Url.Action("Index", "News") : Request.UrlReferrer.ToString();

            News model = new News();
            model.IsDelete = false;//默认排序数字1000
            model.Sequence = 0;
            model.IsView = true;
            model.ClickNum = 0;
            model.NewsTypeID = 1;
            return View(model);
        }


        [HttpPost]
        [ValidateInput(false)]//添加带有html标签的数据的时候必须关闭此验证
        public ActionResult Add(News model, string returnUrl)
        {
            //取消添加 返回页面
            ViewBag.returnUrl = returnUrl;

            if (ModelState.IsValid)
            {

                if (!string.IsNullOrEmpty(model.Content))
                {
                    //内容上传图片
                    model.Content = model.Content.Replace("src=\"/UserUpload/", "src=\"" + Common.Common.WebSiteUrl + "/UserUpload/");
                }
                model.AddTime = DateTime.Now;
                model.IsDelete = false;

                model = newsServices.Add(model);
                if (model.ID > 0)
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return View(model);
                }
            }

            return View(model);
        }


        public ActionResult Edit(int id)
        {
            //取消返回页面
            ViewBag.returnUrl = Request.UrlReferrer == null ? Url.Action("Index", "News") : Request.UrlReferrer.ToString();

            var model = newsServices.LoadEntitys(b => b.ID == id).FirstOrDefault();
            return View(model);
        }


        [HttpPost]
        [ValidateInput(false)]//添加带有html标签的数据的时候必须关闭此验证
        public ActionResult Edit(News model, string returnUrl)
        {
            //取消添加 返回页面
            ViewBag.returnUrl = returnUrl;
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(model.Content))
                {
                    //内容上传图片
                    model.Content = model.Content.Replace("src=\"/UserUpload/", "src=\"" + Common.Common.WebSiteUrl + "/UserUpload/");
                }

                if (newsServices.Update(model))
                {
                    return Redirect(returnUrl);
                }
            }
            return View(model);
        }


        public int Delete(int id)
        {

            var model = newsServices.LoadEntitys(b => b.ID == id).FirstOrDefault();
            if (model == null)
            {
                return 0;
            }
            model.IsDelete = true;
            if (newsServices.Update(model))
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
