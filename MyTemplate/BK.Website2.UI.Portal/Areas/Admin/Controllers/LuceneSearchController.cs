using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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


        /// <summary>
        /// 从lucene常见的文档索引中 搜索数据
        /// </summary>
        /// <returns></returns>
        private List<ViewSarchContentModel> SearchBookContent()
        {
            //该路径需要些到web.config中
            string indexPath = ConfigurationManager.AppSettings["LuceneDir"];


            List<string> kw = Common.Tools.GetPanGuWord(Request["txtContent"]);

            FSDirectory directory = FSDirectory.Open(new DirectoryInfo(indexPath), new NoLockFactory());
            IndexReader reader = IndexReader.Open(directory, true);
            IndexSearcher searcher = new IndexSearcher(reader);
            //搜索条件 PhraseQuery 只能搜索单一的搜索条件
            PhraseQuery query = new PhraseQuery();
            //MultiPhraseQuery query = new MultiPhraseQuery();
            foreach (string word in kw)//先用空格，让用户去分词，空格分隔的就是词“计算机   专业”
            {
                query.Add(new Term("Content", word));
            }
            //query.Add(new Term("body","语言"));--可以添加查询条件，两者是add关系.顺序没有关系.
            //query.Add(new Term("body", "大学生"));
            //query.Add(new Term("body", kw));//body中含有kw的文章

            //多个查询条件的词之间的最大距离.在文章中相隔太远 也就无意义.（例如 “大学生”这个查询条件和"简历"这个查询条件之间如果间隔的词太多也就没有意义了。）
            query.SetSlop(100);
            //TopScoreDocCollector是盛放查询结果的容器
            TopScoreDocCollector collector = TopScoreDocCollector.create(1000, true);
            //根据query查询条件进行查询，查询结果放入collector容器
            searcher.Search(query, null, collector);
            //得到所有查询结果中的文档,GetTotalHits():表示总条数   TopDocs(300, 20);//表示得到300（从300开始），到320（结束）的文档内容.
            ScoreDoc[] docs = collector.TopDocs(0, collector.GetTotalHits()).scoreDocs;
            //可以用来实现分页功能
            List<ViewSarchContentModel> list = new List<ViewSarchContentModel>();
            for (int i = 0; i < docs.Length; i++)
            {
                ViewSarchContentModel viewModel = new ViewSarchContentModel();
                //搜索ScoreDoc[]只能获得文档的id,这样不会把查询结果的Document一次性加载到内存中。降低了内存压力，需要获得文档的详细内容的时候通过searcher.Doc来根据文档id来获得文档的详细内容对象Document.
                int docId = docs[i].doc;//得到查询结果文档的id（Lucene内部分配的id）
                Document doc = searcher.Doc(docId);//找到文档id对应的文档详细信息
                viewModel.Id = doc.Get("Id");
                viewModel.Title = doc.Get("Title");
                viewModel.Content = Common.Tools.CreateHightLight(Request["txtContent"], doc.Get("Content"));//搜索内容关键字高亮显示
                list.Add(viewModel);

            }

            //SearchDetails searchDetail = new SearchDetails();
            //searchDetail.Id = Guid.NewGuid();
            //searchDetail.KeyWords = Request["txtContent"];
            //searchDetail.SearchDateTime = DateTime.Now;
            //SearchDetailsService.AddEntity(searchDetail);

            return list;
        }

        #region 搜索热词
        //public ActionResult AutoComplete()
        //{
        //    string term = Request["term"];
        //    List<string> list = KeyWordsRankService.GetSearchWord(term);
        //    return Json(list.ToArray(), JsonRequestBehavior.AllowGet);
        //}
        #endregion
    }
}
