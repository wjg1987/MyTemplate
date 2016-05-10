using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.EntityClient;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Transactions;
using My.Template.IDAL;
using My.Template.Model;
using System.Data.Common;
using System.Data.Entity.Infrastructure;

namespace My.Template.EFDAL
{
    /// <summary>
    /// 封装对 User 表的增删改查操作
    /// </summary>
    public partial class UserDal : BaseDal<User>, IUserDal
    {


        //public int AddUser(User user, UserInfo userInfo, ShoppingCart shoppingCart)
        //{

            
        //    #region MyRegion
        //    //using (DbConnection conn = ((IObjectContextAdapter)db).ObjectContext.Connection)
        //    //{
        //    //    conn.Open();
        //    //    using (var tran = conn.BeginTransaction())
        //    //    {
        //    //        try
        //    //        {
                       
        //    //            DbCommand comm = conn.CreateCommand();
        //    //            comm.Transaction = tran;

        //    //            #region 添加购物车
        //    //            string sql2 = "insert into [ShoppingCart] (User_ID) values (U_ser_ID)";
        //    //            comm.Parameters.Clear();
        //    //            comm.Parameters.Add(new EntityParameter("U_ser_ID", DbType.Int32) { Value = 1 });
        //    //            comm.CommandText = sql2;
        //    //            comm.CommandType = CommandType.Text;
        //    //            comm.ExecuteNonQuery();
        //    //            #endregion
                       

        //    //            #region 参数化
        //    //            string sql = "insert into [User] (Account,Pwd,IsDelete,CreateTime,IsFrozen,Balance,FrozenBlance) output inserted.ID values (A_ccount,P_wd,Is_Delete,Create_Time,Is_Frozen,B_alance,Frozen_Blance)";
        //    //            comm.Parameters.Add(new EntityParameter("A_ccount", DbType.String) { Value = user.Account });
        //    //            comm.Parameters.Add(new EntityParameter("P_wd", DbType.String) { Value = user.Pwd });
        //    //            comm.Parameters.Add(new EntityParameter("Is_Delete", DbType.Boolean) { Value = user.IsDelete });
        //    //            comm.Parameters.Add(new EntityParameter("Create_Time", DbType.DateTime) { Value = user.CreateTime });
        //    //            comm.Parameters.Add(new EntityParameter("Is_Frozen", DbType.Boolean) { Value = user.IsFrozen });
        //    //            comm.Parameters.Add(new EntityParameter("B_alance", DbType.Decimal) { Value = user.Balance });
        //    //            comm.Parameters.Add(new EntityParameter("Frozen_Blance", DbType.Decimal) { Value = user.FrozenBlance });
        //    //            #endregion

        //    //            comm.CommandText = sql;
        //    //            int userid = Convert.ToInt32(comm.ExecuteNonQuery());


        //    //            #region 添加Userinfo
        //    //            string sql1 = "insert into [UserInfo] (HeadPic,NickName,Age,UserID) output inserted.ID values (@H_eadPic,@N_ickName,@A_ge,@U_serID)";
        //    //            comm.Parameters.Clear();
        //    //            comm.Parameters.Add(new EntityParameter("H_eadPic", DbType.String) { Value = userInfo.HeadPic });
        //    //            comm.Parameters.Add(new EntityParameter("N_ickName", DbType.String) { Value = userInfo.NickName });
        //    //            comm.Parameters.Add(new EntityParameter("A_ge", DbType.Int32) { Value = userInfo.Age });
        //    //            comm.Parameters.Add(new EntityParameter("U_serID", DbType.Int32) { Value = userid });
        //    //            comm.CommandText = sql1;
        //    //            int userinfoid = Convert.ToInt32(comm.ExecuteScalar());
        //    //            #endregion




        //    //            #region 更新User

        //    //            string sql3 = "Update [User] set UserInfo_ID = @UserInfo_ID where ID =@ID";
        //    //            comm.Parameters.Clear();
        //    //            comm.Parameters.Add(new EntityParameter("@ID", DbType.Int32) { Value = userid });
        //    //            comm.Parameters.Add(new EntityParameter("@UserInfo_ID", DbType.Int32) { Value = userinfoid });
        //    //            comm.CommandText = sql3;
        //    //            comm.ExecuteNonQuery();
        //    //            #endregion

