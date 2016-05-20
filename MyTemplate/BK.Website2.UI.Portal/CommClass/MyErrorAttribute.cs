using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServiceStack.Redis;

namespace My.Template.UI.Portal.CommClass
{
    public class MyErrorAttribute:HandleErrorAttribute
    {
        //public static Queue<Exception>  ErrorQueue = new Queue<Exception>(); 

        public override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);

            //记录错误日志 多个优化解决方案
            #region 方法1   //此方法占用了线程，并且写文件IO操作比较慢 不建议使用
            //同一时间如果很多个错误 写同一个文件 也是有问题的 解决这个需要 加锁一个全局的变量
            //File.WriteAllText("/errlog.txt",filterContext.Exception.ToString());
           
            #endregion


            #region 方法2  解决了 多个用户同时写文件的问题 但带来了 性能上的丢失。 加锁 需要耗费一定的时间
            // //这里可以锁当前类。 因为当前类是 在  gloable中的 appliction_start中执行的。
            //在  静态集合中添加的对象 只创建了1个对象 是全局唯一的
            ////也可以锁定一个 字符串常量。。。。。  lock(this) 需要分析 this是否是 全局唯一
            ////lock (this)
            ////{
            ////    File.WriteAllText("/errlog.txt", filterContext.Exception.ToString());
            ////}
        
            #endregion

            #region 方法3 线程池 解决 多个用户 同一时间 写日志的问题
            
            #endregion

            #region 方法4 自定义1个 异常信息收集队列 有异常信息后放入队列(Queue)中就好了 代码继续往下执行 不会阻塞当前请求 有专门的线程去处理异常队列信息 （在application_start方法中开启 错误队列 处理线程。因为错误处理线程是在整个程序运行中都在执行的）
            //ErrorQueue.Enqueue(filterContext.Exception);

            Common.Common.redisClient.EnqueueItemOnList("errormsg",filterContext.Exception.ToString());

            #endregion


            //跳转错误页
            filterContext.HttpContext.Response.Redirect("/Error.html");
        }


        
    }
}