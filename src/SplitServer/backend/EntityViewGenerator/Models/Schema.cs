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

namespace EntityViewGenerator.Models
{
    public class PropertyInfo
    {
        public string Name { get; set; }
        public string Desc { get; set; }
        public string TypeName { get; set; }
        public bool IsNullable { get; set; }
        public bool IsNavigation { get; set; }
        public bool IsCollection { get; set; }
    }

    public class EntityInfo
    {
        public string[] Keys { get; set; } = Array.Empty<string>();
        public string Namespace { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public string FullName { get => $"{this.Namespace}.{this.Name}"; }
        public PropertyInfo[] Properties { get; set; } = Array.Empty<PropertyInfo>();
    }

    public class EnumMember
    {
        public string Name { get; set; }
        public string Desc { get; set; }
        public object Value { get; set; }
    }

    public class EnumInfo
    {
        public string Namespace { get; set; }
        public string Name { get; set; }
        public string FullName { get => $"{this.Namespace}.{this.Name}"; }
        public EnumMember[] Members { get; set; } = Array.Empty<EnumMember>();
    }

    public class Schema
    {
        public EntityInfo[] Entities { get; set; } = Array.Empty<EntityInfo>();
        public EnumInfo[] Enums { get; set; } = Array.Empty<EnumInfo>();
    }
}
