using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using My.Template.IBLL;
using My.Template.Model;


namespace My.Template.BLL
{
    public partial class KeywordsRankServices : BaseServices<KeywordsRank>, IKeywordsRankService
    {
  
        /// <summary>
        /// 删除汇总表中的数据
        /// </summary>
        /// <returns></returns>
        public bool DeleteAllKeyWords()
        {
            string sql = "truncate table KeywordsRank";
            return this.dbSession.ExecuteSql(sql) > 0;

            //return this.CurrentDal.ExecuteNoneQuery(sql) > 0;
        }
        /// <summary>
        /// 向汇总表中插入数据
        /// </summary>
        /// <returns></returns>
        public bool InsertKeyWords()
        {
            string sql = "insert into KeywordsRank(Keywords,Count) select Keywords,count(*) from KeywordsScore where DateDiff(day,AddTime,getdate())<=7 group by Keywords";
            return this.dbSession.ExecuteSql(sql) > 0;
            //return this.CurrentDal.ExecuteNoneQuery(sql) > 0;
        }


        /// <summary>
        /// 返回热词
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public List<string> GetSearchWord(string str)
        {
            string sql = "select top 10 KeyWords from KeywordsRank where Keywords like @msg";
            return this.dbSession.ExecuteSelectQuery<string>(sql, new SqlParameter("@msg", str + "%"));
        }

       
    }
}
