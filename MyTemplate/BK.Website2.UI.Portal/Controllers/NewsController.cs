using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using My.Template.BLL;
using My.Template.IBLL;
using My.Template.Model;

namespace My.Template.UI.Portal.Controllers
{
    public class NewsController : BaseController
    {
        //
        // GET: /News/
        INewsServices services = new NewsServices();
        public ActionResult Index(int pageIndex = 1)
        {

            int pageSize = 5;
            Func<News, bool> whereFunc = (b) =>
            {
                return b.IsDelete == false && b.IsView == true;
            };


            Func<News, int> orderFunc = (b) => { return b.Sequence; };
            int total = 0;
            var NewsList = services.LoadPageEntitys(pageIndex, pageSize, out total, whereFunc, orderFunc, true).ToList();

            //分页字符串
            ViewBag.pageStr = Common.Tools.ShowPageNavigate(pageSize, pageIndex, total, "");

            return View(NewsList);
        }


        public ActionResult Detail(int id)
        {

            var model = services.LoadEntitys(u => u.ID == id).FirstOrDefault();

            return View(model);
        }
    }
}
