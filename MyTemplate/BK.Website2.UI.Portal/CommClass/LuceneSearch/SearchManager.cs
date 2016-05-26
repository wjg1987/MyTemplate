using System.Configuration;
using Lucene.Net.Analysis.PanGu;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using My.Template.BLL;
using My.Template.IBLL;


namespace My.Template.Model.LuceneSearch
{
    /// <summary>
    /// 单例模式 
    /// 1.在Global.asax的 Application_Start()方法中 开启StartThread
    /// 2.当后台添加或编辑 新闻（或任何别的需要搜索的内容时） 调用方法AddQueue 将要更新的内容添加到队列中
    /// 3.当后台删除内容时 调用方法DeleteQueue
    /// </summary>
    public sealed class SearchManager
    {
        private static readonly SearchManager searchIndexManager = new SearchManager();


        //注意和磁盘上文件夹的大小写一致，否则会报错。将创建的分词内容放在该目录下。一定将路径名称写到web.config文件中
        private static string indexPath = ConfigurationManager.AppSettings["LuceneDir"];

        Queue<SearchIndexContent> queue = new Queue<SearchIndexContent>();

        private SearchManager()
        {
        }

        public static SearchManager GetInstance()
        {
            return searchIndexManager;
        }
       

        #region 添加索引数据到队列
        /// <summary>
        /// 向队列中添加数据 后台更新的时候 更新成功了调用 此方法要结合实际需要搜索的列 改造。。
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <param name="indexEmum">索引类型</param>
        public void AddToQueue(string Id, string title, string content, CreateIndexEmum indexEmum)
        {
            SearchIndexContent indexContent = new SearchIndexContent();
            indexContent.Id = Id;
            indexContent.Title = title;
            indexContent.Content = content;
            indexContent.LuceneEnum = My.Template.Model.LuceneSearch.LuceneEnum.AddType;// 添加
            indexContent.IndexEmum = indexEmum;// 索引类型
            queue.Enqueue(indexContent);
        }
        #endregion


        #region 将要删除的索引 添加到队列中 做删除标记
        /// <summary>
        /// 向队列中添加要删除数据 后台更新的时候 更新成功了调用  此方法要结合实际需要搜索的列 改造。。
        /// </summary>
        /// <param name="Id"></param>
        public void DeleteQueue(string Id, CreateIndexEmum indexEmum)
        {
            SearchIndexContent indexContent = new SearchIndexContent();
            indexContent.Id = Id;
            indexContent.LuceneEnum = My.Template.Model.LuceneSearch.LuceneEnum.DeleType;//删除
            indexContent.IndexEmum = indexEmum;// 索引类型
            queue.Enqueue(indexContent);
        }
        #endregion


        #region 开启线程 从队列中出队要创建的索引数据 线程执行CreateIndexContent方法
        /// <summary>
        /// 开启线程，扫描队列，从队列中获取数据  此处可以用redis队列改造 部署在另外的服务器上
        /// </summary>
        public void StartThread()
        {
            Thread myThread = new Thread(WriteIndexContent);
            myThread.IsBackground = true;
            myThread.Start();
        }

        private void WriteIndexContent()
        {
            while (true)
            {
                if (queue.Count > 0)
                {
                    CreateIndexContent();
                    Common.LogHelper.logWriter.Info("--出队1个对象，，创建索引----");
                    
                }
                else
                {
                    Common.LogHelper.logWriter.Info("--创建索引线程休息5秒----");
                    Thread.Sleep(5000);//避免造成CPU空转
                }
            }
        }

        #endregion


        #region 创建索引的方法
        /// <summary>
        /// 创建要保存的数据 这里需要根据要查询的数据 在后台更新的时候调用不同的方法 创建索引-这里仅仅只是查询了新闻...
        /// </summary>
        private void CreateIndexContent()
        {

            FSDirectory directory = FSDirectory.Open(new DirectoryInfo(indexPath), new NativeFSLockFactory());//指定索引文件(打开索引目录) FS指的是就是FileSystem
            bool isUpdate = IndexReader.IndexExists(directory);//IndexReader:对索引进行读取的类。该语句的作用：判断索引库文件夹是否存在以及索引特征文件是否存在。
            if (isUpdate)
            {
                //同时只能有一段代码对索引库进行写操作。当使用IndexWriter打开directory时会自动对索引库文件上锁。
                //如果索引目录被锁定（比如索引过程中程序异常退出），则首先解锁（提示一下：如果我现在正在写着已经加锁了，但是还没有写完，这时候又来一个请求，那么不就解锁了吗？这个问题后面会解决）
                if (IndexWriter.IsLocked(directory))
                {
                    IndexWriter.Unlock(directory);
                }
            }
            IndexWriter writer = new IndexWriter(directory, new PanGuAnalyzer(), !isUpdate, Lucene.Net.Index.IndexWriter.MaxFieldLength.UNLIMITED);//向索引库中写索引。这时在这里加锁。


            while (queue.Count > 0)
            {
                SearchIndexContent indexContent = queue.Dequeue();//将队列中的数据出队

                //根据 索引类型 和 主键表id的组合删掉数据 Id在存储的时候也需要是 两个数据的组合.
                writer.DeleteDocuments(new Term("Primarykey", Convert.ToInt32(indexContent.IndexEmum).ToString() + indexContent.Id.ToString()));
                if (indexContent.LuceneEnum == Model.LuceneSearch.LuceneEnum.DeleType)
                {
                    continue;
                }
                Document document = new Document();//表示一篇文档。

                #region 要保存的数据的列 需要根据实际情况进行改造 有可能程序中可以搜索多个表的数据 那么要根据保存的数据类型进行添加

                //Field.Store.YES:表示是否存储原值。只有当Field.Store.YES在后面才能用doc.Get("number")取出值来.Field.Index. NOT_ANALYZED:不进行分词保存
                document.Add(new Field("Primarykey", Convert.ToInt32(indexContent.IndexEmum).ToString() + indexContent.Id.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));

                document.Add(new Field("Id", indexContent.Id.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));

                document.Add(new Field("IndexEnum", Convert.ToInt32(indexContent.IndexEmum).ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));

                //Field.Index. ANALYZED:进行分词保存:也就是要进行全文的字段要设置分词 保存（因为要进行模糊查询）

                //Lucene.Net.Documents.Field.TermVector.WITH_POSITIONS_OFFSETS:不仅保存分词还保存分词的距离。
                document.Add(new Field("Title", indexContent.Title, Field.Store.YES, Field.Index.ANALYZED, Lucene.Net.Documents.Field.TermVector.WITH_POSITIONS_OFFSETS));

                document.Add(new Field("Content", indexContent.Content, Field.Store.YES, Field.Index.ANALYZED, Lucene.Net.Documents.Field.TermVector.WITH_POSITIONS_OFFSETS));
                #endregion


                writer.AddDocument(document);
            }

            writer.Close();//会自动解锁。
            directory.Close();//不要忘了Close，否则索引结果搜不到
        }
        #endregion



        #region 第一次创建索引,查询后 调用Add方法 根据具体情况 下面查询的代码和要插入的数据有变动
        public void CreatSearchIndexFirst()
        {
            INewsServices newsServices = new NewsServices();
            var list = newsServices.LoadEntitys(u => true).ToList();
            foreach (var item in list)
            {
                AddToQueue(item.ID.ToString(), item.Title, item.Content, CreateIndexEmum.NewsIndex);
            }
        }
        #endregion

    }
}