using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Web;
using My.Template.IDAL;

namespace My.Template.DALFactory
{
    public class AbstractFactory
    {
        public static IUserDal GetUserDal()
        {
            //获得 dal 程序集的名称
            string assemblyName = ConfigurationManager.AppSettings["DalAssemblyName"].ToString();
            //获得命名空间 
            string dalNameSpace = ConfigurationManager.AppSettings["DalNameSpace"].ToString();

            //类的全名称
            string dalClassFullName = dalNameSpace + ".UserDal";


            //创建dal实例    反射  根据
            //load 参数：程序集名称      CreateInstance参数:1.类的全名称（） 2. 忽略大小写
            var obj =  GetInstance(assemblyName,dalClassFullName);

            return obj as IUserDal;
        }


        public  static IUserInfoDal  GetuserInfoDal()
        {
            //获得 dal 程序集的名称
            string assemblyName = ConfigurationManager.AppSettings["DalAssemblyName"].ToString();
            //获得命名空间 
            string dalNameSpace = ConfigurationManager.AppSettings["DalNameSpace"].ToString();

            //类的全名称
            string dalClassFullName = dalNameSpace + ".UserInfoDal";

            //创建dal实例    反射  根据
            //load 参数：程序集名称      CreateInstance参数:1.类的全名称（） 2. 忽略大小写
            var obj = GetInstance(assemblyName, dalClassFullName);

            return obj as IUserInfoDal;
        }


   
        /// <summary>
        /// 在一次 请求当中 该方法只执行一次 因为 dbcontext 是线程内唯一的 
        /// </summary>
        /// <param name="assemblyName"></param>
        /// <param name="classFullName"></param>
        /// <returns></returns>
        public static object GetInstance(string assemblyName, string classFullName)
        {
            #region old 此处不能用 全局缓存 因为我们必须保证 dal在线程内唯一 全局的话就会有冲突  
            //object obj = HttpRuntime.Cache[assemblyName + classFullName];

            //if (obj == null)
            //{
            //    obj = Assembly.Load(assemblyName).CreateInstance(classFullName, true);
            //    HttpRuntime.Cache[assemblyName + classFullName] = obj;
            //}
            //return obj;
            #endregion
       



           var obj =  Assembly.Load(assemblyName).CreateInstance(classFullName, true);
                HttpRuntime.Cache[assemblyName + classFullName] = obj;
         
            return obj;


          
        }

    }
}
