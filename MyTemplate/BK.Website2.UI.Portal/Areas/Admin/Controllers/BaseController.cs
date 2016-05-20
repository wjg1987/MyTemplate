using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using My.Template.BLL;
using My.Template.IBLL;
using My.Template.IDAL;
using My.Template.Model;
using My.Template.Model.AdminDataModel;
using Newtonsoft.Json;
using ServiceStack;
using Spring.Context;
using Spring.Context.Support;
using Spring.Objects.Factory;

namespace My.Template.UI.Portal.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        //重写调用方法之前的处理
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            //页面序号 用于后台首页左侧导航展开还是显示的判断
            ViewBag.pageNum = Request["pageNum"] == null ? 0 : Convert.ToInt32(Request["pageNum"].ToString());


            #region session保存用户状态 检验的方式

            //如果有用户登录信息
            if (Request.Cookies["sessionId"] != null)
            {
                string sessionId = Request.Cookies["sessionId"].Value;
                //根据key从memcache中获取用户信息
                object obj = Common.MemcacheHelper.Get(sessionId);
                //因为memcache中过期时间是懒惰删除 所以一定要检查是否过期
                if (obj != null)
                {
                    //todo:此处只是从session中取了当期登陆的用户 如果在后台进行了操作 那么需要更新当前登陆用户信息
                    LoginAdminInfo CurLoginAdminInfo = Common.SerializerHelper.DeserializeToObject<LoginAdminInfo>(obj.ToString());
                    //模拟session滑动过期时间
                    Common.MemcacheHelper.Set("sessionId", obj.ToString(), DateTime.Now.AddMinutes(20));

                    //前台需要使用的当前登陆用户信息
                    ViewBag.loginadminuser = CurLoginAdminInfo;

                    //验证用户权限
                    var ispass = CheckAction(CurLoginAdminInfo,this.HttpContext, Request, Response);
                    if (!ispass)//没有此权限 提示。。
                    {
                        filterContext.Result = new RedirectResult("/admin/error/noaction");
                    }

                }
                else
                {
                    filterContext.Result = new RedirectResult("/admin/login");
                }
            }
            else
            {
                filterContext.Result = new RedirectResult("/admin/login");
            }
            #endregion
        }


        public bool CheckAction(LoginAdminInfo CurLoginAdminInfo, HttpContextBase httpContext, HttpRequestBase Request, HttpResponseBase Response)
        {
            #region 验证用户权限

            //验证当前用户是否具有所请求的页面的权限
            //1.获得请求的页面
            //string requestPath = Request.Url.AbsolutePath.ToLower();

            var controller = RouteData.Route.GetRouteData(httpContext).Values["controller"];
            var action = RouteData.Route.GetRouteData(httpContext).Values["action"];

            string requestPath = ("/admin/" + controller + "/" + action).ToLower();
            string actionHttpMethod = Request.HttpMethod;//请求方式


            IApplicationContext ctx = ContextRegistry.GetContext();
            UserServices userServices = (UserServices)ctx.GetObject("UserServices");
            ActionServices actionServices = (ActionServices)ctx.GetObject("ActionServices");
            User_ActionServices userActionServices = (User_ActionServices)ctx.GetObject("User_ActionServices");

            //判断数据库中是否有当前权限
            var actionInfo = actionServices.LoadEntitys(u => u.AUrl == requestPath).FirstOrDefault();
            if (actionInfo == null)
            {
                return false;
            }

            //数据库中找到了 该url地址的权限
            //2.判断 User_Action关联中是否设置了用户该权限
            var uc = userActionServices.LoadEntitys(u => u.ActionID == actionInfo.ID && u.UserID == CurLoginAdminInfo.Userid).FirstOrDefault();
            if (uc != null)
            {
                return true;//有特殊权限 直接返回 不检测第3步了
            }


            //3.检查用户角色里是否有该权限
            var actioncount = actionServices.GetLoginAdminAllActionCount(CurLoginAdminInfo.Userid, actionInfo.ID);

            if (actioncount < 1)
            {
                return false;
            }

            return true;

            #endregion
        }
    }
}
