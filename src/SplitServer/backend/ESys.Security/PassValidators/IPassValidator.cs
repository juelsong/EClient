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

namespace ESys.Security.PassValidators
{
    using ESys.Security.Entity;
    using ESys.Security.Models;
    using ESys.Utilty.Security;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text.Json;
    using System.Text.Json.Nodes;
    using System.Text.RegularExpressions;

    /// <summary>
    /// 密码验证器
    /// </summary>
    internal interface IPassValidator
    {
        /// <summary>
        /// 验证密码
        /// </summary>
        /// <param name="pass"></param>
        /// <param name="locale"></param>
        /// <param name="errorMsg"></param>
        /// <returns></returns>
        bool TryValidatePass(string pass, string locale, ref string errorMsg);
        /// <summary>
        /// 获取校验规则，供前端使用
        /// </summary>
        /// <param name="locale"></param>
        /// <returns></returns>
        PasswordRule GetPasswordRule(string locale);
    }

    internal abstract class PassValidatorBase : IPassValidator
    {
        protected static readonly Dictionary<string, JsonObject> localeDic = GetLocaleDic();

        public abstract bool TryValidatePass(string pass, string locale, ref string errorMsg);

        public abstract PasswordRule GetPasswordRule(string locale);

        private static Dictionary<string, JsonObject> GetLocaleDic()
        {
            var ass = Assembly.GetExecutingAssembly();
            var i18nPath = $"{ass.GetName().Name}.i18n";
            var ret = new Dictionary<string, Dictionary<string, string>>();
            return ass.GetManifestResourceNames().Where(name => name.StartsWith(i18nPath)).ToDictionary(
                name => name[i18nPath.Length..].Split('.', StringSplitOptions.RemoveEmptyEntries).First().Replace('_', '-'),
                name =>
                {
                    using var stream = ass.GetManifestResourceStream(name);
                    var obj = JsonNode.Parse(stream) as JsonObject;
                    return obj;
                });
        }
    }

    internal sealed class UserPassHistoryValidator : PassValidatorBase
    {
        private readonly IEnumerable<UserPasswordHistory> userPassHistoryRecord;
        private readonly int userId;
        private readonly int historyLimitCount;

        public override PasswordRule GetPasswordRule(string locale) => null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="historyLimitCount">历史密码放重次数，表示过去几次的密码不能用</param>
        /// <param name="userPassHistoryRecord"></param>
        public UserPassHistoryValidator(int userId, int historyLimitCount, IEnumerable<UserPasswordHistory> userPassHistoryRecord)
        {
            this.userId = userId;
            this.historyLimitCount = historyLimitCount;
            this.userPassHistoryRecord = userPassHistoryRecord;
        }

        public override bool TryValidatePass(string pass, string locale, ref string errorMsg)
        {
            if (!localeDic.TryGetValue(locale, out var localeItem))
            {
                localeItem = localeDic["zh-cn"];
            }
            foreach (var record in this.userPassHistoryRecord
                        .Where(u => u.UserId == this.userId)
                        .OrderByDescending(u => u.CreatedTime)
                        .Take(this.historyLimitCount))
            {
                if (record.Password == PasswordHasher.HashPassword(pass, record.Salt))
                {
                    errorMsg = string.Format(localeItem["PassValidationErrors"]["CanNotRepeatedTimes"].GetValue<string>(), this.historyLimitCount);
                    return false;
                }
            }
            return true;
        }
    }

    internal abstract class RegexPassValidator : PassValidatorBase
    {
        protected abstract string RegexString { get; }
        public override bool TryValidatePass(string pass, string locale, ref string errorMsg)
        {
            var regex = new Regex(this.RegexString);
            var flag = regex.IsMatch(pass);
            if (!flag)
            {
                if (!localeDic.TryGetValue(locale, out var localeItem))
                {
                    localeItem = localeDic["zh-cn"];
                }
                errorMsg = this.GetError((JsonObject)localeItem["PassValidationErrors"]);
            }
            return flag;
        }
        public override PasswordRule GetPasswordRule(string locale)
        {
            if (!localeDic.TryGetValue(locale ?? string.Empty, out var localeItem))
            {
                localeItem = localeDic["zh-cn"];
            }
            var errorMsg = this.GetError((JsonObject)localeItem["PassValidationErrors"]);
            return new PasswordRule()
            {
                RegexString = this.RegexString,
                Prompt = errorMsg,
            };
        }
        protected abstract string GetError(JsonObject localObject);
    }

    internal class MinLengthValidator : RegexPassValidator
    {
        public int MinLen { get; set; }

        protected override string RegexString => $"\\S{{{this.MinLen}}}";

        protected override string GetError(JsonObject localObject)
            => string.Format(localObject["MinLength"].GetValue<string>(), this.MinLen);
    }

    internal class ContainsNumberValidator : RegexPassValidator
    {
        protected override string RegexString => "\\d";

        protected override string GetError(JsonObject localObject)
            => localObject["ContainsNumber"].GetValue<string>();
    }

    internal class ContainsLowerCharValidator : RegexPassValidator
    {
        protected override string RegexString => "[a-z]";

        protected override string GetError(JsonObject localObject)
            => localObject["ContainsLowerChar"].GetValue<string>();
    }


    internal class ContainsUpperCharValidator : RegexPassValidator
    {
        protected override string RegexString => "[A-Z]";

        protected override string GetError(JsonObject localObject)
            => localObject["ContainsUpperChar"].GetValue<string>();
    }


    internal class ContainsSpecialCharValidator : RegexPassValidator
    {
        protected override string RegexString => "[\\x21-\\x2f,\\x3a-\\x40,\\x5b-\\x60,\\x7b-\\x7e]";

        protected override string GetError(JsonObject localObject)
            => localObject["ContainsSpecialChar"].GetValue<string>();
    }
}
