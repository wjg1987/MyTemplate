using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
 
namespace My.Template.UI.Portal.Areas.Admin.Controllers
{
    public class LogoutController : BaseController
    {
        public ActionResult Index()
        {
            Session["curLoginAdminID"] = null;
            Session["curLoginAdminAccount"] = null;
            Session["curLoginAdminHeadImg"] = null;
            Session["AdminValidateCode"] = null;

            return Redirect("/admin/login");
        }
    }
}
