// See https://aka.ms/new-console-template for more information
using EMqttRelay.Service;
using log4net.Config;
using Microsoft.Extensions.Configuration;

Console.WriteLine("Hello, World!");

// 初始化log4net配置
XmlConfigurator.Configure(new FileInfo("log4net.config"));

// 创建配置提供程序
IConfigurationBuilder builder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
IConfigurationRoot configuration = builder.Build();

var myClass = new TcpMultiServer(configuration);


Console.ReadKey();
