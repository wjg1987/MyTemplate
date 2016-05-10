using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using My.Template.BLL;
using My.Template.IBLL;

namespace My.Template.UI.Portal.Controllers
{
    public class VideoController : BaseController
    {
        //
        // GET: /Video/

        public ActionResult Index()
        {
            IVideosServices vser = new VideosServices();

            var list = vser.LoadEntitys(u => u.IsDelete == false).OrderBy(u => u.Sequence).ToList();

            return View(list);
        }

    }
}
