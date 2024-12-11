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

namespace EntityViewGenerator.Impl
{
    using EntityViewGenerator.Interface;
    using EntityViewGenerator.Models;
    using Mono.TextTemplating;
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.IO;
    using System.IO.Compression;
    using System.Linq;
    using System.Text;

    public class ESysHost : ProjectHost
    {
        public new string GetValidateRuleString(EditSetting setting)
        {
            var entityFullName = this.Config.EntityFullName;
            string getTypeString()
            {
                if (setting.TypeName == typeof(string).FullName)
                {
                    return "string";
                }
                if (setting.TypeName == typeof(double).FullName || setting.TypeName == typeof(float).FullName)
                {
                    return "number";
                }
                if (setting.TypeName == typeof(bool).FullName)
                {
                    return "boolean";
                }
                if (setting.IsCollection) // TODO
                {
                    return "array";
                }
                if (setting.TypeName == typeof(DateTime).FullName || setting.TypeName == typeof(DateTimeOffset).FullName)
                {
                    return "date";
                }
                return "any";
            }
            var sb = new StringBuilder($"        {setting.Display}: [\r\n");
            if (setting.Required)
            {
                sb.AppendLine($@"          {{
            required: true,
            message: () => this.$t(""validator.template.required"", [ this.$t(""{this.Entity.Name}.editor.{setting.PropertyName}"") ]),
            trigger: ""blur"",
          }},");
            }
            if (setting.Unique)
            {
                sb.AppendLine($@"          {{
            validator: (rule, value, callback) => {{
              if(value) {{
                this.$exist('{entityFullName}', '{setting.PropertyName}', value, this.modelInner.Id).then(exist=>{{
                  if(exist){{
                    callback(new Error(this.$t(""validator.template.exist"", [this.$t(""{this.Entity.Name}.editor.{setting.PropertyName}"")])));
                  }}
                  else{{
                    callback();
                  }}
                }});
              }}
            }},
            trigger: ""blur"",
          }},");
            }
            foreach (var rule in setting.Rules)
            {
                var triggerStr = Enum.GetName(rule.Trigger).ToLower();
                switch (rule.Validate)
                {
                    case ValidateType.Regex:
                        sb.AppendLine($@"          {{
            pattern: /{rule.MagicString}/ig,
            trigger: ""{triggerStr}"",
            message: () => this.$t(""validator.template.formatWrong"", [ this.$t(""{this.Entity.Name}.editor.{setting.PropertyName}""), this.$t(""{this.Entity.Name}.validator.{setting.PropertyName}.Regex"") ]),
          }},");
                        break;
                    case ValidateType.Type:
                        sb.AppendLine($@"          {{
            type: ""{getTypeString()}"",
            trigger: ""{triggerStr}"",
            message: () => this.$t(""validator.template.typeWrong"", [ this.$t(""{this.Entity.Name}.editor.{setting.PropertyName}""), this.$t(""{this.Entity.Name}.validator.{setting.PropertyName}.Type"") ]),
          }},");
                        break;
                    case ValidateType.Range:
                        {
                            var values = rule.MagicString.Split(',');
                            if (values.Length != 2)
                            {
                                sb.AppendLine($"Invalidate value {rule.MagicString}");
                            }
                            else
                            {
                                sb.AppendLine("          {");
                                var str = new List<string>();
                                var formatStr = string.Empty;
                                if (!string.IsNullOrEmpty(values[0]))
                                {
                                    sb.AppendLine($"            min: {values[0]},");
                                    formatStr = @"""validator.template.min""";
                                    str.Add($@" this.$t(""{this.Entity.Name}.validator.{setting.PropertyName}.Min"")");
                                }
                                if (!string.IsNullOrEmpty(values[1]))
                                {
                                    sb.AppendLine($"            max: {values[1]},");
                                    formatStr = @"""validator.template.max""";
                                    str.Add($@" this.$t(""{this.Entity.Name}.validator.{setting.PropertyName}.Max"")");
                                }
                                if (values.All(s => !string.IsNullOrEmpty(s)))
                                {
                                    formatStr = @"""validator.template.range""";
                                }
                                var msg = $@"this.$t({formatStr}, [{string.Join(',', str)}])";
                                sb.AppendLine($@"            trigger: ""{triggerStr}"",");
                                sb.AppendLine($@"            message: () => {msg}");
                                sb.AppendLine("          },");
                            }
                        }
                        break;
                    default:
                        sb.AppendLine($"Invalidate {rule.Validate}");
                        break;
                }
            }
            sb.Append("        ]");
            return sb.ToString();
        }

        public override Type SpecificHostType => typeof(ESysHost);
    }

    internal class ESysVueGenerator : IGenerator
    {
        public byte[] Generate(Project project, IEnumerable<string> selectedFullName)
        {
            var listTemplate = GetListTemplate();
            var queryModalTemplate = GetQueryTemplate();
            var editModalTemplate = GetEditTemplate();
            var chineseTemplate = GetI18nTemplate(true);
            var englishTemplate = GetI18nTemplate(false);
            using var ms = new MemoryStream();
            {
                using var archive = new ZipArchive(ms, ZipArchiveMode.Update);
                void writeEntry(string entryName, string entityFullName, string template)
                {
                    var entry = archive.CreateEntry(entryName);
                    using var writer = new StreamWriter(entry.Open());
                    writer.Write(Generate(project, entityFullName, template));
                }
                foreach (var item in selectedFullName)
                {
                    var viewSetting = project.ViewSettings.FirstOrDefault(vs => vs.EntityFullName == item);
                    var name = item.Split('.').Last();
                    writeEntry($"{name}.vue", item, listTemplate);
                    writeEntry($"zh-cn/{name}.js", item, chineseTemplate);
                    writeEntry($"en/{name}.js", item, englishTemplate);
                    writeEntry($"QueryModal/{name}Query.vue", item, queryModalTemplate);
                    writeEntry($"EditModal/{name}Editor.vue", item, editModalTemplate);
                }
            }
            var data = ms.ToArray();
            return data;
        }

        public IEnumerable<UITypeSelection> GetUITypeSelections(Project project)
        {
            var entityTypes = project.Schema.Entities.Select(entity => entity.FullName).ToArray();
            var enumTypes = project.Schema.Enums.Select(em => em.FullName);
            var propertyTypes = project.Schema.Entities
                .SelectMany(entity => entity.Properties.Select(p => p.TypeName))
                .Distinct()
                .ToArray();
            var ret = new List<UITypeSelection>(new[]
            {
                new UITypeSelection(){ TypeFullName=typeof(int).FullName, Selections=new []{ UIType.Input }},
                new UITypeSelection(){ TypeFullName=typeof(uint).FullName, Selections=new []{ UIType.Input }},
                new UITypeSelection(){ TypeFullName=typeof(ulong).FullName, Selections=new []{ UIType.Input }},
                new UITypeSelection(){ TypeFullName=typeof(long).FullName, Selections=new []{ UIType.Input }},
                new UITypeSelection(){ TypeFullName=typeof(string).FullName, Selections=new []{ UIType.Input }},
                new UITypeSelection(){ TypeFullName=typeof(decimal).FullName, Selections=new []{ UIType.Input }},
                new UITypeSelection(){ TypeFullName=typeof(TimeSpan).FullName, Selections=new []{ UIType.Input }},
                new UITypeSelection(){ TypeFullName=typeof(double).FullName, Selections=new []{ UIType.Input }},
                new UITypeSelection(){ TypeFullName=typeof(float).FullName, Selections=new []{ UIType.Input }},
                new UITypeSelection(){ TypeFullName=typeof(DateTimeOffset).FullName, Selections=new []{UIType.Date,  UIType.DateTime, UIType.DateRange, UIType.DateTimeRange }},
                new UITypeSelection(){ TypeFullName=typeof(bool).FullName, Selections=new []{ UIType.Switch, UIType.Radio, UIType.CheckBox }},
                new UITypeSelection(){ TypeFullName=typeof(DateTime).FullName, Selections=new []{UIType.Date,  UIType.DateTime, UIType.DateRange, UIType.DateTimeRange }},
            });
            ret.AddRange(enumTypes.Select(em => new UITypeSelection() { TypeFullName = em, Selections = new[] { UIType.CheckBox, UIType.MultipleSelect, UIType.SingleSelect, UIType.Radio } }));
            ret.AddRange(entityTypes.Select(et => new UITypeSelection() { TypeFullName = et, Selections = new[] { UIType.MultipleSelect, UIType.SingleSelect } }));

            var except = propertyTypes.Except(ret.Select(t => t.TypeFullName));
            if (except.Any())
            {
                throw new Exception($"类型不全{string.Join(',', except)}");
            }
            return ret;
        }

        private static string Generate(Project project, string entityFullName, string template)
        {
            var entity = project.Schema.Entities.FirstOrDefault(e => e.FullName == entityFullName);
            var cfg = project.ViewSettings.FirstOrDefault(s => s.EntityFullName == entityFullName);
            if (entity == null || cfg == null)
            {
                return string.Empty;
            }
            //string.Join(',', entity.Keys.Union(cfg.Columns.Where(c => !c.IsNavigation).Select(c => c.Display)).Union(cfg.Editors.Where(c => !c.IsNavigation).Select(c => c.Display)).Distinct());
            //string.Join(',', cfg.Columns.Cast<ISetting>().Union(cfg.Editors).Where(c => c.IsNavigation).GroupBy(c => c.PropertyName).Select(g => $@"{ g.Key} ($select ={ string.Join(',', g.Select(c => c.Display))})"));
            var host = new ESysHost() { Project = project, Entity = entity, Config = cfg };
            //using var reader = new StringReader(template);
            //var line = reader.ReadLine();
            //var idx = 1;
            //while (line != null)
            //{
            //    System.Diagnostics.Debug.WriteLine($"{idx++:d3}\t{line}");
            //    line = reader.ReadLine();
            //}
            host.Refs.Add(typeof(Project).Assembly.Location);
            host.Refs.Add(typeof(ProjectHost).Assembly.Location);
            host.Imports.Add(typeof(Project).Namespace);
            host.Imports.Add(typeof(List<>).Namespace);
            try
            {
                using var ct = host.CompileTemplate(template);
                if (ct == null)
                {
                    foreach (CompilerError error in host.Errors)
                    {
                        Console.WriteLine($"{error.Line}\t{error.Column}\t{error}");
                    }
                    return string.Empty;
                }
                else
                {
                    var str = ct.Process();
                    foreach (CompilerError error in host.Errors)
                    {
                        Console.WriteLine($"{error.Line}\t{error.Column}\t{error}");
                    }
                    //Console.WriteLine(str);
                    return str;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return string.Empty;
            }
        }

        private static string GetUIElementTemplate(string typePerfix, string modelName)
        {
            return $@"<#  switch(filter.Type)
    {{
        case UIType.Input:#>
              <el-input
                ref=""{modelName}.<#= filter.Display #>""
                v-model=""{modelName}.<#= filter.Display #>""
                :placeholder=""$t('<#= Host.Entity.Name #>.{typePerfix}.<#= filter.PropertyName #>')""
              ></el-input>
<#          break;
        case UIType.DateRange:#>
              <el-date-picker
                v-model=""{modelName}.<#= filter.Display #>""
                type=""daterange""
              />
<#          break;
        case UIType.DateTimeRange:#>
              <el-date-picker
                v-model=""{modelName}.<#= filter.Display #>""
                type=""datetimerange""
              />
<#          break;
        case UIType.MultipleSelect:
            if(Host.Project.Schema.Entities.Any(entity=>entity.FullName == filter.TypeName))
            {{#>
              <o-data-selector
                :placeholder=""`${{$t('template.include', [$t('<#= Host.Entity.Name #>.{typePerfix}.<#= filter.PropertyName #>')])}}`""
                :multiple=""true""
                :activeOnly=""<#= Host.Entity.Properties.Any(p=>p.Name == ""IsActive"") ? ""true"" : ""false"" #>""
                v -model=""{modelName}.<#= filter.PropertyName #>""
                entity=""<#= filter.TypeName #>""
                label=""<#= filter.Display #>""
                value=""Id""
              />
<#          }}
            else if(Host.Project.Schema.Enums.Any(em=>em.FullName == filter.TypeName))
            {{
                var enumInfo = Host.Project.Schema.Enums.First(em=>em.FullName == filter.TypeName);#>
              <el-select v-model=""{modelName}.<#= filter.PropertyName #>"" multiple :placeholder=""`${{$t('template.include', [$t('<#= Host.Entity.Name #>.{typePerfix}.<#= filter.PropertyName #>')])}}`"">
<#              foreach(var member in enumInfo.Members)
                {{#>
                <el-option
                  label=""<#= member.Desc #>""
                  value=""<#= enumInfo.FullName #>'<#= member.Name #>'""
                />
<#              }}#>
              </el-select>
<#          }}
            else
            {{
                throw new Exception($""not implement in GetUIElementTemplate {{filter.Type}}""); 
            }}
            break;
        case UIType.Date:#>
              <el-date-picker
                v-model=""{modelName}.<#= filter.Display #>""
                type=""date""
                :placeholder=""`${{$t('template.select', [$t('<#= Host.Entity.Name #>.{typePerfix}.<#= filter.PropertyName #>')])}}`""
              />
<#          break;
        case UIType.DateTime:#>
              <el-date-picker
                v-model=""{modelName}.<#= filter.Display #>""
                type=""datetime""
                :placeholder=""`${{$t('template.select', [$t('<#= Host.Entity.Name #>.{typePerfix}.<#= filter.PropertyName #>')])}}`""
              />
<#          break;
        case UIType.Radio:
            if(typeof(bool).FullName == filter.TypeName)
            {{#>
              <el-radio v-model=""{modelName}.<#= filter.Display #>"" label=""true"">是</el-radio>
              <el-radio v-model=""{modelName}.<#= filter.Display #>"" label=""false"">否</el-radio>
<#          }}
            else if(Host.Project.Schema.Enums.Any(em=>em.FullName == filter.TypeName))
            {{
                var enumInfo = Host.Project.Schema.Enums.First(em=>em.FullName == filter.TypeName);
                foreach(var member in enumInfo.Members)
                {{#>
              <el-radio v-model=""{modelName}.<#= filter.Display #>"" label=""<#= enumInfo.FullName #>'<#= member.Name #>'""><#= member.Desc #></el-radio>
<#              }}
            }}
            else
            {{
                throw new Exception($""not implement in GetUIElementTemplate {{filter.Type}}""); 
            }}
            break;
        case UIType.CheckBox:
            if(Host.Project.Schema.Enums.Any(em=>em.FullName == filter.TypeName))
            {{
                var enumInfo = Host.Project.Schema.Enums.First(em=>em.FullName == filter.TypeName);#>
              <el-checkbox-group v-model=""{modelName}.<#= filter.Display #>"">
<#              foreach(var member in enumInfo.Members)
                {{#>
                <el-checkbox label=""<#= enumInfo.FullName #>'<#= member.Name #>'"" ><#= member.Desc #></el-checkbox>
<#              }}#>
              </el-checkbox-group>
<#          }}
            else if(typeof(bool).FullName == filter.TypeName)
            {{#>
              <el-checkbox true-label=""true"" false-label=""false"" v-model=""{modelName}.<#= filter.Display #>"" />
<#          }}
            else
            {{
                throw new Exception($""not implement in GetUIElementTemplate {{filter.Type}}""); 
            }}
            break;
        case UIType.Switch:
            if(typeof(bool).FullName == filter.TypeName)
            {{#>
              <el-switch
                v-model=""{modelName}.<#= filter.Display #>""
                :active-value=""true""
                :inactive-value=""false""
              />
<#          }}
            else
            {{
                throw new Exception($""not implement in GetUIElementTemplate {{filter.Type}}""); 
            }}
            break;
        case UIType.SingleSelect:
            if(Host.Project.Schema.Entities.Any(entity=>entity.FullName == filter.TypeName))
            {{#>
              <o-data-selector
                ref=""<#= filter.PropertyName.Substring(0, 1).ToLower() + filter.PropertyName.Substring(1) #>""
                :placeholder=""`${{$t('template.select', [$t('<#= Host.Entity.Name #>.{typePerfix}.<#= filter.PropertyName #>')])}}`""
                :multiple=""false""
                :activeOnly=""<#= Host.Entity.Properties.Any(p=>p.Name == ""IsActive"") ? ""true"" : ""false"" #>""
                v-model=""{modelName}.<#= filter.PropertyName #>Id""
                entity=""<#= filter.TypeName #>""
                label=""<#= filter.Display #>""
                value=""Id""
              />
<#          }}
            else if(Host.Project.Schema.Enums.Any(em=>em.FullName == filter.TypeName))
            {{
                var enumInfo = Host.Project.Schema.Enums.First(em=>em.FullName == filter.TypeName);#>
              <el-select v-model=""{modelName}.<#= filter.PropertyName #>"" :placeholder=""`${{$t('template.select', [$t('<#= Host.Entity.Name #>.{typePerfix}.<#= filter.PropertyName #>')])}}`"">
<#              foreach(var member in enumInfo.Members)
                {{#>
                <el-option
                  label=""<#= member.Desc #>""
                  value=""<#= enumInfo.FullName #>'<#= member.Name #>'""
                />
<#              }}#>
              </el-select>
<#          }}
            else
            {{
                throw new Exception($""not implement in GetUIElementTemplate {{filter.Type}}""); 
            }}
            break;
        default:
            throw new Exception($""not implement in GetUIElementTemplate {{filter.Type}} thrown by defalut""); 
            break;
    }}#>";
        }

        private static string GetListTemplate()
        {
            return $@"<#@template language=""C#"" hostspecific=""true"" #>
<#@ Import Namespace=""System.Linq"" #><template>
  <div>
    <!-- 查询区域 -->
    <div class=""table-search"">
      <el-form ref=""queryForm"" :inline=""true"" :model=""queryModel"">
        <el-row :gutter=""24"">
<# foreach(var filter in Host.Config.Filters.Where(f=>f.AllwaysShow))
{{#>
          <el-col :xl=""6"" :lg=""7"" :md=""8"" :sm=""24"" >
            <el-form-item :label=""$t('<#= Host.Entity.Name #>.filter.<#= filter.PropertyName #>')"" prop=""<#= filter.PropertyName #>"">
{GetUIElementTemplate("filter", "queryModel")}
            </el-form-item>
          </el-col>
<#}}#>
          <el-col :xl=""6"" :lg=""7"" :md=""8"" :sm=""24"">
            <span
              style=""float: left; overflow: hidden""
              class=""table-page-search-submitButtons""
            >
              <el-button
                type=""primary""
                @click=""onSearchClick""
              >
                {{{{$t('template.search')}}}}
                <el-icon class=""el-icon--right"">
                  <svg-icon icon-class=""edit"" />
                </el-icon>
              </el-button>
              <el-button
                type=""primary""
                @click=""onResetSearchClick""
                style=""margin-left: 8px""
              >
                {{{{$t('template.reset')}}}}
                <el-icon class=""el-icon--right"">
                  <svg-icon icon-class=""refresh"" />
                </el-icon>
              </el-button>
<# if(Host.Config.Filters.Any(f=>!f.AllwaysShow))
{{#>
              <el-button
                type=""primary""
                @click=""queryModalVisible=true""
                style=""margin-left: 8px""
                >{{{{$t('template.advanced')}}}}
                <el-icon class=""el-icon--right"">
                  <svg-icon icon-class=""operation"" />
                </el-icon>
              </el-button>
<#}}#>
            </span>
          </el-col>
        </el-row>
      </el-form>
    </div>
    <!-- table区域-begin -->
    <el-table
      ref=""dataTable""
      :always=""true""
      :data=""tableData.data""
      border
      stripe
      style=""width: 100%""
      @sort-change=""onSortChange""
      @selection-change=""onSelectionChange""
    >
<# foreach(var column in Host.Config.Columns)
{{
    var columnWidthAttribute = column == Host.Config.Columns.Last() ? string.Empty : ""\rwidth='150'"";
    if(!column.IsNavigation)
    {{#>      <el-table-column
        prop=""<#= column.Display #>""
        :label=""$t('<#= Host.Entity.Name #>.column.<#= column.PropertyName #>')""
        sortable=""custom"" <#= columnWidthAttribute #>
      />
<#  }}
    else if(!column.IsCollection)
    {{#>
      <el-table-column :label=""$t('<#= Host.Entity.Name #>.column.<#= column.PropertyName #>')"" prop=""<#= column.PropertyName #>.<#= column.Display #>""  <#= columnWidthAttribute #>/>
<#  }}
    else
    {{#>
      <el-table-column :label=""$t('<#= Host.Entity.Name #>.column.<#= column.PropertyName #>')"" :formatter=""format<#= column.PropertyName #><#= column.Display #>"" />
<#  }}#>
<#}}#>
      <el-table-column fixed=""right"" :label=""$t('template.operation')"" width=""100"" v-has=""['<#= Host.Entity.Name #>:Add', '<#= Host.Entity.Name #>:Edit', '<#= Host.Entity.Name #>:Disable']"">
        <template #header>
          <div class=""table-operation-header"">
            <span>{{{{ $t(""template.operation"") }}}}</span>
            <el-button
              v-has=""'<#= Host.Entity.Name #>:Add'""
              type=""text""
              @click=""onAddClick""
            >
              {{{{ $t(""template.add"") }}}}
            </el-button>
          </div>
        </template>
        <template #default=""scope"">
          <el-button
            type=""text""
            @click.prevent=""setIsActive(scope.$index, !scope.row.IsActive)""
            v-has=""'<#= Host.Entity.Name #>:Disable'""
          >
            {{{{ scope.row.IsActive ? $t('template.disable') : $t('template.enable')}}}}
          </el-button>
          <el-button
            type=""text""
            @click.prevent=""editRow(scope.$index)""
            v-has=""'<#= Host.Entity.Name #>:Edit'""
          >
            {{{{$t('template.edit')}}}}
          </el-button>
        </template>
      </el-table-column>
    </el-table>
    <el-pagination
      class=""table-pagination""
      v-model:currentPage=""tableData.current""
      v-model:pageSize=""tableData.pageSize""
      :page-sizes=""[10, 20, 50]""
      layout=""total, prev, pager, next, sizes, jumper""
      :total=""tableData.total""
      :pager-count=""5""
    >
    </el-pagination>
    <<#= Host.Entity.Name.ToLower() #>-query
      v-model:visible=""queryModalVisible""
      v-model:queryModel=""queryModel""
      @search=""loadData""
    />
    <<#= Host.Entity.Name.ToLower() #>-editor
      v-model:visible=""editModalVisible""
      v-model:model=""editModel""
      v-model:createNew=""createNew""
      @accept=""onEditAccept""
    />
  </div>
</template>

<script>
import {{ defineComponent }} from ""vue"";
import map from ""lodash.map"";
import {{ ListMixin }} from ""@/mixins/ListMixin"";
<#if(Host.Config.Filters.Any(f=>(f.Type == UIType.MultipleSelect || f.Type == UIType.SingleSelect) && Host.Project.Schema.Entities.Any(entity=>entity.FullName == f.TypeName)) )
{{
#>
import ODataSelector from ""@/components/ODataSelector.vue"";
<#
}}#>
import <#= Host.Entity.Name #>Query from ""./QueryModal/<#= Host.Entity.Name #>Query.vue"";
import <#= Host.Entity.Name #>Editor from ""./EditModal/<#= Host.Entity.Name #>Editor.vue"";
export default defineComponent({{
  name: ""<#= Host.Entity.Name #>List"",
<#if(Host.Config.Filters.Any(f=>(f.Type == UIType.MultipleSelect || f.Type == UIType.SingleSelect) && Host.Project.Schema.Entities.Any(entity=>entity.FullName == f.TypeName)) )
{{
#>
  components: {{ ODataSelector, <#= Host.Entity.Name #>Query, <#= Host.Entity.Name #>Editor }},
<#}}
else{{
#>
  components: {{ <#= Host.Entity.Name #>Query, <#= Host.Entity.Name #>Editor }},
<#}}#>
  mixins: [ListMixin],
  data() {{
    return {{
      entityName: ""<#= Host.Entity.FullName #>"",
      queryModel: {{
    <# foreach(var filter in Host.Config.Filters)
       {{#>
                <#= filter.PropertyName #>: undefined,
    <# }} #>
      }},
      query: {{
        <# var alwaylsSelect = Host.Entity.Properties.Any(p=>p.Name == ""IsActive"") ? new string [] {{""IsActive""}} : Array.Empty<string>();
           var oneOneSelect = Host.Config.Editors.Where(c=>c.IsNavigation && !c.IsCollection).Select(c=>c.PropertyName + ""Id"").ToArray();
           if(Host.Config.Columns.Any(c=>c.IsNavigation) || Host.Config.Editors.Any(e=>e.IsNavigation))
           {{ 
                var dic1 = Host.Config.Columns.Where(c=>c.IsNavigation).GroupBy(c => c.PropertyName).ToDictionary(g=>g.Key, g=>new List<string>(g.Select(c=>c.Display))); 
                var dic2 = Host.Config.Editors.Where(c=>c.IsNavigation).GroupBy(c => c.PropertyName).ToDictionary(g=>g.Key, g=>new List<string>(g.Select(c=>c.Display))); 
                foreach(var kvp in dic2)
                {{
                    if(!dic1.TryGetValue(kvp.Key, out var list))
                    {{
                        list = new List<string>();
                        dic1[kvp.Key] = list;
                    }}
                    list.AddRange(kvp.Value);
                }}
#>
        $expand: ""<#= string.Join(',', dic1.Select(kvp=>$""{{kvp.Key}}($select={{string.Join(',', new []{{""Id""}}.Union(kvp.Value).Distinct())}})"")) #>"",
        <# }} #>
        $select: ""<#= string.Join(',', Host.Entity.Keys.Union(Host.Config.Columns.Where(c=>!c.IsNavigation).Select(c=>c.Display)).Union(Host.Config.Editors.Where(c=>!c.IsNavigation).Select(c=>c.Display)).Union(oneOneSelect).Union(alwaylsSelect).Distinct()) #>"",
      }},
      editModel: {{
    <# foreach(var editor in Host.Config.Editors)
       {{#>
                <#= editor.PropertyName #>: undefined,
    <# }} #>
      }},
    }};
  }},
  methods: {{
    buildFilterStr() {{
      let filterStr = [];
<# foreach(var filter in Host.Config.Filters)
   {{
            switch(filter.Type)
            {{
                case UIType.Input:
                    if(typeof(string).FullName == filter.TypeName)
                    {{#>
 
      if (this.queryModel.<#= filter.PropertyName #> && this.queryModel.<#= filter.PropertyName #>.length > 0) {{
        filterStr.push(`contains(<#= filter.PropertyName #>,'${{this.queryModel.<#= filter.PropertyName #>}}')`);
      }}<#          }}
                    else if(typeof(int).FullName == filter.TypeName)
                    {{#>
 
      if (this.queryModel.<#= filter.PropertyName #>) {{
        filterStr.push(`<#= filter.PropertyName #> eq ${{this.queryModel.<#= filter.PropertyName #>}}`);
      }}<#          }}
                    else
                    {{
                        throw new Exception($""not implement in buildFilterStr {{filter.Type}} {{filter.TypeName}}"");
                    }}
                    break;
                case UIType.DateRange:
                case UIType.DateTimeRange:#>
 
      if (this.queryModel.<#= filter.PropertyName #> && this.queryModel.<#= filter.PropertyName #>.length > 0) {{
        filterStr.push(
          `<#= filter.PropertyName #> ge ${{this.queryModel.<#= filter.PropertyName #>[0].toISOString()}}`
        );
        filterStr.push(
          `<#= filter.PropertyName #> le ${{this.queryModel.<#= filter.PropertyName #>[1].toISOString()}}`
        );
      }}<#           break;
                case UIType.MultipleSelect:
                    if(Host.Project.Schema.Entities.Any(entity=>entity.FullName == filter.TypeName))
                    {{#>

      if (this.queryModel.<#= filter.PropertyName #> && this.queryModel.<#= filter.PropertyName #>.length > 0) {{
        filterStr.push(
          `<#= filter.PropertyName #>/any(<#= Host.GetSingleNameFromCollectionName(filter.PropertyName) #>: <#= Host.GetSingleNameFromCollectionName(filter.PropertyName) #>/Id in (${{this.queryModel.<#= filter.PropertyName #>.join("","")}}))`
        );
      }}<#          }}
                    else if(Host.Project.Schema.Enums.Any(em=>em.FullName == filter.TypeName))
                    {{#>

      if (this.queryModel.<#= filter.PropertyName #> && this.queryModel.<#= filter.PropertyName #>.length > 0) {{
        filterStr.push(
          map(this.queryModel.<#= filter.PropertyName #>, val=>`<#= filter.PropertyName #> eq ${{val}}`).join(' or ')
        );
      }}<#          }}
                    else
                    {{
                        throw new Exception($""not implement in buildFilterStr {{filter.Type}} {{filter.TypeName}}"");
                    }}
                    break;
                case UIType.Date:
                case UIType.DateTime:#>
 
      if (this.queryModel.<#= filter.PropertyName #> ) {{
        filterStr.push(
          `<#= filter.PropertyName #> ge ${{this.queryModel.<#= filter.PropertyName #>.toISOString()}}`
        );
      }}<#           break;
                case UIType.Radio:#>
 
      if (this.queryModel.<#= filter.PropertyName #>) {{
        filterStr.push(`<#= filter.PropertyName #> eq ${{this.queryModel.<#= filter.PropertyName #>}}`);
      }}<#
                     break;
                case UIType.CheckBox:
                     if(Host.Project.Schema.Enums.Any(em=>em.FullName == filter.TypeName))
                     {{#>

      if (this.queryModel.<#= filter.PropertyName #> && this.queryModel.<#= filter.PropertyName #>.length > 0) {{
        filterStr.push(
          map(this.queryModel.<#= filter.PropertyName #>, val=>`<#= filter.PropertyName #> eq ${{val}}`).join(' or ')
        );
      }}<#
                     }}
                     else if(typeof(bool).FullName == filter.TypeName)
                     {{#>
 
      if (this.queryModel.<#= filter.PropertyName #>) {{
        filterStr.push(`<#= filter.PropertyName #> eq ${{this.queryModel.<#= filter.PropertyName #>}}`);
      }}<#           }}
                     else
                     {{
                        throw new Exception($""not implement in buildFilterStr {{filter.Type}} {{filter.TypeName}}"");
                     }}
                     break;
                case UIType.Switch:
                     if(typeof(bool).FullName == filter.TypeName)
                     {{
                        if(filter.PropertyName == ""IsActive"")
                        {{#>

      if (this.queryModel.<#= filter.PropertyName #> != true) {{
        filterStr.push(`<#= filter.PropertyName #> eq true`);
      }}<#              }}
                        else
                        {{#> 

      if (this.queryModel.<#= filter.PropertyName #> != undefined) {{
        filterStr.push(`<#= filter.PropertyName #> eq ${{this.queryModel.<#= filter.PropertyName #>}}`);
      }}<#              }}
                     }}
                     else
                     {{
                        throw new Exception($""not implement in buildFilterStr {{filter.Type}} {{filter.TypeName}}"");
                     }}
                     break;
                case UIType.SingleSelect:
                     if(Host.Project.Schema.Entities.Any(entity=>entity.FullName == filter.TypeName))
                     {{
                        if(filter.IsCollection)
                        {{#>

      if (this.queryModel.<#= filter.PropertyName #>) {{
        filterStr.push(
          `<#= filter.PropertyName #>/any(<#= Host.GetSingleNameFromCollectionName(filter.PropertyName) #>: <#= Host.GetSingleNameFromCollectionName(filter.PropertyName) #>/Id eq ${{this.queryModel.<#= filter.PropertyName #>}}))`
        );
      }}<#
                        }}
                        else
                        {{#>

      if (this.queryModel.<#= filter.PropertyName #>) {{
        filterStr.push(
          `<#= filter.PropertyName #>Id eq ${{this.queryModel.<#= filter.PropertyName #>}}`
        );
      }}<#
                        }}
                    }}
                    else if(Host.Project.Schema.Enums.Any(em=>em.FullName == filter.TypeName))
                    {{#>
 
      if (this.queryModel.<#= filter.PropertyName #>) {{
        filterStr.push(`<#= filter.PropertyName #> eq ${{this.queryModel.<#= filter.PropertyName #>}}`);
      }}
<#                  }}
                    else
                    {{
                        throw new Exception($""not implement in buildFilterStr {{filter.Type}} {{filter.TypeName}}"");
                    }}
                    break;
                default:
                    throw new Exception($""not implement in buildFilterStr {{filter.Type}}, thrown by default"");
                    break;
            }}
   }}
#>

      if (filterStr.length > 1) {{
        return map(filterStr, (f) => `(${{f}})`).join("" and "");
      }} else {{
        return filterStr.join("""");
      }}
    }},
<# foreach(var filter in Host.Config.Filters.Where(f=>f.Type == UIType.MultipleSelect))
{{#>
    on<#= filter.PropertyName #>Change(<#= filter.PropertyName.ToLower() #>) {{
      this.queryModel.<#= filter.PropertyName #> = <#= filter.PropertyName.ToLower() #>;
    }},
<#}}#>
<# foreach(var column in Host.Config.Columns.Where(c=>c.IsNavigation && c.IsCollection))
{{#>
    format<#= column.PropertyName #><#= column.Display #>(row) {{
      return map(row.<#= column.PropertyName #>, <#= Host.GetSingleNameFromCollectionName(column.PropertyName) #> => <#= Host.GetSingleNameFromCollectionName(column.PropertyName) #>.<#= column.Display #>).join("","");
    }},
<#}} if(Host.Config.Editors.Any(editor => editor.IsNavigation))
{{
    if(Host.Config.Editors.Any(editor => editor.IsNavigation && editor.IsCollection))
{{#>
    onEditRowOverride(data) {{
<#  foreach(var editConfig in Host.Config.Editors.Where(editor => editor.IsNavigation && editor.IsCollection))
    {{#>
      data.<#= editConfig.PropertyName#> = map(data.<#= editConfig.PropertyName#>, (r) => r.Id);
<#  }}#>
    }},
<#  }}#>
    onEditAcceptOverride(data) {{
<#  foreach(var editConfig in Host.Config.Editors.Where(editor => editor.IsNavigation))
    {{
        if(editConfig.IsCollection) {{#>
      data.<#= Host.Entity.Name#><#= editConfig.PropertyName#> = map(data.<#= editConfig.PropertyName#>, (r) => {{ return {{ <#= editConfig.TypeName.Split('.').Last()#>Id: r, <#= Host.Entity.Name#>Id: this.editModel.Id}}; }});
      delete data.<#= editConfig.PropertyName#>;
<#      }}
        else {{#>
      delete data.<#= editConfig.PropertyName#>;
<#      }}
    }}#>
  }},
<#}}#>
  }},
}});
</script>

<style lang=""scss"" scoped>
.table-pagination {{
  float: right;
  margin-top: 16px;
}}
</style>

";
        }

        private static string GetQueryTemplate()
        {
            return $@"
<#@template language=""C#"" hostspecific=""true"" #>
<#@ Import Namespace=""System.Linq"" #>
<#var labelWidth = $""{{Host.Config.Filters.Select(f=>f.Header).Max(str=>str.Length) * Host.FontSize}}px"";
#><template>
  <el-dialog v-model=""visibleInner"" :title=""`${{$t('template.search')}}-${{$t('<#= Host.Entity.Name #>.entity')}}`"" width=""400px"" @open=""onDialogOpen"">
    <template #footer>
      <span class=""dialog-footer"">
        <el-button @click=""visibleInner = false"">{{{{$t('template.cancel')}}}}</el-button>
        <el-button type=""primary"" @click=""onAcceptClick"">{{{{$t('template.search')}}}}</el-button>
      </span>
    </template>
    <el-form ref=""queryForm"" :model=""queryModelInner"" label-width=""<#=labelWidth#>"" label-position=""right"">
<#
var firstQueryInput = Host.Config.Filters.FirstOrDefault(e=>e.Type == UIType.Input);
var firstQueryInputRef = firstQueryInput == null ? null : $""queryModelInner.{{firstQueryInput.Display}}"";
foreach(var filter in Host.Config.Filters)
{{#>
            <el-form-item :label=""$t('<#= Host.Entity.Name #>.filter.<#= filter.PropertyName #>')"" prop=""<#= filter.PropertyName #>"">
{GetUIElementTemplate("filter", "queryModelInner")}
            </el-form-item>
<#}}#>
    </el-form>
  </el-dialog>
</template>

<script>
import {{ defineComponent, toRaw, computed }} from ""vue"";
import cloneDeep from ""lodash.clonedeep"";

<#if(Host.Config.Filters.Any(f=>(f.Type == UIType.MultipleSelect || f.Type == UIType.SingleSelect) && Host.Project.Schema.Entities.Any(entity=>entity.FullName == f.TypeName)) )
{{
#>
import ODataSelector from ""@/components/ODataSelector.vue"";
<#
}}#>
export default defineComponent({{
  name: ""<#= Host.Entity.Name #>Search"",
<#if(Host.Config.Filters.Any(f=>(f.Type == UIType.MultipleSelect || f.Type == UIType.SingleSelect) && Host.Project.Schema.Entities.Any(entity=>entity.FullName == f.TypeName)) )
{{
#>
  components: {{ ODataSelector }},
<#}}#>
  props: {{
    visible: {{
      type: Boolean,
      default: false,
    }},
    queryModel: {{
      type: Object,
    }},
  }},
  emits: [""update:visible"", ""update:queryModel"", ""search""],
  setup(props, ctx) {{
    const visibleInner = computed({{
      get: () => props.visible,
      set: (newVal) => ctx.emit(""update:visible"", newVal),
    }});
    return {{ visibleInner }};
  }},
  watch: {{
    visibleInner(newVal) {{
      if (newVal) {{
        let copyQuery = cloneDeep(toRaw(this.queryModel));
        this.queryModelInner = copyQuery;
      }}
      this.$nextTick(() => {{
        this.$refs.queryForm.clearValidate();
<#if(!string.IsNullOrEmpty(firstQueryInputRef))
{{#>
        this.$refs[""<#=firstQueryInputRef#>""].focus();
<#}}#>
      }});
    }},
  }},
  data() {{
    return {{
      queryModelInner: {{}},
    }};
  }},
  methods: {{
    onAcceptClick() {{
      this.$emit(""update:queryModel"", this.queryModelInner);
      this.visibleInner = false;
      this.$emit(""search"");
    }},
    onDialogOpen() {{
<#foreach(var filter in Host.Config.Filters.Where(e=> e.IsNavigation && !e.IsCollection && e.Type == UIType.SingleSelect))
{{#>
        this.$refs.<#= filter.PropertyName.Substring(0, 1).ToLower() + filter.PropertyName.Substring(1) #>.loadData();
<#}}#>
    }}
  }},
}});
</script>
";
        }

        private static string GetEditTemplate()
        {
            return $@"
<#@template language=""C#"" hostspecific=""true"" #>
<#@ Import Namespace=""System.Linq"" #>
<#var labelWidth = $""{{Host.Config.Editors.Select(e=>e.Required ? $""{{e.Header}}D"" : e.Header).Max(str=>str.Length) * Host.FontSize + 12}}px"";#><template>
  <el-dialog v-model=""visibleInner"" :title=""`${{createNew ? $t('template.new') : $t('template.edit')}}-${{$t('<#= Host.Entity.Name #>.entity')}}`"" width=""400px"" @open=""onDialogOpen"">
    <template #footer>
      <span class=""dialog-footer"">
        <el-button @click=""visibleInner = false"">{{{{$t('template.cancel')}}}}</el-button>
        <el-button type=""primary"" @click=""onAcceptClick"">{{{{$t('template.accept')}}}}</el-button>
      </span>
    </template>
    <el-form ref=""editForm"" :model=""modelInner"" :rules=""rules"" label-width=""<#=labelWidth#>"" label-position=""right"">
<# foreach(var filter in Host.Config.Editors)
{{#>
      <el-form-item :label=""$t('<#= Host.Entity.Name #>.editor.<#= filter.PropertyName #>')"" prop=""<#= filter.PropertyName #>"">
{GetUIElementTemplate("editor", "modelInner")}
      </el-form-item>
<#}}#>
    </el-form>
  </el-dialog>
</template>

<script>
import {{ defineComponent, toRaw, computed }} from ""vue"";
import cloneDeep from ""lodash.clonedeep"";
<#
var firstEditorInput = Host.Config.Editors.FirstOrDefault(e=>e.Type == UIType.Input);
var firstEditorInputRef = firstEditorInput == null ? null : $""modelInner.{{firstEditorInput.Display}}"";
if(Host.Config.Editors.Any(f=>(f.Type == UIType.MultipleSelect || f.Type == UIType.SingleSelect) && Host.Project.Schema.Entities.Any(entity=>entity.FullName == f.TypeName)) )
{{
#>
import ODataSelector from ""@/components/ODataSelector.vue"";
<#
}}#>
export default defineComponent({{
  name: ""<#= Host.Entity.Name #>Editor"",
<#if(Host.Config.Editors.Any(f=>(f.Type == UIType.MultipleSelect || f.Type == UIType.SingleSelect) && Host.Project.Schema.Entities.Any(entity=>entity.FullName == f.TypeName)) )
{{
#>
  components: {{ ODataSelector }},
<#}}#>
  props: {{
    visible: {{
      type: Boolean,
      default: false,
    }},
    model: {{
      type: Object,
    }},
    createNew: {{
      type: Boolean,
      default: false,
    }},
  }},
  emits: [""update:visible"", ""update:model"", ""accept""],
  setup(props, ctx) {{
    const visibleInner = computed({{
      get: () => props.visible,
      set: (newVal) => ctx.emit(""update:visible"", newVal),
    }});
    return {{ visibleInner }};
  }},
  watch: {{
    visibleInner(newVal) {{
      if (newVal) {{
        let copyQuery = cloneDeep(toRaw(this.model));
        this.modelInner = copyQuery;
      }}
      this.$nextTick(() => {{
        this.$refs.editForm.clearValidate();
<#if(!string.IsNullOrEmpty(firstEditorInputRef))
{{#>
        this.$refs[""<#=firstEditorInputRef#>""].focus();
<#}}#>
      }});
    }},
  }},
  data() {{
    return {{
      modelInner: {{}},
      rules: {{
        <#= string.Join("",\r\n"", Host.Config.Editors.Where(e=> !e.IsNavigation && (e.Required || e.Rules.Any())).Select(e=>Host.GetValidateRuleString(e))) #>
      }},
    }};
  }},
  methods: {{
    onAcceptClick() {{
      this.$refs.editForm.validate((valid) => {{
        if (valid) {{
          this.$emit(""update:model"", this.modelInner);
          this.visibleInner = false;
          this.$emit(""accept"");
        }} else {{
          return false;
        }}
      }});
    }},
<# if(Host.Config.Editors.Any(e=> e.IsNavigation && !e.IsCollection && e.Type == UIType.SingleSelect)){{#>
    onDialogOpen() {{
      this.$nextTick(() => {{
<#foreach(var editor in Host.Config.Editors.Where(e=> e.IsNavigation && !e.IsCollection && e.Type == UIType.SingleSelect))
{{#>
        this.$refs.<#= editor.PropertyName.Substring(0, 1).ToLower() + editor.PropertyName.Substring(1) #>.loadData();
<#}}#>
      }});
    }}
<#}}#>
  }},
}});
</script>
";
        }

        private static string GetI18nTemplate(bool chinese)
        {
            var path = chinese ? "Header" : "Display";
            var entityStr = chinese ? "Desc" : "Name";
            return $@"<#@template language=""C#"" hostspecific=""true"" #>
<#@ Import Namespace=""System.Linq"" #>export default {{
    entity: '<#= Host.Entity.{entityStr} #>',
    filter: {{
<# foreach(var filter in Host.Config.Filters){{#>
        <#= filter.PropertyName #>: ""<#= filter.{path} #>"",
<# }} #>
    }},
    column: {{
<# foreach(var column in Host.Config.Columns){{#>
        <#= column.PropertyName #>: ""<#= column.{path} #>"",
<# }} #>
    }},
    editor: {{
<# foreach(var editor in Host.Config.Editors){{#>
        <#= editor.PropertyName #>: ""<#= editor.{path} #>"",
<# }} #>
    }}
}}
";
        }
    }
}
