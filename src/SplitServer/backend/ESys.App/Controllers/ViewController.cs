using ESys.Contract.Attributes;
using ESys.Security.Entity;
using ESys.Utilty.Attributes;
using ESys.Utilty.Defs;
using ESys.Utilty.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;
using System.Text.Unicode;
using System.Xml;

namespace ESys.Controllers
{
    [Microsoft.AspNetCore.Authorization.AllowAnonymous]
    [ApiController]
    [Route("[controller]/[action]")]
    public class ViewController : Controller
    {
        private readonly IServiceProvider serviceProvider;
        private IActionDescriptorCollectionProvider actionProvider;
        public ViewController(
            IServiceProvider serviceProvider,
            IActionDescriptorCollectionProvider actionProvider)
        {
            this.serviceProvider = serviceProvider;
            this.actionProvider = actionProvider;
        }


        [HttpGet]
        public Result<string> Version()
        {
            var db = this.serviceProvider.GetWritableRepository<User>().Database;
            var version = db.GetAppliedMigrations().Last();
            version = version.Split('_').Last();
            return ResultBuilder.Ok(version);
        }

        [HttpGet]
        public IActionResult Assemblies()
        {
            var names = Furion.App.Assemblies
                        .Where(ass => ass.GetName().Name.Contains(".Metaura."))
                        .Select(ass => $"{ass.GetName().Name}_{GetGitVersion(ass)}");
            return this.Json(new { Assembiles = names });
        }

        [HttpGet]
        public IActionResult AssembilesMeta()
        {
            var sb = new StringBuilder();
            foreach (var item in DependencyContext.Default.RuntimeLibraries.OrderBy(l => l.Name))
            {
                sb.AppendLine($"{item.Name} {item.Type}");
            }
            return this.Content(sb.ToString());
        }

        [HttpGet]
        public IActionResult EsignCategorys()
        {
            var methods = this.actionProvider.ActionDescriptors.Items.OfType<ControllerActionDescriptor>()
                              .Select(x => x.MethodInfo)
                              .ToArray();
            var assembies = methods.Select(m => m.DeclaringType?.Assembly)
                                   .Where(a => a != null)
                                   .Distinct()
                                   .ToArray();
            var comments = assembies.Select(ass => Path.Join(Path.GetDirectoryName(ass.Location), $"{ass.GetName().Name}.xml"))
                    .Where(path => System.IO.File.Exists(path))
                    .SelectMany(path =>
                    {
                        var doc = new XmlDocument();
                        doc.Load(path);
                        return doc.SelectNodes("//doc/members/member").Cast<XmlNode>();
                    })
                    .ToArray()
                    .ToDictionary(
                        n => n.Attributes.GetNamedItem("name").Value[2..],
                        n => n.ChildNodes[0].InnerText.Trim());
            var objs = methods.Where(x => x.GetCustomAttribute<CheckESignAttribute>() != null)
                               .Select(x =>
                               {
                                   var kvp = comments.FirstOrDefault(kp => kp.Key.StartsWith($"{x.DeclaringType.FullName}.{x.Name}("));
                                   return new { Key = x.GetCustomAttribute<CheckESignAttribute>().Key, Comment = kvp.Value };
                               })
                               .ToArray();
            var json = new JsonObject();
            foreach (var obj in objs)
            {
                json[obj.Key] = obj.Comment;
            }
            return this.Json(json);
        }

#if DEBUG
        [HttpGet]
        public string ErrorCodes()
        {
            var errorCodeType = typeof(ErrorCode);
            var errorCode = new JsonObject();
            foreach (var field in errorCodeType.GetFields(BindingFlags.Public | BindingFlags.Static))
            {
                errorCode.Add(field.Name, (int)field.GetValue(null));
            }
            foreach (var nestedType in errorCodeType.GetNestedTypes())
            {
                var nestedObj = new JsonObject();
                foreach (var field in nestedType.GetFields(BindingFlags.Public | BindingFlags.Static))
                {
                    nestedObj.Add(field.Name, (int)field.GetValue(null));
                }
                errorCode.Add(nestedType.Name, nestedObj);
            }
            var str = JsonSerializer.Serialize(errorCode, new JsonSerializerOptions() { WriteIndented = true });
            var regex = new Regex(@"\""([a-zA-Z]+)*\"":");
            str = regex.Replace(str, "$1:");
            return str;
        }

        [HttpGet]
        public IActionResult ErrorCodeFunction()
        {
            var errorCodeType = typeof(ErrorCode);
            var dic = new Dictionary<int, string>();
            foreach (var field in errorCodeType.GetFields(BindingFlags.Public | BindingFlags.Static))
            {
                dic.Add((int)field.GetValue(null), $"ErrorPrompt.{field.Name}");
            }
            foreach (var nestedType in errorCodeType.GetNestedTypes())
            {
                foreach (var field in nestedType.GetFields(BindingFlags.Public | BindingFlags.Static))
                {
                    dic.Add((int)field.GetValue(null), $"ErrorPrompt.{nestedType.Name}.{field.Name}");
                }
            }
            var str = string.Join("\n", dic.Select(kvp => $"    case {kvp.Key}:\n\t    return '{kvp.Value}';"));
            return this.Ok(str);
        }

        [HttpGet]
        public string ErrorPrompts()
        {
            var errorCodeType = typeof(ErrorCode);
            var errorCode = new JsonObject();
            foreach (var field in errorCodeType.GetFields(BindingFlags.Public | BindingFlags.Static))
            {
                errorCode.Add(field.Name, field.GetCustomAttribute<DescriptionAttribute>().Description);
            }
            foreach (var nestedType in errorCodeType.GetNestedTypes())
            {
                var nestedObj = new JsonObject();
                foreach (var field in nestedType.GetFields(BindingFlags.Public | BindingFlags.Static))
                {
                    JsonNode desc = JsonValue.Create(field.GetCustomAttribute<DescriptionAttribute>().Description);
                    nestedObj.Add(field.Name, desc);
                }
                errorCode.Add(nestedType.Name, nestedObj);
            }
            var str = JsonSerializer.Serialize(errorCode, new JsonSerializerOptions()
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
                WriteIndented = true
            });
            var regex = new Regex(@"\""([a-zA-Z]+)*\"":");
            str = regex.Replace(str, "$1:");
            return str;
        }
#endif

        static string GetGitVersion(Assembly assembly)
        {
            var attr = assembly.GetCustomAttribute<GitHashAttribute>();
            return attr?.Hash ?? string.Empty;
        }
    }
}
