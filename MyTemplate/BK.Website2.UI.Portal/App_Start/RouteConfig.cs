using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace My.Template.UI.Portal
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");



           // //新闻访问
           // routes.MapRoute(
           //    name: "newsdetailpage",
           //    url: "newspage/{id}",
           //    defaults: new { controller = "News", action = "Detail", id = UrlParameter.Optional },
           //    namespaces: new string[] { "My.Template.UI.Portal.Controllers" }
           //);

           // //没有外链的产品静态页
           // routes.MapRoute(
           //    name: "market",
           //    url: "{t}.html",
           //    defaults: new { controller = "Market", action = "index", t = UrlParameter.Optional },
           //    namespaces: new string[] { "My.Template.UI.Portal.Controllers" }
           //);

           // //没有外链的产品静态页
           // routes.MapRoute(
           //    name: "market1",
           //    url: "md1/{id}.html",
           //    defaults: new { controller = "Market", action = "Detail", id = UrlParameter.Optional },
           //    namespaces: new string[] { "My.Template.UI.Portal.Controllers" }
           //);

           // //带有外链的产品静态页
           // routes.MapRoute(
           //     name: "market2",
           //     url: "md2/{id}/{outlinkdeid}.html",
           //     defaults: new { controller = "Market", action = "Detail", id = UrlParameter.Optional, outlinkdeid = UrlParameter.Optional },
           //     namespaces: new string[] { "My.Template.UI.Portal.Controllers" }
           // );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Default", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "My.Template.UI.Portal.Controllers" }
            );
           
        }
    }
}