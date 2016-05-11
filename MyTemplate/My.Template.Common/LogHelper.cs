using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;

namespace My.Template.Common
{
    public class LogHelper
    {
        public static ILog logWriter = LogManager.GetLogger("WebLogger");
    }
}
