using EHost.Contract.Model;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace EWebServer.Helper
{
    public class JwtHelper
    {
        public JwtSettings JwtSettings { get; set; }
        public JwtHelper()
        {

        }

        /// <summary>
        /// 添加Jwt服务
        /// </summary>
        public void AddJwtService()
        {

        }

        /// <summary>
        /// 最简单的JwtToken
        /// </summary>
        /// <returns></returns>
        public string GetJwtToken()
        {
            var claims = new List<Claim>();
            var jwtSecurityToken = new JwtSecurityToken(
                    JwtSettings.Issuer,
                    JwtSettings.Audience,
                    claims,
                    JwtSettings.NotBefore,
                    JwtSettings.Expiration,
                    JwtSettings.SigningCredentials
                );
            var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            return token;
        }


    }
}
