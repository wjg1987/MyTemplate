using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using My.Template.Model;
using WxPayAPI;

namespace My.Template.UI.Portal.Controllers
{
    public class wxpaytestController : Controller
    {
        /// <summary>
        /// 调用js获取收货地址时需要传入的参数
        /// 格式：json串
        /// 包含以下字段：
        ///     appid：公众号id
        ///     scope: 填写“jsapi_address”，获得编辑地址权限
        ///     signType:签名方式，目前仅支持SHA1
        ///     addrSign: 签名，由appid、url、timestamp、noncestr、accesstoken参与签名
        ///     timeStamp：时间戳
        ///     nonceStr: 随机字符串
        /// </summary>
        public static string wxEditAddrParam { get; set; }


        public static string wxJsApiParam { get; set; } //H5调起JS API参数

        public ActionResult Index()
        {
            Log.Info(this.GetType().ToString(), "page load");

            JsApiPay jsApiPay = new JsApiPay(this.HttpContext);
            try
            {
                //调用【网页授权获取用户信息】接口获取用户的openid和access_token
                jsApiPay.GetOpenidAndAccessToken();

                //获取收货地址js函数入口参数
                ViewData["wxEditAddrParam"] = jsApiPay.GetEditAddressParameters();
                ViewData["openid"] = jsApiPay.openid;
            }
            catch (Exception ex)
            {
                Response.Write("<span style='color:#FF0000;font-size:20px'>" + "页面加载出错，请重试" + "</span>");
            }

            return View();
        }


        public ActionResult gorder(string openid)
        {
            //OrderInfo order = new OrderInfo();
            //order.PayMoney = 1;
            if (!string.IsNullOrEmpty(openid))
            {

                string url = "http://www.beekoo.hk/wxpaytest/pay?openid=" + openid + "&total_fee=1";
                Response.Redirect(url);
            }
            else
            {
                Response.Write("<span style='color:#FF0000;font-size:20px'>" + "页面缺少参数，请返回重试" + "</span>");
            }
            return View();
        }


        public ActionResult pay(string openid, string total_fee)
        {
            //检测是否给当前页面传递了相关参数
            if (string.IsNullOrEmpty(openid) || string.IsNullOrEmpty(total_fee))
            {
                Response.Write("<span style='color:#FF0000;font-size:20px'>" + "页面传参出错,请返回重试" + "</span>");
                Log.Error(this.GetType().ToString(), "This page have not get params, cannot be inited, exit...");
              
                return View();
            }

            //若传递了相关参数，则调统一下单接口，获得后续相关接口的入口参数
            JsApiPay jsApiPay = new JsApiPay(this.HttpContext);
            jsApiPay.openid = openid;
            jsApiPay.total_fee = int.Parse(total_fee);

            //JSAPI支付预处理
            try
            {
                WxPayData unifiedOrderResult = jsApiPay.GetUnifiedOrderResult();
                wxJsApiParam = jsApiPay.GetJsApiParameters();//获取H5调起JS API参数   
                ViewData["wxJsApiParam"] = wxJsApiParam;
                Log.Debug(this.GetType().ToString(), "wxJsApiParam : " + wxJsApiParam);
                //在页面上显示订单信息
                Response.Write("<span style='color:#00CD00;font-size:20px'>订单详情：</span><br/>");
                Response.Write("<span style='color:#00CD00;font-size:20px'>" + unifiedOrderResult.ToPrintStr() + "</span>");
                return View();
            }
            catch (Exception ex)
            {
                Response.Write("<span style='color:#FF0000;font-size:20px'>" + "下单失败，请返回重试" + "</span>");
                return View();
            }
        }


        public ActionResult payResult()
        {
            return View();
        }
    }
}
