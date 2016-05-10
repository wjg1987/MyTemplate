using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using My.Template.BLL;
using My.Template.Model;

namespace My.Template.UI.Portal.Controllers
{
    public class VipValidateController : Controller
    {


        //重写调用方法之前的处理
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            //页面序号 用于后台首页左侧导航展开还是显示的判断
            ViewBag.pageNum = Request["pageNum"] == null ? 0 : Convert.ToInt32(Request["pageNum"].ToString());

            #region session保存用户状态 检验的方式


            //Session["curLoginAdminMenu"] = dbUser.ID;


            //如果有用户登录信息
            if (Session["CurLoginUserID"] != null)
            {
                //获得当前登录的用户
                UserServices userser = new UserServices();
                int uid = Convert.ToInt32(Session["CurLoginUserID"]);
                User loginUser = userser.LoadEntitys(u => u.ID == uid).FirstOrDefault();

                if (loginUser == null)
                {
                   Response.Redirect("/Admin/Login");
                }
            }
            else
            {
                Response.Redirect("/Admin/Login");
            }
            #endregion
        }

    }
}
