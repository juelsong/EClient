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
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace EntityViewGenerator.Models
{
    public enum UIType
    {
        Input,
        Radio,
        CheckBox,
        Switch,
        SingleSelect,
        MultipleSelect,
        Date,
        DateTime,
        DateRange,
        DateTimeRange
    }

    public class UITypeSelection
    {
        public string TypeFullName { get; set; }
        public IEnumerable<UIType> Selections { get; set; } = Array.Empty<UIType>();
    }
    public interface ISetting
    {
        public UIType Type { get; set; }
        public string Header { get; set; }
        public string Display { get; set; }
        public string PropertyName { get; set; }
        public string TypeName { get; set; }
        public bool IsNavigation { get; set; }
        public bool IsCollection { get; set; }
    }
    public class ColumnSetting
    {
        public string PropertyName { get; set; }
        public bool IsNavigation { get; set; }
        public bool IsCollection { get; set; }
        public string Header { get; set; }
        public string Display { get; set; }
        public string TypeName { get; set; }
    }
    public class FilterSetting : ISetting
    {
        public string PropertyName { get; set; }
        public bool IsNavigation { get; set; }
        public bool IsCollection { get; set; }
        public bool AllwaysShow { get; set; }
        [JsonPropertyName("uiType")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public UIType Type { get; set; }
        public string Header { get; set; }
        public string Display { get; set; }
        public string TypeName { get; set; }
    }

    public enum ValidateType
    {
        Regex,
        Type,
        Range,
    }

    public enum TriggerType
    {
        Blur,
        Change
    }
    public class ValidateRule
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ValidateType Validate { get; set; }
        public string ErrorMsg { get; set; }
        /// <summary>
        /// 根据ValidateType不同意义不同，ValidateType.Regex就是正则串，Range是逗号分隔的最小最大值，Type无效
        /// </summary>
        public string MagicString { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TriggerType Trigger { get; set; }
    }

    public class EditSetting : ISetting
    {
        public string PropertyName { get; set; }
        public string Header { get; set; }
        public string Display { get; set; }
        [JsonPropertyName("uiType")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public UIType Type { get; set; }
        public string TypeName { get; set; }
        public bool IsNavigation { get; set; }
        public bool IsCollection { get; set; }
        public bool Required { get; set; }
        public bool Unique { get; set; }
        public ValidateRule[] Rules { get; set; } = Array.Empty<ValidateRule>();
    }

    public class ViewSetting
    {
        public string EntityFullName { get; set; }
        public ColumnSetting[] Columns { get; set; } = Array.Empty<ColumnSetting>();
        public FilterSetting[] Filters { get; set; } = Array.Empty<FilterSetting>();
        public EditSetting[] Editors { get; set; } = Array.Empty<EditSetting>();
    }
}
