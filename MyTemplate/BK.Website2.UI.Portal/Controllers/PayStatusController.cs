using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using My.Template.BLL;
using My.Template.IBLL;
using My.Template.Model;

namespace My.Template.UI.Portal.Controllers
{
    public class PayStatusController : BaseController
    {
        #region  支付宝回调
        public ActionResult ReturnUrl()
        {
            //MarketGoodsServices marketGoodsServices = new MarketGoodsServices();
            //List<MarketGoods> model = marketGoodsServices.LoadEntitys(m => m.IsDelete == false && m.IsView).Take(4).ToList();
            
            //SortedDictionary<string, string> sPara = GetRequestGet();
            //if (sPara.Count > 0)//判断是否有带返回参数
            //{
            //    Com.Alipay.Notify aliNotify = new Com.Alipay.Notify();
            //    bool verifyResult = aliNotify.Verify(sPara, Request.QueryString["notify_id"], Request.QueryString["sign"]);

            //    if (verifyResult)//验证成功
            //    {
            //        //获取支付宝的通知返回参数，可参考技术文档中页面跳转同步通知参数列表

            //        //商户订单号
            //        string out_trade_no = Request.QueryString["out_trade_no"];
            //        //支付宝交易号
            //        string trade_no = Request.QueryString["trade_no"];
            //        //交易状态
            //        string trade_status = Request.QueryString["trade_status"];
                  
            //        //AdoHelper db = MyDB.GetDBHelper();
            //        if (trade_status == "TRADE_SUCCESS")//即时到账 所以付款直接成功 但此时商户网站状态应该更改为 付款成功 等待卖家发货
            //        {
            //            //判断该笔订单是否在商户网站中已经做过处理
            //            //如果没有做过处理，根据订单号（out_trade_no）在商户网站的订单系统中查到该笔订单的详细，并执行商户的业务程序
            //            //如果有做过处理，不执行商户的业务程序
            //            //查找到订单
            //            OrderInfoServices oser = new OrderInfoServices();
            //            var order = oser.LoadEntitys(u => u.OrderNum == out_trade_no).FirstOrDefault();
            //            //如果有做过处理，不执行商户的业务程序
            //            if (order.OrderStatusID != 2)//即时到账成功 但此时还没发货  商户网站订单更新为发货
            //            {
            //                order.OrderStatusID = 2;
            //                oser.Update(order);
            //                //SendShortMsg(order.User.Account);
            //            }

            //            ViewBag.status = 1;//在回调里已经支付成功 
            //            return View(model);
            //        }
            //        else
            //        {
            //            Response.Write("trade_status=" + Request.QueryString["trade_status"]);
            //            ViewBag.status = -2;//不是支付成功 
            //        }
            //    }
            //    else//验证失败 不是支付宝发出的请求
            //    {
            //        ViewBag.status = 0;
            //    }
            //}
            //else
            //{
            //    ViewBag.status = -1;//无返回参数
            //}

           // return View(model);
            return View();
        }

