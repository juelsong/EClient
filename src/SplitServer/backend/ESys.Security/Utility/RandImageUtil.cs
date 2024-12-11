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

namespace ESys.Security.Utility
{
    using SkiaSharp;
    using System;

    internal static class RandImageUtil
    {
        /// <summary>
        /// 定义图形大小
        /// </summary>
        private static readonly int width = 105;
        /// <summary>
        /// 定义图形大小
        /// </summary>
        private static readonly int height = 35;
        /// <summary>
        /// 定义干扰线数量
        /// </summary>
        private static readonly int count = 200;

        /// <summary>
        /// 干扰线的长度=1.414*lineWidth
        /// </summary>
        private static readonly int lineWidth = 2;

        private static readonly string BASE64_PRE = "data:image/png;base64,";
        private static readonly Random random = new((int)DateTime.Now.Ticks);
        private static readonly SKTypeface font = SKTypeface.FromFamilyName("Times New Roman", SKFontStyle.Bold);

        private static SKColor GetRandColor(int a = 0, int b = 255)
        { // 取得给定范围随机颜色

            a = Math.Min(255, Math.Max(a, 0));
            b = Math.Min(255, Math.Max(b, 0));
            var min = Math.Min(a, b);
            var max = Math.Max(a, b);
            return new SKColor(
                (byte)random.Next(min, max),
                (byte)random.Next(min, max),
                (byte)random.Next(min, max));
        }

        private static byte[] GenerateCodeImage(string captchaText)
        {
            using var borderPaint = new SKPaint()
            {
                IsStroke = true,
                Color = GetRandColor(100, 200),
                StrokeWidth = 2,
                StrokeCap = SKStrokeCap.Square
            };
            using var image2d = new SKBitmap(width, height, SKColorType.Bgra8888, SKAlphaType.Premul);
            using var canvas = new SKCanvas(image2d);
            canvas.DrawColor(SKColors.White); // Clear 
            canvas.DrawRect(1, 1, width - 2, height - 2, borderPaint);

            for (var i = 0; i < captchaText.Length; i++)
            {
                using var strPaint = new SKPaint()
                {
                    Color = GetRandColor(),
                    Typeface = font,
                    IsAntialias = true,
                    TextSize = 24,
                };
                canvas.DrawText(captchaText.Substring(i, 1), new SKPoint(23 * i+10, 25), strPaint);
            }

            // 随机产生干扰线，使图象中的认证码不易被其它程序探测到
            for (var i = 0; i < count; i++)
            {
                using var linePaint = new SKPaint()
                {
                    Color = GetRandColor(150,200),
                    IsStroke = true,
                    IsAntialias = true,
                    TextSize = 24,
                    StrokeWidth = 2
                };
                var x = random.Next(0, width - lineWidth - 1) + 1; // 保证画在边框之内
                var y = random.Next(0, height - lineWidth - 1) + 1;
                var xl = random.Next(0, lineWidth);
                var yl = random.Next(0, lineWidth);
                canvas.DrawLine(new SKPoint(x, y), new SKPoint(x + xl, y + yl), linePaint);
            }

            using var img = SKImage.FromBitmap(image2d);
            using var p = img.Encode(SKEncodedImageFormat.Png, 100);
            var imageBytes = p.ToArray();
            return imageBytes;
        }

        public static string GenerateCodeImageBase64(string resultCode)
        {
            var bytes = GenerateCodeImage(resultCode);
            //转换成base64串
            var base64 = Convert.ToBase64String(bytes);
            base64 = base64.Replace("\n", "").Replace("\r", "");//删除 \r\n

            return BASE64_PRE + base64;
        }
    }
}
