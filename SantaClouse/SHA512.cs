using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace SantaClouse.DACANCELLARE
{
    public class SHA512
    {
        public static String HashPassword(string p, string s)
        {
            var combinedPassword = String.Concat(p, s);
            var sha512 = new SHA512Managed();
            var bytes = UTF8Encoding.UTF8.GetBytes(combinedPassword);
            var hash = sha512.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        public static String GetRandomSalt(Int32 size)
        {
            var random = new RNGCryptoServiceProvider();
            var salt = new Byte[size];
            random.GetBytes(salt);
            return Convert.ToBase64String(salt);
        }
    }
}