        //    //            tran.Commit();
        //    //            return true;
        //    //        }
        //    //        catch (Exception e)
        //    //        {
        //    //            tran.Rollback();
        //    //            return false;
        //    //        }

        //    //    }
        //    //}
        //    #endregion


        //    #region sqlConnnection 写法
        //    using (SqlConnection conn = new SqlConnection(Common.Common.ConnStr))
        //    {
        //        conn.Open();
        //        using (var tran = conn.BeginTransaction())
        //        {
        //            try
        //            {
        //                SqlCommand comm = conn.CreateCommand();
        //                comm.Transaction = tran;
        //                #region 参数化
        //                string sql = "insert into [User] (Account,Pwd,IsDelete,CreateTime,IsFrozen,Balance,FrozenBlance,UserInfo_ID) output inserted.ID values (@Account,@Pwd,@IsDelete,@CreateTime,@IsFrozen,@Balance,@FrozenBlance,@UserInfo_ID)";
        //                comm.Parameters.Add(new SqlParameter("@Account", SqlDbType.NVarChar) { Value = user.Account });
        //                comm.Parameters.Add(new SqlParameter("@Pwd", SqlDbType.NVarChar) { Value = user.Pwd });
        //                comm.Parameters.Add(new SqlParameter("@IsDelete", SqlDbType.Bit) { Value = user.IsDelete });
        //                comm.Parameters.Add(new SqlParameter("@CreateTime", SqlDbType.DateTime) { Value = user.CreateTime });
        //                comm.Parameters.Add(new SqlParameter("@IsFrozen", SqlDbType.Bit) { Value = user.IsFrozen });
        //                comm.Parameters.Add(new SqlParameter("@Balance", SqlDbType.Decimal) { Value = user.Balance });
        //                comm.Parameters.Add(new SqlParameter("@FrozenBlance", SqlDbType.Decimal) { Value = user.FrozenBlance });
        //                comm.Parameters.Add(new SqlParameter("@UserInfo_ID", SqlDbType.Decimal) { Value = 0 });
        //                #endregion

        //                comm.CommandText = sql;
        //                //执行sql 不需要执行 db.avechanges 但如果是用 框架里的  db.set<>().add需要 savechanges
        //                int userid = Convert.ToInt32(comm.ExecuteScalar());

        //                //uinfo.NickName = "";
        //                //uinfo.HeadPic = "--------";
        //                //uinfo.Age = 28;
        //                #region 添加Userinfo
        //                string sql1 = "insert into [UserInfo] (HeadPic,NickName,Age,UserID) output inserted.ID values (@HeadPic,@NickName,@Age,@UserID)";
        //                comm.Parameters.Clear();
        //                comm.Parameters.Add(new SqlParameter("@HeadPic", DbType.String) { Value = userInfo.HeadPic });
        //                comm.Parameters.Add(new SqlParameter("@NickName", DbType.String) { Value = userInfo.NickName });
        //                comm.Parameters.Add(new SqlParameter("@Age", DbType.Int32) { Value = userInfo.Age });
        //                comm.Parameters.Add(new SqlParameter("@UserID", DbType.Int32) { Value = userid });
        //                comm.CommandText = sql1;
        //                int userinfoid = Convert.ToInt32(comm.ExecuteScalar());
        //                #endregion



        //                #region 添加购物车

        //                string sql2 = "insert into [ShoppingCart] (User_ID) values(@User_ID)";
        //                comm.Parameters.Clear();
        //                comm.Parameters.Add(new SqlParameter("@User_ID", DbType.Int32) { Value = userid });
        //                comm.CommandText = sql2;
        //                comm.ExecuteNonQuery();
        //                #endregion

        //                #region 更新User

        //                string sql3 = "Update [User] set UserInfo_ID=@UserInfo_ID where ID =@ID";
        //                comm.Parameters.Clear();
        //                comm.Parameters.Add(new SqlParameter("@ID", DbType.Int32) { Value = userid });
        //                comm.Parameters.Add(new SqlParameter("@UserInfo_ID", DbType.Int32) { Value = userinfoid });
        //                comm.CommandText = sql3;
        //                comm.ExecuteNonQuery();
        //                #endregion

        //                tran.Commit();
        //                return userid;
        //            }
        //            catch (Exception e)
        //            {
        //                tran.Rollback();
        //                return -1;
        //            }

        //        }
        //    }
        //    #endregion

        //}


    }


}
