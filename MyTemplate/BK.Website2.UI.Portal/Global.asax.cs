using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using My.Template.Model.LuceneSearch;
using My.Template.UI.Portal.CommClass;
using Spring.Web.Mvc;

namespace My.Template.UI.Portal
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : SpringMvcApplication //System.Web.HttpApplication 继承spring 开启属性注入
    {
        protected void Application_Start()
        {
            //开启线程扫描是否更新lucene磁盘文件
            SearchIndexManager.GetInstance().StartThread();
            //启动Log4Net 配置
            log4net.Config.XmlConfigurator.Configure();
            
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);

            //过滤器 里面可以自定义我们自己的异常处理过滤器
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);

            RouteConfig.RegisterRoutes(RouteTable.Routes);


            #region 开启线程处理错误日志

            ThreadPool.QueueUserWorkItem((a) =>
                {
                    while (true)
                    {
                        if (Common.Common.redisClient.GetListCount("errormsg") > 0)
                        {
                            //出队1个错误
                            //Exception ex = MyErrorAttribute.ErrorQueue.Dequeue();
                            Common.LogHelper.logWriter.Error(Common.Common.redisClient.DequeueItemFromList("errormsg"));
                        }
                        else
                        {
                            Thread.Sleep(3000);//错误队列中没有数据，暂停3秒，避免浪费CPU资源
                        }
                    }
                });

            #endregion
        }

        /// <summary>
        /// 异常处理
        /// </summary>
        protected void Application_Error()
        {
        }
    }
}