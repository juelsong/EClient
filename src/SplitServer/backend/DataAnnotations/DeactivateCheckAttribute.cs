namespace ESys.DataAnnotations
{
    using System;

    /// <summary>
    /// 禁用检查特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class DeactivateCheckAttribute : Attribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="property">导航属性</param>
        /// <param name="propertyDisplay">导航属性显示的属性</param>
        public DeactivateCheckAttribute(string property, string propertyDisplay)
        {
            this.Property = property;
            this.PropertyDisplay = propertyDisplay;
        }
        /// <summary>
        /// 检查导航属性名，如果导航实体的IsActive属性为true，则不能禁用
        /// </summary>
        public string Property { get; }

        public string PropertyDisplay { get; set; }
    }
}
