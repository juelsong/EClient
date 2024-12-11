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
namespace ESys.UnitTest
{
    using ESys.Utilty.Defs;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.TestHost;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.IO;
    using System.Linq;
    using System.Net.Http;
    using System.Text;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    public class UnitTestContext
    {
        private IHost host;

        private UnitTestContext() { }
        public static UnitTestContext Instance { get; } = new UnitTestContext();
        public JsonSerializerOptions DefaultJsonSerializerOptions { get; } = new JsonSerializerOptions
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            PropertyNameCaseInsensitive = false
        };

        public void Initialize<TStartup>(string configFile, Action<IServiceCollection> configServices = null) where TStartup : class
        {
            var builder = Host.CreateDefaultBuilder()
                .ConfigureAppConfiguration((builderContext, builder) =>
                {
                    builderContext.HostingEnvironment.EnvironmentName = "Development";
                    Environment.SetEnvironmentVariable("NETCORE_ENVIRONMENT", "Development");
                    Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Development");
                    var configPath = Path.Combine(AppContext.BaseDirectory, configFile);
                    if (File.Exists(configPath))
                    {
                        builder.AddJsonFile(configPath);
                    }
                });
            HostBuilderExtensions.Inject(builder, (b, config) =>
            {
                config.AutoRegisterBackgroundService = false;
            });
            this.host = builder.ConfigureServices((ctx, service) =>
            {
                if (configServices != null)
                {
                    configServices(service);
                }
            })
              .ConfigureWebHost(builder =>
              {
                  builder
                  .UseTestServer()
                  .Inject()
                  .UseStartup<TStartup>()
                  ;
              })
              .Build();
            this.host.Start();
        }

        public HttpClient GetClient()
        {
            var client = this.host.GetTestClient();
            client.DefaultRequestHeaders.Add("X-TENANT", "Emis");
            return client;
        }

        public IServiceProvider ServiceProvider { get { return this.host.Services; } }

        public HttpClient GetLoginedClient(string account, string pass)
        {
            var client = this.GetClient();
            using var content = new StringContent(
                @$"{{""Account"":""{account}"",""Password"":""{pass}"",
""Captcha"": ""admin"",""CheckKey"": ""admin""}}",
                Encoding.UTF8,
                "application/json");
            var loginRsp = client.PostAsync("/api/user/login", content).Result;

            var rspContent = loginRsp.Content.ReadAsStringAsync().Result;
            var loginResult = JsonSerializer.Deserialize<Result>(rspContent);
            Assert.IsTrue(loginResult.Success, rspContent);
            loginRsp.Headers.GetValues("access-token").First();
            client.DefaultRequestHeaders.Add(
                "Authorization",
                $"Bearer {loginRsp.Headers.GetValues("access-token").First()}");
            client.DefaultRequestHeaders.Add(
                "X-Authorization",
                $"Bearer {loginRsp.Headers.GetValues("x-access-token").First()}");
            return client;
        }

        public HttpClient GetAdminClient()
        {
            return this.GetLoginedClient("Admin", "ESys_Admin");
        }

        public void Cleanup()
        {
            this.host.StopAsync().Wait();
            this.host.Dispose();
        }
    }
}
