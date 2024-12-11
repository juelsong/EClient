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
using System.Threading.Tasks;

namespace EntityViewGenerator.Impl
{
    internal class VueGenerator : IGenerator
    {

        public byte[] Generate(Project project, IEnumerable<string> selectedFullName)
        {
            var listTemplate = GetListTemplate();
            var queryModalTemplate = GetQueryTemplate();
            var editModalTemplate = GetEditTemplate();
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
                new UITypeSelection(){ TypeFullName=typeof(string).FullName, Selections=new []{ UIType.Input }},
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
            var host = new ProjectHost() { Project = project, Entity = entity, Config = cfg };
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

        private static string GetUIElementTemplate(string modelName)
        {
            return $@"<#  switch(filter.Type)
    {{
        case UIType.Input:#>
              <el-input
                ref=""{modelName}.<#= filter.Display #>""
                v-model=""{modelName}.<#= filter.Display #>""
                placeholder=""<#= filter.Header #>""
              ></el-input>
<#          break;
        case UIType.DateRange:#>
              <el-date-picker
                v-model=""{modelName}.<#= filter.Display #>""
                type=""daterange""
                range-separator=""至""
                start-placeholder=""起始日期""
                end-placeholder=""结束日期""
              />
<#          break;
        case UIType.DateTimeRange:#>
              <el-date-picker
                v-model=""{modelName}.<#= filter.Display #>""
                type=""datetimerange""
                range-separator=""至""
                start-placeholder=""起始日期""
                end-placeholder=""结束日期""
              />
<#          break;
        case UIType.MultipleSelect:
            if(Host.Project.Schema.Entities.Any(entity=>entity.FullName == filter.TypeName))
            {{#>
              <o-data-selector
                placeholder=""包含<#= filter.Header #>""
                :multiple=""true""
                v-model=""{modelName}.<#= filter.PropertyName #>""
                entity=""<#= filter.TypeName #>""
                label=""<#= filter.Display #>""
                value=""Id""
              />
<#          }}
            else if(Host.Project.Schema.Enums.Any(em=>em.FullName == filter.TypeName))
            {{
                var enumInfo = Host.Project.Schema.Enums.First(em=>em.FullName == filter.TypeName);#>
              <el-select v-model=""{modelName}.<#= filter.PropertyName #>"" multiple placeholder=""包含<#= filter.Header #>"">
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
                placeholder=""<#= filter.Header #>""
              />
<#          break;
        case UIType.DateTime:#>
              <el-date-picker
                v-model=""{modelName}.<#= filter.Display #>""
                type=""datetime""
                placeholder=""<#= filter.Header #>""
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
                active-text=""是""
                inactive-text=""否""
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
                placeholder=""包含<#= filter.Header #>""
                :multiple=""false""
                v-model=""{modelName}.<#= filter.PropertyName #>""
                entity=""<#= filter.TypeName #>""
                label=""<#= filter.Display #>""
                value=""Id""
              />
<#          }}
            else if(Host.Project.Schema.Enums.Any(em=>em.FullName == filter.TypeName))
            {{
                var enumInfo = Host.Project.Schema.Enums.First(em=>em.FullName == filter.TypeName);#>
              <el-select v-model=""{modelName}.<#= filter.PropertyName #>"" placeholder=""<#= filter.Header #>"">
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
            return $@"
<#@template language=""C#"" hostspecific=""true"" #>
<#@ Import Namespace=""System.Linq"" #><template>
  <div>
    <!-- 查询区域 -->
    <div class=""table-search"">
      <el-form ref=""queryForm"" :inline=""true"" :model=""queryModel"">
        <el-row :gutter=""24"">
<# foreach(var filter in Host.Config.Filters.Where(f=>f.AllwaysShow))
{{#>
          <el-col :xl=""6"" :lg=""7"" :md=""8"" :sm=""24"" >
            <el-form-item label=""<#= filter.Header #>"" prop=""<#= filter.PropertyName #>"">
{GetUIElementTemplate("queryModel")}
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
                icon=""icon-el-search""
              >
                查询
              </el-button>
              <el-button
                type=""primary""
                @click=""onResetSearchClick""
                icon=""icon-el-refresh-right""
                style=""margin-left: 8px""
              >
                重置
              </el-button>
<# if(Host.Config.Filters.Any(f=>!f.AllwaysShow))
{{#>
              <el-button
                type=""primary""
                @click=""queryModalVisible=true""
                icon=""icon-el-operation""
                style=""margin-left: 8px""
                >高级
              </el-button>
<#}}#>
            </span>
          </el-col>
        </el-row>
      </el-form>
    </div>
    <div class=""el-alert el-alert--info is-light"" style=""margin-bottom: 16px"">
      <i class=""anticon anticon-info-circle ant-alert-icon""></i>已选择&nbsp;<a
        style=""font-weight: 600""
        >{{{{ multipleSelection.length }}}}</a
      >&nbsp;项&nbsp;
      <el-link :underline=""false"" @click=""onClearSelected"">清空</el-link>
    </div>

    <!-- 操作按钮区域 -->
    <div class=""table-operator"" v-has=""['<#= Host.Entity.Name #>:Add', '<#= Host.Entity.Name #>:Del']"">
      <el-button v-has=""'<#= Host.Entity.Name #>:Add'"" type=""primary"" @click=""onAddClick"">
        新增
      </el-button>
      <el-button v-has=""'<#= Host.Entity.Name #>:Del'"" type=""primary"" @click=""batchDelete"">
        批量删除
      </el-button>
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
      <el-table-column fixed type=""selection"" width=""35"" />
<# foreach(var column in Host.Config.Columns)
{{
    if(!column.IsNavigation)
    {{#>      <el-table-column
        prop=""<#= column.Display #>""
        label=""<#= column.Header #>""
        sortable=""custom""
        width=""150""
      />
<#  }}
    else if(!column.IsCollection)
    {{#>
        TODO
<#  }}
    else
    {{#>
      <el-table-column label=""<#= column.Header #>"" :formatter=""format<#= column.PropertyName #><#= column.Display #>"" />
<#  }}#>
<#}}#>
      <el-table-column fixed=""right"" label=""操作"" width=""120"" v-has=""['<#= Host.Entity.Name #>:Edit', '<#= Host.Entity.Name #>:Del']"">
        <template #default=""scope"">
          <el-button
            type=""text""
            @click.prevent=""deleteRow(scope.$index)""
            v-has=""'<#= Host.Entity.Name #>:Del'""
          >
            删除
          </el-button>
          <el-button
            type=""text""
            @click.prevent=""editRow(scope.$index)""
            v-has=""'<#= Host.Entity.Name #>:Edit'""
          >
            编辑
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
<#if(Host.Config.Filters.Any(f=>(f.Type == UIType.MultipleSelect || f.Type == UIType.SingleSelect) && Host.Project.Schema.Entities.Any(entity=>entity.FullName == f.TypeName)) 
    ||Host.Config.Editors.Any(f=>(f.Type == UIType.MultipleSelect || f.Type == UIType.SingleSelect) && Host.Project.Schema.Entities.Any(entity=>entity.FullName == f.TypeName)) )
{{
#>
import ODataSelector from ""@/components/ODataSelector.vue"";
<#
}}#>
import <#= Host.Entity.Name #>Query from ""./QueryModal/<#= Host.Entity.Name #>Query.vue"";
import <#= Host.Entity.Name #>Editor from ""./EditModal/<#= Host.Entity.Name #>Editor.vue"";
export default defineComponent({{
  name: ""<#= Host.Entity.Name #>List"",
<#if(Host.Config.Filters.Any(f=>(f.Type == UIType.MultipleSelect || f.Type == UIType.SingleSelect) && Host.Project.Schema.Entities.Any(entity=>entity.FullName == f.TypeName)) 
    ||Host.Config.Editors.Any(f=>(f.Type == UIType.MultipleSelect || f.Type == UIType.SingleSelect) && Host.Project.Schema.Entities.Any(entity=>entity.FullName == f.TypeName)) )
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
        <# if(Host.Config.Columns.Any(c=>c.IsNavigation) || Host.Config.Editors.Any(e=>e.IsNavigation))
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
        $select: ""<#= string.Join(',', Host.Entity.Keys.Union(Host.Config.Columns.Where(c=>!c.IsNavigation).Select(c=>c.Display)).Union(Host.Config.Editors.Where(c=>!c.IsNavigation).Select(c=>c.Display)).Distinct()) #>"",
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
                        throw new Exception($""not implement in buildFilterStr {{filter.Type}}"");
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
                        throw new Exception($""not implement in buildFilterStr {{filter.Type}}"");
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
                        throw new Exception($""not implement in buildFilterStr {{filter.Type}}"");
                     }}
                     break;
                case UIType.Switch:
                     if(typeof(bool).FullName == filter.TypeName)
                     {{#>
 
      if (this.queryModel.<#= filter.PropertyName #> != undefined) {{
        filterStr.push(`<#= filter.PropertyName #> eq ${{this.queryModel.<#= filter.PropertyName #>}}`);
      }}<#           }}
                     else
                     {{
                        throw new Exception($""not implement in buildFilterStr {{filter.Type}}"");
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
                        throw new Exception($""not implement in buildFilterStr {{filter.Type}}""); 
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
<#}} if(Host.Config.Editors.Any(editor => editor.IsNavigation && editor.IsCollection))
{{#>
    onEditRowOverride(data) {{
<#  foreach(var editConfig in Host.Config.Editors.Where(editor => editor.IsNavigation && editor.IsCollection))
    {{#>
      data.<#= editConfig.PropertyName#> = map(data.<#= editConfig.PropertyName#>, (r) => r.Id);
<#  }}#>
    }},
    onEditAcceptOverride(data) {{
<#  foreach(var editConfig in Host.Config.Editors.Where(editor => editor.IsNavigation && editor.IsCollection))
    {{#>
      data.<#= Host.Entity.Name#><#= editConfig.PropertyName#> = map(data.<#= editConfig.PropertyName#>, (r) => {{ return {{ <#= editConfig.TypeName.Split('.').Last()#>Id: r, <#= Host.Entity.Name#>Id: this.editModel.Id}}; }});
      delete data.<#= editConfig.PropertyName#>;
<#  }}#>
    }},
<#}}#>
  }},
}});
</script>

<style lang=""scss"" scoped>
.el-alert--info.is-light {{
  background-color: var(--el-color-primary-light-9);
  border: 1px solid var(--el-color-primary-light-5);
  a {{
    color: var(--el-color-primary);
  }}
}}
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
  <el-dialog v-model=""visibleInner"" title=""查询-<#= Host.Entity.Desc #>"" width=""400px"">
    <template #footer>
      <span class=""dialog-footer"">
        <el-button @click=""visibleInner = false"">取消</el-button>
        <el-button type=""primary"" @click=""onAcceptClick"">查询</el-button>
      </span>
    </template>
    <el-form ref=""queryForm"" :model=""queryModelInner"" label-width=""<#=labelWidth#>"" label-position=""right"">
<#
var firstQueryInput = Host.Config.Filters.FirstOrDefault(e=>e.Type == UIType.Input);
var firstQueryInputRef = firstQueryInput == null ? null : $""queryModelInner.{{firstQueryInput.Display}}"";
foreach(var filter in Host.Config.Filters)
{{#>
            <el-form-item label=""<#= filter.Header #>"" prop=""<#= filter.PropertyName #>"">
{GetUIElementTemplate("queryModelInner")}
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
<#var labelWidth = $""{{Host.Config.Editors.Select(e=>e.Required ? $""{{e.Header}}D"" : e.Header).Max(str=>str.Length) * Host.FontSize + 12}}px"";

#><template>
  <el-dialog v-model=""visibleInner"" :title=""`${{createNew ? '新建' : '编辑'}}-<#= Host.Entity.Desc #>`"" width=""400px"">
    <template #footer>
      <span class=""dialog-footer"">
        <el-button @click=""visibleInner = false"">取消</el-button>
        <el-button type=""primary"" @click=""onAcceptClick"">确定</el-button>
      </span>
    </template>
    <el-form ref=""editForm"" :model=""modelInner"" :rules=""rules"" label-width=""<#=labelWidth#>"" label-position=""right"">
<# foreach(var filter in Host.Config.Editors)
{{#>
      <el-form-item label=""<#= filter.Header #>"" prop=""<#= filter.PropertyName #>"">
{GetUIElementTemplate("modelInner")}
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
  }},
}});
</script>
";
        }


    }

    public class ProjectHost : TemplateGenerator
    {
        //private static readonly Dictionary<int, double> fontSizeDic = new()
        //{
        //    { 12, 16 },
        //    { 14, 18.7 },
        //    { 16, 21.3 },
        //    { 18, 24 },
        //    { 22, 29.3 },
        //    { 24, 32 },
        //    { 26, 34.7 },
        //    { 36, 48 },
        //    { 42, 56 },
        //    { 54, 71.7 },
        //    { 63, 83.7 },
        //    { 72, 95.6 },
        //};
        public Project Project { get; set; }
        public EntityInfo Entity { get; set; }
        public ViewSetting Config { get; set; }
        public int FontSize { get; set; } = 14;
        public string GetSingleNameFromCollectionName(string collectionName)
        {
            return collectionName[0..^1].ToLower();
        }
        public string GetValidateRuleString(EditSetting setting)
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
                return setting.TypeName == typeof(DateTime).FullName || setting.TypeName == typeof(DateTimeOffset).FullName ? "date" : "any";
            }
            string getMsgString(ValidateRule rule, string defaultStr = null)
            {
                return string.IsNullOrEmpty(rule.ErrorMsg) ? defaultStr : rule.ErrorMsg;
            }
            var sb = new StringBuilder($"        {setting.Display}: [\r\n");
            if (setting.Required)
            {
                sb.AppendLine($@"          {{
            required: true,
            message: ""{setting.Header}必填"",
            trigger: ""blur"",
          }},");
            }
            if (setting.Unique)
            {
                sb.AppendLine($@"          {{
            validator: (rule, value, callback) => {{
              if(value) {{
                this.$exist('{entityFullName}', '{setting.PropertyName}', value, this.modelInner.Id).then(exist=>{{
                  if(exist){{callback(new Error('{setting.Header}重复'));}}
                  else{{callback();}}
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
            message: ""{getMsgString(rule, $"{setting.Header}格式错误")} "",
          }},");
                        break;
                    case ValidateType.Type:
                        sb.AppendLine($@"          {{
            type: ""{getTypeString()}"",
            trigger: ""{triggerStr}"",
            message: ""{getMsgString(rule, $"{setting.Header}类型错误")} "",
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
                                var str = "";
                                if (!string.IsNullOrEmpty(values[0]))
                                {
                                    sb.AppendLine($"            min: {values[0]},");
                                    str = $"{setting.Header}必须大于{values[0]}";
                                }
                                if (!string.IsNullOrEmpty(values[1]))
                                {
                                    sb.AppendLine($"            max: {values[1]},");
                                    str = $"{setting.Header}必须小于{values[1]}";
                                }
                                if (values.All(s => !string.IsNullOrEmpty(s)))
                                {
                                    str = $"{setting.Header}必须在{values[0]},{values[1]}之间";
                                }
                                sb.AppendLine($@"            trigger: ""{triggerStr}"",");
                                sb.AppendLine($@"            message: ""{getMsgString(rule, str)} "",");
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


        public override Type SpecificHostType => typeof(ProjectHost);
    }
}
