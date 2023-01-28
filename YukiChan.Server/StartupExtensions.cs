using System.Net.Mime;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Tomlyn.Extensions.Configuration;
using YukiChan.Server.Core;
using YukiChan.Server.Databases;
using YukiChan.Server.Services.Console;
using YukiChan.Server.Services.Console.Arcaea;
using YukiChan.Server.Services.Console.Assets;
using YukiChan.Server.Utils;
using YukiChan.Shared.Data;
using YukiChan.Shared.Utils;

namespace YukiChan.Server;

internal static class StartupExtensions
{
    internal static void ConfigureInfrastructure(this WebApplicationBuilder builder, string[] args)
    {
        void AddYukiConfig(string name) =>
            builder.Configuration
                .AddTomlFile($"{YukiServerDir.Configs}/{name}.toml", true, true)
                .AddTomlFile($"{YukiServerDir.Configs}/{name}.{builder.Environment.EnvironmentName.ToLower()}.toml",
                    true, true);

        // 清除所有配置源
        builder.Configuration.Sources.Clear();
        // 添加 TOML 配置文件
        AddYukiConfig("serilog");
        AddYukiConfig("services");
        AddYukiConfig("tokens");
        builder.Configuration
            // 添加环境变量配置
            .AddEnvironmentVariables("ASPNETCORE_")
            // 添加命令行配置
            .AddCommandLine(args);
    }

    internal static void ConfigureSerilog(this WebApplicationBuilder builder)
    {
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .CreateLogger();
        builder.Logging.ClearProviders();
        builder.Host.UseSerilog(dispose: true);
    }

    internal static void AddTokenAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<TokenService>();
        services.Configure<TokenServiceOptions>(configuration);
        services.AddAuthentication()
            .AddScheme<AuthenticationSchemeOptions, TokenAuthenticationHandler>(
                TokenAuthenticationHandler.SchemeName, null);
    }

    internal static void AddDatabases(this IServiceCollection services)
    {
        void AddAndEnsureCreated<TDbContext>() where TDbContext : DbContext, new()
        {
            services.AddDbContext<TDbContext>();
            using var tempDb = new TDbContext();
            tempDb.Database.EnsureCreated();
        }

        AddAndEnsureCreated<ArcaeaDbContext>();
        AddAndEnsureCreated<UserDataDbContext>();
        AddAndEnsureCreated<GuildDataDbContext>();
        AddAndEnsureCreated<CommandHistoryDbContext>();
    }

    internal static void AddConsoleServices(this IServiceCollection services, IConfiguration configuration)
    {
        var config = configuration.GetSection("Services:Console");

        services // Root
            .AddScoped<PrecheckService>();

        services // Arcaea
            .AddDbContext<ArcaeaSongDbContext>()
            .AddScoped<ArcaeaService>()
            .AddSingleton<ArcaeaAuaService>()
            .AddSingleton<ArcaeaAlaService>()
            .AddSingleton<ArcaeaAssetsService>()
            .Configure<ArcaeaServiceOptions>(config.GetSection("Arcaea"));
    }

    internal static void UseYukiErrorHandler(this WebApplication app)
    {
        app.UseExceptionHandler(handler =>
        {
            handler.Run(async ctx =>
            {
                ctx.Response.StatusCode = 500;
                ctx.Response.ContentType = MediaTypeNames.Application.Json;
                await ctx.Response.WriteAsJsonAsync(new YukiResponse
                {
                    Code = YukiErrorCode.InternalServerError,
                    Message = ctx.Features.Get<IExceptionHandlerPathFeature>()?.Error.Message
                              ?? YukiErrorCode.InternalServerError.GetMessage()
                }, YukiResponse.JsonSerializerOptions);
            });
        });
    }
}