using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace My.Template.UI.Portal.Controllers
{
    public class IsVipLoginController : Controller
    {
        //重写调用方法之前的处理 验证用户是否登录
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //页面序号 用于后台首页左侧导航展开还是显示的判断
            ViewBag.navindex = Request["t"] == null ? 0 : Convert.ToInt32(Request["t"].ToString());

            var user = new My.Template.UI.Portal.CommClass.Comms().IsVipLogin();
            if (user == null)
            {
                filterContext.Result = new RedirectResult("/login/index");
            }
            else
            {
                Session.Add("CurLoginUser", user);
            }


            base.OnActionExecuting(filterContext);
        }
    }
}
