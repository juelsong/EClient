using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EHost.Contract
{
    public static class ObjectExtensions
    {
        public static void CopyPropertiesFrom<Target, Source>(this Target target ,Source source)
        {
            Type sourceType = source.GetType();
            Type targetType = target.GetType();

            // 获取目标对象的所有属性
            PropertyInfo[] targetProperties = targetType.GetProperties();

            foreach (PropertyInfo targetProperty in targetProperties)
            {
                // 查找源对象中具有相同名称和类型的属性
                PropertyInfo sourceProperty = sourceType.GetProperty(targetProperty.Name, targetProperty.PropertyType);

                // 如果源对象有这个属性，并且它是可写的，则复制值
                if (sourceProperty != null/* && sourceProperty.CanRead && targetProperty.CanWrite*/)
                {
                    object value = sourceProperty.GetValue(source, null);
                    targetProperty.SetValue(target, value, null);
                }
            }
        }
    }
}
