using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace My.Template.AdoNetDAL
{
    public class BaseAdoNetDal<T> where T: class ,new ()
    {
        public IQueryable<T> LoadEntitys(Func<T, bool> whereLambdaFun)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> LoadPageEntitys<TOrerByKey>(int pageIndex, int pageSize, out int total, Func<T, bool> whereLambdaFunc, Func<T, TOrerByKey> orderByFunc, bool isAsc)
        {
            throw new NotImplementedException();
        }

        public T Add(T entity)
        {
            throw new NotImplementedException();
        }

        public int Add(List<T> entityList)
        {
            throw new NotImplementedException();
        }

        public bool Update(T entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public bool Delete(T entity)
        {
            throw new NotImplementedException();
        }
        #region 执行executeNoneQuery的方法
        public int ExecuteNoneQuery(string sql)
        {
            throw new NotImplementedException();
        }
        #endregion 
        public object ExecuteScalar(string sql)
        {
            throw new NotImplementedException();
        }
    }
}
