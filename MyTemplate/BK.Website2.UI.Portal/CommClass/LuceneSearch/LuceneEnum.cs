using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My.Template.Model.LuceneSearch
{
    public enum LuceneEnum
    {
        /// <summary>
        /// 添加
        /// </summary>
        AddType,
        /// <summary>
        /// 删除
        /// </summary>
        DeleType
    }



    /// <summary>
    /// 创建Lucene索引的类型 这里根据实际项目情况 进行调整..
    /// </summary>
    public enum  CreateIndexEmum
    {
        /// <summary>
        /// 所有种类查询
        /// </summary>
        ALLIndex,

        /// <summary>
        /// 创建新闻类的索引
        /// </summary>
        NewsIndex,
        /// <summary>
        /// 楼盘数据的 索引--
        /// </summary>
        Community,
    }
}