        public void NotifyUrl()
        {
            SortedDictionary<string, string> sPara = GetRequestPost();

            //if (sPara.Count > 0)//判断是否有带返回参数
            //{
            //    Com.Alipay.Notify aliNotify = new Com.Alipay.Notify();
            //    bool verifyResult = aliNotify.Verify(sPara, Request.Form["notify_id"], Request.Form["sign"]);

            //    if (verifyResult)//验证成功
            //    {
            //        //获取支付宝的通知返回参数，可参考技术文档中服务器异步通知参数列表

            //        //商户订单号

            //        string out_trade_no = Request.Form["out_trade_no"];

            //        //支付宝交易号

            //        string trade_no = Request.Form["trade_no"];

            //        //交易状态
            //        string trade_status = Request.Form["trade_status"];

            //        //查找到订单
            //        OrderInfoServices oser = new OrderInfoServices();
            //        var order = oser.LoadEntitys(u => u.OrderNum == out_trade_no).FirstOrDefault();
            //        if (Request.Form["trade_status"] == "WAIT_BUYER_PAY")
            //        {
            //            //该判断表示买家已在支付宝交易管理中产生了交易记录，但没有付款

            //            //判断该笔订单是否在商户网站中已经做过处理
            //            //如果没有做过处理，根据订单号（out_trade_no）在商户网站的订单系统中查到该笔订单的详细，并执行商户的业务程序
            //            //如果有做过处理，不执行商户的业务程序
            //            if (order.OrderStatusID != 1)//不是等待买家发货  说明还没有处理
            //            {
            //                order.OrderStatusID = 1;
            //                oser.Update(order);
            //            }
            //        }
            //        else if (Request.Form["trade_status"] == "TRADE_SUCCESS")
            //        {
            //            //如果有做过处理，不执行商户的业务程序
            //            if (order.OrderStatusID != 2)//即时到账成功 但此时还没发货  商户网站订单更新为发货
            //            {
            //                order.OrderStatusID = 2;
            //                oser.Update(order);
            //                SendShortMsg(order.User.Account);
            //            }
            //        }
            //        else if (Request.Form["trade_status"] == "TRADE_FINISHED")
            //        {
            //            //该判断表示买家已经确认收货，这笔交易完成

            //            //如果有做过处理，不执行商户的业务程序
            //            if (order.OrderStatusID != 4)//不是等待买家发货  说明还没有处理
            //            {
            //                order.OrderStatusID = 4;
            //                oser.Update(order);
            //                return;
            //            }

            //        }
            //        else
            //        {
            //            Response.Write("success");  //其他状态判断。
            //        }
            //    }
            //    else//验证失败
            //    {
            //        Response.Write("fail");
            //    }
            //}
            //else
            //{
            //    Response.Write("无通知参数");
            //}
        }
        #endregion


        /// <summary>
        /// 获取支付宝GET过来通知消息，并以“参数名=参数值”的形式组成数组
        /// </summary>
        /// <returns>request回来的信息组成的数组</returns>
        public SortedDictionary<string, string> GetRequestGet()
        {
            int i = 0;
            SortedDictionary<string, string> sArray = new SortedDictionary<string, string>();
            NameValueCollection coll;
            //Load Form variables into NameValueCollection variable.
            coll = Request.QueryString;

            // Get names of all forms into a string array.
            String[] requestItem = coll.AllKeys;

            for (i = 0; i < requestItem.Length; i++)
            {
                sArray.Add(requestItem[i], Request.QueryString[requestItem[i]]);
            }

            return sArray;
        }




        /// <summary>
        /// 获取支付宝POST过来通知消息，并以“参数名=参数值”的形式组成数组
        /// </summary>
        /// <returns>request回来的信息组成的数组</returns>
        public SortedDictionary<string, string> GetRequestPost()
        {
            int i = 0;
            SortedDictionary<string, string> sArray = new SortedDictionary<string, string>();
            NameValueCollection coll;
            //Load Form variables into NameValueCollection variable.
            coll = Request.Form;

            // Get names of all forms into a string array.
            String[] requestItem = coll.AllKeys;

            for (i = 0; i < requestItem.Length; i++)
            {
                sArray.Add(requestItem[i], Request.Form[requestItem[i]]);
            }

            return sArray;
        }



    




