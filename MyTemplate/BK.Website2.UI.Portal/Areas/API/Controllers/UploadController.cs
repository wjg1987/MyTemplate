using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using My.Template.BLL;
using My.Template.Model;
using My.Template.IBLL;

namespace My.Template.UI.Portal.Areas.mobile.Controllers
{
    public class UploadController : Controller
    {

        #region 上传方法 可以借鉴
        //// GET: /API/Upload/
        ////保存用户上传的照片
        //[AcceptVerbs(HttpVerbs.Post)]
        //public JsonResult Index(int WID, int isH)
        //{
        //    #region 例子
        //    //int WID = Convert.ToInt32(Request["WID"]);
        //    //bool isH = Convert.ToBoolean(Request["isH"]);
        //    //bool isH = true;
        //    //用cookie中的数据 验证是否是有效登录
        //    //if (!new Common().IsLogin(Request))
        //    //{
        //    //    return Json(new { Success = false, Message = "登录超时或没有登录！" });
        //    //}

        //    //Entity.LT_WeddingInfo m = BLL.CURD.GetModel<Entity.LT_WeddingInfo>(WID);

        //    //if (m == null)
        //    //{
        //    //    return Json(new { Success = false, Message = "未找到与卡号对应的婚宴信息！" });
        //    //}

        //    //HttpPostedFileBase fileData = Request.Files["fileData"];

        //    //if (fileData != null)
        //    //{
        //    //    try
        //    //    {
        //    //        string rootDir = "/UserUpload/";

        //    //        string fileName = Path.GetFileName(fileData.FileName);// 原始文件名称
        //    //        string fileExtension = Path.GetExtension(fileName).ToLower(); // 文件扩展名

        //    //        if (CheckFileExtension(fileExtension,fileData))
        //    //        {
        //    //            //根据文件后缀选择上传目录
        //    //            if (CheckImgExtension(fileExtension))
        //    //            {
        //    //                rootDir = rootDir + "UserImgs" + "/";
        //    //            }
        //    //            else
        //    //            {
        //    //                rootDir = rootDir + "Files" + "/";
        //    //            }
        //    //            // 文件上传后的保存路径
        //    //            string filePath = Server.MapPath(rootDir);
        //    //            if (!Directory.Exists(filePath))
        //    //            {
        //    //                Directory.CreateDirectory(filePath);
        //    //            }
        //    //            string saveName = Guid.NewGuid().ToString() + fileExtension; // 保存文件名称

        //    //            string imgAbsolutPath = General.Common.WebSiteUrl + rootDir + saveName;

        //    //            System.Drawing.Image img = System.Drawing.Image.FromStream(fileData.InputStream, true, true);
        //    //            if (img.GetPropertyItem(0x0112).Value[0] == 6) img.RotateFlip(RotateFlipType.Rotate90FlipNone);//6则需要旋转，解决iphone下图片方向不正确的问题，具体看：http://blog.csharphelper.com/2011/08/29/use-exif-information-to-orient-an-image-properly-in-c.aspx or http://www.impulseadventure.com/photo/exif-orientation.html or http://blog.csdn.net/u010889390/article/details/13775521
        //    //            img.Save(filePath + saveName);
        //    //            //fileData.SaveAs(filePath + saveName);
        //    //            string min_img = "";
        //    //            if (isH>0)//如果是头像
        //    //            {
        //    //                //m.HeadImg = imgAbsolutPath;
        //    //                min_img = General.ptimage.cutforcustomByWidth(filePath + saveName, filePath, 640, 0, 100, "min", General.Common.WebSiteUrl + rootDir);
        //    //                m.HeadImg = min_img;
        //    //            }
        //    //            else
        //    //            {
        //    //                //m.WeddingDressImg = m.WeddingDressImg + "|" + imgAbsolutPath;
        //    //                min_img = General.ptimage.cutforcustom(filePath + saveName, filePath, 590, 384, 100, "min", General.Common.WebSiteUrl + rootDir);
        //    //                m.WeddingDressImg = m.WeddingDressImg + "|" + min_img;
        //    //            }

        //    //            BLL.CURD.Update<Entity.LT_WeddingInfo>(m);

