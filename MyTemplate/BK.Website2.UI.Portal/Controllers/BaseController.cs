using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using My.Template.Model;
using My.Template.UI.Portal.CommClass;

namespace My.Template.UI.Portal.Controllers
{
    public class BaseController : Controller
    {
        //重写调用方法之前的处理
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            //页面序号 用于后台首页左侧导航展开还是显示的判断
            ViewBag.navindex = Request["t"] == null ? 0 : Convert.ToInt32(Request["t"].ToString());

        }
    }
}
