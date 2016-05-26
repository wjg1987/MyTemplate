using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using My.Template.BLL;
using My.Template.Model.LuceneSearch;
using My.Template.UI.Portal.CommClass.LuceneSearch;

namespace My.Template.UI.Portal.Controllers
{
    public class SearchDemoController : Controller
    {
        //
        // GET: /Search/

        public ActionResult CreatSearchIndexFirst()
        {
            SearchManager sms = SearchManager.GetInstance();

            sms.CreatSearchIndexFirst();
            return View();
        }


        /// <summary>
        /// 搜索结果
        /// </summary>
        /// <param name="keyword">搜索关键字</param>
        /// <param name="pageIndex">页数</param>
        /// <param name="pageSize">每页条数</param>
        /// <returns></returns>
        public ActionResult Index(string keywords="",int pageIndex=1,int pageSize =10)
        {
            List<ViewSarchContentModel>  list  = new List<ViewSarchContentModel>();
            if (!string.IsNullOrEmpty(keywords))
            {
                Search ss = new Search(new KeywordsRecordsServices());

                list = ss.SearchContent(keywords, CreateIndexEmum.NewsIndex, pageIndex, pageSize);
            }
          
            ViewBag.keywords = keywords;
            return View(list);
        }
    }
}
