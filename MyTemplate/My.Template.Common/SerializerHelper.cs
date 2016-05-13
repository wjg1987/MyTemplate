using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace My.Template.Common
{
    public static class SerializerHelper
    {
        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string SerializeToString(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="str"></param>
        /// <returns></returns>
        public static T DeserializeToObject<T>(string str)
        {
            return JsonConvert.DeserializeObject<T>(str);
        }
    }
}