        //    //            return Json(new { Success = true, FileName = fileName, SaveName = saveName, SavePath = min_img });
        //    //        }
        //    //        else {
        //    //            return Json(new { Success = false, Message = "不允许的文件后缀或者文件不符合规定！\n"+ General.Common.AllowUploadImageExtension +"," + General.Common.AllowUploadMediaExtension }, JsonRequestBehavior.AllowGet);
        //    //        }
        //    //    }
        //    //    catch (Exception ex)
        //    //    {
        //    //        return Json(new { Success = false, Message = ex.Message }, JsonRequestBehavior.AllowGet);
        //    //    }
        //    //}
        //    //else
        //    //{
        //    //    return Json(new { Success = false, Message = "请选择要上传的文件！" }, JsonRequestBehavior.AllowGet);
        //    //}
        //    #endregion

        //}

        //[AcceptVerbs(HttpVerbs.Post)]
        //public JsonResult Video(int WID) 
        //{

        //    Entity.LT_WeddingInfo m = BLL.CURD.GetModel<Entity.LT_WeddingInfo>(WID);

        //    if (m == null) {
        //        return Json(new { Success = false, Message = "未找到与卡号对应的婚宴信息！" });
        //    }

        //    HttpPostedFileBase fileData = Request.Files["fileData"];

        //    if (fileData != null)
        //    {
        //        try
        //        {
        //            string rootDir = "/UserUpload/";

        //            string fileName = Path.GetFileName(fileData.FileName);// 原始文件名称
        //            string fileExtension = Path.GetExtension(fileName).ToLower(); // 文件扩展名

        //            if (CheckMediaExtension(fileExtension))
        //            {

        //                rootDir = rootDir + "Videos" + "/";

        //                // 文件上传后的保存路径
        //                string filePath = Server.MapPath(rootDir);
        //                if (!Directory.Exists(filePath))
        //                {
        //                    Directory.CreateDirectory(filePath);
        //                }
        //                string saveName = Guid.NewGuid().ToString() + fileExtension; // 保存文件名称

        //                fileData.SaveAs(filePath + saveName);//保存视频文件

        //                General.VideoConverter vc = new General.VideoConverter();
        //                string thumb = vc.CatchImg(rootDir + saveName);//取得视频缩略图,不知道是否有同步异步问题

        //                m.VideoThumbnails = thumb;
        //                m.VideoUrl = General.Common.WebSiteUrl + rootDir + saveName;

        //                BLL.CURD.Update<Entity.LT_WeddingInfo>(m);

        //                return Json(new { Success = true, FileName = fileName, SaveName = saveName, SavePath = General.Common.WebSiteUrl + rootDir + saveName, VideoImg = thumb });

        //            }
        //            else
        //            {
        //                return Json(new { Success = false, Message = "不允许的文件后缀或者文件不符合规定！\n" + General.Common.AllowUploadImageExtension + "," + General.Common.AllowUploadMediaExtension }, JsonRequestBehavior.AllowGet);
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            return Json(new { Success = false, Message = ex.Message }, JsonRequestBehavior.AllowGet);
        //        }
        //    }
        //    else
        //    {
        //        return Json(new { Success = false, Message = "请选择要上传的文件！" }, JsonRequestBehavior.AllowGet);
        //    }
        //}
        #endregion


