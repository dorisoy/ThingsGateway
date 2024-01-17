#region copyright

//------------------------------------------------------------------------------
//  此代码版权声明为全文件覆盖，如有原作者特别声明，会在下方手动补充
//  此代码版权（除特别声明外的代码）归作者本人Diego所有
//  源代码使用协议遵循本仓库的开源协议及附加协议
//  Gitee源代码仓库：https://gitee.com/diego2098/ThingsGateway
//  Github源代码仓库：https://github.com/kimdiego2098/ThingsGateway
//  使用文档：https://diego2098.gitee.io/thingsgateway-docs/
//  QQ群：605534569
//------------------------------------------------------------------------------

#endregion

using Furion;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.WebEncoders;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

using System.Text.Encodings.Web;
using System.Text.Unicode;

using ThingsGateway.Admin.Application;
using ThingsGateway.Admin.Core;

namespace ThingsGateway.Web.Core;

/// <inheritdoc/>
[AppStartup(99)]
public class Startup : AppStartup
{
    /// <inheritdoc/>
    public void ConfigureServices(IServiceCollection services)
    {
        services.Configure<WebEncoderOptions>(options => options.TextEncoderSettings = new TextEncoderSettings(UnicodeRanges.All));

        //认证组件
        services.AddComponent<AuthComponent>();

        //启动LoggingMonitor操作日志写入数据库组件
        services.AddComponent<LoggingMonitorComponent>();

        //启动Web设置SignalRComponent组件
        services.AddComponent<SignalRComponent>();

        services.AddRazorPages();
        services.AddControllers()
            .AddNewtonsoftJson(
            options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();// 首字母小写（驼峰样式）
            })
         .AddFriendlyException()
         .AddInjectWithUnifyResult<UnifyResultProvider>();//规范化

        //Nginx代理的话获取真实IP
        services.Configure<ForwardedHeadersOptions>(options =>
        {
            options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            //新增如下两行
            options.KnownNetworks.Clear();
            options.KnownProxies.Clear();
        });

        services.AddServerSideBlazor(options =>
        {
            options.RootComponents.MaxJSRootComponents = 500;
        }).AddHubOptions(options =>
        {
            options.MaximumReceiveMessageSize = 64 * 1024;
            //options.ClientTimeoutInterval = TimeSpan.FromMinutes(1);
            //options.KeepAliveInterval = TimeSpan.FromSeconds(30);
            //options.HandshakeTimeout = TimeSpan.FromSeconds(30);
        });

        services.AddHealthChecks();
    }

    /// <inheritdoc/>
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseHsts();
        }

        // 启用HTTPS
        //app.UseHttpsRedirection();

        // 添加状态码拦截中间件
        app.UseUnifyResultStatusCodes();
        app.UseStaticFiles();

        // 任务调度看板
        app.UseScheduleUI();

        //UseCorsAccessor
        app.UseCorsAccessor();

        app.UseInject("api");

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseForwardedHeaders();//Nginx代理的话获取真实IP

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapDefaultControllerRoute();

            endpoints.MapHubs();
            endpoints.MapBlazorHub(options =>
            {
                //options.WebSockets.CloseTimeout = new TimeSpan(30);
            }
            );
            endpoints.MapFallbackToPage("/_Host");
        });
    }
}