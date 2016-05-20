using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using My.Template.EFDAL;
using My.Template.IDAL;

namespace My.Template.DALFactory
{

  
    /// <summary>
    /// 模拟了 dbcontext 上下文   封装的是整个dal层统一访问入口
    /// </summary>
    public partial class DbSession:IDbSession
    {

        //#region 模拟了 dbcontext 上下文   封装的是整个dal层统一访问入口
        //private IUserInfoDal _userInfoDal;
        //public IUserInfoDal UserInfoDal
        //{
        //    get
        //    {
        //        if(_userInfoDal == null)
        //        {
        //            _userInfoDal = SimpleDalFactory.GetUserInfoDal();
        //        }
        //        return _userInfoDal;
        //    }
        //}




        //private IUserDal _userDal;
        //public IUserDal UserDal
        //{
        //    get
        //    {
        //        if (_userDal == null)
        //        {
        //            _userDal = SimpleDalFactory.GetUserDal();
        //        }
        //        return _userDal;
        //    }
        //}

        //#endregion
      


        ///// <summary>
        ///// 让 EF上下文 保存 EF内部默认启用了 事务。 一次性提交
        ///// </summary>
        ///// <returns></returns>
        //public int SaveChanges()
        //{
        //    return DbContextFactory.GetCurDbContext().SaveChanges();
        //}
        public int ExecuteSql(string sql, params System.Data.SqlClient.SqlParameter[] pars)
        {
            return Db.Database.ExecuteSqlCommand(sql, pars);
        }
        public List<T> ExecuteSelectQuery<T>(string sql, params System.Data.SqlClient.SqlParameter[] pars)
        {
            return Db.Database.SqlQuery<T>(sql, pars).ToList();
        }
    }
}
