using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace My.Template.Model.LuceneSearch
{
    public class ViewSarchContentModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        /// <summary>
        /// 索引类型
        /// </summary>
        public CreateIndexEmum IndexEmum { get; set; }

        //详情页连接地址
        public string LinkUrl { get; set; }
    }
}
