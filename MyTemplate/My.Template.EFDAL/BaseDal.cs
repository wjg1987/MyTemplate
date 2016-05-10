using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using My.Template.Model;

namespace My.Template.EFDAL
{
    public class BaseDal<T> where T:class, new()
    {
        //DbContextFactory.GetCurDbContext() 方法 实现线程内 dbcontext 唯一。
        protected DbContext db = DbContextFactory.GetCurDbContext();

        #region 多种方法查询  Query

        /// <summary>
        /// 根据过滤条件 获取实体
        /// </summary>
        /// <param name="whereLambdaFun"></param>
        /// <returns></returns>
        public IQueryable<T> LoadEntitys(Func<T, bool> whereLambdaFun)
        {
            return db.Set<T>().Where(whereLambdaFun).AsQueryable().AsNoTracking();
        }


        /// <summary>
        /// 根据过滤条件 获取实体
        /// </summary>
        /// <param name="whereLambdaFun"></param>
        /// <returns></returns>
        public IQueryable<T> LoadEntitysAsNotracking(Func<T, bool> whereLambdaFun)
        {
            return db.Set<T>().Where(whereLambdaFun).AsQueryable().AsNoTracking();
        }

        /// <summary>
        /// 分页查询实体
        /// </summary>
        /// <typeparam name="TOrerByKey">泛型类型 排序的字段类型</typeparam>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页条数</param>
        /// <param name="total">过滤后的数据总数</param>
        /// <param name="whereLambdaFunc">过滤条件 表达式</param>
        /// <param name="orderByFunc">排序表达式</param>
        /// <param name="isAsc">是否升序排序</param>
        /// <returns></returns>
        public IQueryable<T> LoadPageEntitys<TOrerByKey>(int pageIndex, int pageSize, out int total,
            Func<T, bool> whereLambdaFunc, Func<T, TOrerByKey> orderByFunc, bool isAsc)
        {
            total = db.Set<T>().Where(whereLambdaFunc).Count();

            if (isAsc)
            {
                return db.Set<T>().Where(whereLambdaFunc)
                  .OrderBy<T, TOrerByKey>(orderByFunc)
                  .Skip((pageIndex - 1) * pageSize)
                  .Take(pageSize)
                  .AsQueryable().AsNoTracking();
            }
            else
            {
                return db.Set<T>().Where(whereLambdaFunc)
                  .OrderByDescending<T, TOrerByKey>(orderByFunc)
                  .Skip((pageIndex - 1) * pageSize)
                  .Take(pageSize)
                  .AsQueryable().AsNoTracking();
            }

        }


        /// <summary>
        /// 分页查询实体
        /// </summary>
        /// <typeparam name="TOrerByKey">泛型类型 排序的字段类型</typeparam>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页条数</param>
        /// <param name="total">过滤后的数据总数</param>
        /// <param name="whereLambdaFunc">过滤条件 表达式</param>
        /// <param name="orderByFunc">排序表达式</param>
        /// <param name="isAsc">是否升序排序</param>
        /// <param name="orderByFunc2">排序表达式2</param>
        /// <param name="isAsc2">是否升序排序2</param>
        /// <returns></returns>
        public IQueryable<T> LoadPageEntitys<TOrerByKey, TOrerByKey2>(int pageIndex, int pageSize, out int total,
            Func<T, bool> whereLambdaFunc, Func<T, TOrerByKey> orderByFunc, bool isAsc, Func<T, TOrerByKey2> orderByFunc2, bool isAsc2)
        {
            total = db.Set<T>().Where(whereLambdaFunc).Count();

            if (isAsc && isAsc2)//都是升序
            {
                return db.Set<T>().Where(whereLambdaFunc)
                  .OrderBy<T, TOrerByKey>(orderByFunc)
                  .ThenBy<T, TOrerByKey2>(orderByFunc2)
                  .Skip((pageIndex - 1) * pageSize)
                  .Take(pageSize)
                  .AsQueryable().AsNoTracking();
            }
            else if (!isAsc && !isAsc2)//都是降序
            {
                return db.Set<T>().Where(whereLambdaFunc)
                  .OrderByDescending<T, TOrerByKey>(orderByFunc)
                  .ThenByDescending<T, TOrerByKey2>(orderByFunc2)
                  .Skip((pageIndex - 1) * pageSize)
                  .Take(pageSize)
                  .AsQueryable().AsNoTracking();
            }else if (isAsc && !isAsc2)//第一个升序 第2个降序
            {
                return db.Set<T>().Where(whereLambdaFunc)
                .OrderBy<T, TOrerByKey>(orderByFunc)
                .ThenByDescending<T, TOrerByKey2>(orderByFunc2)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .AsQueryable().AsNoTracking();
            }
            else//第1个降序 第2个升序
            {
                return db.Set<T>().Where(whereLambdaFunc)
                .OrderByDescending<T, TOrerByKey>(orderByFunc)
                .ThenBy<T, TOrerByKey2>(orderByFunc2)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .AsQueryable().AsNoTracking();
            }
        }



