using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using My.Template.BLL;

namespace My.Template.UI.Portal.Controllers
{
    public class AlipayController : BaseController
    {
        #region PC支付
        public ActionResult Index(int orderID)
        {

            //#region  获得订单信息
            //OrderInfoServices oser = new OrderInfoServices();
            //var orderInfo = oser.LoadEntitys(u => u.ID == orderID).FirstOrDefault();

            ////测试将支付金额改为0.01
            //if (Common.Common.IsTestAlipay == "1")
            //{
            //    orderInfo.PayMoney = (decimal)0.01;
            //}
            //#endregion

            ////支付类型
            //string payment_type = "1";
            ////必填，不能修改
            ////服务器异步通知页面路径
            //string notify_url = "http://web2.beekoo.hk/payStatus/NotifyUrl";
            ////需http://格式的完整路径，不能加?id=123这类自定义参数

            ////页面跳转同步通知页面路径
            //string return_url = "http://web2.beekoo.hk/payStatus/ReturnUrl";
            ////需http://格式的完整路径，不能加?id=123这类自定义参数，不能写成http://localhost/

            ////卖家支付宝帐户
            //string seller_email = "beekoo@beekoo.hk";
            ////必填

            ////商户订单号
            ////string out_trade_no = orderInfo.OrderNum;
            //string out_trade_no = orderInfo.OrderNum;
            ////商户网站订单系统中唯一订单号，必填

            ////订单名称
            ////string subject = orderInfo.RecName;
            //string subject = orderInfo.OrderNum+ "蜂巢商城";
            ////必填

            ////付款金额
            ////string total_fee = payMoney.ToString();
            //string total_fee = orderInfo.PayMoney.ToString();
            ////必填

            ////订单描述
            ////string body = orderInfo.Remark;
            //string body ="蜂巢PC订单,"+orderInfo.OrderNum+orderInfo.BeekooRemark;


            ////商品展示地址
            //string show_url = "";
            ////需以http://开头的完整路径，例如：http://www.xxx.com/myorder.html

            ////防钓鱼时间戳
            //string anti_phishing_key = "";
            ////若要使用请调用类文件submit中的query_timestamp函数

            ////客户端的IP地址
            //string exter_invoke_ip = "";
            ////非局域网的外网IP地址，如：221.0.0.1


            //////////////////////////////////////////////////////////////////////////////////////////////////

            ////把请求参数打包成数组
            //SortedDictionary<string, string> sParaTemp = new SortedDictionary<string, string>();
            //sParaTemp.Add("partner", Com.Alipay.Config.Partner);
            //sParaTemp.Add("_input_charset", Com.Alipay.Config.Input_charset.ToLower());
            //sParaTemp.Add("service", "create_direct_pay_by_user");
            //sParaTemp.Add("payment_type", payment_type);
            //sParaTemp.Add("notify_url", notify_url);
            //sParaTemp.Add("return_url", return_url);
            //sParaTemp.Add("seller_email", seller_email);
            //sParaTemp.Add("out_trade_no", out_trade_no);
            //sParaTemp.Add("subject", subject);
            //sParaTemp.Add("total_fee", total_fee);
            //sParaTemp.Add("body", body);
            //sParaTemp.Add("show_url", show_url);
            //sParaTemp.Add("anti_phishing_key", anti_phishing_key);
            //sParaTemp.Add("exter_invoke_ip", exter_invoke_ip);

            ////建立请求
            //string sHtmlText = Com.Alipay.Submit.BuildRequest(sParaTemp, "get", "确认");
            //Response.Write(sHtmlText);
            return Content("");
        }

        #endregion


        #region 手机版本支付订单
        public void PhonePay(int orderID)
        {

            //#region 获得订单信息
            //OrderInfoServices oser = new OrderInfoServices();
            //var orderInfo = oser.LoadEntitys(u => u.ID == orderID).FirstOrDefault();
            //if (orderInfo.PayMoney < 10)
            //{
            //    orderInfo.PayMoney = (decimal)0.01;
            //}
            //#endregion

            //////////////////////////////////////////////请求参数////////////////////////////////////////////

            ////支付类型
            //string payment_type = "1";
            ////必填，不能修改
            ////服务器异步通知页面路径
            //string notify_url = "http://www.beekoo.hk/payStatus/PhoneNotifyUrl";
            ////需http://格式的完整路径，不能加?id=123这类自定义参数

            ////页面跳转同步通知页面路径
            //string return_url = "http://www.beekoo.hk/payStatus/PhoneReturnUrl";
            ////需http://格式的完整路径，不能加?id=123这类自定义参数，不能写成http://localhost/

            ////商户订单号
            //string out_trade_no = orderInfo.OrderNum;
            ////商户网站订单系统中唯一订单号，必填

            ////订单名称
            //string subject = "蜂巢宅配手机支付订单,单号"+orderInfo.OrderNum;
            ////必填

            ////付款金额
            //string total_fee = orderInfo.PayMoney.ToString();
            ////必填

            ////商品展示地址
            //string show_url = "";
            ////必填，需以http://开头的完整路径，例如：http://www.商户网址.com/myorder.html

            ////订单描述
            //string body = "";
            ////选填

            ////超时时间
            //string it_b_pay = "";
            ////选填

            ////钱包token
            //string extern_token = "";
            ////选填


            //////////////////////////////////////////////////////////////////////////////////////////////////

            ////把请求参数打包成数组
            //SortedDictionary<string, string> sParaTemp = new SortedDictionary<string, string>();
            //sParaTemp.Add("partner", Com.Alipay.Phone.Config.Partner);
            //sParaTemp.Add("seller_id", Com.Alipay.Phone.Config.Seller_id);
            //sParaTemp.Add("_input_charset", Com.Alipay.Phone.Config.Input_charset.ToLower());
            //sParaTemp.Add("service", "alipay.wap.create.direct.pay.by.user");
            //sParaTemp.Add("payment_type", payment_type);
            //sParaTemp.Add("notify_url", notify_url);
            ////sParaTemp.Add("return_url", return_url);
            ////sParaTemp.Add("out_trade_no", out_trade_no);
            ////sParaTemp.Add("subject", subject);
            ////sParaTemp.Add("total_fee", total_fee);
            ////sParaTemp.Add("show_url", show_url);
            ////sParaTemp.Add("body", body);
            ////sParaTemp.Add("it_b_pay", it_b_pay);
            ////sParaTemp.Add("extern_token", extern_token);

            ////建立请求
            //string sHtmlText = Com.Alipay.Phone.Submit.BuildRequest(sParaTemp, "get", "确认");
            //Response.Write(sHtmlText);
        }
        #endregion

    }
}
