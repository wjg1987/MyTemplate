using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace My.Template.UI.Portal.CommClass
{
    public class LoginFiterAttribute : ActionFilterAttribute
    {
        //重写调用方法之前的处理
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            
            var user = new My.Template.UI.Portal.CommClass.Comms().IsVipLogin();
            if (user == null)
            {
               filterContext.Result = new RedirectResult("/login/index");
            }
            else
            {
                filterContext.RequestContext.HttpContext.Session.Add("CurLoginUser", user);
            }


            base.OnActionExecuting(filterContext);
        }
    }
}