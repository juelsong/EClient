using EConsole.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Environment = EConsole.Model.Environment;

namespace EConsole.Service
{
    public class EClient
    {
        private readonly IConfiguration _configuration;

        // 加密算法偏移量
        private static int Offset = 16;
        //Token
        public string Token { get; private set; } = string.Empty;

        private DateTime GetTokenTime { get; set; }
        private string appId { get; set; }
        private string key { get; set; }
        private int dbUpdateId { get; set; } = 0;
        public EClient(string appId, string key, IConfiguration configuration)
        {
            this.appId = appId;
            this.key = key;
            this._configuration = configuration;
        }
        public (bool,string) Post(string url, string content)
        {
            try
            {
                using HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.ExpectContinue = false; // httpclient默认开启expect，而有些服务器不会正确应答，容易导致请求无法提交
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")); // 设置响应数据的ContentType
                var param = new StringContent(content);
                param.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"); // 设置请求数据的ContentType
                return (true,client.PostAsync(url, param).Result.Content.ReadAsStringAsync().Result);
            }
            catch (Exception ex)
            {
                this.GetLog(ex.Message);
                return (false, string.Empty);
            }
        }
        public async Task<string> PostAsync(string url, string content)
        {
            try
            {
                //var client = _httpClientFactory.CreateClient();
                using HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.ExpectContinue = false;
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var stringContent = new StringContent(content, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(url, stringContent).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                }
                else
                {
                    // Handle non-success status codes here
                    this.GetLog($"Request failed with status code: {response.StatusCode}");
                    return string.Empty;
                }
            }
            catch (HttpRequestException httpEx)
            {
                // Handle HTTP request exceptions here
                this.GetLog($"HTTP request exception: {httpEx.Message}");
                return string.Empty;
            }
            catch (TaskCanceledException tcEx)
            {
                // Handle timeout or task cancellation here
                this.GetLog($"Task canceled exception: {tcEx.Message}");
                return string.Empty;
            }
            catch (Exception ex)
            {
                // Handle other types of exceptions here
                this.GetLog($"An error occurred: {ex.Message}");
                return string.Empty;
            }
        }
        private void GetLog(string s)
        {
            Console.WriteLine($"{DateTime.Now.ToLocalTime()}--------{s}");
        }
        public async Task GetToken(string url)
        {
            this.GetLog($"GetToken");
            var content = JsonSerializer.Serialize(new User()
            {
                appId = this.appId,
                signature = this.GetSignature(),
            });
            try
            {
                string response = await this.PostAsync(url + "/auth/getToken", content);
                if (string.IsNullOrEmpty(response))
                {
                    return;
                }
                var rspData = JsonSerializer.Deserialize<Response>(response);
                this.GetLog("Response from server: " + response);
                if (!string.IsNullOrEmpty(response) && rspData != null && rspData.code == 200)
                {
                    // 处理成功的响应
                    this.Token = rspData.result.ToString()??"";
                    this.GetTokenTime = DateTime.Now.ToLocalTime();
                }
                else
                {
                    // 处理空响应

                }
            }
            catch (Exception ex)
            {
                // 处理可能抛出的异常
                this.GetLog("An error occurred: " + ex.Message);
            }
        }

        public async Task<bool> PushData(string url, Environment environment)
        {
            var content = JsonSerializer.Serialize(environment);
            this.GetLog("Send to server: " + content);

            var response = await this.PostAsync(url + "/environment/saveEnvData", content);
            if (string.IsNullOrEmpty(response))
            {
                return false;
            }
            var rspData = JsonSerializer.Deserialize<Response>(response);
            this.GetLog("Response from server: " + response);
            if (!string.IsNullOrEmpty(response) && rspData != null /*&& rspData.code == 200*/)
            {
                // 处理成功的响应
                //this.GetLog("Response from server: " + response);
                return true;
            }
            else
            {
                // 处理空响应
                //this.GetLog("No response received or response was empty.");
                return false;
            }
        }
        public (bool, List<Environment>) GetEnvironmentData()
        {
            var status = true;
            var environments = new List<Environment>();
            using var context = new EDbContext(_configuration);
            var db = context.monitor_min202005;
            //var count = await context.monitor_min202005.CountAsync();
            var currentDate = DateTimeOffset.Now.ToLocalTime();

            var deviceIds = this._configuration.GetSection("ServerData:deviceIds")
                .GetChildren()
                .Select(i => int.Parse(i.Value));
#if DEBUG         
            var datas = db.Where(i => deviceIds.Contains(i.device_of_data) && i.source_of_data == 4)
                .OrderByDescending(i => i.id)
                .Take(200)
                .AsEnumerable();
#else
            var datas = db.Where(i => i.device_of_data == 64030 && i.source_of_data == 4)
                .OrderByDescending(i => i.id)
                .Take(200)
                .AsEnumerable();
#endif
            if (dbUpdateId != 0)
            {
                datas = datas.Where(i => i.id > dbUpdateId);
            }
            else
            {
                datas = datas.Where(i => i.update_time.ToLocalTime() >= currentDate.AddMinutes(-2)
                && i.update_time.ToLocalTime() <= currentDate);
            }
            var environmenLists = datas.ToList().OrderBy(i => i.id);
            if (environmenLists.Count() > 0)
            {
                dbUpdateId = environmenLists.Last().id;
            }
            foreach (var item in environmenLists)
            {
                environments.Add(new Environment()
                {
                    deviceNo = item.device_of_data.ToString(),
                    superviseNumber = "监督编号GD24017", //固定参数
                    deviceDesc = "西闸扬尘",//固定参数
                    pm10 = item.pm10_average / 1000,
                    pm25 = item.pm2_5_average / 1000,
                    tsp = item.tsp_average / 1000,
                    noise = item.noise_average / 10,
                    temp = item.temperature_average / 10,
                    windDirection = this.GetWindDirectionLevel(item.wind_direction_average).levelValue,
                    windSpeed = item.wind_speed_average / 10,
                    recordTime = item.update_time.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss"),
                });
            }
            this.GetLog($"environments Data Count is {environments.Count}");
            return (status, environments);
        }