        public int QueryTotalCount(Func<T, bool> whereLambdaFunc)
        {
          return  db.Set<T>().Where(whereLambdaFunc).Count();
        }

        #endregion

        #region CUD

        /// <summary>
        /// 添加实体  EF中自动增长的主键  添加到数据库中后  自动将id赋值给新添加的对象
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public T Add(T entity)
        {
            db.Entry<T>(entity).State = EntityState.Added;
            //db.SaveChanges();//dbsession中 调用了 savechanges 方法。 将数据库的保存提高到了 BLL层
            return entity;
        }

        public int Add(List<T> entityList)
        {
            foreach (var entity in entityList)
            {
                db.Entry<T>(entity).State = EntityState.Added;
            }
            //return db.SaveChanges();//dbsession中 调用了 savechanges 方法。 将数据库的保存提高到了 BLL层
            return 0;
        }


        public bool Update(T entity)
        {
         
           db.Entry<T>(entity).State = EntityState.Modified;
          
            //return db.SaveChanges() > 0;//dbsession中 调用了 savechanges 方法。 将数据库的保存提高到了 BLL层
            return true;
        }


        public bool Update(List<T> entityList)
        {
            foreach (var entity in entityList)
            {
                db.Entry<T>(entity).State = EntityState.Modified;
            }
            //return db.SaveChanges() > 0;//dbsession中 调用了 savechanges 方法。 将数据库的保存提高到了 BLL层
            return true;
        }

        public bool Delete(int id)
        {
            var entity = db.Set<T>().Find(id);
            db.Entry<T>(entity).State = EntityState.Deleted;
            //return db.SaveChanges() > 0;//dbsession中 调用了 savechanges 方法。 将数据库的保存提高到了 BLL层
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ids">","逗号分割的ids字符串,例如:"1,2,3,4,".末尾有无逗号已经处理</param>
        /// <returns></returns>
        public bool Delete(string ids)
        {
            int[] intArr = Common.Tools.GetIntIds(ids);
            for (int i = 0; i < intArr.Length; i++)
            {
                var entity = db.Set<T>().Find(intArr[i]);
                db.Entry<T>(entity).State = EntityState.Deleted;
            }
            //return db.SaveChanges() > 0;//dbsession中 调用了 savechanges 方法。 将数据库的保存提高到了 BLL层
            return true;
        }

        public bool Delete(T entity)
        {
            db.Entry<T>(entity).State = EntityState.Deleted;
            //return db.SaveChanges() > 0; //dbsession中 调用了 savechanges 方法。 将数据库的保存提高到了 BLL层
            return true;
        }

        public bool Delete(List<T> entityList)
        {
            foreach (var entity in entityList)
            {
                db.Entry<T>(entity).State = EntityState.Deleted;
            }
            //return db.SaveChanges() > 0;//dbsession中 调用了 savechanges 方法。 将数据库的保存提高到了 BLL层
            return true;
        }

        #endregion



        #region 执行executeNoneQuery的方法
        public int ExecuteNoneQuery(string sql)
        {
            return db.Database.ExecuteSqlCommand(sql);
        }
      
        #endregion  

        
       
        #region 执行ExecuteScalar()的方法
        public object ExecuteScalar(string sql)
        {
            return db.Database.ExecuteSqlCommand(sql);
        }

        #endregion  
        
    }
}
