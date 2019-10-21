using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace EBBProc.Helper
{
    public class Utility
    {
        /// <summary>
        /// Generates Random Strings
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GenerateRandomString(string str)
        {
            StringBuilder builder = new StringBuilder();
            MD5 md5 = new MD5CryptoServiceProvider();
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(str));
            byte[] hash = md5.Hash;
            for(int i =  0; i < hash.Length; i++)
            {
                builder.Append(hash[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }
}