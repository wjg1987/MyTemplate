using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace My.Template.UI.Portal.Areas.mobile.Controllers
{
    public class ShortMsgController : Controller
    {
        #region 发送短信全局变量区域
        string ProcessMsgUrl = "http://222.73.117.156/msg/HttpBatchSendSM";
        string userName = "fengzhai";
        string pwd = "beekoo83100900./";

        private string content4regist = "（蜂巢会员注册验证码），马上完成注册，领取会员优惠吧！"; //发给客户的注册验证码短信内容
        private string content4apply = "";
        private string content4Findpwd = "（蜂巢会员修改密码验证码），一次有效！";
        private string content4YuYue = "（蜂巢会员报名验证码），马上完成报名，享受超低折扣吧！";
        private string contentYuYueSuccess = "提交成功！稍后将有专员与您联系，请耐心等候！";
        private string contentGetSakeCodeSuccess = "（给利码）恭喜您成功领取给利码，快登陆蜂巢领取返利吧";
        #endregion

        #region  获取给利码成功短信通知用户， 同时通知蜂巢
        public string SendSakeCodeSuccessMsg(string sakecode ,string agentName )
        {
            var user = new My.Template.UI.Portal.CommClass.Comms().IsVipLogin();
            string content = "用户：" + user.Account + "，手机号码：" + user.Account + "， 领取了 " + agentName + "商家给利码";
            SendMsg(Common.Common.phoneAttendAcivity, content);
            return SendMsg(user.Account, sakecode+contentGetSakeCodeSuccess);

        }
        #endregion

        #region 发送预约短信验证码 
        public string SendYuYueCode( string mobile, string stype)
        {
            if (!Regex.IsMatch(mobile, @"^1[3|4|5|8][0-9]\d{4,8}$"))
            {
                return "-1";
            }

            if (string.IsNullOrEmpty(stype))
            {
                return "-2";//发送验证码的类型为空
            }

            Random r = new Random();
            string code = r.Next(100000, 999999).ToString();
           
            Session.Add(mobile + stype, code);
           
            content4regist = code + content4YuYue;

           
          return   SendMsg(mobile, content4regist);
        }

        #endregion

        #region 发送预约成功通知短信验证码 同时通知蜂巢
        public string SendYuYueSuccessMsg(string name, string mobile)
        {

            string content = "用户：" + name + "，手机号码：" + mobile + "， 预约设计师，请及时联系";
            SendMsg(Common.Common.phoneAttendAcivity, content);
            return SendMsg(mobile, contentYuYueSuccess);
        }
      

        #endregion


        #region  发送注册短信验证码
        public string SendValidateCode(string mobile,string stype)
        {
            if (!Regex.IsMatch(mobile, @"^1[3|4|5|8][0-9]\d{4,8}$"))
            {
                return "-1";
            }

            if (string.IsNullOrEmpty(stype))
            {
                return "-2";//发送验证码的类型为空
            }

            Random r = new Random();
            string code = r.Next(100000, 999999).ToString();
          
            Session.Add(mobile + stype,code);
          
            content4regist = code + content4regist;
           
        return SendMsg(mobile, content4regist);
        }
        #endregion

        #region 获取注册验证码
        public string GetValidateCode(string mobile,int type)
        {
            string stype = "";
            switch (type)
            {
                case 1:
                    stype = "regist";
                    break;
                case 2:
                    stype = "yuyue";
                    break;
            }
            return SendValidateCode(mobile, stype);
        }
        #endregion

        #region 发送短信给 获取 返利码的用户 同事 发送短信通知 小蜜蜂
        public string SendApplyShortMsg(string txtname, string txtPhone, string txtvalcode)
        {
            if (!Regex.IsMatch(txtPhone, @"^1[3|4|5|8][0-9]\d{4,8}$"))
            {
                return "-1";
            }

            if (string.IsNullOrEmpty(txtvalcode) || Session[txtPhone + "code"] == null)
            {
                return "-2";
            }
            else
            {
                if (txtvalcode != Session[txtPhone + "code"].ToString())
                {
                    return "-3";
                }
            }

            string content = "您申请的品牌返利已经申请成功，稍后我们的场景顾问会与您联系，谢谢！";
            string content2 = "客户" + txtname + ",号码:" + txtPhone + ",申请品牌返利。";



            SendMsg("18503083235", content2);//发送短信给小蜜蜂

            return SendMsg(txtPhone, content);
        }

        #endregion

        #region 发送短信用户找回会员密码
        public string SendShortMsg4Findpwd(string mobile)
        {
            if (!Regex.IsMatch(mobile, @"^1[3|4|5|8][0-9]\d{4,8}$"))
            {
                return "-1";
            }

            Random r = new Random();
            string code = r.Next(100000, 999999).ToString();
            
            Session.Add(mobile + "code4pwd",code);
            content4Findpwd = code + content4Findpwd;

            return SendMsg(mobile, content4Findpwd);
        }
        #endregion




        #region  预约服务 发送短信给客户 同时发送短信给 beekoo小蜜蜂


        public string YuYue(string uname, string mobile, string validatecode, int type)
        {
            if (!Regex.IsMatch(mobile, @"^1[3|4|5|8][0-9]\d{4,8}$"))
            {
                return "-1";
            }

            if (string.IsNullOrEmpty(validatecode) || Session["yuyue" + mobile] == null)
            {
                return "-2";
            }
            else
            {
                if (validatecode != Session["yuyue" + mobile].ToString())
                {
                    return "-3";
                }
            }

            string stype = "";
            switch (type)
            {
                case 1:
                    stype = "免费设计预约";
                    break;
                case 2:
                    stype = "给利优惠预约";
                    break;
                case 3:
                    stype = "免费质检预约";
                    break;
                case 4:
                    stype = "公益验房预约";
                    break;
                case 5:
                    stype = "咨询顾问预约";
                    break;
            }

            string content = "您申请的" + stype + "已经成功，稍后我们会与您联系，谢谢！";
            string content2 = "客户" + uname + ",号码:" + mobile + "," + stype;

            SendMsg("18503083235", content2);
            return SendMsg(mobile, content);
        }

        #endregion


        #region 指定手机号码和短信内容发送短信
        public string SendMsg(string mobile, string content)
        {
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
        #endregion

        #region  封装好 发送内容 以及的电话号码 的 方法
        public string SendMsg(string param)
        {

            string ProcessMsgUrl = ConfigurationManager.AppSettings["SMSAddress"];

            byte[] bs = System.Text.Encoding.UTF8.GetBytes(param);

            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(ProcessMsgUrl + "?" + param);
            req.Method = "Post";
            req.Timeout = 120 * 1000;
            req.ContentType = "application/x-www-form-urlencoded;";
            req.ContentLength = bs.Length;

            using (Stream reqStream = req.GetRequestStream())
            {
                reqStream.Write(bs, 0, bs.Length);
                reqStream.Flush();
            }
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
                return sb.ToString();
            }
        }
        #endregion

        #region demo
        public string SendMsgAndEmail(string contactPerson = "黄先生", string isJoin = "0", string mobilePhone = "18503083235", string content = "这家伙很懒，什么都没留下.")
        {
            string ProcessMsgUrl = ConfigurationManager.AppSettings["SMSAddress"];
            JObject json = new JObject();

            if (isJoin == "1")
            {
                isJoin = "是";
            }
            else
            {
                isJoin = "否";
            }

            content = "收到留言:联系人:" + contactPerson + "\\手机:" + mobilePhone + "\\是否参加:" + isJoin + "\\留言内容:" + content;

            //获得发送邮件和短信的一些基本配置信息
            My.Template.Common.EmailXML emaliXml = new My.Template.Common.EmailXML();
            string fromEmail = emaliXml.GetEmailValue("from_email");
            string toEmail = emaliXml.GetEmailValue("to_email");
            //配置文件中的密码 现在是错误的
            string fromEmailPwd = emaliXml.GetEmailValue("psw");
            string smtp = emaliXml.GetEmailValue("smtpHost");
            string fromEmailUser = emaliXml.GetEmailValue("email_user");
            string sendToMoblie = emaliXml.GetEmailValue("sendToMoblie");
            string emailTopic = emaliXml.GetEmailValue("emailTopic");


            //下面是发送短信
            string param =
                  "action=send&userid=820&account=lantuo&password=168168&mobile=" + sendToMoblie + "&content=" + content + "&sendTime=&extno=";

            byte[] bs = System.Text.Encoding.UTF8.GetBytes(param);

            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(ProcessMsgUrl + "?" + param);
            req.Method = "Post";
            req.Timeout = 120 * 1000;
            req.ContentType = "application/x-www-form-urlencoded;";
            req.ContentLength = bs.Length;

            using (Stream reqStream = req.GetRequestStream())
            {
                reqStream.Write(bs, 0, bs.Length);
                reqStream.Flush();
            }
           
            //下面是发送邮件
            SmtpClient client =
              new SmtpClient(smtp, 25);
            MailMessage msg =
                new MailMessage(fromEmail, toEmail, emailTopic, content);
            client.UseDefaultCredentials = false;
            System.Net.NetworkCredential basicAuthenticationInfo =
                new System.Net.NetworkCredential(fromEmailUser, fromEmailPwd);
            client.Credentials = basicAuthenticationInfo;
            client.Send(msg);

            json.Add(new JProperty("records", "1"));
            return json.ToString();

        }

        #endregion


        #region 会员发送短信找回密码（phone）
        public string PhoneSendShortMsg4Findpwd(string mobile)
        {
            if (!Regex.IsMatch(mobile, @"^1[3|4|5|8][0-9]\d{4,8}$"))
            {
                return "-1";
            }

            Random r = new Random();
            string code = r.Next(100000, 999999).ToString();
            code = "123456";
            Session["findpwdmobile"] = mobile;
            Session.Timeout = 10;

            Session.Add(mobile + "code4pwd", code);
            content4Findpwd = code + content4Findpwd;
            return "0";
           // return SendMsg(mobile, content4Findpwd);
        }
        #endregion
    }
}
