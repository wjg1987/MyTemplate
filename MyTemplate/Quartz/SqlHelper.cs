using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace DAL
{
    public static class SqlHelper
    {

         private static string connStr = ConfigurationManager.AppSettings["ConnStr"];

        #region 01.执行  增删改 的操作 (sql语句)
         public static int ExecuteNonQuery(string sql, params SqlParameter[] parameters)
         {
             using (SqlConnection conn  = new SqlConnection(connStr))
             {
                 conn.Open();
                 using (SqlCommand cmd = conn.CreateCommand())
                 {
                     cmd.CommandText = sql;
                     if (parameters != null)
                     {
                         cmd.Parameters.AddRange(parameters);
                     }
                     return cmd.ExecuteNonQuery();
                 }
             }
         }
        #endregion

        #region 02.返回单条查询结果 的操作 (sql语句)
         public static object ExecuteScalar(string sql, params SqlParameter[] parameters)
         {
             using (SqlConnection conn = new SqlConnection(connStr))
             {
                 conn.Open();
                 using (SqlCommand cmd = conn.CreateCommand())
                 {
                     cmd.CommandText = sql;
                     if (parameters != null)
                     {
                         cmd.Parameters.AddRange(parameters);
                     }
                     return cmd.ExecuteScalar();
                 }
             }
         }
         #endregion

        #region 03.返回 DataReader 的操作 (sql语句)
         public static SqlDataReader ExecuteDataReader(string sql, params SqlParameter[] parameters)
         {
             SqlConnection conn = new SqlConnection(connStr);
             
                 
                 using (SqlCommand cmd = conn.CreateCommand())
                 {
                     cmd.CommandText = sql;
                     if (parameters != null)
                     {
                         cmd.Parameters.AddRange(parameters);
                     }

                     try
                     {
                         conn.Open();
                         //返回SqlDataReader 指定外部释放 SqlDataReader 的同时关闭连接
                         return cmd.ExecuteReader(CommandBehavior.CloseConnection);
                     
                     }
                     catch (Exception)
                     {
                         conn.Close();
                         conn.Dispose();
                         throw;
                     }
                    
                 }
            
         }
         #endregion
        

        #region 04.返回 页面数据的 DataReader  的操作 (存储过程 )
         public static SqlDataReader ExecutePageDataReader(string storedProcedure, params SqlParameter[] parameters)
         {
             SqlConnection conn = new SqlConnection(connStr);


             using (SqlCommand cmd = conn.CreateCommand())
             {
                 cmd.CommandType = CommandType.StoredProcedure;
                 cmd.CommandText = storedProcedure;
                 if (parameters != null)
                 {
                     cmd.Parameters.AddRange(parameters);
                 }

                 try
                 {
                     conn.Open();
                     //返回SqlDataReader 指定外部释放 SqlDataReader 的同时关闭连接
                     return cmd.ExecuteReader(CommandBehavior.CloseConnection);

                 }
                 catch (Exception)
                 {
                     conn.Close();
                     conn.Dispose();
                     throw;
                 }

             }

         }
         #endregion



        #region 05.返回DataTable 的操作 (sql语句)
         public static DataTable ExecuteDataTable(string sql, params SqlParameter[] parameters)
         {
             DataTable table = new DataTable();
             using (SqlDataAdapter adapter = new SqlDataAdapter(sql,connStr))
             {
                 if(parameters != null)
                 {
                    adapter.SelectCommand.Parameters.AddRange(parameters);
                 }
                 adapter.Fill(table);
             }

             return table;
         }
         #endregion


        #region 06. nullToDBNull的方法
         public static object nullToDBNull(object obj)
         {
             if (obj == null)
             {
                 return DBNull.Value;
             }
             else
             {
                 return obj;
             }
         }
         #endregion

        #region 07.DbNullToNull方法
         public static object DbNullToNull(object obj)
         {
             if (obj == DBNull.Value)
             {
                 return null;
             }
             else
             {
                 return obj;
             }
         }
         #endregion
    }
}
