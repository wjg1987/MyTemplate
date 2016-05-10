using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;

namespace My.Template.Common
{
    public class LogHelper
    {
        public static void WriteLog(string msg)
        {
            //Log4Net只需要配置一下 就可以将日志写入到 不同的位置  文件 或者数据库等
            ILog logWriter = LogManager.GetLogger("ssssss");
            logWriter.Error(msg);
        }
    }
}
