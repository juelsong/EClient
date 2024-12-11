using EHost.App.Db;
using EHost.Contract.Entity;
using EHost.Infrastructure.Entity.Environment;
using EService.Service;
using log4net;
using log4net.Config;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Configuration;
using System.Reflection;
using System.Xml;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();
builder.Configuration.AddConfiguration(configuration);



//var loggerFactory = LoggerFactory.Create(builder =>
//{
//    builder.SetMinimumLevel(LogLevel.Information); // ������С��־����
//    builder.AddDebug();
//    builder.AddConsole();
//});
//builder.Services.AddLogging(builder =>
//{
//    builder.AddDebug();
//    builder.AddConsole();
//    builder.SetMinimumLevel(LogLevel.Information); // ������С��־����
//});


builder.Logging.AddLog4Net("log4net.config");

builder.Services.AddSingleton<EServerDbContext>(provider =>
{
    //Replace with your desired port 
    var connectionString = builder.Configuration.GetValue<string>("ConnectionStrings:DefaultConnection");
    if (string.IsNullOrEmpty(connectionString))
    {
        var _logger = LogManager.GetLogger("Db");
        _logger.Error("Check Db connect");
        throw new InvalidOperationException("���ݿ������ַ���δ���û�Ϊ�ա�");
    }
    return new EServerDbContext(connectionString);

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

var db = app.Services.GetRequiredService<EServerDbContext>();
if (db.Database.EnsureCreated())
{

}
else
{

}

int port = builder.Configuration.GetValue<int>("TcpServer:Port");
//���� TCP �������Ͷ��м������
var environmentFactory = new EnvironmentFactory<EnvironmentalSensor>(db, builder.Configuration);
try
{
    await environmentFactory.StartAsync();
}
catch (Exception ex)
{
    // �������������е��쳣
    Console.WriteLine(ex.Message);
    await environmentFactory.StartAsync();
}

await app.RunAsync();

