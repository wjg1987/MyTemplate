using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using Memcached.ClientLibrary;

namespace My.Template.Common
{
    public  static class MemcacheHelper
    {
        //客户端实例
        private static readonly MemcachedClient mc;
        /// <summary>
        /// 读取配置文件中的ip地址
        /// </summary>
        private static string memcachedipaddress = ConfigurationManager.AppSettings["memcachedipaddress"];
        static MemcacheHelper()
        {
            //分布Memcachedf服务IP 端口
            //string[] servers = { "192.168.0.250:11211", "192.168.0.100:11211" };
            string[] servers = memcachedipaddress.Trim(';').Split(';');
            //初始化池
            SockIOPool pool = SockIOPool.GetInstance();
            pool.SetServers(servers);
            pool.InitConnections = 3;
            pool.MinConnections = 3;
            pool.MaxConnections = 5;
            pool.SocketConnectTimeout = 1000;
            pool.SocketTimeout = 3000;
            pool.MaintenanceSleep = 30;
            pool.Failover = true;
            pool.Nagle = false;
            pool.Initialize();
          
            mc = new Memcached.ClientLibrary.MemcachedClient();
            mc.EnableCompression = false;
        }

        /// <summary>
        /// 写数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void Set(string key,object value)
        {
            mc.Set(key, value);
        }

        /// <summary>
        /// 写数据 有过期时间
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="time"></param>
        public static void Set(string key, object value,DateTime time)
        {
            mc.Set(key, value,time);
        }

        /// <summary>
        /// 读数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static object  Get(string key)
        {
            return mc.Get(key);
        }


        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool Delete(string key)
        {
            if (mc.KeyExists(key))
            {
               return mc.Delete(key);
            }
            else
            {
                return false;
            }
        }
    }
}
