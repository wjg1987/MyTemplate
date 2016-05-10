using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace My.Template.UI.Portal.Controllers
{
    public class LogoutController : Controller
    {
        //
        // GET: /Logout/

        public ActionResult Index()
        {
            Session["CurLoginUserID"] = null;//注销
            Session["CurLoginUserName"] = null;//注销
            return Redirect("/default/index");
        }
    }
}
