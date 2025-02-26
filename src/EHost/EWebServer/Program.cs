using EHost.Contract.Model;
using EHost.Contract.Service;
using EHost.Db;
using EHost.Db.Service;
using EWebServer.Db;
using EWebServer.Helper;
using EWebServer.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Configuration;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddLog4Net("log4net.config");

builder.Services.AddEndpointsApiExplorer();
// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();
builder.Configuration.AddConfiguration(configuration);


builder.Services.AddScoped<ITenant>(sp =>
{
    // 从IServiceProvider获取IHttpContextAccessor服务实例
    var tenantString = sp.GetRequiredService<IHttpContextAccessor>().HttpContext?.Request.Query["TenantId"];
    if (!string.IsNullOrEmpty(tenantString))
    {
        // 从配置中获取租户的连接字符串
        var tenantConfigSections = configuration.GetSection($"TenantConfigurations").Get<TenantConfiguration[]>();
        var tenantConfig = tenantConfigSections?.FirstOrDefault(i => i.Name == tenantString.Value);
        if (null != tenantConfig)
        {
            var tenantName = tenantConfig.Name;
            var tenantConnectionString = tenantConfig.ConnectionString;
            // 创建Tenant实例
            var tenant = new Tenant(tenantName ?? "", tenantConnectionString ?? "");
            return tenant;
        }
    }
    throw new ArgumentNullException(nameof(tenantString), "TenantId cannot be null or empty.");
});

builder.Services.AddScoped<ITenantDbContextOptionsBuilder, TenantDbContextOptionsBuilder>();

// 注册 DbContext 服务
builder.Services.AddScoped<EnvironmentDbContext>(sp =>
{
    
    // 获取 ITenantDbContextOptionsBuilder 服务
    var tenantOptionsBuilder = sp.GetRequiredService<ITenantDbContextOptionsBuilder>();
    // 使用 ITenantDbContextOptionsBuilder 服务为 MyDbContext 构建选项
    var options = tenantOptionsBuilder.BuildOptions<EnvironmentDbContext>();
    // 创建 MyDbContext 实例
    return new EnvironmentDbContext(options);
});
// 添加Swagger生成器服务
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "My API",
        Description = "A simple example ASP.NET Core Web API",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Example Contact",
            Url = new Uri("https://example.com/contact")
        },
        License = new OpenApiLicense
        {
            Name = "Example License",
            Url = new Uri("https://example.com/license")
        }
    });
    options.OperationFilter<AddAuthTokenHeaderParameter>();

    // 如果你的API项目中有XML注释文件，可以添加以下代码来显示XML注释
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Description = "在下框中输入请求头中需要添加Jwt授权Token：Bearer Token",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme{
                                Reference = new OpenApiReference {
                                            Type = ReferenceType.SecurityScheme,
                                            Id = "Bearer"}
                           },new string[] { }
                        } });
});

var jwtSettings = configuration.GetSection("JwtSettings");
//TODO 验证令牌和过期时间
builder.Services.Configure<JwtSettings>(jwtSettings);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var jwtSettings = configuration.GetSection("JwtSettings").Get<JwtSettings>();
        if (null != jwtSettings)
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings.Issuer,
                ValidAudience = jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey)),
                ClockSkew = TimeSpan.Zero
            };
        }
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;

    });
var app = builder.Build();

// Configure the HTTP request pipeline.