        #region 仅仅上传图片 返回上传成功的图片的 绝对地址。（网络地址）
        //
        // GET: /API/Upload/
        //上传方法，后面应该拓展，不同会员上传目录不同，可根据会员名称创建会员目录
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult UploadImg(HttpPostedFileBase fileData)
        {
            if (fileData != null)
            {
                try
                {
                    string rootDir = "/UserUpload/";

                    string fileName = Path.GetFileName(fileData.FileName);// 原始文件名称
                    string fileExtension = Path.GetExtension(fileName).ToLower(); // 文件扩展名

                    if (CheckFileExtension(fileExtension, fileData))
                    {
                        //根据文件后缀选择上传目录
                        if (CheckImgExtension(fileExtension))
                        {
                            rootDir = rootDir + "images" + "/";
                        }
                        else
                        {
                            rootDir = rootDir + "files" + "/";
                        }
                        // 文件上传后的保存路径
                        string filePath = Server.MapPath(rootDir);
                        if (!Directory.Exists(filePath))
                        {
                            Directory.CreateDirectory(filePath);
                        }
                        string saveName = Guid.NewGuid().ToString() + fileExtension; // 保存文件名称

                        fileData.SaveAs(filePath + saveName);

                        return Json(new { Success = true, FileName = fileName, SaveName = saveName, SavePath = Common.Common.WebSiteUrl + rootDir + saveName });
                    }
                    else
                    {
                        return Json(new { Success = false, Message = "不允许的文件后缀或者文件不符合规定！\n" + Common.Common.AllowUploadImageExtension + "," + Common.Common.AllowUploadMediaExtension }, JsonRequestBehavior.AllowGet);
                    }
                }
                catch (Exception ex)
                {
                    return Json(new { Success = false, Message = ex.Message }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { Success = false, Message = "请选择要上传的文件！" }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        //场景图片多上传方法
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult UploadHPicImg(HttpPostedFileBase fileData, int sid=0)
        {
            if (fileData != null || sid>0)
            {
                try
                {
                    string rootDir = "/UserUpload/";

                    string fileName = Path.GetFileName(fileData.FileName);// 原始文件名称
                    string fileExtension = Path.GetExtension(fileName).ToLower(); // 文件扩展名

                    if (CheckFileExtension(fileExtension, fileData))
                    {
                        //根据文件后缀选择上传目录
                        if (CheckImgExtension(fileExtension))
                        {
                            rootDir = rootDir + "images" + "/";
                        }
                        else
                        {
                            rootDir = rootDir + "files" + "/";
                        }
                        // 文件上传后的保存路径
                        string filePath = Server.MapPath(rootDir);
                        if (!Directory.Exists(filePath))
                        {
                            Directory.CreateDirectory(filePath);
                        }
                        string saveName = Guid.NewGuid().ToString() + fileExtension; // 保存文件名称

                        fileData.SaveAs(filePath + saveName);
                        //ISpHouse_HseArea_PicsServices SpH_H_PicsServices = new SpHouse_HseArea_PicsServices();
                        //SpHouse_HseArea_Pics spPic = new SpHouse_HseArea_Pics()
                        //{
                        //    HouseAreaPic = Path.Combine(Common.Common.WebSiteUrl,rootDir, saveName),
                        //    SampleHouse_HouseAreaID = sid
                        //};
                        //SpHouse_HseArea_Pics ReturnspPic = SpH_H_PicsServices.Add(spPic);
                        //if (ReturnspPic.ID > 0)
                        //{
                        //    return Json(new { Success = true, FileName = fileName, SaveName = saveName, SavePath = Common.Common.WebSiteUrl + rootDir + saveName,Pid=ReturnspPic.ID });
                        //}
                        //else
                        //{

                        //    return Json(new { Success = false, Message = "未知错误删除失败" + Common.Common.AllowUploadImageExtension + "," + Common.Common.AllowUploadMediaExtension }, JsonRequestBehavior.AllowGet);
                        //}

                        return Json(new { Success = false, Message = "未知错误删除失败" + Common.Common.AllowUploadImageExtension + "," + Common.Common.AllowUploadMediaExtension }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { Success = false, Message = "不允许的文件后缀或者文件不符合规定！\n" + Common.Common.AllowUploadImageExtension + "," + Common.Common.AllowUploadMediaExtension }, JsonRequestBehavior.AllowGet);
                    }
                }
                catch (Exception ex)
                {
                    return Json(new { Success = false, Message = ex.Message }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { Success = false, Message = "请选择要上传的文件！" }, JsonRequestBehavior.AllowGet);
            }
        }



        #region 删除文件
        /// <summary>
        /// 注意，此方法不宜公开，会带来权限问题，服务器上上传的文件可能被恶意删除，如果要删除，应该加权限控制
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult Delete(string fileName, int cid = 0, int type = 0)
        {
            //todo:用cookie中的数据 验证是否是有效登录
            //if (!new BLL.Comm().IsChefLogin())
            //{
            //    return Json(new { Success = false, Message = "登录超时或没有登录！" });
            //}

            try
            {
                #region 前台uploadfity上传 和 后台ckfinder 上传图片 路径不一致 所以要考虑 不同路径的问题
                string rootDir = "/UserUpload/";
                string fileExtension = fileName.Split('.')[1];
                if (CheckImgExtension(fileExtension))
                {
                    rootDir = rootDir + "images" + "/";
                }
                else
                {
                    rootDir = rootDir + "files" + "/";
                }
                string filePath = Server.MapPath(rootDir);
                System.IO.FileInfo file = new System.IO.FileInfo(filePath + fileName);

                #endregion


                #region 前台uploadfity上传 和 后台ckfinder 上传图片 路径不一致 所以要考虑 不同路径的问题
                string rootDir2 = "/UserUpload/";
                string fileExtension2 = fileName.Split('.')[1];
                if (CheckImgExtension(fileExtension2))
                {
                    rootDir2 = rootDir2 + "images" + "/";
                }
                else
                {
                    rootDir2 = rootDir2 + "files" + "/";
                }
                string filePath2 = Server.MapPath(rootDir2);
                System.IO.FileInfo file2 = new System.IO.FileInfo(filePath2 + fileName);

                #endregion

                bool flag = false;
                if (file.Exists)
                {

                    file.Delete();
                    flag = true;

                }

                if (file2.Exists)
                {
                    file2.Delete();
                    flag = true;
                }

                switch (type)
                {
                    case 1:
                        ////更新商品大图片为空
                        //var model = BLL.CURD.GetModel<Entity.LT_GoodsInfo>(cid);
                        //if (model != null)
                        //{
                        //    model.GoodsBigPic = "";
                        //    BLL.CURD.Update<Entity.LT_GoodsInfo>(model);
                        //}
                        break;
                    case 2:
                        ////更新商品详细图片
                        //var model2 = BLL.CURD.GetModel<Entity.LT_GoodsInfo>(cid);
                        //if (model2 != null)
                        //{
                        //    model2.GoodsDetailPic = model2.GoodsDetailPic.Replace(General.Common.WebSiteUrl + rootDir + fileName + "|", "");
                        //    BLL.CURD.Update<Entity.LT_GoodsInfo>(model2);
                        //}
                        break;
                    case 3:
                        ////更新加盟商信息里的 店铺logo为空
                        //var model3 = BLL.CURD.GetModel<Entity.LT_SupplierInfo>(cid);
                        //if (model3 != null)
                        //{
                        //    model3.LogoImage = "";
                        //    BLL.CURD.Update<Entity.LT_SupplierInfo>(model3);
                        //}
                        break;
                    case 4:
                        ////更新加盟商信息里的 店铺背景为空
                        //var model4 = BLL.CURD.GetModel<Entity.LT_SupplierInfo>(cid);
                        //if (model4 != null)
                        //{
                        //    model4.StoreBackground = "";
                        //    BLL.CURD.Update<Entity.LT_SupplierInfo>(model4);
                        //}
                        break;
                    default:
                        break;
                }

                if (flag)
                {
                    return Json(new { Success = true, Message = "文件删除成功！" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { Success = true, Message = "文件删除失败！未找到文件!" }, JsonRequestBehavior.AllowGet);
                }

            }
            catch
            {
                return Json(new { Success = false, Message = "遇到未知错误，删除失败！" }, JsonRequestBehavior.AllowGet);
            }

        }

        //场景图片方法
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult DeleteHPic(string fileName, int pid = 0, int type = 0)
        {
            //todo:用cookie中的数据 验证是否是有效登录
            //if (!new BLL.Comm().IsChefLogin())
            //{
            //    return Json(new { Success = false, Message = "登录超时或没有登录！" });
            //}

            try
            {
                #region 前台uploadfity上传 和 后台ckfinder 上传图片 路径不一致 所以要考虑 不同路径的问题
                string rootDir = "/UserUpload/";
                string fileExtension = fileName.Split('.')[1];
                if (CheckImgExtension(fileExtension))
                {
                    rootDir = rootDir + "images" + "/";
                }
                else
                {
                    rootDir = rootDir + "files" + "/";
                }
                string filePath = Server.MapPath(rootDir);
                System.IO.FileInfo file = new System.IO.FileInfo(filePath + fileName);

                #endregion


                #region 前台uploadfity上传 和 后台ckfinder 上传图片 路径不一致 所以要考虑 不同路径的问题
                string rootDir2 = "/UserUpload/";
                string fileExtension2 = fileName.Split('.')[1];

                if (CheckImgExtension(fileExtension2))
                {

                    rootDir2 = rootDir2 + "images" + "/";
                }
                else
                {
                    rootDir2 = rootDir2 + "files" + "/";
                }
                string filePath2 = Server.MapPath(rootDir2);
                System.IO.FileInfo file2 = new System.IO.FileInfo(filePath2 + fileName);

                #endregion

                bool flag = false;
                //ISpHouse_HseArea_PicsServices SpH_H_PicsServices = new SpHouse_HseArea_PicsServices();
                //if (file.Exists)
                //{
                //    SpHouse_HseArea_Pics SpPic = SpH_H_PicsServices.LoadEntitys(s => s.ID == pid).FirstOrDefault();
                //    if (SpPic != null)
                //    {
                //        if (SpH_H_PicsServices.Delete(SpPic))
                //        {
                //            file.Delete();
                //            flag = true;
                //        }
                //        else
                //        {
                //            throw new Exception("");
                //        }
                       
                //    }
                //    else
                //    {
                //        throw new Exception("");
                //    }

                //}

                //if (file2.Exists)
                //{
                //    SpHouse_HseArea_Pics SpPic = SpH_H_PicsServices.LoadEntitys(s => s.ID == pid).FirstOrDefault();
                //    if (SpPic != null)
                //    {
                //        if (SpH_H_PicsServices.Delete(SpPic))
                //        {
                //            file2.Delete();
                //            flag = true;
                //        }
                //        else
                //        {
                //            throw new Exception("");
                //        }
                       
                //    }
                //    else
                //    {
                //        throw new Exception("");
                //    }
                //    file2.Delete();
                //    flag = true;
                //}

                switch (type)
                {
                    case 1:
                        ////更新商品大图片为空
                        //var model = BLL.CURD.GetModel<Entity.LT_GoodsInfo>(cid);
                        //if (model != null)
                        //{
                        //    model.GoodsBigPic = "";
                        //    BLL.CURD.Update<Entity.LT_GoodsInfo>(model);
                        //}
                        break;
                    case 2:
                        ////更新商品详细图片
                        //var model2 = BLL.CURD.GetModel<Entity.LT_GoodsInfo>(cid);
                        //if (model2 != null)
                        //{
                        //    model2.GoodsDetailPic = model2.GoodsDetailPic.Replace(General.Common.WebSiteUrl + rootDir + fileName + "|", "");
                        //    BLL.CURD.Update<Entity.LT_GoodsInfo>(model2);
                        //}
                        break;
                    case 3:
                        ////更新加盟商信息里的 店铺logo为空
                        //var model3 = BLL.CURD.GetModel<Entity.LT_SupplierInfo>(cid);
                        //if (model3 != null)
                        //{
                        //    model3.LogoImage = "";
                        //    BLL.CURD.Update<Entity.LT_SupplierInfo>(model3);
                        //}
                        break;
                    case 4:
                        ////更新加盟商信息里的 店铺背景为空
                        //var model4 = BLL.CURD.GetModel<Entity.LT_SupplierInfo>(cid);
                        //if (model4 != null)
                        //{
                        //    model4.StoreBackground = "";
                        //    BLL.CURD.Update<Entity.LT_SupplierInfo>(model4);
                        //}
                        break;
                    default:
                        break;
                }

                if (flag)
                {
                    return Json(new { Success = true, Message = "文件删除成功！",Res=1 }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { Success = true, Message = "文件删除失败！未找到文件!" ,Res =-1}, JsonRequestBehavior.AllowGet);
                }

            }
            catch
            {
                return Json(new { Success = false, Message = "遇到未知错误，删除失败！" ,Res=-2 }, JsonRequestBehavior.AllowGet);
            }

        }
        #endregion



        #region 验证文件合法性
        /// <summary>
        /// 判断上传文件后缀是否合法，并验证文件的有效性
        /// </summary>
        /// <param name="fileExtension"></param>
        /// <param name="fileData"></param>
        /// <returns></returns>
        private bool CheckFileExtension(string fileExtension, HttpPostedFileBase fileData)
        {
            if (CheckImgExtension(fileExtension))
            {
                try
                {
                    System.Drawing.Image img = System.Drawing.Image.FromStream(fileData.InputStream, true, true);
                    //if(fileData.ContentType.Contains("image")) //另外一种方法，判断文件头是否包含image
                    return true;//成功转换说明是图片文件
                }
                catch
                {
                    return false; //不是有效的图片文件，有可能是木马
                }
            }
            else if (CheckMediaExtension(fileExtension))
            {
                if (fileData.ContentLength > 204800 && fileData.ContentLength < 5120000)
                {
                    return true; //如果视频大于200K小于50M才允许上传
                }
                else
                {
                    return false; //否则不让上传
                }
            }
            else
            {
                return false;   //上传的后缀不在可上传名单中，在web.config中配置
            }
        }

        private bool CheckImgExtension(string extension)
        {
            string arrowExtension = Common.Common.AllowUploadImageExtension;
            return arrowExtension.Contains(extension);
        }

        private bool CheckMediaExtension(string extension)
        {
            string arrowExtension = Common.Common.AllowUploadMediaExtension;
            return arrowExtension.Contains(extension);
        }

        #endregion

    }

}
