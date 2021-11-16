using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Server
{
    public class Utility
    {

        public static string Encrypt(string password)
        {
            var provider = MD5.Create();
            
            string salt = "R0M3!@13trhd";
            byte[] bytes = provider.ComputeHash(Encoding.UTF32.GetBytes(salt + password));
            return BitConverter.ToString(bytes).Replace("-", "").ToLower();

        }

    }
}
