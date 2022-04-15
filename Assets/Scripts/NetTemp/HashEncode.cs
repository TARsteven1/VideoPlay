using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace DotNet.Utilities
{
    /// <summary>
    /// 得到随机安全码（哈希加密）。
    /// </summary>
    public class HashEncode : MonoBehaviour
    {
        public InputField inputField;
        private static string Checkbit;
        //public HashEncode()
        //{
        //    //
        //    // TODO: 在此处添加构造函数逻辑
        //    //
        //    HashEncoding(GetSecurity())
        //   ;
        //}
        public void GenerateKey()
        {
            //DESEncrypt(GetRandomTimeValue(), "159413112", "00000000");
            // Debug.Log(Checkbit+DESEncrypt(GetRandomTimeValue(), "TAR", "0"));
            GetRandomTimeValue();
        }       
        public void Decode()
        {

            //DESDecrypt(inputField.text, "159413112", "00000000");
            // DESEncrypt(GetRandomTimeValue(), "159413112", "00000000");
            if (inputField.text != null || inputField.text != "")
            {
                string tempInput = inputField.text.Substring(1, inputField.text.Length - 1);
                Checkbit = inputField.text.Substring(0, 1);
                // Debug.Log(DESDecrypt(tempInput, "TAR", "0"));
                try
                { 
                if (CheckingBit(DESDecrypt(tempInput, "TAR", "0")) == Checkbit)
                {
                        PlayerPrefs.SetInt("ischeckCodeOk", 1);
                        Debug.Log("验证成功！");
                }
                else
                {
                        PlayerPrefs.SetInt("ischeckCodeOk", 0);
                        Debug.Log("验证失败！");

                }
            
             }
                catch (Exception e){
                    PlayerPrefs.SetInt("ischeckCodeOk", 0);
                    Debug.Log("验证失败！");
                    Debug.Log(e);
                }
            }
        }

        ///// <summary>
        ///// 得到随机哈希加密字符串
        ///// </summary>
        ///// <returns></returns>
        //public static string GetSecurity()
        //{
        //    string Security = HashEncoding(GetRandomValue());
            
          
        //    return Security;
        //}
        ///// <summary>
        ///// 得到一个随机数值
        ///// </summary>
        ///// <returns></returns>
        //public static string GetRandomValue()
        //{
        //    System.Random Seed = new System.Random();
        //    string RandomVaule = Seed.Next(1, 9999999).ToString();
        //    Debug.Log(RandomVaule + "--");
        //    return RandomVaule;
        //}
        public string CheckingBit(string v)
        {
            long tempValue = long.Parse(v);
            Debug.Log(tempValue + "--");
            //float tempValue2=  tempValue % 10;
            return (tempValue % 10).ToString();
      
        }
        /// <summary>
        /// 哈希加密一个字符串，sharejs.com
        /// </summary>
        /// <param name="Security"></param>
        /// <returns></returns>
        //public static string HashEncoding(string Security)
        //{
        //    byte[] Value;
        //    UnicodeEncoding Code = new UnicodeEncoding();
        //    byte[] Message = Code.GetBytes(Security);
        //    SHA512Managed Arithmetic = new SHA512Managed();
        //    Value = Arithmetic.ComputeHash(Message);
        //    Debug.Log(Value + "--");
        //    Security = "";
        //    foreach (byte o in Value)
        //    {
        //        Security += (int)o + "O";
        //    }
        //  Debug.Log(Security + "--");
        //    return Security;
        //}

        public static string GetRandomTimeValue()
        {
            string RandomVaule = DateTime.Now.ToString("yyMMddHHmmss");

            long tempValue = long.Parse(RandomVaule);
           
            //float tempValue2=  tempValue % 10;
            Checkbit = (tempValue % 10).ToString();
            //RandomVaule += Checkbit;
            Debug.Log(Checkbit + DESEncrypt(RandomVaule, "TAR", "0"));

            return RandomVaule;
        }
        /// <summary>
        /// 使用DES加密指定字符串
        /// </summary>
        /// <param name="encryptStr">待加密的字符串</param>
        /// <param name="key">密钥(最大长度8)</param>
        /// <param name="IV">初始化向量(最大长度8)</param>
        /// <returns>加密后的字符串</returns>
        public static string DESEncrypt(string encryptStr, string key, string IV)
        {
            //将key和IV处理成8个字符
            key += "12345678";
            IV += "12345678";
            key = key.Substring(0, 8);
            IV = IV.Substring(0, 8);
            SymmetricAlgorithm sa;
            ICryptoTransform ict;
            MemoryStream ms;
            CryptoStream cs;
            byte[] byt;
            sa = new DESCryptoServiceProvider();
            sa.Key = Encoding.UTF8.GetBytes(key);
            sa.IV = Encoding.UTF8.GetBytes(IV);
            ict = sa.CreateEncryptor();
            byt = Encoding.UTF8.GetBytes(encryptStr);
            ms = new MemoryStream();
            cs = new CryptoStream(ms, ict, CryptoStreamMode.Write);
            cs.Write(byt, 0, byt.Length);
            cs.FlushFinalBlock();
            cs.Close();
            //加上一些干扰字符
            string retVal = Convert.ToBase64String(ms.ToArray());
            System.Random ra = new System.Random();
            for (int i = 0; i < 8; i++)
            {
                int radNum = ra.Next(36);
                char radChr = Convert.ToChar(radNum + 65);//生成一个随机字符
                retVal = retVal.Substring(0, 2 * i + 1) + radChr.ToString() + retVal.Substring(2 * i + 1);
            }
            return retVal;
        }
        /// <summary>
        /// 使用DES解密指定字符串
        /// </summary>
        /// <param name="encryptedValue">待解密的字符串</param>
        /// <param name="key">密钥(最大长度8)</param>
        /// <param name="IV">初始化向量(最大长度8)</param>
        /// <returns>解密后的字符串</returns>
        public static string DESDecrypt(string encryptedValue, string key, string IV)
        {
            //去掉干扰字符
            string tmp = encryptedValue;
            if (tmp.Length < 16)
            {
                return "";
            }
            for (int i = 0; i < 8; i++)
            {
                tmp = tmp.Substring(0, i + 1) + tmp.Substring(i + 2);
            }
            encryptedValue = tmp;
            //将key和IV处理成8个字符
            key += "12345678";
            IV += "12345678";
            key = key.Substring(0, 8);
            IV = IV.Substring(0, 8);
            SymmetricAlgorithm sa;
            ICryptoTransform ict;
            MemoryStream ms;
            CryptoStream cs;
            byte[] byt;
            try
            {
                sa = new DESCryptoServiceProvider();
                sa.Key = Encoding.UTF8.GetBytes(key);
                sa.IV = Encoding.UTF8.GetBytes(IV);
                ict = sa.CreateDecryptor();
                byt = Convert.FromBase64String(encryptedValue);
                ms = new MemoryStream();
                cs = new CryptoStream(ms, ict, CryptoStreamMode.Write);
                cs.Write(byt, 0, byt.Length);
                cs.FlushFinalBlock();
                cs.Close();
                return Encoding.UTF8.GetString(ms.ToArray());
            }
            catch (System.Exception)
            {
                PlayerPrefs.SetInt("ischeckCodeOk", 0);
                Debug.Log("验证码无效！");
                return "";
            }
        }
    }

}