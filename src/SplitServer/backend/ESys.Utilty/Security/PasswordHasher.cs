/*
 *        ┏┓   ┏┓+ +
 *       ┏┛┻━━━┛┻┓ + +
 *       ┃       ┃
 *       ┃   ━   ┃ ++ + + +
 *       ████━████ ┃+
 *       ┃       ┃ +
 *       ┃   ┻   ┃
 *       ┃       ┃ + +
 *       ┗━┓   ┏━┛
 *         ┃   ┃
 *         ┃   ┃ + + + +
 *         ┃   ┃    Code is far away from bug with the animal protecting       
 *         ┃   ┃ +     神兽保佑,代码无bug  
 *         ┃   ┃
 *         ┃   ┃  +
 *         ┃    ┗━━━┓ + +
 *         ┃        ┣┓
 *         ┃        ┏┛
 *         ┗┓┓┏━┳┓┏┛ + + + +
 *          ┃┫┫ ┃┫┫
 *          ┗┻┛ ┗┻┛+ + + +
 */

using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Security.Cryptography;
using System.Text;

namespace ESys.Utilty.Security
{
#pragma warning disable 1591
    public static class PasswordHasher
    {
        private static string GenerateSalt()
        {
            byte[] randomBytes = new byte[128 / 8];
            using (var generator = RandomNumberGenerator.Create())
            {
                generator.GetBytes(randomBytes);
                return Convert.ToBase64String(randomBytes);
            }
        }

        public static string HashPassword(string value, out string salt)
        {
            salt = GenerateSalt();
            return HashPassword(value, salt);
        }

        public static string HashPassword(string value, string salt)
        {
            if (string.IsNullOrWhiteSpace(salt))
            {
                throw new ArgumentNullException(nameof(salt));
            }
            var valueBytes = KeyDerivation.Pbkdf2(
                password: value,//密码
                salt: Encoding.UTF8.GetBytes(salt),//盐
                prf: KeyDerivationPrf.HMACSHA512,//伪随机函数，这里是SHA-512
                iterationCount: 10000,//迭代次数
                numBytesRequested: 256 / 8);//最后输出的秘钥长度

            var hash = Convert.ToBase64String(valueBytes);
            return hash;
        }
    }
#pragma warning restore 1591
}
