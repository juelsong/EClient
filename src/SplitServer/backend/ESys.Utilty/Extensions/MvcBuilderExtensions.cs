/*
 *        ┏┓   ┏┓+ +
 *       ┏┛┻━━━┛┻┓ + +
 *       ┃       ┃
 *       ┃   ━   ┃ +++ + +
 *       ████━████ ┃+
 *       ┃       ┃ +
 *       ┃   ┻   ┃
 *       ┃       ┃ + +
 *       ┗━┓   ┏━┛
 *         ┃   ┃
 *         ┃   ┃ + + + +
 *         ┃   ┃    Code is far away from bug with the animal protecting
 *         ┃   ┃ +     神兽保佑, 代码无bug
 *         ┃   ┃
 *         ┃   ┃  +
 *         ┃    ┗━━━┓ + +
 *         ┃        ┣┓
 *         ┃        ┏┛
 *         ┗┓┓┏━┳┓┏┛ + + + +
 *          ┃┫┫ ┃┫┫
 *          ┗┻┛ ┗┻┛+ + + +
 */

namespace ESys.Utilty.Extensions
{
    using ESys.Utilty.Attributes;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Formatters;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using System;
    using System.Linq;
    using System.Text.Json;

    /// <summary>
    /// IMvcBuilder 扩展方法
    /// </summary>
    public static class MvcBuilderExtensions
    {
        private class SpecificSystemTextJsonInputFormatter : SystemTextJsonInputFormatter
        {
            public SpecificSystemTextJsonInputFormatter(string settingsName, JsonOptions options, ILogger<SpecificSystemTextJsonInputFormatter> logger)
                : base(options, logger)
            {
                this.SettingsName = settingsName;
            }

            public string SettingsName { get; }

            public override bool CanRead(InputFormatterContext context)
            {
                var jsonSettingsName = context.HttpContext.GetAllMetadata<JsonSettingsNameAttribute>().FirstOrDefault()?.Name;
                return this.SettingsName == jsonSettingsName && base.CanRead(context);
            }
        }

        private class SpecificSystemTextJsonOutputFormatter : SystemTextJsonOutputFormatter
        {
            public SpecificSystemTextJsonOutputFormatter(string settingsName, JsonSerializerOptions jsonSerializerOptions) : base(jsonSerializerOptions)
            {
                this.SettingsName = settingsName;
            }

            public string SettingsName { get; }

            public override bool CanWriteResult(OutputFormatterCanWriteContext context)
            {
                var jsonSettingsName = context.HttpContext.GetAllMetadata<JsonSettingsNameAttribute>().FirstOrDefault()?.Name;
                return this.SettingsName == jsonSettingsName && base.CanWriteResult(context);
            }
        }
        private class ConfigureMvcJsonOptions : IConfigureOptions<MvcOptions>
        {
            private readonly string jsonSettingsName;
            private readonly IOptionsMonitor<JsonOptions> jsonOptions;
            private readonly ILoggerFactory loggerFactory;

            public ConfigureMvcJsonOptions(
                string jsonSettingsName,
                IOptionsMonitor<JsonOptions> jsonOptions,
                ILoggerFactory loggerFactory)
            {
                this.jsonSettingsName = jsonSettingsName;
                this.jsonOptions = jsonOptions;
                this.loggerFactory = loggerFactory;
            }

            public void Configure(MvcOptions options)
            {
                var jsonOptions = this.jsonOptions.Get(this.jsonSettingsName);
                var logger = this.loggerFactory.CreateLogger<SpecificSystemTextJsonInputFormatter>();
                options.InputFormatters.Insert(
                    0,
                    new SpecificSystemTextJsonInputFormatter(
                        this.jsonSettingsName,
                        jsonOptions,
                        logger));
                options.OutputFormatters.Insert(
                    0,
                    new SpecificSystemTextJsonOutputFormatter(
                        this.jsonSettingsName,
                        jsonOptions.JsonSerializerOptions));
            }
        }
        /// <summary>
        /// 添加Json配置
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="settingsName"></param>
        /// <param name="configure"></param>
        /// <returns></returns>
        public static IMvcBuilder AddJsonOptions(
            this IMvcBuilder builder,
            string settingsName,
            Action<JsonOptions> configure)
        {
            ArgumentNullException.ThrowIfNull(builder);
            ArgumentNullException.ThrowIfNull(configure);
            builder.Services.Configure(settingsName, configure);
            builder.Services.AddSingleton<IConfigureOptions<MvcOptions>>(sp =>
            {
                var options = sp.GetRequiredService<IOptionsMonitor<JsonOptions>>();
                var loggerFactory = sp.GetRequiredService<ILoggerFactory>();
                return new ConfigureMvcJsonOptions(settingsName, options, loggerFactory);
            });
            return builder;
        }

    }
}
