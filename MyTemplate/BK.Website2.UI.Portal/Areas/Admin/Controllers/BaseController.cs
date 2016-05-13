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

namespace My.Template.UI.Portal.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {

        public Model.AdminDataModel.LoginAdminInfo CurLoginAdminInfo = null;

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
                    CurLoginAdminInfo = Common.SerializerHelper.DeserializeToObject<LoginAdminInfo>(obj.ToString());

                    //前台需要使用的当前登陆用户信息
                    ViewBag.loginadminuser = CurLoginAdminInfo;


                    #region 验证用户权限
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
                    #endregion
                }
                else
                {
                    filterContext.Result = new RedirectResult("/admin/login");
                }
            }
            else
            {
                filterContext.Result = new RedirectResult("/admin/login");
                //跳转的另一个写法
                //filterContext.HttpContext.Response.Redirect("/admin/login");
                //return;//记得要return
            }
            #endregion
        }
    }
}
