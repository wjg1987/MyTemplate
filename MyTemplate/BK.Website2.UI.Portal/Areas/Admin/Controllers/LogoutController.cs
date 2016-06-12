using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
 
namespace My.Template.UI.Portal.Areas.Admin.Controllers
{
    public class LogoutController : Controller
    {
        public ActionResult Index()
        {
            Response.Cookies["sessionId"].Expires = DateTime.Now.AddDays(-1);

            //todo:cookie 可以不用过期  因为此处做了 直接登录 避免死循环 所以过期处理了
            Response.Cookies["cp1"].Expires = DateTime.Now.AddDays(-1);
            Response.Cookies["cp2"].Expires = DateTime.Now.AddDays(-1);
            return Redirect("/admin/login");
        }
    }
}
