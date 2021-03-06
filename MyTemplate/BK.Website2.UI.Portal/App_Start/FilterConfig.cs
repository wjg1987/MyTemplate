﻿using System.Web;
using System.Web.Mvc;
using My.Template.UI.Portal.CommClass;


namespace My.Template.UI.Portal
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //默认过滤器
            //filters.Add(new HandleErrorAttribute());

            //使用我们自定义的错误过滤器
            filters.Add(new MyErrorAttribute());
        }
    }
}