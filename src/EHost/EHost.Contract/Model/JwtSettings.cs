using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Text;

namespace EHost.Contract.Model
{

    public class JwtSettings
    {
        /// <summary>
        /// 过期时间（分钟）
        /// </summary>
        public int Expires { get; set; } // 统一使用 Expires

        /// <summary>
        /// 密钥
        /// </summary>
        public string SecretKey { get; set; }

        /// <summary>
        /// 发布者
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// 接受者
        /// </summary>
        public string Audience { get; set; }

        /// <summary>
        /// 生效时间
        /// </summary>
        public DateTime NotBefore => DateTime.Now;

        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime Expiration => DateTime.Now.AddMinutes(Expires);

        /// <summary>
        /// 密钥Bytes
        /// </summary>
        private SecurityKey SigningKey => new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));

        /// <summary>
        /// 加密后的密钥，使用HmacSha256加密
        /// </summary>
        public SigningCredentials SigningCredentials =>
            new SigningCredentials(SigningKey, SecurityAlgorithms.HmacSha256);

        /// <summary>
        /// 用于生成JWT
        /// </summary>
        /// <param name="keySizeInBytes"></param>
        /// <returns></returns>
        public static string GenerateSecureSecretKey(int keySizeInBytes = 32)
        {
            using var rng = RandomNumberGenerator.Create();

            var key = new byte[keySizeInBytes];
            rng.GetBytes(key);
            return Convert.ToBase64String(key);
        }
    }
}
