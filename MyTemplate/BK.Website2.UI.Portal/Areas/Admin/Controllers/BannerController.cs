using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using My.Template.BLL;
using My.Template.IBLL;
using My.Template.Model;

namespace My.Template.UI.Portal.Areas.Admin.Controllers
{
    public class BannerController : BaseController
    {
       
        IBannerServices bannerServices = new BannerServices();
        IBannerTypeServices bannerTypeServices = new BannerTypeServices();
        //todo:从session中取 当前登陆的用户账号和图片信息  每次在 Action执行之前都会 进行权限校验（session是安全 存放在服务端 所以不需要每次都更新seesion中的当前用户登录信息  不过在 对用户的信息进行编辑操作后 如果操作的是 当前本人的信息 此时不对seesion中的信息进行更新的话 那么如果不注销登录进行重新登陆的话 显示的信息有可能会不正确）
        //todo:将 登陆用户的菜单数据保存到 session中 。只有在用户 重新登陆或者更新菜单权限的时候才更新 保存的数据 这样就不需要每次都去数据库查询了
        //todo:将登陆用户的 所有的权限ID 和当前登陆用户保存到同一处。在 进入Action的Filter中 校验用户请求的地址ID是否包含于用户所有权限ID中。只有当 用户重新登陆或者更新了权限操作才更新权限ID列表
        //todo:左侧导航栏 激活问题解决。 返回当前登陆的权限的顶级ID以及上级路径ID 一一激活 前台判断并添加样式。。

        public ActionResult Index(int pageSize = 10, int pageIndex = 1, string searchWords = "")
        {
            //返回前台页数和每页条数
            ViewBag.pageIndex = pageIndex;
            ViewBag.pageSize = pageSize;
            ViewBag.searchWords = searchWords;

            //过滤条件后的数据总条数 
            int totalCount;

            Func<Banner,bool> whereFunc = (b) =>
            {
                if (!string.IsNullOrEmpty(searchWords))
                {
                    return b.IsDelete == false && b.Title.Contains(searchWords);
                }
                else
                {
                    return b.IsDelete == false;
                }
            };


            Func<Banner, int> orderFunc = (b) => { return b.Sequence; };

            var tmp = bannerServices.LoadPageEntitys(pageIndex, pageSize, out totalCount, whereFunc,orderFunc,true).ToList();

            //分页字符串
            ViewBag.pageStr = Common.Tools.ShowPageNavigate(pageSize, pageIndex, totalCount, "&searchWords=" + searchWords);

            return View(tmp);
        }

   
        public ActionResult Add()
        {
            //取消返回页面
            ViewBag.returnUrl = Request.UrlReferrer == null ? Url.Action("Index", "Banner") : Request.UrlReferrer.ToString();

            #region 获取BannerType类型列表
            var bannertypeList = bannerTypeServices.LoadEntitys(b=>true).ToList();
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (var item in bannertypeList)
            {
                list.Add(new SelectListItem { Text = item.TName, Value = item.ID.ToString() });
            }
            ViewBag.BannerTypeID = list;
            #endregion
             
            Banner model = new Banner();
            model.Sequence = 1000;//默认排序数字1000
            model.LinkUrl = "javascript:;";
            return View(model);
        }

        //
        // POST: /Admin/Banner/Create

        [HttpPost]
        public ActionResult Add(Banner model,string returnUrl)
        {
             //取消添加 返回页面
            ViewBag.returnUrl = returnUrl;

            #region 获取BannerType类型列表
            var bannertypeList = bannerTypeServices.LoadEntitys(b => true).ToList();
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (var item in bannertypeList)
            {
                list.Add(new SelectListItem { Text = item.TName, Value = item.ID.ToString() });
            }
            ViewBag.BannerTypeID = list;
            #endregion

            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(model.ImgPic) && model.ImgPic.IndexOf("/UserUpload/") == 0)
                {
                    model.ImgPic = Common.Common.WebSiteUrl + model.ImgPic;
                }
                model.AddTime = DateTime.Now;
                model.IsDelete = false;

                model = bannerServices.Add(model);
                if (model.ID > 0)
                {
                    return Redirect(returnUrl);
                }
            }

            return View(model);
        }

        //
        // GET: /Admin/Banner/Edit/5

        public ActionResult Edit(int id)
        {
            //取消返回页面
            ViewBag.returnUrl = Request.UrlReferrer == null ?Url.Action("Index","Banner") : Request.UrlReferrer.ToString();

            #region 获取BannerType类型列表
            var bannertypeList = bannerTypeServices.LoadEntitys(b => true).ToList();
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (var item in bannertypeList)
            {
                list.Add(new SelectListItem { Text = item.TName, Value = item.ID.ToString() });
            }
            ViewBag.BannerTypeID = list;
            #endregion

            var model =  bannerServices.LoadEntitys(b => b.ID == id).FirstOrDefault();
            return View(model);
        }

        //
        // POST: /Admin/Banner/Edit/5

        [HttpPost]
        public ActionResult Edit(Banner model,string returnUrl)
        {
            //取消添加 返回页面
            ViewBag.returnUrl = returnUrl;

            #region 获取BannerType类型列表
            var bannertypeList = bannerTypeServices.LoadEntitys(b => true).ToList();
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (var item in bannertypeList)
            {
                list.Add(new SelectListItem { Text = item.TName, Value = item.ID.ToString() });
            }
            ViewBag.BannerTypeID = list;
            #endregion


            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(model.ImgPic) && model.ImgPic.IndexOf("/UserUpload/") == 0)
                {
                    model.ImgPic = Common.Common.WebSiteUrl + model.ImgPic;
                }

                if (bannerServices.Update(model))
                {
                    return Redirect(returnUrl);
                }
            }
            return View(model);
        }

        //
        // GET: /Admin/Banner/Delete/5

        public int Delete(int id)
        {
          
            var model = bannerServices.LoadEntitys(b => b.ID == id).FirstOrDefault();
            if (model == null)
            {
                return 0;
            }
            model.IsDelete = true;
            if (bannerServices.Update(model))
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }



        
    }
}