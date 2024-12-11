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

namespace CN.Metaura.EMIS.Contract.Workflow
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// 动态数据
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DynamicData<T>
    {
        /// <summary>
        /// 存储
        /// </summary>
        public Dictionary<string, T> Storage { get; set; } = new Dictionary<string, T>();
        /// <summary>
        /// 获取项
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public T this[string propertyName]
        {
            get
            {
                return this.Storage.TryGetValue(propertyName, out var value) ? value : default;
            }
            set
            {
                this.Storage[propertyName] = value;
            }
        }
    }

    /// <summary>
    /// 流程上下文
    /// </summary>
    /// 可空数据类型不能用在Input， Workflow-core BuildScalarInputAction 使用System.Convert.ChangeType 会报错
    public class WorkflowContext
    {
        /// <summary>
        /// 整数数据
        /// </summary>
        public DynamicData<int> Integer { get; set; } = new DynamicData<int>();
        /// <summary>
        /// 可空整数数据
        /// </summary>
        public DynamicData<int?> NullableInteger { get; set; } = new DynamicData<int?>();
        /// <summary>
        /// 长整数数据
        /// </summary>
        public DynamicData<long> Long { get; set; } = new DynamicData<long>();
        /// <summary>
        /// 布尔数据
        /// </summary>
        public DynamicData<bool> Boolean { get; set; } = new DynamicData<bool>();
        /// <summary>
        /// 可空布尔数据
        /// </summary>
        public DynamicData<bool?> NullableBoolean { get; set; } = new DynamicData<bool?>();
        /// <summary>
        /// 字符串数据
        /// </summary>
        public DynamicData<string> String { get; set; } = new DynamicData<string>();
        /// <summary>
        /// decimal数据
        /// </summary>
        public DynamicData<decimal> Decimal { get; set; } = new DynamicData<decimal>();
        /// <summary>
        /// 浮点数据
        /// </summary>
        public DynamicData<double> Double { get; set; } = new DynamicData<double>();

        /// <summary>
        /// 时间点数据
        /// </summary>
        public DynamicData<DateTimeOffset> DateTimeOffset { get; set; } = new();

        /// <summary>
        /// 整数数据数组
        /// </summary>
        public DynamicData<int[]> IntegerArray { get; set; } = new DynamicData<int[]>();
        /// <summary>
        /// 长整数数据数组
        /// </summary>
        public DynamicData<long[]> LongArray { get; set; } = new DynamicData<long[]>();
        /// <summary>
        /// 布尔数据数组
        /// </summary>
        public DynamicData<bool[]> BooleanArray { get; set; } = new DynamicData<bool[]>();
        /// <summary>
        /// 字符串数据数组
        /// </summary>
        public DynamicData<string[]> StringArray { get; set; } = new DynamicData<string[]>();
        /// <summary>
        /// decimal数据数组
        /// </summary>
        public DynamicData<decimal[]> DecimalArray { get; set; } = new DynamicData<decimal[]>();
        /// <summary>
        /// 浮点数据数组
        /// </summary>
        public DynamicData<double[]> DoubleArray { get; set; } = new DynamicData<double[]>();
    }
}
