using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace My.Template.UI.Portal.Areas.Admin.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult NoAction()
        {
            return View();
        }
    }
}
