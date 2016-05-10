using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using My.Template.DALFactory;
using My.Template.EFDAL;
using My.Template.IDAL;
using My.Template.Model;

namespace My.Template.BLL
{
    public abstract class BaseServices<T> where T :class , new ()
    {
        public IDbSession dbSession = DbSessionFactory.GetCurDbSession();
        protected IBaseDal<T> CurrentDal; 

        #region 查询

        /// <summary>
        /// 根据过滤条件 获取实体
        /// </summary>
        /// <param name="whereLambdaFun"></param>
        /// <returns></returns>
        public IQueryable<T> LoadEntitys(Func<T, bool> whereLambdaFun)
        {
            return CurrentDal.LoadEntitys(whereLambdaFun);
        }


        /// <summary>
        /// 根据过滤条件 获取实体 无跟踪状态
        /// </summary>
        /// <param name="whereLambdaFun"></param>
        /// <returns></returns>
        public IQueryable<T> LoadEntitysAsNotracking(Func<T, bool> whereLambdaFun)
        {
            return CurrentDal.LoadEntitysAsNotracking(whereLambdaFun);
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
            return CurrentDal.LoadPageEntitys(pageIndex, pageSize, out total, whereLambdaFunc, orderByFunc, isAsc);
        }
        #endregion

        public IQueryable<T> LoadPageEntitys<TOrerByKey, TOrerByKey2>(int pageIndex, int pageSize, out int total,
            Func<T, bool> whereLambdaFunc, Func<T, TOrerByKey> orderByFunc, bool isAsc, Func<T, TOrerByKey2> orderByFunc2,
            bool isAsc2)
        {
            return CurrentDal.LoadPageEntitys(pageIndex, pageSize, out total, whereLambdaFunc, orderByFunc, isAsc,orderByFunc2,isAsc2);
        }

        public int QueryTotalCount(Func<T, bool> whereLambdaFunc)
        {
            return CurrentDal.QueryTotalCount(whereLambdaFunc);
        }

        #region CUD

     

        //第一种方法实现 tdal的赋值
        //public BaseServices(IBaseDal<T>  tdal)
        //{
        //    this.tdal = tdal;
        //}

        
        protected BaseServices()
        {
            SetCurrentDal();
        }

        //第2种方法  子类必须实现一个 SetCurDal的抽象方法  设置当前dal
        public abstract void SetCurrentDal();


        public T Add(T entity)
        {
            CurrentDal.Add(entity);
            dbSession.SaveChanges();
            return entity;
        }



        public int Add(List<T> entityList)
        {
            if (entityList.Count <= 100)
            {
                foreach (var T in entityList)
                {
                    CurrentDal.Add(T);
                }
                return dbSession.SaveChanges();
            }
            else
            {
                int total = 0;
                for (int i = 0; i < entityList.Count; i++)
                {
                    CurrentDal.Add(entityList[i]);
                    if ((i+1)%100 == 0)
                    {
                       total+= dbSession.SaveChanges();
                    }
                }
                return total;
            }
        }

        public int Delete(Func<T,bool> whereLambdaFun )
        {
            List<T> tmp = CurrentDal.LoadEntitys(whereLambdaFun).AsQueryable().ToList();
            foreach (var item in tmp)
            {
                CurrentDal.Delete(item);
            }
            return dbSession.SaveChanges();
        }

        public bool Delete(T entity)
        {
            CurrentDal.Delete(entity);
            return dbSession.SaveChanges() > 0;
        }

        public bool Update(T entity)
        {
            CurrentDal.Update(entity);
            return dbSession.SaveChanges() > 0;
        }

        public bool Update(List<T> entity)
        {
            CurrentDal.Update(entity);
            return dbSession.SaveChanges() > 0;
        }


        #endregion

        #region 执行executeNoneQuery的方法
        public int ExecuteNoneQuery(string sql) {
            return CurrentDal.ExecuteNoneQuery(sql);
        }
        public object ExecuteScalar(string sql)
        {
            return CurrentDal.ExecuteScalar(sql);
        }
        #endregion  
        
    }
}
