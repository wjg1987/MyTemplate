using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using My.Template.BLL;
using My.Template.Model;


namespace My.Template.UI.Portal.Areas.Admin.Controllers
{
    public class BannerTypeController : BaseController
    {
        //
        // GET: /Admin/BannerType/
        BannerTypeServices bannerTypeServices = new BannerTypeServices();

        public ActionResult Index(int pageSize = 10, int pageIndex = 1, string searchWords = "")
        {

            //返回前台页数和每页条数
            ViewBag.pageIndex = pageIndex;
            ViewBag.pageSize = pageSize;
            ViewBag.searchWords = searchWords;

            //过滤条件后的数据总条数 
            int totalCount;


            Func<BannerType, int> orderFunc = (b) => { return b.ID; };

            var tmp = bannerTypeServices.LoadPageEntitys(pageIndex, pageSize, out totalCount, b=>true, orderFunc, true).ToList();



            //分页字符串
            ViewBag.pageStr = Common.Tools.ShowPageNavigate(pageSize, pageIndex, totalCount, "&searchWords=" + searchWords);

            return View(tmp);

            
        }

        [HttpGet]
        public ActionResult Add()
        {
            //取消返回页面
            ViewBag.returnUrl = Request.UrlReferrer == null ? Url.Action("Index", "BannerType") : Request.UrlReferrer.ToString();

            return View();
        }

        [HttpPost]
        public ActionResult Add(BannerType bannertype , string returnUrl)
        {
            //取消添加 返回页面
            ViewBag.returnUrl = returnUrl;

            if(ModelState.IsValid)
            {
               var bt= bannerTypeServices.Add(bannertype);
               if (bt.ID > 0)
               {
                   return Redirect(returnUrl);
               }
               else
               {
                   return View(bannertype);
               }
            }

            return View(bannertype);
        }

        [HttpGet]
        public ActionResult Edit(int bannertypeId =0 )
        {
            //取消返回页面
            ViewBag.returnUrl = Request.UrlReferrer == null ? Url.Action("Index", "BannerType") : Request.UrlReferrer.ToString();

            BannerType bannertype = bannerTypeServices.LoadEntitys(b=>b.ID ==bannertypeId).FirstOrDefault();
            if (bannertype == null)
            {
                return Redirect(ViewBag.returnUrl);
            }

            return View(bannertype);

        }
         [HttpPost]
        public ActionResult Edit(BannerType bannertype , string returnUrl)
        {
            //取消添加 返回页面
            ViewBag.returnUrl = returnUrl;

            if (ModelState.IsValid)
            {
             
                if (bannerTypeServices.Update(bannertype))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return View(bannertype);
                }
            }

            return View(bannertype);
        }


        public int Delete(int bannnertypeId = 0)
        {
            BannerType bannertype = bannerTypeServices.LoadEntitys(b => b.ID == bannnertypeId).FirstOrDefault();
            if (bannertype == null)
            {
                return -1;
            }
            else
            {

                if (bannerTypeServices.Update(bannertype))
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
}