        /// <summary>
        /// Encrypt
        /// 加密器类型:加密算法为AES,加密模式为CBC,补码方式为PKCS5Padding
        /// 算法类型：用于指定生成AES的密钥
        /// </summary>
        /// <param name="content">appId</param>
        /// <param name="key">appSec</param>
        /// <returns></returns>
        private string GetSignature()
        {
            if (string.Empty == key && string.Empty == appId)
            {
                this.GetLog("BaseData is empty ,Please check key && appId");
                return string.Empty;
            }
            try
            {
                // 构造密钥
                byte[] keyBytes = Encoding.UTF8.GetBytes(key);
                using var aes = Aes.Create();
                aes.Key = keyBytes;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                // 创建初始向量iv用于指定密钥偏移量
                byte[] ivBytes = new byte[Offset];
                Array.Copy(keyBytes, ivBytes, Offset);
                aes.IV = ivBytes;

                // 创建AES加密器
                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                // TODO 加密content ? appId+时间戳 怎么表示 
                var content = appId + GetTimestampMilliseconds();
                byte[] byteContent = Encoding.UTF8.GetBytes(content);
                using var msEncrypt = new MemoryStream();
                using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    csEncrypt.Write(byteContent, 0, byteContent.Length);
                    csEncrypt.FlushFinalBlock();
                }

                // 使用Base64对加密后的二进制数组进行编码
                byte[] encrypted = msEncrypt.ToArray();
                return Convert.ToBase64String(encrypted);
            }
            catch (Exception e)
            {
                this.GetLog(e.Message);
                return string.Empty;
            }
        }
        //获取当前时间戳 毫秒
        static long GetTimestampMilliseconds()
        {
            // 获取自1970年1月1日以来的时间间隔
            TimeSpan timeSpan = DateTime.UtcNow - new DateTime(1970, 1, 1);
            // 将时间间隔转换为毫秒
            return (long)timeSpan.TotalMilliseconds;
        }
        public bool RefreshToken(DateTime dt)
        {
            if (this.GetTokenTime == DateTime.MinValue)
            {
                return true;
            }
            var timespan = dt - this.GetTokenTime;
            //12 小时不用 则过期
            return timespan.TotalHours > 11 ? true : false;
        }
        private (string levelValue, int levelInt) GetWindDirectionLevel(double directionValue)
        {
            string levelValue;
            int levelInt;

            if (11.25 < directionValue && directionValue <= 33.75)
            {
                levelValue = "北东北";
                levelInt = 2;
            }
            else if (33.75 < directionValue && directionValue <= 56.25)
            {
                levelValue = "东北";
                levelInt = 3;
            }
            else if (56.25 < directionValue && directionValue <= 78.75)
            {
                levelValue = "东东北";
                levelInt = 4;
            }
            else if (78.75 < directionValue && directionValue <= 101.25)
            {
                levelValue = "东";
                levelInt = 5;
            }
            else if (101.25 < directionValue && directionValue <= 123.75)
            {
                levelValue = "东东南";
                levelInt = 6;
            }
            else if (123.75 < directionValue && directionValue <= 146.25)
            {
                levelValue = "东南";
                levelInt = 7;
            }
            else if (146.25 < directionValue && directionValue <= 168.75)
            {
                levelValue = "南东南";
                levelInt = 8;
            }
            else if (168.75 < directionValue && directionValue <= 191.25)
            {
                levelValue = "南";
                levelInt = 9;
            }
            else if (191.25 < directionValue && directionValue <= 213.75)
            {
                levelValue = "南西南";
                levelInt = 10;
            }
            else if (213.75 < directionValue && directionValue <= 236.25)
            {
                levelValue = "西南";
                levelInt = 11;
            }
            else if (236.25 < directionValue && directionValue <= 258.75)
            {
                levelValue = "西西南";
                levelInt = 12;
            }
            else if (258.75 < directionValue && directionValue <= 281.25)
            {
                levelValue = "西";
                levelInt = 13;
            }
            else if (281.25 < directionValue && directionValue <= 303.75)
            {
                levelValue = "西西北";
                levelInt = 14;
            }
            else if (303.75 < directionValue && directionValue <= 326.25)
            {
                levelValue = "西北";
                levelInt = 15;
            }
            else if (326.25 < directionValue && directionValue <= 348.75)
            {
                levelValue = "北西北";
                levelInt = 16;
            }
            else
            {
                levelValue = "北";
                levelInt = 1;
            }

            return (levelValue, levelInt);
        }

    }
}
