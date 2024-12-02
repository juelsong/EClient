// See https://aka.ms/new-console-template for more information
using EMqttRelay.Service;
using log4net.Config;
using Microsoft.Extensions.Configuration;

// 初始化log4net配置
XmlConfigurator.Configure(new FileInfo("log4net.config"));

// 创建配置提供程序
IConfigurationBuilder builder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
IConfigurationRoot configuration = builder.Build();

Console.WriteLine("Start application!");

Console.CancelKeyPress += new ConsoleCancelEventHandler(CancelKeyPressed);
Console.WriteLine("Press Ctrl+C to exit...");

var server = new TcpMultiServer(configuration);

await server.StartAsync();

static void CancelKeyPressed(object? sender, ConsoleCancelEventArgs e)
{
    // 阻止默认的退出行为
    e.Cancel = true;

    // 执行清理工作
    Console.WriteLine("Exiting application...");

    // 执行其他必要的清理操作...

    // 退出程序
    System.Environment.Exit(0);
}
