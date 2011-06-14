using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace Nova.Common
{
    public class PasswordUtility
    {
        public string CalculateHash(string plainText)
        {
            byte[] plainBytes = ASCIIEncoding.Default.GetBytes(plainText);
            byte[] hashBytes = new MD5CryptoServiceProvider().ComputeHash(plainBytes);
            return BitConverter.ToString(hashBytes);
        }
    }
}
