using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace My.Template.IDAL
{
    public  interface IBaseDal<T> where T :class ,new ()
    {
 
        IQueryable<T> LoadEntitys(Func<T, bool> whereLambdaFun);

        IQueryable<T> LoadEntitysAsNotracking(Func<T, bool> whereLambdaFun);

        IQueryable<T> LoadPageEntitys<TOrerByKey>(int pageIndex, int pageSize, out int total,
            Func<T, bool> whereLambdaFunc, Func<T, TOrerByKey> orderByFunc, bool isAsc);

        IQueryable<T> LoadPageEntitys<TOrerByKey, TOrerByKey2>(int pageIndex, int pageSize, out int total,
            Func<T, bool> whereLambdaFunc, Func<T, TOrerByKey> orderByFunc, bool isAsc, Func<T, TOrerByKey2> orderByFunc2, bool isAsc2);

        int QueryTotalCount(Func<T, bool> whereLambdaFunc);


        T Add(T entity);
        int Add(List<T> entityList);

        bool Update(T entity);
        bool Update(List<T> entityList);

        bool Delete(int id);
        bool Delete(string ids);
        bool Delete(T entity);
        bool Delete(List<T> entityList);
        int ExecuteNoneQuery(string sql);
        object ExecuteScalar(string sql);
        
    }
}
