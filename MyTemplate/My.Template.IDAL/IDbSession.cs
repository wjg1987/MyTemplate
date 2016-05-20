using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace My.Template.IDAL
{
    public partial interface IDbSession
    {
      //此处可以定义 dbsesion可以执行的方法
    int ExecuteSql(string sql, params System.Data.SqlClient.SqlParameter[] pars);
    List<T> ExecuteSelectQuery<T>(string sql, params System.Data.SqlClient.SqlParameter[] pars);
    }
}
