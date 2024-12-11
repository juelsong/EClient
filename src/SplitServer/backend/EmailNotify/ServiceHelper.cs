namespace EmailNotify
{
    using log4net;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ConnectConfig
    {
        public string Url { get; set; } = string.Empty;
        public double Intervel { get; set; }
        public string Tenant { get; set; } = string.Empty;
        public TimeSpan Timeout { get; set; }
    }
    public class ServiceHelper
    {
        private static readonly HttpClient client = new HttpClient();
        private static ILog log = LogManager.GetLogger(nameof(ServiceHelper));
        //System.Timers.Timer timer;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectConfig"></param>
        public ServiceHelper(ConnectConfig connectConfig)
        {
            this.ConnectConfig = connectConfig;
            client.DefaultRequestHeaders.Add("X-TENANT", this.ConnectConfig.Tenant);
            client.Timeout = this.ConnectConfig.Timeout;
        }

        public async Task<string> GetStatus()
        {
            log.Info($"send url is {this.ConnectConfig.Url}");
            log.Info($"X-TENANT is {this.ConnectConfig.Tenant}");

            try
            {
                var result = await client.GetStringAsync(this.ConnectConfig.Url);
                log.Info(result);
                return result;
            }
            catch (Exception ee)
            {
                log.Error(ee);
                return "Error";
            }
             
            //发送Get请求
        }




        public ConnectConfig ConnectConfig { get; private set; }
    }
}
