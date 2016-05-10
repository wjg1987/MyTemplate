using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using System.Security.Cryptography;
using System.Configuration;

namespace My.Template.Common
{
    public class Encryption
    {
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="PWDValue">待加密字符串</param>
        /// <param name="bIsHalf">是否16位</param>
        /// <returns></returns>
        public static string MD5(string PWDValue, bool bIsHalf)
        {
            string sOutPut = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(PWDValue, "MD5").ToLower();
            if (bIsHalf)//16位MD5加密（取32位加密的8~24字符）
                sOutPut = sOutPut.Substring(8, 16);
            return sOutPut;
        }

        /// <summary>  
        /// AES加密  
        /// </summary>  
        /// <param name="data">要加密的字符串</param>  
        /// <param name="sKey">密钥串（16位或32位）</param>  
        /// <returns>加密后的字符串</returns>  
        public static string EncryptAES(string data)
        {
            try
            {
                RijndaelManaged aes = new RijndaelManaged();
                string sKey = ConfigurationSettings.AppSettings["AESKey"];
                byte[] bData = UTF8Encoding.UTF8.GetBytes(data);
                aes.Key = UTF8Encoding.UTF8.GetBytes(sKey);
                aes.IV = UTF8Encoding.UTF8.GetBytes(sKey);
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                ICryptoTransform iCryptoTransform = aes.CreateEncryptor();
                byte[] bResult = iCryptoTransform.TransformFinalBlock(bData, 0, bData.Length);

                // return Convert.ToBase64String(bResult); //返回base64加密;  
                return ByteToHex(bResult);                //返回十六进制数据;  
            }
            catch
            {
                throw;
            }
        }

        /// <summary>  
        /// AES解密  
        /// </summary>  
        /// <param name="data">要解密的字符串</param>  
        /// <param name="sKey">密钥串（16位或32位字符串）</param>  
        /// <returns>解密后的字符串</returns>  
        public static string DecryptAES(string data)
        {
            try
            {
                RijndaelManaged aes = new RijndaelManaged();
                string sKey = ConfigurationSettings.AppSettings["AESKey"];
                //byte[] bData = Convert.FromBase64String(data); //解密base64;  
                byte[] bData = HexToByte(data);                  //16进制to byte[];  
                aes.Key = UTF8Encoding.UTF8.GetBytes(sKey);
                aes.IV = UTF8Encoding.UTF8.GetBytes(sKey);
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                ICryptoTransform iCryptoTransform = aes.CreateDecryptor();
                byte[] bResult = iCryptoTransform.TransformFinalBlock(bData, 0, bData.Length);
                return Encoding.UTF8.GetString(bResult);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>  
        /// DES加密  
        /// </summary>  
        /// <param name="data">要加密的字符串</param>  
        /// <param name="key">密钥串（8位字符串）</param>  
        /// <returns>加密后的字符串</returns>  
        public static string EncryptDES(string data)
        {
            DES des = new DESCryptoServiceProvider();
            string key = ConfigurationSettings.AppSettings["DESKey"];
            des.Mode = CipherMode.ECB;
            des.Key = Encoding.UTF8.GetBytes(key);
            des.IV = Encoding.UTF8.GetBytes(key);

            byte[] bytes = Encoding.UTF8.GetBytes(data);
            byte[] resultBytes = des.CreateEncryptor().TransformFinalBlock(bytes, 0, bytes.Length);

            return Convert.ToBase64String(resultBytes); //密文以base64返回;(可根据实际需要返回16进制数据;)  
            //return ByteToHex(resultBytes);//十六位  
        }

        /// <summary>  
        /// DES解密  
        /// </summary>  
        /// <param name="data">要解密的字符串</param>  
        /// <param name="key">密钥串（8位字符串）</param>  
        /// <returns>解密后的字符串</returns>  
        public static string DecryptDES(string data)
        {
            DES des = new DESCryptoServiceProvider();
            string key = ConfigurationSettings.AppSettings["DESKey"];
            des.Mode = CipherMode.ECB;
            des.Key = Encoding.UTF8.GetBytes(key);
            des.IV = Encoding.UTF8.GetBytes(key);

            //传入的是Base64数据
            byte[] bytes = Convert.FromBase64String(data);
            //如果传入的是16进制的数据，则注释上行代码，并取消下行代码注释
            //byte[] bytes = HexToByte(data);  
            byte[] resultBytes = des.CreateDecryptor().TransformFinalBlock(bytes, 0, bytes.Length);

            return Encoding.UTF8.GetString(resultBytes);
        }

        // method to convert hex string into a byte array    
        private static byte[] HexToByte(string data)
        {
            data = data.Replace(" ", "");

            byte[] comBuffer = new byte[data.Length / 2];

            for (int i = 0; i < data.Length; i += 2)
                comBuffer[i / 2] = (byte)Convert.ToByte(data.Substring(i, 2), 16);

            return comBuffer;
        }

        // method to convert a byte array into a hex string    
        private static string ByteToHex(byte[] comByte)
        {
            StringBuilder builder = new StringBuilder(comByte.Length * 3);

            foreach (byte data in comByte)
                builder.Append(Convert.ToString(data, 16).PadLeft(2, '0').PadRight(3, ' '));

            return builder.ToString().ToUpper().Replace(" ", "");
        }  

    }
}
