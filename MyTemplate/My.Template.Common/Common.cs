using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using My.Template.Model;
using log4net;

namespace My.Template.Common
{
    public static class Common
    {
        public static string WebSiteUrl = ConfigurationManager.AppSettings["websiteUrl"];
        public static string AllowUploadImageExtension = ConfigurationManager.AppSettings["AllowUploadImageExtension"];
        public static string AllowUploadMediaExtension = ConfigurationManager.AppSettings["AllowUploadMediaExtension"];
        public static string appid = ConfigurationManager.AppSettings["appid"];//用于没注册用户购物时 拉取用户微信信息
        public static string AppSecret = ConfigurationManager.AppSettings["AppSecret"];//用于没注册用户购物时 拉取用户微信
        public static Model.SiteInfo SiteInfo;
        public static string lockStr = "lockstring";
        //-客户领取给利码 蜂巢用于接收短信手机号码
        public static string phoneGetCode = ConfigurationManager.AppSettings["phoneGetCode"];
        //客户参加活动 蜂巢用于接收短信的手机号码
        public static string phoneAttendAcivity = ConfigurationManager.AppSettings["phoneAttendAcivity"];
        
        public static  log4net.ILog logWriter = LogManager.GetLogger("");

        public  static string IsTestAlipay =  ConfigurationManager.AppSettings["IsTestAlipay"];

        public static string ConnStr = ConfigurationManager.AppSettings["Connstr"];
       
    }





}