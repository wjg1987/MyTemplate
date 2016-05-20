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
            Response.Cookies["cp1"].Expires = DateTime.Now.AddDays(-1);
            Response.Cookies["cp2"].Expires = DateTime.Now.AddDays(-1);
            return Redirect("/admin/login");
        }
    }
}
