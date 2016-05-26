using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.PanGu;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Store;
using My.Template.BLL;
using My.Template.IBLL;
using My.Template.Model;
using My.Template.Model.LuceneSearch;
using PanGu;

namespace My.Template.UI.Portal.CommClass.LuceneSearch
{
    public class Search
    {
        IKeywordsRecordsServices KeywordsScoreServicesEntity { get; set; }

        public Search(IKeywordsRecordsServices KeywordsScoreServicesEntity)
        {
            this.KeywordsScoreServicesEntity = KeywordsScoreServicesEntity;
        }


        /// <summary>
        /// 从lucene常见的文档索引中 搜索数据
        /// </summary>
        /// <param name="keywords">搜索关键字</param>
        /// <returns></returns>
        public List<ViewSarchContentModel> SearchContent(string keywords, int pageIndex = 1, int pageSize = 20)
        {
            //该路径需要些到web.config中
            string indexPath = ConfigurationManager.AppSettings["LuceneDir"];


            //对搜索词分词
            List<string> kw = GetPanGuWord(keywords);

            FSDirectory directory = FSDirectory.Open(new DirectoryInfo(indexPath), new NoLockFactory());
            IndexReader reader = IndexReader.Open(directory, true);
            IndexSearcher searcher = new IndexSearcher(reader);


            BooleanQuery booleanQuery = new BooleanQuery();
            //Query query1 = new TermQuery(new Term("id", "1")); 


            //搜索条件 PhraseQuery  多关键字的搜索 － PhraseQuery 意思是 Content中 包含多个词语的搜索结果
            //http://kb.cnblogs.com/page/52661/
            PhraseQuery query = new PhraseQuery();
            PhraseQuery query2 = new PhraseQuery();

            foreach (string word in kw)//先用空格，让用户去分词，空格分隔的就是词“计算机   专业”
            {
                query.Add(new Term("Content", word));//--可以添加查询条件，两者是add关系.顺序没有关系.
                query2.Add(new Term("Title", word));//--可以添加查询条件，两者是add关系.顺序没有关系.
            }

            //http://blog.csdn.net/wzhg0508/article/details/11107647
            //1．MUST和MUST：取得连个查询子句的交集。 
            //2．MUST和MUST_NOT：表示查询结果中不能包含MUST_NOT所对应得查询子句的检索结果。 
            //3．SHOULD与MUST_NOT：连用时，功能同MUST和MUST_NOT。
            //4．SHOULD与MUST连用时，结果为MUST子句的检索结果,但是SHOULD可影响排序。
            //5．SHOULD与SHOULD：表示“或”关系，最终检索结果为所有检索子句的并集。
            //6．MUST_NOT和MUST_NOT：无意义，检索无结果。
            booleanQuery.Add(query, BooleanClause.Occur.SHOULD);
            booleanQuery.Add(query2, BooleanClause.Occur.SHOULD);

            //多个查询条件的词之间的最大距离.在文章中相隔太远 也就无意义.（例如 “大学生”这个查询条件和"简历"这个查询条件之间如果间隔的词太多也就没有意义了。）
            query.SetSlop(100);
            //TopScoreDocCollector是盛放查询结果的容器
            TopScoreDocCollector collector = TopScoreDocCollector.create(1000, true);
            //根据query查询条件进行查询，查询结果放入collector容器
            searcher.Search(query, null, collector);
            //得到所有查询结果中的文档,collector.GetTotalHits():表示总条数   TopDocs(300, 20);//表示（从300开始），到320（结束）的文档内容.
            ScoreDoc[] docs = collector.TopDocs((pageIndex -1) * pageSize+1, pageSize).scoreDocs;
            //可以用来实现分页功能
            List<ViewSarchContentModel> list = new List<ViewSarchContentModel>();

            #region 分页数据放入 显示的Model类中
            for (int i = 0; i < docs.Length; i++)
            {
                ViewSarchContentModel viewModel = new ViewSarchContentModel();
                //搜索ScoreDoc[]只能获得文档的id,这样不会把查询结果的Document一次性加载到内存中。降低了内存压力，需要获得文档的详细内容的时候通过searcher.Doc来根据文档id来获得文档的详细内容对象Document.
                int docId = docs[i].doc;//得到查询结果文档的id（Lucene内部分配的id）
                Document doc = searcher.Doc(docId);//找到文档id对应的文档详细信息
                viewModel.Id = doc.Get("Id");
                viewModel.Title = doc.Get("Title");
                viewModel.Content = CreateHightLight(keywords, doc.Get("Content"));//搜索内容关键字高亮显示
                viewModel.IndexEmum = CreateIndexEmum.ALLIndex;

                list.Add(viewModel);

            }
            #endregion


            #region 搜索词加入搜索记录中 此处只进行添加 统计热词由 定时任务在服务器闲置时进行更新统计
            KeywordsRecords searchDetail = new KeywordsRecords();
            searchDetail.Keywords = keywords;
            searchDetail.AddTime = DateTime.Now;
            KeywordsScoreServicesEntity.Add(searchDetail);
            #endregion
          

            return list;
        }



