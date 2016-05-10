using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Schema;
using My.Template.BLL;
using My.Template.IBLL;
using My.Template.Model;
using System.Text.RegularExpressions;
using My.Template.UI.Portal.Areas.mobile.Controllers;
using Newtonsoft.Json.Linq;

namespace My.Template.UI.Portal.Controllers
{
    public class DefaultController : BaseController
    {


        public ActionResult Index()
        {
            return Redirect("/admin/login");
            return View();
        }

    }
}
