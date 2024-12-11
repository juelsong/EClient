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

using System;
using System.Text;

namespace ESys.Security.Utility
{
    internal static class RandomUtil
    {
        private static Random random = new Random((int)DateTime.Now.Ticks);
        public static string RandomString(string baseString, int length)
        {
            if (string.IsNullOrWhiteSpace(baseString))
            {
                throw new ArgumentNullException(nameof(baseString));
            }
            var sb = new StringBuilder(length);
            if (length < 1)
            {
                length = 1;
            }

            var baseLength = baseString.Length;

            for (var i = 0; i < length; ++i)
            {
                var number = RandomInt(baseLength);
                sb.Append(baseString[number]);
            }

            return sb.ToString();
        }
        public static int RandomInt(int limit)
        {
            return random.Next(0, limit);
        }
    }
}
