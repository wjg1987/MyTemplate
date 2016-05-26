using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServiceStack.Redis;

namespace My.Template.UI.Portal.CommClass
{
    public class MyErrorAttribute:HandleErrorAttribute
    {
        public static string Redisservicesipaddress = ConfigurationManager.AppSettings["redisservicesipaddress"];

        public static IRedisClientsManager ClientsManager =
           new PooledRedisClientManager(Redisservicesipaddress.Split(';'));

        public static IRedisClient redisClient = ClientsManager.GetClient();

        public override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);

            #region 方法4 自定义1个 异常信息收集队列 有异常信息后放入队列(Queue)中就好了 代码继续往下执行 不会阻塞当前请求 有专门的线程去处理异常队列信息 （在application_start方法中开启 错误队列 处理线程。因为错误处理线程是在整个程序运行中都在执行的）
            //ErrorQueue.Enqueue(filterContext.Exception);

            redisClient.EnqueueItemOnList("errormsg",filterContext.Exception.ToString());

            #endregion


            //跳转错误页
            filterContext.HttpContext.Response.Redirect("/Error.html");
        }


        
    }
}