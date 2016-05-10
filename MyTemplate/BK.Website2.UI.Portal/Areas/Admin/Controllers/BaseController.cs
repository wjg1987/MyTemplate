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
using Newtonsoft.Json;

namespace My.Template.UI.Portal.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
     

        //获得登录用户所有权限
        protected List<Model.Action> GetLoginUserActions(User loginUser)
        {
            UserServices userServices = new UserServices();

            var userActions = new List<Model.Action>();

            userServices.dbSession.ActionDal.LoadEntitys(
                u => u.ID == loginUser.ID
                );

            return userActions;
        }


        //重写调用方法之前的处理
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            //页面序号 用于后台首页左侧导航展开还是显示的判断
            ViewBag.pageNum = Request["pageNum"] == null ? 0 : Convert.ToInt32(Request["pageNum"].ToString());

            #region session保存用户状态 检验的方式


            //Session["curLoginAdminMenu"] = dbUser.ID;


            //如果有用户登录信息
            if (Session["curLoginAdminID"] != null)
            {
                //获得当前登录的用户
                UserServices userser = new UserServices();
                int uid = Convert.ToInt32(Session["curLoginAdminID"]);
                User loginUser = userser.LoadEntitys(u => u.ID == uid).FirstOrDefault();

                if (loginUser == null)
                {
                    filterContext.Result = new RedirectResult("/admin/login");
                }

                ////验证当前用户是否具有所请求的页面的权限
                ////1.获得请求的页面
                //string requestPath = Request.FilePath.ToLower();
                ////2.获得当前登录用户所有的权限
                //List<Model.Action> userActions = GetLoginUserActions(loginUser);
                ////3。检查当前请求页面 是否在用户权限里
                //bool isHaveAction = false;
                //foreach (var item in userActions)
                //{
                //    string dbUrl = item.AUrl.ToLower();

                //    //如果当前权限地址action是 index，有可能带参数 那么 /index以及后面的参数进行比较地址
                //    if (dbUrl.Contains("/index") && dbUrl.Substring(0, dbUrl.IndexOf("/index")) == requestPath)
                //    {
                //        isHaveAction = true;
                //    }

                //    if (dbUrl == requestPath)//如果当前请求页面和 用户某一个权限一样
                //    {
                //        isHaveAction = true;
                //    }


                //    if (dbUrl.IndexOf('?') > 0 && dbUrl.Substring(0, dbUrl.IndexOf('?')) == requestPath)//如果当前请求页面带参数 并且去掉参数后 地址一样
                //    {
                //        isHaveAction = true;
                //    }
                //}

                //if (!isHaveAction)//没有权限
                //{
                //    if (requestPath == "/houtai/default")
                //    {
                //        Session["curLoginAdminID"] = null;//清空登录信息,避免循环登录的问题
                //    }
                //}
            }
            else
            {
                filterContext.Result = new RedirectResult("/admin/login");
            }
            #endregion
        }
    }
}
