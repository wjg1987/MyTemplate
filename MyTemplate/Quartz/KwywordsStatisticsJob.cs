using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quartz
{
    public class KwywordsStatisticsJob:IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            DAL.SqlHelper.ExecuteNonQuery("truncate table KeywordsRank");
            DAL.SqlHelper.ExecuteNonQuery(
                "insert into KeywordsRank(Keywords,Count) select Keywords,count(*) from KeywordsRecords where DateDiff(day,AddTime,getdate())<=7 group by Keywords");
        }
    }
}
