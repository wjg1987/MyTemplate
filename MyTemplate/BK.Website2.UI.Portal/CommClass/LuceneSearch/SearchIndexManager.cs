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


namespace My.Template.Model.LuceneSearch
{
    /// <summary>
    /// 单例模式 
    /// 1.在Global.asax的 Application_Start()方法中 开启StartThread
    /// 2.当后台添加或编辑 新闻（或任何别的需要搜索的内容时） 调用方法AddQueue 将要更新的内容添加到队列中
    /// 3.当后台删除内容时 调用方法DeleteQueue
    /// </summary>
    public sealed class SearchIndexManager
    {
        private static readonly SearchIndexManager searchIndexManager = new SearchIndexManager();
        private SearchIndexManager()
        {
        }
        public static SearchIndexManager GetInstance()
        {
            return searchIndexManager;
        }
        Queue<IndexContent> queue = new Queue<IndexContent>();
        /// <summary>
        /// 向队列中添加数据
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="title"></param>
        /// <param name="content"></param>
        public   void AddQueue(string Id,string title,string content)
        {
            IndexContent indexContent = new IndexContent();
            indexContent.Id = Id;
            indexContent.Title = title;
            indexContent.Content = content;
            indexContent.LuceneEnum = My.Template.Model.LuceneSearch.LuceneEnum.AddType;// 添加
            queue.Enqueue(indexContent);
        }
        /// <summary>
        /// 向队列中添加要删除数据
        /// </summary>
        /// <param name="Id"></param>
        public  void DeleteQueue(string Id)
        {
            IndexContent indexContent = new IndexContent();
            indexContent.Id = Id;
            indexContent.LuceneEnum = My.Template.Model.LuceneSearch.LuceneEnum.DeleType;//删除
            queue.Enqueue(indexContent);
        }

        /// <summary>
        /// 开启线程，扫描队列，从队列中获取数据
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
                }
                else
                {
                    Thread.Sleep(5000);//避免造成CPU空转
                }
            }
        }
        private void CreateIndexContent()
        {
            string indexPath = ConfigurationManager.AppSettings["LuceneDir"];//注意和磁盘上文件夹的大小写一致，否则会报错。将创建的分词内容放在该目录下。一定将路径名称写到web.config文件中
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
          

           while(queue.Count>0)
            {
                IndexContent indexContent=queue.Dequeue();//将队列中的数据出队
                writer.DeleteDocuments(new Term("Id", indexContent.Id.ToString()));
                if (indexContent.LuceneEnum == Model.LuceneSearch.LuceneEnum.DeleType)
                {
                    continue;
                }
                Document document = new Document();//表示一篇文档。
                //Field.Store.YES:表示是否存储原值。只有当Field.Store.YES在后面才能用doc.Get("number")取出值来.Field.Index. NOT_ANALYZED:不进行分词保存
                document.Add(new Field("Id", indexContent.Id.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));

                //Field.Index. ANALYZED:进行分词保存:也就是要进行全文的字段要设置分词 保存（因为要进行模糊查询）

                //Lucene.Net.Documents.Field.TermVector.WITH_POSITIONS_OFFSETS:不仅保存分词还保存分词的距离。
                document.Add(new Field("Title", indexContent.Title, Field.Store.YES, Field.Index.ANALYZED, Lucene.Net.Documents.Field.TermVector.WITH_POSITIONS_OFFSETS));

                document.Add(new Field("Content", indexContent.Content, Field.Store.YES, Field.Index.ANALYZED, Lucene.Net.Documents.Field.TermVector.WITH_POSITIONS_OFFSETS));

                writer.AddDocument(document);
            }

            writer.Close();//会自动解锁。
            directory.Close();//不要忘了Close，否则索引结果搜不到
        }
    }
}