// 配置HTTP请求管道
if (app.Environment.IsDevelopment())
{
    // 在开发环境中启用Swagger和Swagger UI
    app.UseSwagger();
    //app.UseSwaggerUI(options =>
    //{
    //    options.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    //    // 如果你想让Swagger UI在根路径上可用，可以取消注释以下行
    //    // options.RoutePrefix = string.Empty;
    //});
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");

        // 注入自定义JavaScript代码和CSS样式

        //    var jsAndCss = @"
        //<style>
        //    #tenantInput {
        //        display: block;
        //        width: 200px;
        //        margin: 10px auto;
        //        padding: 5px;
        //        font-size: 14px;
        //    }
        //</style>
        //<script type='text/javascript'>
        //window.onload = function() {
        //console.log('Window loaded');
        //    var tenantInput = document.createElement('input');
        //    tenantInput.type = 'text';
        //    tenantInput.id = 'tenantInput';
        //    tenantInput.placeholder = 'Enter tenant ID';

        //    var swaggerContainer = document.querySelector('.swagger-ui .information-container');
        //    if (swaggerContainer) {
        //        swaggerContainer.insertBefore(tenantInput, swaggerContainer.firstChild);
        //    }

        //    // 修改Swagger UI的请求拦截器，添加租户信息
        //    window.ui = window.ui || {};
        //    window.ui.preRequest = function(xhr) {
        //        var tenantId = document.getElementById('tenantInput').value;
        //        xhr.setRequestHeader('X-Tenant', tenantId);
        //    };
        //console.log('Window 2222');

        //};
        //</script>
        //";
        //    c.HeadContent += jsAndCss;
        //        var sb = new StringBuilder(c.HeadContent ?? "");
        //        sb.AppendLine(@$"<script type='text/javascript'>
        //(function ()
        //{{
        //    const overrider = () =>
        //    {{
        //        const swagger = window.ui;
        //        if (!swagger) 
        //        {{
        //            console.error('Swagger wasn\'t found');
        //            return;
        //        }}

        //        ensureAuthorization(swagger);
        //        reloadSchemaOnAuth(swagger);
        //        clearInputPlaceHolder(swagger);
        //        showLoginUI(swagger);
        //                swagger.preRequest = function(xhr) {{
        //                xhr.setRequestHeader('X-Tenant', '234');
        //            }};
        //    }}

        //    const getAuthorization = (swagger) => swagger.auth()._root.entries.find(e => e[0] === 'authorized');
        //    const isAuthorized = (swagger) =>
        //    {{
        //        const auth = getAuthorization(swagger);
        //        return auth && auth[1].size !== 0;
        //    }};

        //    // a hacky way to append authorization header - we are basically intercepting 
        //    // all requests, if no authorization was attached while user did authorized himself,
        //    // append token to request
        //    const ensureAuthorization = (swagger) => 
        //    {{
        //        // retrieve bearer token from authorization
        //        const getBearer = () => 
        //        {{
        //            const auth = getAuthorization(swagger);
        //            const def = auth[1]._root.entries.find(e => e[0] === 'Bearer');
        //            if (!def)
        //                return undefined;

        //            const token = def[1]._root.entries.find(e => e[0] === 'value');
        //            if (!token)
        //                return undefined;

        //            return token[1];
        //        }}

        //        // override fetch function of Swagger to make sure
        //        // that on every request of the client is authorized append auth-header
        //        const fetch = swagger.fn.fetch;
        //        swagger.fn.fetch = (req) => 
        //        {{
        //            if (!req.headers.Authorization && isAuthorized(swagger)) 
        //            {{
        //                const bearer = getBearer();
        //                if (bearer) 
        //                {{
        //                    req.headers.Authorization = bearer;
        //                }}
        //            }}
        //            return fetch(req);
        //        }}
        //    }};
        //    // makes that once user triggers performs authorization,
        //    // the schema will be reloaded from backend url
        //    const reloadSchemaOnAuth = (swagger) => 
        //    {{
        //        const getCurrentUrl = () => 
        //        {{
        //            const spec = swagger.getState()._root.entries.find(e => e[0] === 'spec');
        //            if (!spec)
        //                return undefined;

        //            const url = spec[1]._root.entries.find(e => e[0] === 'url');
        //            if (!url)
        //                return undefined;

        //            return url[1];
        //        }}
        //        const reload = () => 
        //        {{
        //            const url = getCurrentUrl();
        //            if (url) 
        //            {{
        //                swagger.specActions.download(url);
        //            }}
        //        }};

        //        const handler = (caller, args) => 
        //        {{
        //            const result = caller(args);
        //            if (result.then) 
        //            {{
        //                result.then(() => reload())
        //            }}
        //            else
        //            {{
        //                reload();
        //            }}
        //            return result;
        //        }}

        //        const auth = swagger.authActions.authorize;
        //        swagger.authActions.authorize = (args) => handler(auth, args);
        //        const logout = swagger.authActions.logout;
        //        swagger.authActions.logout = (args) => handler(logout, args);
        //    }};
        //    /**
        //     * Reset input element placeholder
        //     * @param {{any}} swagger
        //     */
        //    const clearInputPlaceHolder = (swagger) =>
        //    {{
        //        //https://github.com/api-platform/core/blob/main/src/Bridge/Symfony/Bundle/Resources/public/init-swagger-ui.js#L6-L41
        //        new MutationObserver(function (mutations, self)
        //        {{
        //            var elements = document.querySelectorAll('input[type=text]');
        //            for (var i = 0; i < elements.length; i++)
        //                elements[i].placeholder = '';
        //        }}).observe(document, {{ childList: true, subtree: true }});
        //    }}
        //    /**
        //     * Show login UI
        //     * @param {{any}} swagger
        //     */
        //    const showLoginUI = (swagger) =>
        //    {{
        //        //https://github.com/api-platform/core/blob/main/src/Bridge/Symfony/Bundle/Resources/public/init-swagger-ui.js#L6-L41
        //        new MutationObserver(function (mutations, self)
        //        {{
        //            var rootDiv = document.querySelector('#swagger-ui > section > div.swagger-ui > div:nth-child(2)');
        //            if (rootDiv == null)
        //                return;

        //            var informationContainerDiv = rootDiv.querySelector('div.information-container.wrapper');
        //            if (informationContainerDiv == null)
        //                return;

        //            var descriptionDiv = informationContainerDiv.querySelector('section > div > div > div.description');
        //            if (descriptionDiv == null)
        //                return;

        //            var loginDiv = descriptionDiv.querySelector('div.login');
        //            if (loginDiv != null)
        //                return;

        //            //Check authentication
        //            if (isAuthorized(swagger))
        //                return;

        //            //Remove elements different from information-container wrapper
        //            for (var i = 0; i < rootDiv.children.length; i++)
        //            {{
        //                var child = rootDiv.children[i];
        //                if (child !== informationContainerDiv)
        //                    child.remove();
        //            }}

        //            //Create UI di login
        //            createLoginUI(descriptionDiv);

        //        }}).observe(document, {{ childList: true, subtree: true }});

        //        /**
        //         * Create login ui elements
        //         * @param {{any}} rootDiv
        //         */
        //        const createLoginUI = function (rootDiv)
        //        {{
        //            var div = document.createElement('div');
        //            div.className = 'login';

        //            rootDiv.appendChild(div);
        //            //Tenant
        //            var tenantLabel = document.createElement('label');
        //            div.appendChild(tenantLabel);

        //            var tenantSpan = document.createElement('span');
        //            tenantSpan.innerText = 'Tenant';
        //            tenantLabel.appendChild(tenantSpan);

        //            var tenantInput = document.createElement('input');
        //            tenantInput.type = 'text';
        //            tenantInput.placeholder = 'Enter tenant ID';
        //            tenantInput.style = 'margin-left: 10px; margin-right: 10px;';
        //            tenantLabel.appendChild(tenantInput);

        //            //UserName
        //            var userNameLabel = document.createElement('label');
        //            div.appendChild(userNameLabel);

        //            var userNameSpan = document.createElement('span');
        //            userNameSpan.innerText = 'User';
        //            userNameLabel.appendChild(userNameSpan);

        //            var userNameInput = document.createElement('input');
        //            userNameInput.type = 'text';
        //            userNameInput.value = 'admin';
        //            userNameInput.style = 'margin-left: 10px; margin-right: 10px;';
        //            userNameLabel.appendChild(userNameInput);

        //            //Password
        //            var passwordLabel = document.createElement('label');
        //            div.appendChild(passwordLabel);

        //            var passwordSpan = document.createElement('span');
        //            passwordSpan.innerText = 'Password';
        //            passwordLabel.appendChild(passwordSpan);

        //            var passwordInput = document.createElement('input');
        //            passwordInput.type = 'password';
        //            passwordInput.value = 'admin';
        //            passwordInput.style = 'margin-left: 10px; margin-right: 10px;';
        //            passwordLabel.appendChild(passwordInput);

        //            //Login button
        //            var loginButton = document.createElement('button')
        //            loginButton.type = 'submit';
        //            loginButton.type = 'button';
        //            loginButton.classList.add('btn');
        //            loginButton.classList.add('auth');
        //            loginButton.classList.add('authorize');
        //            loginButton.classList.add('button');
        //            loginButton.innerText = 'Login';
        //            loginButton.onclick = function ()
        //            {{
        //                var userName = userNameInput.value;
        //                var password = passwordInput.value;

        //                if (userName === '' || password === '')
        //                {{
        //                    alert('Insert userName and password!');
        //                    return;
        //                }}

        //                login(userName, password);
        //            }};

        //            div.appendChild(loginButton);
        //        }}
        //        /**
        //         * Manage login
        //         * @param {{any}} userName UserName
        //         * @param {{any}} password Password
        //         */
        //        const login = function (userName, password)
        //        {{
        //            var xhr = new XMLHttpRequest();

        //            xhr.onreadystatechange = function ()
        //            {{
        //                if (xhr.readyState == XMLHttpRequest.DONE)
        //                {{
        //                    if (xhr.status == 200 || xhr.status == 400)
        //                    {{
        //                        var response = JSON.parse(xhr.responseText);
        //                        if (!response.success)
        //                        {{
        //                            alert(response.message);
        //                            return;
        //                        }}

        //                        var accessToken = xhr.getResponseHeader('access-token');
        //                        var xtoken =  xhr.getResponseHeader('x-access-token');

        //                        var obj = {{
        //                            'Bearer': {{
        //                                'name': 'Bearer',
        //                                'schema': {{
        //                                    'type': 'apiKey',
        //                                    'description': 'Please enter into field the word ""Bearer"" following by space and JWT',
        //                                    'name': 'Authorization',
        //                                    'in': 'header'
        //                                }},
        //                                value: 'Bearer ' + accessToken
        //                            }}
        //                        }};

        //                        swagger.authActions.authorize(obj);
        //                    }}
        //                    else
        //                    {{
        //                        alert('error ' + xhr.status);
        //                    }}
        //                }}
        //            }};

        //            xhr.open('POST', '/api/user/login', true);
        //            xhr.setRequestHeader('Content-Type', 'application/json');
        //            xhr.setRequestHeader('X-TENANT', '1');

        //            var json = JSON.stringify({{ 'Account': userName, 'Password': password }});

        //            xhr.send(json);
        //        }}
        //    }}

        //    // append to event right after SwaggerUIBundle initialized
        //    window.addEventListener('load', () => setTimeout(overrider, 0), false);
        //}}());
        //</script>");
        //        sb.AppendLine(@$"<style media='screen' type='text/css'>

        //</style>");

        //        c.HeadContent = sb.ToString();
    });

    app.UseMiddleware<SwaggerTenantIdMiddleware>();

}

app.UseHttpsRedirection();
//TODO JWT 设置登录验证
app.UseAuthorization();

app.MapControllers();

app.Run();
