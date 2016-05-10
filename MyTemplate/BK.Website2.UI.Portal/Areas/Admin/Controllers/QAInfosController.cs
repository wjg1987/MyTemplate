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
    public class QAInfosController : BaseController
    {
        IQAInfosServices qAInfosServices = new QAInfosServices();

        public ActionResult Index(int pageSize = 10, int pageIndex = 1, string searchWords = "")
        {
            //返回前台页数和每页条数
            ViewBag.pageIndex = pageIndex;
            ViewBag.pageSize = pageSize;
            ViewBag.searchWords = searchWords;

            //过滤条件后的数据总条数 
            int totalCount;

            Func<QAInfos, bool> whereFunc = (b) =>
            {
                if (!string.IsNullOrEmpty(searchWords))
                {
                    return b.IsDelete == false && b.Question.Contains(searchWords);
                }
                else
                {
                    return b.IsDelete == false;
                }
            };

            Func<QAInfos, int> orderFunc = (b) => { return b.ID; };

            var tmp = qAInfosServices.LoadPageEntitys(pageIndex, pageSize, out totalCount, whereFunc, orderFunc, true).ToList();



            //分页字符串
            ViewBag.pageStr = Common.Tools.ShowPageNavigate(pageSize, pageIndex, totalCount, "&searchWords=" + searchWords);

            return View(tmp);
        }


        public ActionResult Add()
        {
            //取消返回页面
            ViewBag.returnUrl = Request.UrlReferrer == null ? Url.Action("Index", "QAInfos") : Request.UrlReferrer.ToString();

            QAInfos model = new QAInfos();
            model.IsView = true;
            model.IsDelete = false;
            return View(model);
        }


        [HttpPost]
        [ValidateInput(false)]//添加带有html标签的数据的时候必须关闭此验证
        public ActionResult Add(QAInfos model, string returnUrl)
        {
            //取消添加 返回页面
            ViewBag.returnUrl = returnUrl;

            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(model.Answer))
                {
                    //内容上传图片
                    model.Answer = model.Answer.Replace("src=\"/UserUpload/", "src=\"" + Common.Common.WebSiteUrl + "/UserUpload/");
                }
                model.IsDelete = false;
                model = qAInfosServices.Add(model);
               

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
            ViewBag.returnUrl = Request.UrlReferrer == null ? Url.Action("Index", "QAInfos") : Request.UrlReferrer.ToString();

            var model = qAInfosServices.LoadEntitys(b => b.ID == id).FirstOrDefault();
            return View(model);
        }


        [HttpPost]
        [ValidateInput(false)]//添加带有html标签的数据的时候必须关闭此验证
        public ActionResult Edit(QAInfos model, string returnUrl)
        {
            //取消添加 返回页面
            ViewBag.returnUrl = returnUrl;
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(model.Answer))
                {
                    //内容上传图片
                    model.Answer = model.Answer.Replace("src=\"/UserUpload/", "src=\"" + Common.Common.WebSiteUrl + "/UserUpload/");
                }
                if (qAInfosServices.Update(model))
                {
                    return Redirect(returnUrl);
                }
            }
            return View(model);
        }


        public int Delete(int id)
        {

            var model = qAInfosServices.LoadEntitys(b => b.ID == id).FirstOrDefault();
            if (model == null)
            {
                return 0;
            }
            model.IsDelete = true;
            if (qAInfosServices.Update(model))
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
