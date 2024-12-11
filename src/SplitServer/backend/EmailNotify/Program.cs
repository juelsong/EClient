// See https://aka.ms/new-console-template for more information
using ESys.Utilty.Defs;
using EmailNotify;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using System.Xml;

var logCfg = new FileInfo(fileName: AppDomain.CurrentDomain.BaseDirectory + "log4net.config");
log4net.Config.XmlConfigurator.ConfigureAndWatch(logCfg);
//writeXml(false);

IConfigurationBuilder builder = new ConfigurationBuilder().AddJsonFile("EmailNotify.json", false, true);
var connectConfig = builder.Build().GetSection("AppConnect").Get<ConnectConfig>();
var connect = new ServiceHelper(connectConfig);
var emailConfig = builder.Build().GetSection("Email").Get<EMailConfig>();
var email = new Email(emailConfig, connectConfig.Tenant);
System.Timers.Timer timer = new System.Timers.Timer();
timer = new System.Timers.Timer();
timer.Elapsed += Timer_Elapsed;
timer.Interval = connect.ConnectConfig.Intervel;
timer.Enabled = true;
timer.AutoReset = true;
var state = true;
while (state)
{
    
}

async void Timer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
{
    //getXmlState();
    var result = await connect.GetStatus();

    if ("Error" != result)
    {
        var resultDeserialize = JsonSerializer.Deserialize<Result>(result);
        if (resultDeserialize.Code != 0)
        {
            email.SendEMails("请尽快检查数据库服务");
            state = false;
        }
        else
        {

        }
    }
    else
    {
        email.SendEMails("后台服务已宕机，请尽快恢复");
        state = false;
    }
}

//bool getXmlState()
//{
//    var xml = new XmlDocument();
//    xml.Load("EmailState.xml");
//    var state = bool.Parse(xml.SelectSingleNode("/root/Email").InnerText) ;
//    return state;
//}
//void writeXml(bool state)
//{
//    var xml = new XmlDocument();
//    xml.Load("EmailState.xml");
//    xml.SelectSingleNode("/root/Email").InnerText = state.ToString();
//    xml.Save("EmailState.xml");
//}

