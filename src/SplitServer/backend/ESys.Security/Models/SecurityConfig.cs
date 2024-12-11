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

namespace ESys.Security.Models
{
    using ESys.Security.PassValidators;
    using ESys.Utilty.Serialization;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    /// <summary>
    /// 安全配置
    /// </summary>
    internal class SecurityConfig
    {
        /// <summary>
        /// 验证器配置项
        /// </summary>
        public class PassValidatorItem
        {
            [JsonIgnore]
            private IPassValidator validator;
            /// <summary>
            /// 验证器类型全名
            /// </summary>
            public string Type { get; set; }
            /// <summary>
            /// 验证器JSON
            /// </summary>
            public string JsonInfo { get; set; }
            /// <summary>
            /// 验证器对象，通过 Type 确定类型后反序列化 JsonInfo
            /// </summary>
            [JsonIgnore]
            public IPassValidator Validator
            {
                get
                {
                    lock (this)
                    {
                        if (this.validator == null)
                        {
                            var validatorType = Assembly.GetExecutingAssembly().GetType(this.Type);
                            this.validator = (IPassValidator)JsonSerializer.Deserialize(this.JsonInfo, validatorType);
                        }
                    }
                    return this.validator;
                }
                set
                {
                    if (value == null)
                    {
                        throw new ArgumentNullException("value");
                    }
                    this.validator = value;
                    this.Type = value.GetType().FullName;
                    this.JsonInfo = JsonSerializer.Serialize(value, value.GetType());
                }
            }
        }
        /// <summary>
        /// 密码验证器数组<see cref="PassValidators.IPassValidator"/>
        /// </summary>
        public PassValidatorItem[] PassValidators { get; set; }
        /// <summary>
        /// 首次登录时是否修改密码
        /// </summary>
        public bool ChangePassUponFirstLogin { get; set; }
        /// <summary>
        /// 近几次密码不能重复,0表示不限定
        /// </summary>
        public int CanNotRepeatedTimes { get; set; }
        /// <summary>
        /// 有效期，空为不过期
        /// </summary>
        [JsonConverter(typeof(NullableTimeSpanConverter))]
        public TimeSpan? ExpiryPeriod { get; set; }
        /// <summary>
        /// 效登录尝试次数，超过则锁定账户，0不限制
        /// </summary>
        public int InvalidLoginAttempts { get; set; }
    }

    /// <summary>
    /// 安全配置模型
    /// </summary>
    public class SecurityModel
    {
        /// <summary>
        /// 密码规则
        /// </summary>
        public class PasswordRules
        {
            /// <summary>
            /// 最小密码长度
            /// </summary>
            public int? MinLength { get; set; }
            /// <summary>
            /// 包含数字
            /// </summary>
            public bool IncludeNumber { get; set; }
            /// <summary>
            /// 包含小写字母
            /// </summary>
            public bool IncludeLower { get; set; }
            /// <summary>
            /// 包含大写字母
            /// </summary>
            public bool IncludeUpper { get; set; }
            /// <summary>
            /// 包含特殊字符
            /// </summary>
            public bool IncludeSpecial { get; set; }
        }
        /// <summary>
        /// 密码规则
        /// </summary>
        public PasswordRules Rules { get; set; }
        /// <summary>
        /// 首次登录时是否修改密码
        /// </summary>
        public bool ChangePassUponFirstLogin { get; set; }
        /// <summary>
        /// 近几次密码不能重复,0表示不限定
        /// </summary>
        public int CanNotRepeatedTimes { get; set; }
        /// <summary>
        /// 有效期，空为不过期
        /// </summary>
        public TimeSpan? ExpiryPeriod { get; set; }
        /// <summary>
        /// 效登录尝试次数，超过则锁定账户，0不限制
        /// </summary>
        public int InvalidLoginAttempts { get; set; }

        internal SecurityConfig GetSecurityConfig()
        {
            var config = new SecurityConfig()
            {
                ChangePassUponFirstLogin = this.ChangePassUponFirstLogin,
                CanNotRepeatedTimes = this.CanNotRepeatedTimes,
                ExpiryPeriod = this.ExpiryPeriod,
                InvalidLoginAttempts = this.InvalidLoginAttempts,
            };
            var validators = new List<SecurityConfig.PassValidatorItem>();
            if (this.Rules != null)
            {
                if (this.Rules.MinLength.HasValue && this.Rules.MinLength.Value > 0)
                {
                    validators.Add(new SecurityConfig.PassValidatorItem()
                    {
                        Validator = new MinLengthValidator()
                        {
                            MinLen = this.Rules.MinLength.Value,
                        }
                    });
                }
                if (this.Rules.IncludeSpecial)
                {
                    validators.Add(new SecurityConfig.PassValidatorItem()
                    {
                        Validator = new ContainsSpecialCharValidator()
                    });
                }
                if (this.Rules.IncludeUpper)
                {
                    validators.Add(new SecurityConfig.PassValidatorItem()
                    {
                        Validator = new ContainsUpperCharValidator()
                    });
                }
                if (this.Rules.IncludeLower)
                {
                    validators.Add(new SecurityConfig.PassValidatorItem()
                    {
                        Validator = new ContainsLowerCharValidator()
                    });
                }
                if (this.Rules.IncludeNumber)
                {
                    validators.Add(new SecurityConfig.PassValidatorItem()
                    {
                        Validator = new ContainsNumberValidator()
                    });
                }
                config.PassValidators = validators.ToArray();
            }
            return config;
        }

        internal static SecurityModel GetSecurityModel(SecurityConfig config)
        {
            var model = new SecurityModel()
            {
                Rules = new()
            };
            if (config != null)
            {
                model.ChangePassUponFirstLogin = config.ChangePassUponFirstLogin;
                model.CanNotRepeatedTimes = config.CanNotRepeatedTimes;
                model.ExpiryPeriod = config.ExpiryPeriod;
                model.InvalidLoginAttempts = config.InvalidLoginAttempts;
                if (config.PassValidators != null)
                {
                    model.Rules.IncludeNumber = config.PassValidators.Any(item => item.Validator is ContainsNumberValidator);
                    model.Rules.IncludeLower = config.PassValidators.Any(item => item.Validator is ContainsLowerCharValidator);
                    model.Rules.IncludeUpper = config.PassValidators.Any(item => item.Validator is ContainsUpperCharValidator);
                    model.Rules.IncludeSpecial = config.PassValidators.Any(item => item.Validator is ContainsSpecialCharValidator);
                    var minLengthValidatorItem = config.PassValidators.FirstOrDefault(item => item.Validator is MinLengthValidator);
                    if (minLengthValidatorItem != null)
                    {
                        model.Rules.MinLength = ((MinLengthValidator)minLengthValidatorItem.Validator).MinLen;
                    }
                }
            }
            return model;
        }
    }
}
