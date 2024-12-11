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

namespace ESys.App.ModelBinding
{
    using ESys.Utilty.Defs;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
    using System;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Text.Json;
    using System.Text.Json.Serialization;
    using System.Threading.Tasks;

    internal class EntityWithNamesBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context.Metadata is DefaultModelMetadata dma)
            {
                if (dma.Attributes.Attributes.OfType<EntityWithNamesAttribute>().Any())
                {
                    var type = dma.ModelType.GenericTypeArguments[0];
                    return new EntityWithNamesBinder(type,true);
                }
                if (dma.Attributes.Attributes.OfType<EntityAttribute>().Any())
                {
                    return new EntityWithNamesBinder(dma.ModelType, false);
                }
            }
            return null;
        }
    }
    internal class TimeSpanISO8601Converter : JsonConverterFactory
    {
        private class InnerConverter : JsonConverter<TimeSpan>
        {
            public override TimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                return typeToConvert != typeof(TimeSpan) || reader.TokenType != JsonTokenType.String
                    ? throw new NotSupportedException()
                    : System.Xml.XmlConvert.ToTimeSpan(reader.GetString());
            }

            public override void Write(Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options)
            {
                writer.WriteStringValue(System.Xml.XmlConvert.ToString(value));
            }
        }
        public override bool CanConvert(Type typeToConvert)
        {
            return typeToConvert == typeof(TimeSpan);
        }

        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            return new InnerConverter();
        }
    }

    internal class EntityWithNamesBinder : IModelBinder
    {
      
        private class NullableJsonStringEnumConverter : JsonConverterFactory
        {
            internal enum EnumConverterOptions
            {
                // Token: 0x04000342 RID: 834
                AllowStrings = 1,
                // Token: 0x04000343 RID: 835
                AllowNumbers = 2
            }
            private class InnerConverter<T> : JsonConverter<T?> where T : struct, Enum
            {
                private readonly JsonConverter<T> _converter;
                private readonly Type _underlyingType;

                public InnerConverter() : this(null) { }

                public InnerConverter(JsonSerializerOptions options)
                {
                    // for performance, use the existing converter if available
                    if (options != null)
                    {
                        this._converter = (JsonConverter<T>)options.GetConverter(typeof(T));
                    }

                    // cache the underlying type
                    this._underlyingType = Nullable.GetUnderlyingType(typeof(T?));
                }

                public override bool CanConvert(Type typeToConvert)
                {
                    return typeof(T).IsAssignableFrom(typeToConvert);
                }

                public override T? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
                {
                    if (this._converter != null)
                    {
                        return this._converter.Read(ref reader, this._underlyingType, options);
                    }

                    var value = reader.GetString();

                    if (string.IsNullOrEmpty(value))
                    {
                        return default;
                    }

                    // for performance, parse with ignoreCase:false first.
                    if (!Enum.TryParse(this._underlyingType, value,
                        ignoreCase: false, out object result)
                    && !Enum.TryParse(this._underlyingType, value,
                        ignoreCase: true, out result))
                    {
                        throw new JsonException(
                            $"Unable to convert \"{value}\" to Enum \"{this._underlyingType}\".");
                    }

                    return (T)result;
                }

                public override void Write(Utf8JsonWriter writer,
                    T? value, JsonSerializerOptions options)
                {
                    writer.WriteStringValue(value?.ToString());
                }
            }

            //private readonly JsonNamingPolicy _namingPolicy;

            //// Token: 0x04000273 RID: 627
            //private readonly EnumConverterOptions _converterOptions;
            //public NullableJsonStringEnumConverter(JsonNamingPolicy namingPolicy = null, bool allowIntegerValues = true)
            //{
            //    this._namingPolicy = namingPolicy;
            //    this._converterOptions = (allowIntegerValues ? (EnumConverterOptions.AllowStrings | EnumConverterOptions.AllowNumbers) : EnumConverterOptions.AllowStrings);
            //}

            public override bool CanConvert(Type typeToConvert)
            {
                return typeToConvert.IsGenericType
                    && typeToConvert.GetGenericTypeDefinition() == typeof(Nullable<>)
                    && typeToConvert.GenericTypeArguments.Length == 1
                    && typeToConvert.GenericTypeArguments[0].IsEnum;
            }

            public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
            {
                var converterType = typeof(InnerConverter<>).MakeGenericType(typeToConvert.GenericTypeArguments[0]);
                return Activator.CreateInstance(converterType, options) as JsonConverter;

            }
        }

        private readonly Type entityType;
        private readonly string[] propertyNames;
        private static readonly MethodInfo createEntityWithNamesMethod =
            typeof(EntityWithNamesBinder)
            .GetMethod(nameof(CreateEntityWithNames), BindingFlags.Static | BindingFlags.NonPublic);
        private readonly bool withName;
        static JsonSerializerOptions GetJsonOptions()
        {
            var ret = new JsonSerializerOptions();
            ret.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
            ret.Converters.Add(new NullableJsonStringEnumConverter());
            // OData 默认timespan（duration）序列化格式为 [ISO 8601]
            // http://docs.oasis-open.org/odata/odata-json-format/v4.01/odata-json-format-v4.01.html
            ret.Converters.Add(new TimeSpanISO8601Converter());
            ret.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            return ret;
        }

        public static JsonSerializerOptions options = GetJsonOptions();

        public EntityWithNamesBinder(Type entityType,bool withName)
        {
            this.entityType = entityType;
            this.propertyNames = this.entityType.GetProperties().Select(p => p.Name).ToArray();
            this.withName = withName;
        }

        public async Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }
            using var reader = new StreamReader(bindingContext.HttpContext.Request.Body, Encoding.UTF8);
            var str = await reader.ReadToEndAsync();
            try
            {
                using var doc = JsonDocument.Parse(str);
                var entity = JsonSerializer.Deserialize(str, this.entityType, options);
                if (this.withName)
                {
                    var names = doc.RootElement.EnumerateObject()
                        .Select(e => e.Name)
                        .Where(n => this.propertyNames.Contains(n))
                        .ToArray();
                    var result = createEntityWithNamesMethod.MakeGenericMethod(this.entityType).Invoke(null, new object[] { entity, names });
                    bindingContext.Result = ModelBindingResult.Success(result);
                }
                else
                {
                    bindingContext.Result = ModelBindingResult.Success(entity);
                }
            }
            catch (Exception)
            {
                bindingContext.Result = ModelBindingResult.Failed();
            }
        }

        private static (T Entity, string[] Names) CreateEntityWithNames<T>(T entity, string[] names)
        {
            return (Entity: entity, Names: names);
        }
    }
}