        /// <summary>
        /// 从lucene常见的文档索引中 搜索数据
        /// </summary>
        /// <param name="keywords">搜索关键字</param>
        /// <param name="indexEmum">搜索类型</param>
        /// <returns></returns>
        public List<ViewSarchContentModel> SearchContent(string keywords,CreateIndexEmum indexEmum,int pageIndex=1,int pageSize=20)
        {
            //该路径需要些到web.config中
            string indexPath = ConfigurationManager.AppSettings["LuceneDir"];


            //此处Request对象如果是封装在类中不是些在controller中 那么需要传递参数过来 如果是在
            List<string> kw = GetPanGuWord(keywords);

            FSDirectory directory = FSDirectory.Open(new DirectoryInfo(indexPath), new NoLockFactory());
            IndexReader reader = IndexReader.Open(directory, true);
            IndexSearcher searcher = new IndexSearcher(reader);


            //搜索条件 PhraseQuery  多关键字的搜索 － PhraseQuery 意思是 Content中 包含多个词语的搜索结果
            //http://kb.cnblogs.com/page/52661/
            BooleanQuery booleanQuery = new BooleanQuery();
            PhraseQuery query = new PhraseQuery();
            PhraseQuery query2 = new PhraseQuery();
            //多个查询条件的词之间的最大距离.在文章中相隔太远 也就无意义.（例如 “大学生”这个查询条件和"简历"这个查询条件之间如果间隔的词太多也就没有意义了。）
            ///PhraseQuery 可设置
            query.SetSlop(100);
            query2.SetSlop(100);

            foreach (string word in kw)//先用空格，让用户去分词，空格分隔的就是词“计算机   专业”
            {
                query.Add(new Term("Content", word));//--可以添加查询条件，两者是add关系.顺序没有关系.
                query2.Add(new Term("Title", word));//--可以添加查询条件，两者是add关系.顺序没有关系.
            }

            //query.Add(new Term("IndexEmum", Convert.ToInt32(indexEmum).ToString()));
            //http://blog.csdn.net/wzhg0508/article/details/11107647
            //1．MUST和MUST：取得连个查询子句的交集。 
            //2．MUST和MUST_NOT：表示查询结果中不能包含MUST_NOT所对应得查询子句的检索结果。 
            //3．SHOULD与MUST_NOT：连用时，功能同MUST和MUST_NOT。
            //4．SHOULD与MUST连用时，结果为MUST子句的检索结果,但是SHOULD可影响排序。
            //5．SHOULD与SHOULD：表示“或”关系，最终检索结果为所有检索子句的并集。
            //6．MUST_NOT和MUST_NOT：无意义，检索无结果。
            booleanQuery.Add(query, BooleanClause.Occur.SHOULD);
            booleanQuery.Add(query2, BooleanClause.Occur.SHOULD);


          
         
            //todo:试试 能否 Id列 按类型，文本列按关键字
            TermQuery termQuery = new TermQuery(new Term("IndexEnum", Convert.ToInt32(indexEmum).ToString()));
           

            BooleanQuery booleanTotal = new BooleanQuery();
            booleanTotal.Add(booleanQuery,BooleanClause.Occur.MUST);
            booleanTotal.Add(termQuery, BooleanClause.Occur.MUST);

            //TopScoreDocCollector是盛放查询结果的容器
            TopScoreDocCollector collector = TopScoreDocCollector.create(1000, true);
            //根据query查询条件进行查询，查询结果放入collector容器
            searcher.Search(booleanTotal, null, collector);
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
                viewModel.Title = CreateHightLight(keywords, doc.Get("Title"));
                viewModel.Content = CreateHightLight(keywords, doc.Get("Content"));//搜索内容关键字高亮显示


                CreateIndexEmum tmpindexEmum;
                CreateIndexEmum.TryParse(doc.Get("IndexEnum"), out tmpindexEmum);
                viewModel.IndexEmum = tmpindexEmum;
               

                switch (viewModel.IndexEmum)
                {
                    case CreateIndexEmum.NewsIndex:
                        //todo:连接要考虑静态化。。考虑mvc的路由设置
                        //viewModel.LinkUrl = "/news/detail?id";
                        break;
                    case CreateIndexEmum.Community:
                        break;
                    default:
                        break;
                }

                list.Add(viewModel);
            }

            #region 搜索词加入搜索记录中 此处只进行添加 统计热词由 定时任务在服务器闲置时进行更新统计
            KeywordsRecords searchDetail = new KeywordsRecords();
            searchDetail.Keywords = keywords;
            searchDetail.AddTime = DateTime.Now;
            KeywordsScoreServicesEntity.Add(searchDetail);
            #endregion

            return list;
        }


        /// <summary>
        /// 对输入的搜索的条件进行分词
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private  List<string> GetPanGuWord(string str)
        {
            Analyzer analyzer = new PanGuAnalyzer();
            TokenStream tokenStream = analyzer.TokenStream("", new StringReader(str));
            Lucene.Net.Analysis.Token token = null;
            List<string> list = new List<string>();
            while ((token = tokenStream.Next()) != null)
            {
                list.Add(token.TermText());
            }
            return list;
        }


        // /创建HTMLFormatter,参数为高亮单词的前后缀
        private static string CreateHightLight(string keywords, string Content)
        {
            PanGu.HighLight.SimpleHTMLFormatter simpleHTMLFormatter =
             new PanGu.HighLight.SimpleHTMLFormatter("<font color=\"red\">", "</font>");
            //创建Highlighter ，输入HTMLFormatter 和盘古分词对象Semgent
            PanGu.HighLight.Highlighter highlighter =
            new PanGu.HighLight.Highlighter(simpleHTMLFormatter,
            new Segment());
            //设置每个摘要段的字符数
            highlighter.FragmentSize = 150;
            //获取最匹配的摘要段
            var res = highlighter.GetBestFragment(keywords, Content);
            if (string.IsNullOrEmpty(res))
            {
                int length = Content.Length > 150 ? 150 : Content.Length;
                res = Content.Substring(0, length);
            }
            return res;
        }

    }
}