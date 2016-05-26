using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.PanGu;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Search;
using Lucene.Net.Store;
using My.Template.Model.LuceneSearch;
using My.Template.BLL;
using My.Template.Model;

namespace My.Template.UI.Portal.Areas.Admin.Controllers
{
    public class LuceneSearchController : Controller
    {

        public ActionServices ActionServicesEntity { get; set; }

        public ActionResult Index()
        {
            return View();
        }


    }
}
