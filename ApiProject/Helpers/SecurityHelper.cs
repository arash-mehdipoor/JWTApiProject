using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ApiProject.Helpers
{
    public static class SecurityHelper
    {
        private static RandomNumberGenerator _randomNumber = RandomNumberGenerator.Create();

        public static string GetSHa256Hash(string value)
        {
            var algoritm = new SHA256CryptoServiceProvider();
            var byteValue = Encoding.UTF8.GetBytes(value);
            var byteHash = algoritm.ComputeHash(byteValue);

            return Convert.ToBase64String(byteHash);
        }
    }
}
