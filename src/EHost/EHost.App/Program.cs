using EHost.App.Db;
using EHost.App.Service;
using EHost.Contract.Model;
using EHost.Infrastructure.Entity.Environment;
using EService.Service;
using log4net;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 注册 JwtSettings
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();
builder.Configuration.AddConfiguration(configuration);
//var loggerFactory = LoggerFactory.Create(builder =>
//{
//    builder.SetMinimumLevel(LogLevel.Information); // 
//    builder.AddDebug();
//    builder.AddConsole();
//});
//builder.Services.AddLogging(builder =>
//{
//    builder.AddDebug();
//    builder.AddConsole();
//    builder.SetMinimumLevel(LogLevel.Information); // 
//});


builder.Logging.AddLog4Net("log4net.config");

// 注册数据库上下文
builder.Services.AddSingleton<EServerDbContext>(provider =>
{
    //Replace with your desired port 
    var connectionString = builder.Configuration.GetValue<string>("ConnectionStrings:DefaultConnection");
    if (string.IsNullOrEmpty(connectionString))
    {
        var _logger = LogManager.GetLogger("Db");
        _logger.Error("Check Db connect");
        throw new InvalidOperationException("数据库连接字符串未配置或为空。");
    }
    return new EServerDbContext(connectionString);

});
// 注册服务
builder.Services.AddScoped<ILoginService, LoginService>();

//builder.Services.AddScoped<IDeviceService, DeviceService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();


app.MapControllers();
var environment = app.Services.GetService<IHostEnvironment>();

var db = app.Services.GetRequiredService<EServerDbContext>();
if (db.Database.EnsureCreated())
{
    // 数据库已创建
}
else
{
    // 数据库未创建
}

var environmentFactory = new EnvironmentFactory<MonitorData>(db, builder.Configuration);
var deviceFactory = new DeviceFactory<EnvironmentalSensor>(builder.Configuration);

try
{
    environmentFactory.Start();
    deviceFactory.Start();
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
    deviceFactory.Stop();
}

await app.RunAsync();

