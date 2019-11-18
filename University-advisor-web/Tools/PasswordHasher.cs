using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using University_advisor_web.Interfaces;

namespace University_advisor_web.Tools
{
    public class PasswordHasher : IPasswordHasher
    {
        public string CreateMD5(string input)
        {
            var md5 = MD5.Create();
            var inputBytes = Encoding.ASCII.GetBytes(input);
            var outputBytes = md5.ComputeHash(inputBytes);
            md5.Dispose();

            var sb = new StringBuilder();
            for (var i = 0; i < outputBytes.Length; i++)
            {
                sb.Append(outputBytes[i].ToString("X2"));
            }
            return sb.ToString();
        }
    }
}
