using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;

namespace Log4Net
{
    class Program
    {
        static void Main(string[] args)
        {
            //初始化配置文件 让log4Net 起作用
            log4net.Config.XmlConfigurator.Configure();

            log4net.ILog logWriter = LogManager.GetLogger("");
            logWriter.Debug("debug级别错误");
            logWriter.Fatal("崩溃级别错误");
        }
    }
}
