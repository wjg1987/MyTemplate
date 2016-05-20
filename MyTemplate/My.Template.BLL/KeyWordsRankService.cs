using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CZBK.ItcastOA.BLL
{
    //public partial class KeyWordsRankService : BaseService<KeyWordsRank>, IKeyWordsRankService
    //{
    //    /// <summary>
    //    /// 删除汇总表中的数据
    //    /// </summary>
    //    /// <returns></returns>
    //    public bool DeleteAllKeyWords()
    //    {
    //        string sql = "truncate table KeyWordsRank";
    //       return this.GetCurrentDbSession.ExecuteSql(sql)>0;
    //    }
    //    /// <summary>
    //    /// 向汇总表中插入数据
    //    /// </summary>
    //    /// <returns></returns>
    //    public bool InsertKeyWords()
    //    {
    //        string sql = "insert into KeyWordsRank(Id,KeyWords,SearchCount) select newid(),KeyWords,count(*) from SearchDetails where DateDiff(day,SearchDateTime,getdate())<=7 group by KeyWords";
    //        return this.GetCurrentDbSession.ExecuteSql(sql)>0;

    //    }
    //    /// <summary>
    //    /// 返回热词
    //    /// </summary>
    //    /// <param name="str"></param>
    //    /// <returns></returns>
    //    public List<string> GetSearchWord(string str)
    //    {
    //        string sql = "select KeyWords from KeyWordsRank where KeyWords like @msg";
    //      return  this.GetCurrentDbSession.ExecuteSelectQuery<string>(sql, new SqlParameter("@msg",str+"%"));
    //    }
    //}
}
