using EHost.App.Db;
using EHost.App.Helper;
using EHost.App.Models;
using EHost.Contract.Model;
using EHost.Security.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Npgsql;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EHost.App.Service
{
    /// <summary>
    /// 登录服务接口
    /// </summary>
    public interface ILoginService
    {
        /// <summary>
        /// 登录结果 返回token
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        LoginResponse Authenticate(string username, string password);
        /// <summary>
        /// 根据用户名和密码验证的方法
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>        
        bool ValidateCredentials(string username, string password);
        /// <summary>
        /// 验证令牌的方法
        /// </summary>
        /// <param name="token"></param>
        /// <param name="claimsPrincipal"></param>
        /// <returns></returns>        
        bool ValidateToken(string token, out ClaimsPrincipal claimsPrincipal);

    }
    /// <summary>
    /// 登录服务实现
    /// </summary>
    public class LoginService : ILoginService
    {
        private readonly ILogger<LoginService> log;
        private readonly IOptions<JwtSettings> jwtSettings;
        private readonly EServerDbContext dbContext; // 假设使用 Entity Framework Core

        public LoginService(
            ILogger<LoginService> log,
            IOptions<JwtSettings> jwtSettings,
            EServerDbContext dbContext)
        {
            this.log = log;
            this.jwtSettings = jwtSettings;
            this.dbContext = dbContext;
        }

        public LoginResponse Authenticate(string username, string password)
        {
            try
            {
                var user = dbContext.User
                    .AsNoTracking()
                    .FirstOrDefault(u => u.Account == username);

                if (user == null)
                {
                    return new LoginResponse
                    {
                        Status = "error",
                        Message = "用户不存在",
                        Data = new LoginData { Token = string.Empty },
                        Code = ErrorCode.UsernameNotFound.ToString()
                    };
                }

                // 验证密码
                var hashedPassword = PasswordHasher.HashPassword(password, user.Salt);
                if (user.Password != hashedPassword)
                {
                    return new LoginResponse
                    {
                        Status = "error",
                        Message = "密码错误",
                        Data = new LoginData { Token = string.Empty },
                        Code = ErrorCode.IncorrectPassword.ToString()
                    };
                }

                // 生成访问令牌
                var accessToken = GenerateToken(user);

                return new LoginResponse
                {
                    Status = "success",
                    Message = "登录成功",
                    Data = new LoginData { Token = accessToken },
                    Code = ErrorCode.None.ToString()
                };

            }
            catch (PostgresException ex)
            {

                throw;
            }
            // 从数据库中查找用户
        }
        private string GenerateToken(User user)
        {
            try
            {
                var key = Encoding.UTF8.GetBytes(jwtSettings.Value.SecretKey);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                    new Claim(ConstDefs.Jwt.UserId, user.Id.ToString()),
                    new Claim(ConstDefs.Jwt.Account, user.Account)
                }),
                    //没有设置时效时长
                    Expires = DateTime.UtcNow.AddMinutes(jwtSettings.Value.Expires),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);

            }
            catch (Exception ex)
            {

                throw;
            }

        }

        // 验证令牌的方法，返回令牌是否有效，并输出 ClaimsPrincipal
        public bool ValidateToken(string token, out ClaimsPrincipal claimsPrincipal)
        {
            claimsPrincipal = null;

            if (string.IsNullOrEmpty(token))
            {
                return false;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(jwtSettings.Value.SecretKey);

            try
            {
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };

                claimsPrincipal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);

                if (validatedToken is JwtSecurityToken jwtToken)
                {
                    if (jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                    {
                        // 从令牌中提取用户ID
                        var userIdClaim = claimsPrincipal.FindFirst(ConstDefs.Jwt.UserId);
                        if (userIdClaim != null)
                        {
                            var userId = int.Parse(userIdClaim.Value);

                            // 检查数据库中是否存在该用户
                            var userExists = dbContext.User.Any(u => u.Id == userId);
                            if (userExists)
                            {
                                return true; // 令牌有效且用户存在
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Token validation failed: {ex.Message}");
            }

            return false; // 令牌无效或用户不存在
        }

        // 实现根据用户名和密码验证的方法
        public bool ValidateCredentials(string username, string password)
        {
            // 从数据库中查找用户
            var user = dbContext.User
                .AsNoTracking()
                .FirstOrDefault(u => u.Account == username);

            if (user == null)
            {
                return false; // 用户不存在
            }

            // 验证密码
            var hashedPassword = PasswordHasher.HashPassword(password, user.Salt);
            return user.Password == hashedPassword; // 返回密码验证结果
        }
    }
}
