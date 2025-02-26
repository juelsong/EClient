using EHost.App.Db;
using EHost.App.Helper;
using EHost.App.Service;
using EHost.Contract.Model;
using EHost.Infrastructure.Entity.Environment;
using EHost.TcpServer.ParserHelper;
using EService.Service;
using log4net.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

namespace UniteTest
{
    [TestClass]
    public sealed class TestLoginAndJWT
    {
        private readonly LoginService loginService;
        private readonly EServerDbContext db;

        
        public TestLoginAndJWT()
        {
            // 初始化log4net配置
            XmlConfigurator.Configure(new FileInfo("log4net.config"));

            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.UnitTest.json", optional: true, reloadOnChange: true);
            IConfigurationRoot configuration = builder.Build();

            var dbString = configuration.GetSection("ConnectionStrings").GetValue<string>("connectionstr")
            ?? throw new ArgumentNullException("未找到数据库连接字符串");

            db = new EServerDbContext(dbString);
            using var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Information); // 设置最小日志级别
                builder.AddDebug();
                builder.AddConsole();
            });
            // 使用LoggerFactory创建ILogger实例
            var _logger = loggerFactory.CreateLogger<LoginService>();


            var jwt = configuration.GetSection("JwtSettings").Get<JwtSettings>();

            _logger.LogInformation("Example log message");

            loginService = new LoginService(_logger, Options.Create(jwt), db);
        }
        [TestInitialize]
        public void TestInit()
        {

        }
        [TestCleanup]
        public void TestClean()
        {
        }
        [TestMethod]
        public void GetPsw()
        {
            var psw = "password";
            var username = "";
            var salt = "";
            var outPsw = PasswordHasher.HashPassword(psw,out salt);
            Console.WriteLine(salt,outPsw);
            var s = JwtSettings.GenerateSecureSecretKey();
        }
        [TestMethod]
        public void GetToken()
        {          
            var response = loginService.Authenticate("super", "sr160608");
            Assert.IsNotNull(response);
            ClaimsPrincipal c;
            var state = loginService.ValidateToken(response.Data.Token, out c);
            Assert.IsTrue(state);
        }
    }
}
