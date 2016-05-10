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
    public class NoticeController : BaseController
    {
        INoticeServices NoticeServices = new NoticeServices();



        public ActionResult Index(int pageSize = 10, int pageIndex = 1, string searchWords = "")
        {
            //返回前台页数和每页条数
            ViewBag.pageIndex = pageIndex;
            ViewBag.pageSize = pageSize;
            ViewBag.searchWords = searchWords;

            //过滤条件后的数据总条数 
            int totalCount;

            Func<Notice, bool> whereFunc = (b) =>
            {
                if (!string.IsNullOrEmpty(searchWords))
                {
                    return !b.IsDelete && (b.Title.Contains(searchWords) || b.NDetails.Contains(searchWords));
                }
                else
                {
                    return !b.IsDelete;
                }
            };

            Func<Notice, int> orderFunc = (b) => { return b.ID; };

            var tmp = NoticeServices.LoadPageEntitys(pageIndex, pageSize, out totalCount, whereFunc, orderFunc, false).ToList();



            //分页字符串
            ViewBag.pageStr = Common.Tools.ShowPageNavigate(pageSize, pageIndex, totalCount, "&searchWords=" + searchWords);

            return View(tmp);
        }


        public ActionResult Add()
        {
            //取消返回页面
            ViewBag.returnUrl = Request.UrlReferrer == null ? Url.Action("Index", "Notice") : Request.UrlReferrer.ToString();

            Notice model = new Notice();
            model.IsDelete = false;
            model.Sequence = 1000;//默认排序数字1000
            model.AddTime = DateTime.Now;
            model.ExpireTime = DateTime.Now.AddMonths(1);
            model.LinkUrl = "javascript:;";
            return View(model);
        }


        [HttpPost]
        [ValidateInput(false)]//添加带有html标签的数据的时候必须关闭此验证
        public ActionResult Add(Notice model, string returnUrl)
        {
            //取消添加 返回页面
            ViewBag.returnUrl = returnUrl;

            if (ModelState.IsValid)
            {
                model.AddTime = DateTime.Now;
                model.IsDelete = false;
                model = NoticeServices.Add(model);
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
            ViewBag.returnUrl = Request.UrlReferrer == null ? Url.Action("Index", "Notice") : Request.UrlReferrer.ToString();

            var model = NoticeServices.LoadEntitys(b => b.ID == id).FirstOrDefault();
            return View(model);
        }


        [HttpPost]
        [ValidateInput(false)]//添加带有html标签的数据的时候必须关闭此验证
        public ActionResult Edit(Notice model, string returnUrl)
        {
            //取消添加 返回页面
            ViewBag.returnUrl = returnUrl;
            if (ModelState.IsValid)
            {
                if (NoticeServices.Update(model))
                {
                    return Redirect(returnUrl);
                }
            }
            return View(model);
        }


        public int Delete(int id)
        {

            var model = NoticeServices.LoadEntitys(b => b.ID == id).FirstOrDefault();
            if (model == null)
            {
                return 0;
            }
            model.IsDelete = true;
            if (NoticeServices.Update(model))
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
