using ESys.App.ModelBinding;
using ESys.Contract.Db;
using ESys.Contract.Entity;
using ESys.Contract.Service;
using ESys.Db.DbContext;
using ESys.Extensions;
using ESys.Security.Handler;
using ESys.Security.Middleware;
using ESys.Service;
using ESys.Swagger;
using ESys.Utilty.Mvc;
using ESys.Utilty.Service;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using StackExchange.Redis.Extensions.Core.Configuration;
using StackExchange.Redis.Extensions.System.Text.Json;
using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;

namespace ESys.App
{
    public class ODataServiceConfigure : IODataServiceConfigure
    {
        public IServiceCollection Configure(IServiceCollection services)
        {
            var configuration = Furion.App.Configuration as IConfigurationRoot;
            services.AddSingleton<IConfigurationRoot>(configuration);
            services.AddSingleton<IConfiguration>(configuration);
            services.AddHttpContextAccessor();
            services.AddMemoryCache();
            return services;
        }
    }
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<KestrelServerOptions>(opt => opt.AllowSynchronousIO = true);
            services.Configure<IISServerOptions>(opt => opt.AllowSynchronousIO = true);
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder
                        .SetIsOriginAllowed(_ => true)
                        .AllowCredentials()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        ;
                    });
            });
            static string SchemaIdSelector(Type modelType)
            {
                if (!modelType.IsConstructedGenericType)
                {
                    if (modelType == typeof(string))
                    {
                        return "string";
                    }
                    else if (modelType == typeof(bool))
                    {
                        return "boolean";
                    }
                    else if (modelType.IsArray)
                    {
                        return modelType.Name.Replace("String", "string").Replace("Boolean", "boolean");
                    }
                    return modelType.Name;
                }
                else if (modelType.IsAssignableTo(typeof(IEnumerable)))
                {
                    return $"{string.Join(",", modelType.GetGenericArguments().Select(genericArg => SchemaIdSelector(genericArg)))}[]";
                }
                return $"{modelType.Name.Split('`', StringSplitOptions.None).First()}<{string.Join(",", modelType.GetGenericArguments().Select(genericArg => SchemaIdSelector(genericArg)))}>";

            }
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ESys", Version = "v1" });
                c.OperationFilter<AddRequiredHeader>();
                c.SchemaFilter<EnumSchemaFilter>();
                //c.CustomOperationIds(opt => $"{opt.ActionDescriptor.AttributeRouteInfo?.Name}_{Guid.NewGuid()}");
                c.SchemaGeneratorOptions.SchemaIdSelector = SchemaIdSelector;
                var xmlFiles = Furion.App.Assemblies
                   .Select(ass => Path.Combine(AppContext.BaseDirectory, $"{ass.GetName().Name}.xml"))
                   .Where(xml => File.Exists(xml))
                   .ToArray();

                foreach (var xmlFile in xmlFiles)
                {
                    c.IncludeXmlComments(xmlFile);
                }

            });


            services.AddDatabaseAccessor(options =>
            {
                Action<IServiceProvider, DbContextOptionsBuilder> dbAction = (sp, opt) =>
                {
                    var factory = sp.GetRequiredService<ILoggerFactory>();
                    opt.UseLoggerFactory(factory)
                       .EnableSensitiveDataLogging()
                       .EnableDetailedErrors()
                    ;
                };
                options
                .CustomizeMultiTenants()
                .AddDb<MasterDbContext>()
                .AddDb<TenantMasterDbContext, TenantMasterLocator>(dbAction)
                .AddDb<TenantSlaveDbContext, TenantSlaveLocator>(dbAction)

                ;
            });

            services.Configure<MvcOptions>(opt =>
            {
                // Furion 会添加AutoSaveChangesFilter，DbContext在Filter中保存
                // 导致电子签名数据项为正常2倍
                opt.Filters.Clear();
            });
            services
                .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, b =>
                {
                    b.LoginPath = "/User/Login";
                });
            services.AddJwt<AuthHandler>(enableGlobalAuthorize: true);
            services.AddSchedule();
            //services
            //    .AddJwt<AuthHandler>(options =>
            //    {
            //        options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            //        options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            //    }, enableGlobalAuthorize:true);
            services.AddLogging(lb =>
            {
                lb
                .AddConsole()
                .AddDebug()
                .AddLog4Net()
                ;
            });
            services.Scan(scan =>
            {
                // 注入IApplicationBuilderConfigure
                scan.FromAssemblies(Furion.App.Assemblies)
                    .AddClasses(c => c.AssignableTo<IApplicationBuilderConfigure>()).As<IApplicationBuilderConfigure>().WithSingletonLifetime();
                // 注入DbContext拦截器
                scan.FromAssemblies(Furion.App.Assemblies)
                    .AddClasses(c => c.AssignableTo<SaveChangesInterceptor>()).As<SaveChangesInterceptor>().WithScopedLifetime();

                scan.FromAssemblies(Furion.App.Assemblies)
                .AddClasses(c => c.Where(t => t.GetInterfaces().Any(ift => ift.IsGenericType && ift.GetGenericTypeDefinition() == typeof(IEntityCsvProcessor<>)))).As(t =>
                {
                    var type = t.GetInterfaces().FirstOrDefault(ift => ift.IsGenericType && ift.GetGenericTypeDefinition() == typeof(IEntityCsvProcessor<>));
                    return new Type[] { type };
                }).WithTransientLifetime();
            });
            services.AddHttpContextAccessor();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddScoped<IInterceptorProvider, DbInterceptorProvider>();
            services.TryAddEnumerable(
                 ServiceDescriptor.Transient<IApplicationModelProvider, ControllerNameConvention>());


            var redisConfiguration = this.Configuration.GetSection("Redis").Get<RedisConfiguration>();
            if (redisConfiguration != null)
            {
                services.AddStackExchangeRedisExtensions<SystemTextJsonSerializer>(redisConfiguration);
                services.AddStackExchangeRedisCache(options =>
                {
                    var o = new StackExchange.Redis.ConfigurationOptions();
                    foreach (var host in redisConfiguration.Hosts)
                    {
                        o.EndPoints.Add(host.Host, host.Port);
                    }
                    o.Ssl = redisConfiguration.Ssl;
                    o.Password = redisConfiguration.Password;
                    //o.Password
                    o.ConnectTimeout = redisConfiguration.ConnectTimeout;
                    o.DefaultDatabase = redisConfiguration.Database;
                    // 键名前缀
                    options.InstanceName = "ESys:";
                    options.ConfigurationOptions = o;
                });
            }
            else
            {
                services.AddDistributedMemoryCache();
            }
            services.AddViewEngine();
            services.AddScoped<ESignDataHelper>()
                .AddScoped<IDataProvider>(sp => sp.GetRequiredService<ESignDataHelper>())
                .AddScoped<IDataInjector>(sp => sp.GetRequiredService<ESignDataHelper>())
                ;

            services.AddControllers()
                .AddODataEntity(out var edmModel)
                .AddDynamicApiControllers()
                ;
            var mvcBuilder = services.AddMvc(option =>
            {
                option.EnableEndpointRouting = false;
                option.ModelBinderProviders.Insert(0, new EntityWithNamesBinderProvider());
            }).AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                options.JsonSerializerOptions.Converters.Add(new TimeSpanISO8601Converter());
                //options.JsonSerializerOptions.DictionaryKeyPolicy 
                //options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
            });

            services.AddSingleton(edmModel);

            var serviceConfigure = Furion.App.EffectiveTypes
                .Where(t => t.IsClass && t.IsAssignableTo(typeof(IServiceConfigure)))
                .ToArray();
            foreach (var configType in serviceConfigure)
            {
                var config = Activator.CreateInstance(configType) as IServiceConfigure;
#if DEBUG
                config?.Configure(services, false);
#else
                config?.Configure(services, false);
#endif
            }
            var buildConfigure = Furion.App.EffectiveTypes
                .Where(t => t.IsClass && !t.IsAbstract && t.IsAssignableTo(typeof(IMvcBuilderConfigure)))
                .ToArray();
            foreach (var buildType in buildConfigure)
            {
                var config = Activator.CreateInstance(buildType) as IMvcBuilderConfigure;
                config?.Configure(mvcBuilder);
            }
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var ass = Furion.App.Assemblies.Where(a => a.GetName().Name.Contains("ESys")).ToArray();
            //System.Diagnostics.Debugger.Launch();
            AppDomain currentDomain = AppDomain.CurrentDomain;
            var loadedAssemblies = currentDomain.GetAssemblies().Where(a => a.GetName().Name.Contains("ESys"));

            Console.WriteLine("\nLoaded assemblies in the current AppDomain:");
            foreach (Assembly assembly in loadedAssemblies)
            {
                Console.WriteLine(assembly.FullName);
            }
            //if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseMiddleware<DashboardIcoMiddleware>();
                app.UseScheduleUI();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ESys v1");
                    var sb = new StringBuilder(c.HeadContent ?? "");
                    sb.AppendLine(@$"<script type='text/javascript'>
(function ()
{{
    const overrider = () =>
    {{
        const swagger = window.ui;
        if (!swagger) 
        {{
            console.error('Swagger wasn\'t found');
            return;
        }}

        ensureAuthorization(swagger);
        reloadSchemaOnAuth(swagger);
        clearInputPlaceHolder(swagger);
        showLoginUI(swagger);
    }}

    const getAuthorization = (swagger) => swagger.auth()._root.entries.find(e => e[0] === 'authorized');
    const isAuthorized = (swagger) =>
    {{
        const auth = getAuthorization(swagger);
        return auth && auth[1].size !== 0;
    }};

    // a hacky way to append authorization header - we are basically intercepting 
    // all requests, if no authorization was attached while user did authorized himself,
    // append token to request
    const ensureAuthorization = (swagger) => 
    {{
        // retrieve bearer token from authorization
        const getBearer = () => 
        {{
            const auth = getAuthorization(swagger);
            const def = auth[1]._root.entries.find(e => e[0] === 'Bearer');
            if (!def)
                return undefined;

            const token = def[1]._root.entries.find(e => e[0] === 'value');
            if (!token)
                return undefined;

            return token[1];
        }}

        // override fetch function of Swagger to make sure
        // that on every request of the client is authorized append auth-header
        const fetch = swagger.fn.fetch;
        swagger.fn.fetch = (req) => 
        {{
            if (!req.headers.Authorization && isAuthorized(swagger)) 
            {{
                const bearer = getBearer();
                if (bearer) 
                {{
                    req.headers.Authorization = bearer;
                }}
            }}
            return fetch(req);
        }}
    }};
    // makes that once user triggers performs authorization,
    // the schema will be reloaded from backend url
    const reloadSchemaOnAuth = (swagger) => 
    {{
        const getCurrentUrl = () => 
        {{
            const spec = swagger.getState()._root.entries.find(e => e[0] === 'spec');
            if (!spec)
                return undefined;

            const url = spec[1]._root.entries.find(e => e[0] === 'url');
            if (!url)
                return undefined;

            return url[1];
        }}
        const reload = () => 
        {{
            const url = getCurrentUrl();
            if (url) 
            {{
                swagger.specActions.download(url);
            }}
        }};

        const handler = (caller, args) => 
        {{
            const result = caller(args);
            if (result.then) 
            {{
                result.then(() => reload())
            }}
            else
            {{
                reload();
            }}
            return result;
        }}

        const auth = swagger.authActions.authorize;
        swagger.authActions.authorize = (args) => handler(auth, args);
        const logout = swagger.authActions.logout;
        swagger.authActions.logout = (args) => handler(logout, args);
    }};
    /**
     * Reset input element placeholder
     * @param {{any}} swagger
     */
    const clearInputPlaceHolder = (swagger) =>
    {{
        //https://github.com/api-platform/core/blob/main/src/Bridge/Symfony/Bundle/Resources/public/init-swagger-ui.js#L6-L41
        new MutationObserver(function (mutations, self)
        {{
            var elements = document.querySelectorAll('input[type=text]');
            for (var i = 0; i < elements.length; i++)
                elements[i].placeholder = '';
        }}).observe(document, {{ childList: true, subtree: true }});
    }}
    /**
     * Show login UI
     * @param {{any}} swagger
     */
    const showLoginUI = (swagger) =>
    {{
        //https://github.com/api-platform/core/blob/main/src/Bridge/Symfony/Bundle/Resources/public/init-swagger-ui.js#L6-L41
        new MutationObserver(function (mutations, self)
        {{
            var rootDiv = document.querySelector('#swagger-ui > section > div.swagger-ui > div:nth-child(2)');
            if (rootDiv == null)
                return;

            var informationContainerDiv = rootDiv.querySelector('div.information-container.wrapper');
            if (informationContainerDiv == null)
                return;

            var descriptionDiv = informationContainerDiv.querySelector('section > div > div > div.description');
            if (descriptionDiv == null)
                return;

            var loginDiv = descriptionDiv.querySelector('div.login');
            if (loginDiv != null)
                return;

            //Check authentication
            if (isAuthorized(swagger))
                return;

            //Remove elements different from information-container wrapper
            for (var i = 0; i < rootDiv.children.length; i++)
            {{
                var child = rootDiv.children[i];
                if (child !== informationContainerDiv)
                    child.remove();
            }}

            //Create UI di login
            createLoginUI(descriptionDiv);
            
        }}).observe(document, {{ childList: true, subtree: true }});

        /**
         * Create login ui elements
         * @param {{any}} rootDiv
         */
        const createLoginUI = function (rootDiv)
        {{
            var div = document.createElement('div');
            div.className = 'login';

            rootDiv.appendChild(div);

            //UserName
            var userNameLabel = document.createElement('label');
            div.appendChild(userNameLabel);

            var userNameSpan = document.createElement('span');
            userNameSpan.innerText = 'User';
            userNameLabel.appendChild(userNameSpan);
            
            var userNameInput = document.createElement('input');
            userNameInput.type = 'text';
            userNameInput.value = 'super';
            userNameInput.style = 'margin-left: 10px; margin-right: 10px;';
            userNameLabel.appendChild(userNameInput);

            //Password
            var passwordLabel = document.createElement('label');
            div.appendChild(passwordLabel);

            var passwordSpan = document.createElement('span');
            passwordSpan.innerText = 'Password';
            passwordLabel.appendChild(passwordSpan);

            var passwordInput = document.createElement('input');
            passwordInput.type = 'password';
            passwordInput.value = 'sr160608';
            passwordInput.style = 'margin-left: 10px; margin-right: 10px;';
            passwordLabel.appendChild(passwordInput);

            //Login button
            var loginButton = document.createElement('button')
            loginButton.type = 'submit';
            loginButton.type = 'button';
            loginButton.classList.add('btn');
            loginButton.classList.add('auth');
            loginButton.classList.add('authorize');
            loginButton.classList.add('button');
            loginButton.innerText = 'Login';
            loginButton.onclick = function ()
            {{
                var userName = userNameInput.value;
                var password = passwordInput.value;

                if (userName === '' || password === '')
                {{
                    alert('Insert userName and password!');
                    return;
                }}

                login(userName, password);
            }};

            div.appendChild(loginButton);
        }}
        /**
         * Manage login
         * @param {{any}} userName UserName
         * @param {{any}} password Password
         */
        const login = function (userName, password)
        {{
            var xhr = new XMLHttpRequest();

            xhr.onreadystatechange = function ()
            {{
                if (xhr.readyState == XMLHttpRequest.DONE)
                {{
                    if (xhr.status == 200 || xhr.status == 400)
                    {{
                        var response = JSON.parse(xhr.responseText);
                        if (!response.success)
                        {{
                            alert(response.message);
                            return;
                        }}

                        var accessToken = xhr.getResponseHeader('access-token');
                        var xtoken =  xhr.getResponseHeader('x-access-token');

                        var obj = {{
                            'Bearer': {{
                                'name': 'Bearer',
                                'schema': {{
                                    'type': 'apiKey',
                                    'description': 'Please enter into field the word ""Bearer"" following by space and JWT',
                                    'name': 'Authorization',
                                    'in': 'header'
                                }},
                                value: 'Bearer ' + accessToken
                            }}
                        }};

                        swagger.authActions.authorize(obj);
                    }}
                    else
                    {{
                        alert('error ' + xhr.status);
                    }}
                }}
            }};

            xhr.open('POST', '/api/user/login', true);
            xhr.setRequestHeader('Content-Type', 'application/json');
            xhr.setRequestHeader('X-TENANT', 'Emis');

            var json = JSON.stringify({{ 'Account': userName, 'Password': password }});

            xhr.send(json);
        }}
    }}

    // append to event right after SwaggerUIBundle initialized
    window.addEventListener('load', () => setTimeout(overrider, 0), false);
}}());
</script>");
                    sb.AppendLine(@$"<style media='screen' type='text/css'>

</style>");

                    c.HeadContent = sb.ToString();

                });
            }
            //app.UseHttpsRedirection();
#if DEBUG
            app.UseODataRouteDebug();
#endif
            app.UseODataBatching();
            app.UseRouting();
            app.UseCors();
            app.UseMiddleware<TenantChecker>();
            app.UseMiddleware<ExposeAccessTokenHeader>();
            app.UseMiddleware<ODataMetaJsonMiddleware>();
            app.UseRequestLocalization("en-US");

            app.UseAuthentication();
            app.UseAuthorization();

            foreach (var config in app.ApplicationServices.GetServices<IApplicationBuilderConfigure>())
            {
                config.Configure(app);
            }

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
