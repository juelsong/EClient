// See https://aka.ms/new-console-template for more information
using EConsole.Model;
using EConsole.Service;
using Microsoft.Extensions.Configuration;
using System.Xml;

var builder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

IConfigurationRoot configuration = builder.Build();
var connectionString = configuration.GetConnectionString("DefaultConnection");
var testid = configuration.GetSection("ServerData:testid").Value;
var appSec = configuration.GetSection("ServerData:appSec").Value;

Console.WriteLine("Start application!");

Console.CancelKeyPress += new ConsoleCancelEventHandler(CancelKeyPressed);
Console.WriteLine("Press Ctrl+C to exit...");

var url = configuration.GetSection("ServerData:url").Value;
var interval = 1000; // 1000毫秒，即1秒

var client = new EClient(testid, appSec, configuration);
if (string.Empty == testid && string.Empty == appSec && string.Empty == url)
{
    Console.WriteLine("BaseData is empty ,Please check key && appId && url");
}
else
{

    while (true)
    {
        // 轮询 请求服务三次失败 ，退出程序
        var maxRetries = 3;
        var (neetInsert, environments) = client.GetEnvironmentData();
        if (!neetInsert)
        {
            continue; // 如果获取环境数据失败，则跳过后续操作
        }
        #region

        //else
        //{
        //    var noToken = true;
        //    var token = string.Empty;
        //    //推送失败，继续推送

        //    while (true)
        //    {
        //        token = await client.GetToken(url).ConfigureAwait(false);
        //        noToken = string.IsNullOrEmpty(token);
        //        if (!noToken)
        //        {
        //            break;
        //        }
        //        else
        //        {
        //            times++;
        //        }
        //        if (times >= 3)
        //        {
        //            break;
        //        }
        //        Thread.Sleep(interval);
        //    }
        //    var pushStatus = true;
        //    while (!noToken)
        //    {
        //        pushStatus = await client.PushData(url, environment).ConfigureAwait(false);
        //        if (pushStatus)
        //        {
        //            break;
        //        }
        //        else
        //        {
        //            times++;
        //        }
        //        if (times >= 3)
        //        {
        //            break;
        //        }
        //        Thread.Sleep(interval);
        //    }
        //}
        //if (times >= 3)
        //{
        //    break;
        //}
        #endregion
        if (client.RefreshToken(DateTime.Now.ToLocalTime()))
        {
            // 尝试获取token，最多重试3次
            bool tokenObtained = await TryActionWithRetriesAsync(async () =>
            {
                await client.GetToken(url).ConfigureAwait(false);
                return !string.IsNullOrEmpty(client.Token);
            }, maxRetries, interval);
            //if (!tokenObtained)
            //{
            //    break; // 如果获取token失败3次，则退出程序
            //}
        }
        var pushStatus = true;
        foreach (var item in environments)
        {
            item.token = client.Token;
            pushStatus = await TryActionWithRetriesAsync(async () =>
            {
                return await client.PushData(url, item).ConfigureAwait(false);
            }, maxRetries, interval);

            //if (!pushStatus)
            //{
            //    continue; // 如果推送数据失败3次，则退出程序
            //}
            // 尝试推送数据，最多重试3次
        }
        //if (!pushStatus)
        //{
        //    break; // 推送数据失败3次，则退出程序
        //}

        // 其他逻辑...

        // 休息一段时间后继续下一次轮询
        Thread.Sleep(interval * 30);
    }
}
Console.WriteLine("Exiting application...");

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
async Task<bool> TryActionWithRetriesAsync(Func<Task<bool>> action, int maxRetries, int interval)
{
    int retries = 0;
    while (retries < maxRetries)
    {
        if (await action().ConfigureAwait(false))
        {
            return true;
        }
        retries++;
        if (retries < maxRetries)
        {
            Thread.Sleep(interval);
        }
    }
    Console.WriteLine("The server has been requested three times. Please check the target server");

    return false;
}