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
    public class SinglePageController : BaseController
    {
            ISinglePageServices singlePageServices = new SinglePageServices();



            public ActionResult Index(int pageSize = 10, int pageIndex = 1, string searchWords = "")
            {
                //返回前台页数和每页条数
                ViewBag.pageIndex = pageIndex;
                ViewBag.pageSize = pageSize;
                ViewBag.searchWords = searchWords;

                //过滤条件后的数据总条数 
                int totalCount;

                Func<SinglePage, bool> whereFunc = (b) =>
                {
                    if (!string.IsNullOrEmpty(searchWords))
                    {
                        return b.IsDelete == false && b.PageName.Contains(searchWords);
                    }
                    else
                    {
                        return b.IsDelete == false;
                    }
                };

                Func<SinglePage, int> orderFunc = (b) => { return b.ID; };

                var tmp = singlePageServices.LoadPageEntitys(pageIndex, pageSize, out totalCount, whereFunc, orderFunc, true).ToList();

               

                //分页字符串
                ViewBag.pageStr = Common.Tools.ShowPageNavigate(pageSize, pageIndex, totalCount, "&searchWords=" + searchWords);

                return View(tmp);
            }


            public ActionResult Add()
            {
                //取消返回页面
                ViewBag.returnUrl = Request.UrlReferrer == null ? Url.Action("Index", "SinglePage") : Request.UrlReferrer.ToString();

                SinglePage model = new SinglePage();
                model.IsDelete = false;//默认排序数字1000
                return View(model);
            }

      
            [HttpPost]
            [ValidateInput(false)]//添加带有html标签的数据的时候必须关闭此验证
            public ActionResult Add(SinglePage model, string returnUrl)
            {
                //取消添加 返回页面
                ViewBag.returnUrl = returnUrl;

                if (ModelState.IsValid)
                {

                    if (!string.IsNullOrEmpty(model.PageContent))
                    {
                        //内容上传图片
                        model.PageContent = model.PageContent.Replace("src=\"/UserUpload/", "src=\"" + Common.Common.WebSiteUrl + "/UserUpload/");
                    }
                    model.AddTime = DateTime.Now;
                    model.IsDelete = false;

                    model = singlePageServices.Add(model);
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
                ViewBag.returnUrl = Request.UrlReferrer == null ? Url.Action("Index", "SinglePage") : Request.UrlReferrer.ToString();

                var model = singlePageServices.LoadEntitys(b => b.ID == id).FirstOrDefault();
                return View(model);
            }

        
            [HttpPost]
            [ValidateInput(false)]//添加带有html标签的数据的时候必须关闭此验证
            public ActionResult Edit(SinglePage model, string returnUrl)
            {
                //取消添加 返回页面
                ViewBag.returnUrl = returnUrl;
                if (ModelState.IsValid)
                {
                    if (!string.IsNullOrEmpty(model.PageContent))
                    {
                        //内容上传图片
                        model.PageContent = model.PageContent.Replace("src=\"/UserUpload/", "src=\"" + Common.Common.WebSiteUrl + "/UserUpload/");
                    }

                    if (singlePageServices.Update(model))
                    {
                        return Redirect(returnUrl);
                    }
                }
                return View(model);
            }

       
            public int Delete(int id)
            {

                var model = singlePageServices.LoadEntitys(b => b.ID == id).FirstOrDefault();
                if (model == null)
                {
                    return 0;
                }
                model.IsDelete = true;
                if (singlePageServices.Update(model))
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