        #region 手机支付成功回调
        public ActionResult PhoneReturnUrl()
        {
            SortedDictionary<string, string> sPara = GetRequestGet();
            //OrderInfo order = new OrderInfo() { ID = 0 };
            //if (sPara.Count > 0)//判断是否有带返回参数
            //{
            //    Com.Alipay.Phone.Notify aliNotify = new Com.Alipay.Phone.Notify();
            //    bool verifyResult = aliNotify.Verify(sPara, Request.Form["notify_id"], Request.QueryString["sign"]);


            //    if (verifyResult)//验证成功
            //    {
            //        /////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //        //请在这里加上商户的业务逻辑程序代码


            //        //——请根据您的业务逻辑来编写程序（以下代码仅作参考）——
            //        //获取支付宝的通知返回参数，可参考技术文档中页面跳转同步通知参数列表

            //        //商户订单号

            //        string out_trade_no = Request.QueryString["out_trade_no"];

            //        //支付宝交易号

            //        string trade_no = Request.QueryString["trade_no"];

            //        //交易状态
            //        string trade_status = Request.QueryString["trade_status"];

            //        OrderInfoServices oser = new OrderInfoServices();
            //        order = oser.LoadEntitys(u => u.OrderNum == out_trade_no).FirstOrDefault();
            //        if (Request.QueryString["trade_status"] == "TRADE_FINISHED" || Request.QueryString["trade_status"] == "TRADE_SUCCESS")
            //        {
            //            //判断该笔订单是否在商户网站中已经做过处理
            //            //如果没有做过处理，根据订单号（out_trade_no）在商户网站的订单系统中查到该笔订单的详细，并执行商户的业务程序
            //            //如果有做过处理，不执行商户的业务程序
            //            if (order.OrderStatusID == 1)//即时到账成功 但此时还没发货  商户网站订单更新为发货
            //            {
            //                order.OrderStatusID = 2;
            //                oser.Update(order);
            //                SendShortMsg(order.User.Account);
            //            }
            //            ViewBag.Msg = "<h1>交易成功!</h1>";
            //        }
            //        else
            //        {
            //            Response.Write("trade_status=" + Request.QueryString["trade_status"]);
            //        }

            //        //打印页面
            //        Response.Write("验证成功<br />");

            //        //——请根据您的业务逻辑来编写程序（以上代码仅作参考）——

            //        /////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //    }
            //    else//验证失败
            //    {
            //        Response.Write("验证失败");
            //    }
            //}
            //else
            //{
            //    Response.Write("无返回参数");
            //}
            //return Redirect("/phone/order/payresult?oid=" + order.ID);

            return View();
        }


        public void PhoneNotifyUrl()
        {
            SortedDictionary<string, string> sPara = GetRequestPost();

            //OrderInfo order = new OrderInfo() { ID = 0 };
            //if (sPara.Count > 0)//判断是否有带返回参数
            //{
            //    Com.Alipay.Phone.Notify aliNotify = new Com.Alipay.Phone.Notify();
            //    bool verifyResult = aliNotify.Verify(sPara, Request.Form["notify_id"], Request.Form["sign"]);



            //    if (verifyResult)//验证成功
            //    {



            //        /////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //        //请在这里加上商户的业务逻辑程序代码


            //        //——请根据您的业务逻辑来编写程序（以下代码仅作参考）——
            //        //获取支付宝的通知返回参数，可参考技术文档中页面跳转同步通知参数列表

            //        //商户订单号
            //        string out_trade_no = Request.QueryString["out_trade_no"];

            //        //支付宝交易号
            //        string trade_no = Request.QueryString["trade_no"];

            //        //交易状态
            //        string trade_status = Request.Form["trade_status"];


            //        //1.测试数据1111
            //        ViewBag.sParaCount = sPara.Count;
            //        ViewBag.out_trade_no = out_trade_no;
            //        ViewBag.trade_no = trade_no;
            //        ViewBag.result = trade_status;

            //        //判断是否在商户网站中已经做过了这次通知返回的处理
            //        //如果没有做过处理，那么执行商户的业务程序
            //        //如果有做过处理，那么不执行商户的业务程序
            //        OrderInfoServices oser = new OrderInfoServices();
            //        order = oser.LoadEntitys(u => u.OrderNum == out_trade_no).FirstOrDefault();
            //        if (trade_status == "TRADE_FINISHED")//即时到账 所以付款直接成功 但此时商户网站状态应该更改为 付款成功 等待卖家发货
            //        {
            //            //////////22222222222222222222
            //            ViewBag.result2 = 2222;
            //            //判断该笔订单是否在商户网站中已经做过处理
            //            //如果没有做过处理，根据订单号（out_trade_no）在商户网站的订单系统中查到该笔订单的详细，并执行商户的业务程序
            //            //如果有做过处理，不执行商户的业务程序

            //            //如果有做过处理，不执行商户的业务程序
            //            if (order.OrderStatusID == 1)//即时到账成功 但此时还没发货  商户网站订单更新为发货
            //            {
            //                order.OrderStatusID = 2;
            //                oser.Update(order);
            //                SendShortMsg(order.User.Account);
            //            }
            //            ViewBag.Msg = "<h1>交易成功!</h1>";
            //        }
            //        else if (trade_status == "TRADE_SUCCESS")
            //        {
            //            //判断该笔订单是否在商户网站中已经做过处理
            //            //如果没有做过处理，根据订单号（out_trade_no）在商户网站的订单系统中查到该笔订单的详细，并执行商户的业务程序
            //            //请务必判断请求时的total_fee、seller_id与通知时获取的total_fee、seller_id为一致的
            //            //如果有做过处理，不执行商户的业务程序

            //            //注意：
            //            //付款完成后，支付宝系统发送该交易状态通知
            //        }
            //        else//验证失败
            //        {
            //            ViewBag.Msg = "<h1>交易失败!</h1>";
            //        }
            //    }
            //    else
            //    {
            //        ViewBag.Msg = "<h1>无返回参数!</h1>";
            //    }
            //}
            //else
            //{
            //    Response.Write("无通知参数");
            //}
        }


