using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace My.Template.Model.LuceneSearch
{
    public class SearchIndexContent
    {
        /// <summary>
        /// 搜索的表的主键ID
        /// </summary>
        public string Id { get; set; }
        
        /// <summary>
        /// 标题(搜索范围--内容1--可根据实际情况相应添加搜索字段 那么在创建索引的时候也需要相应进行改变)
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 内容(搜索范围--内容2)
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 操作类型
        /// </summary>
        public LuceneEnum LuceneEnum { get; set; }

        /// <summary>
        /// 索引类型
        /// </summary>
        public CreateIndexEmum IndexEmum { get; set; }
    }
}