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
    public class SiteInfoController : BaseController
    {
        ISiteInfoServices SiteInfoServicesEntity { get; set; }

        public ActionResult Edit()
        {

            //取消返回页面
            ViewBag.returnUrl = Request.UrlReferrer == null ? Url.Action("Index", "Home") : Request.UrlReferrer.ToString();

            var model = SiteInfoServicesEntity.LoadEntitys(b => true).FirstOrDefault();

            return View(model);
        }


        [HttpPost]
        [ValidateInput(false)]//添加带有html标签的数据的时候必须关闭此验证
        public ActionResult Edit(SiteInfo model, string returnUrl)
        {
            //取消添加 返回页面
            ViewBag.returnUrl = returnUrl;
            if (ModelState.IsValid)
            {
                if (SiteInfoServicesEntity.Update(model))
                {
                    Common.Common.SiteInfo = model;
                    return Redirect(returnUrl);
                }
            }
            return View(model);
        }

    }
}
