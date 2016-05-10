using Newtonsoft.Json.Linq;
using NPOI.HSSF.UserModel;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace My.Template.UI.Portal.Areas.Admin.Controllers
{
    public class GeneralCardController : BaseController
    {

        private Dictionary<string, string> dic = new Dictionary<string, string>();//保存生成的卡号密码
        private string sql = "";
        private string LockStr = "lockExceptionQueue";
        private List<string> oldDBCardList = new List<string>();//生成卡之前 将数据库已经存在的卡读取出来 存入内存..

        //只生成实体卡
        public ActionResult Index()
        {
            return View();
        }

        public int GetList(int count, int cardType)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            JObject json = new JObject();

          
            //1.根据数量和类型 生成count个卡号 插入数据库
            //Model.DiscountCoupon card = new Model.DiscountCoupon();

            //card.Value = 100;
            //card.DisCardTypeID = 1;
            //card.IsUsed = false;
            //card.SendTime = new DateTime(1990, 1, 1);
            //card.UsedTime = new DateTime(1990, 1, 1);
            //card.UserID = 0;
            //card.Expires = DateTime.Now.AddYears(3);
         
            //查询出所有的卡号
            //string getOldCardSql = "select CardNum from LT_CardInfo";
            //var reader = MyDB.Instance.ExecuteDataReader(getOldCardSql);
            //while (reader.Read())
            //{
            //    oldDBCardList.Add(reader.GetString(0));//将卡号填入集合
            //}

            //AdoHelper db = MyDB.GetDBHelper();
            //db.BeginTransaction();

            #region 开启50个线程
            //for (int i = 0; i < 50; i++)
            //{
            //    Thread thread = new Thread(ProcessGeneral);
            //    thread.IsBackground = true;
            //    thread.Start(new ProcessGeneralModel() { ci = card, count = count});
            //}
            #endregion

            while (dic.Count < count)
            {
                Thread.Sleep(50);//当还没有创建出 想要的卡的数量时候 等待
            }

            ////循环将 字典中的集合 拼成sql
            //foreach (var item in dic.Keys)
            //{
            //    sql += string.Format(" insert into dbo.LT_CardInfo(CardNum, Password, Parvalue, Price, IsUsed, UsedTime, CardTypeID, GeneralTime,Remark)values('{0}', '{1}', {2}, {3}, {4}, '{5}', {6}, '{7}','{8}');",
            //                   item.Replace(" ", ""),//key为 卡号                           
            //                   General.Encryption.EncryptDES(dic[item]),//value为 密码
            //                   cardTypeModel.Parvalue,
            //                   cardTypeModel.Price,
            //                   0,
            //                   card.UsedTime.ToString(),//已用时间
            //                   cardType,
            //                   card.GeneralTime.ToString(),//生成时间
            //                   card.Remark);
            //}
            //  去掉生成的sql 最末尾的 分好";"
            //sql = sql.TrimEnd(';');

            //if (dic.Count > count)//判断实际数量
            //{
            //    for (int i = 0; i < dic.Count - count; i++)
            //    {
            //        sql = sql.Substring(0, sql.LastIndexOf(';') + 1);
            //    }
            //}


            //int res = db.ExecuteNonQuery(sql);


            #region 添加生成记录文件
            //if (res != count)//影响的行数 不等于要插入的行数
            //{
            //    sw.Stop();
            //    db.Rollback();
            //    json.Add(new JProperty("Res", -2));
            //    json.Add(new JProperty("Msg", "批量生成中断!需要生成" + count + ",实际生成:" + res));
            //    json.Add(new JProperty("Time", sw.ElapsedMilliseconds));
            //    return json.ToString();
            //}
            //else
            //{
            //    try
            //    {
            //        #region 插入文件

            //        //创建 IWorkbook对象(工作薄)
            //        IWorkbook wk = new HSSFWorkbook();

            //        //创建一个 工作表
            //        ISheet sheet = wk.CreateSheet("卡号密码");
            //        //创建表头
            //        IRow rowHead = sheet.CreateRow(0);

            //        ICell cell0 = rowHead.CreateCell(0);
            //        cell0.SetCellValue("序号");
            //        ICell cell1 = rowHead.CreateCell(1);
            //        cell1.SetCellValue("卡号");
            //        ICell cell2 = rowHead.CreateCell(2);
            //        cell2.SetCellValue("密码");

            //        //将生成的卡号 插入文件
            //        int index2 = 1;//从第2行开始 第1行是表头
            //        foreach (var item in dic)
            //        {
            //            //创建行
            //            IRow row = sheet.CreateRow(index2);
            //            ICell cell3 = row.CreateCell(0);
            //            cell3.SetCellValue(index2);
            //            ICell cell4 = row.CreateCell(1);
            //            cell4.SetCellValue(item.Key);
            //            ICell cell5 = row.CreateCell(2);
            //            cell5.SetCellValue(item.Value);
            //            index2++;
            //        }
            //        string dir = System.AppDomain.CurrentDomain.BaseDirectory + "CardInfoRecords/";
            //        dir = dir.Replace("\\", "/");

            //        if (!Directory.Exists(dir))
            //        {
            //            Directory.CreateDirectory(dir);
            //        }

            //        string fileName = DateTime.Now.ToString("yyyyMMdd_") + DateTime.Now.Millisecond + ".xls";
            //        using (FileStream fs = System.IO.File.Create(Path.Combine(dir, fileName)))
            //        {
            //            wk.Write(fs);
            //        }
            //        #endregion
            //        //添加一条记录
            //        Entity.LT_GenerationRecord gen = new Entity.LT_GenerationRecord();

            //        gen.Count = count;
            //        gen.Parvalue = cardTypeModel.Parvalue;
            //        gen.Price = cardTypeModel.Price;
            //        gen.FileUrl = Common.Common.WebSiteUrl + "/CardInfoRecords/" + fileName;
            //        gen.AddTime = DateTime.Now;
            //        gen.IsDelete = false;
            //        if (BLL.CURD.Add<Entity.LT_GenerationRecord>(gen) <= 0)
            //        {
            //            db.Rollback();
            //            json.Add(new JProperty("Res", -3));
            //            json.Add(new JProperty("Msg", "插入生成记录失败!"));
            //            json.Add(new JProperty("Time", sw.ElapsedMilliseconds));
            //            return json.ToString();
            //        }
            //    }
            //    catch (Exception e)
            //    {
            //        sw.Stop();
            //        db.Rollback();
            //        json.Add(new JProperty("Res", 0));
            //        json.Add(new JProperty("Msg", "生成失败:" + e.ToString()));
            //        json.Add(new JProperty("Time", sw.ElapsedMilliseconds));
            //        return json.ToString();
            //    }

            //    db.Commit();
            //    sw.Stop();
            //    json.Add(new JProperty("Res", 1));
            //    json.Add(new JProperty("Msg", "生成成功!"));
            //    json.Add(new JProperty("Time", sw.ElapsedMilliseconds.ToString()));
            //    return json.ToString();
            //}
            #endregion

            return 1;
        }

        //生成卡
        private string GetCardNum(int StorID)
        {
            StringBuilder str = new StringBuilder();
            //switch (StorID)
            //{
            //    case 0:
            //        str.Append("SKPD-");
            //        break;
            //    default:
            //        str.Append("SKPD-");
            //        break;
            //}
            char[] cArr = new char[36] { '0','1','2','3','4','5','6','7','8','9','A','B','C','D',
            'E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z'};

            for (int i = 0; i < 3; i++) //生成字符串
            {
                for (int j = 0; j < 3; j++)
                {
                    My.Template.Common.ThreadSafeRandom ran = new My.Template.Common.ThreadSafeRandom();
                    int tmp = ran.Next(0, 1000);
                    str.Append(cArr[tmp % 36]);
                    System.Threading.Thread.Sleep(15);
                }
                //str.Append("-");
            }
            return str.ToString().TrimEnd('-');
        }

        public string GetCardPass()
        {
            StringBuilder sb = new StringBuilder();

            char[] cArr = new char[10] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            My.Template.Common.ThreadSafeRandom r = new My.Template.Common.ThreadSafeRandom();
            for (int j = 0; j < 6; j++)
            {
                int temp = r.Next(0, 10000);
                sb.Append(cArr[temp % 10]);
                System.Threading.Thread.Sleep(30);
            }
            return sb.ToString();
        }

        private string ChangeNumber(string str)
        {
            string res = "";
            if (str.Length != 14) return str;
            res += str.Substring(0, 2) + " ";
            res += str.Substring(2, 4) + " ";
            res += str.Substring(4, 4) + " ";
            res += str.Substring(8, 4);
            return res;
        }

        private void ProcessGeneral(object o)
        {
            ProcessGeneralModel model = o as ProcessGeneralModel;
            while (dic.Count < model.count)
            {
                string cardNum = GetCardNum(model.type);

                if(oldDBCardList.Contains(cardNum))
                {
                    continue;
                }

                //在关键代码外部 做赋值操作
                //model.ci.CardNum = cardNum;
               
                //将卡号 设置为key
                string key = ChangeNumber(cardNum);

                if (dic.Count < model.count && !dic.ContainsKey(key))//没有找到此卡号 就生成
                {
                    lock (LockStr)//防止别的线程在该线程给字符串增加 以及 改变字段值得时候发生意外
                    {
                        if (dic.Count < model.count && !dic.ContainsKey(key))//没有找到此卡号 就生成
                        {
                            ////计数+记录生成的卡号密码
                            //dic.Add(key, model.ci.Password);
                        }
                    }
                }
            }
        }

        public class ProcessGeneralModel
        {
            public int count { get; set; }
            public int type { get; set; }
            //public  Model.DiscountCoupon ci { get; set; }
        }
    }
}