        #endregion




        #region 记录错误的方法，写到bugLog.txt文件
        /// <summary>
        /// 记录bug，以便调试
        /// </summary>
        /// <returns></returns>
        public bool WriteTxt(string str)
        {
            str = "\r\n" + str + DateTime.Now.ToString();
            try
            {
                FileStream fs = new FileStream(Server.MapPath("/bugLog.txt"), FileMode.Append);
                StreamWriter sw = new StreamWriter(fs);
                //开始写入
                sw.WriteLine(str);
                //清空缓冲区
                sw.Flush();
                //关闭流
                fs.Close();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        #endregion



        public string SendShortMsg(string mobile)
        {
            if (!Regex.IsMatch(mobile, @"^1[3|4|5|8][0-9]\d{4,8}$"))
            {
                return "-1";
            }


            string content = "亲爱的用户，您在蜂巢官网的订单已支付成功！详情可登录www.beekoo.hk查看，更多需求，欢迎致电400-775-1314.";
            string ProcessMsgUrl = "http://222.73.117.158/msg/HttpBatchSendSM";
            string userName = "fengzhai";
            string pwd = "beekoo83100900./";
            //下面是发送短信
            string param = string.Format("account={0}&pswd={1}&mobile={2}&msg={3}&needstatus={4}", userName, pwd, mobile, content, "true");

            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(ProcessMsgUrl + "?" + param);


            #region GET方式
            req.Method = "Get";
            req.Timeout = 120 * 1000;
            req.ContentType = "application/x-www-form-urlencoded;";
            #endregion


            using (WebResponse wr = req.GetResponse())
            {
                //在这里对接收到的页面内容进行处理 

                Stream strm = wr.GetResponseStream();

                StreamReader sr = new StreamReader(strm, System.Text.Encoding.UTF8);

                string line;

                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                while ((line = sr.ReadLine()) != null)
                {
                    sb.Append(line + System.Environment.NewLine);
                }
                sr.Close();
                strm.Close();

                string str = sb.ToString();
                string[] strs = str.Split(new string[] { "\r\n" }, StringSplitOptions.None);
                string res = strs[0].Split(',')[1];
                return res;
            }
        }
    }
}
