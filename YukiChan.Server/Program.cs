using System.Text.Json.Serialization;
using Serilog;
using YukiChan.Server;
using YukiChan.Server.Utils;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    Args = args,
    ContentRootPath = YukiServerDir.Root
});

builder.ConfigureInfrastructure(args);

// 日志记录
builder.ConfigureSerilog();

builder.Services.AddControllers().AddJsonOptions(options =>
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// API 版本控制
builder.Services.AddApiVersioning().AddMvc();

// Token 验证
builder.Services.AddTokenAuthentication(builder.Configuration);

builder.Services.AddDatabases();
builder.Services.AddConsoleServices(builder.Configuration);

var app = builder.Build();

app.UseSerilogRequestLogging();

// 身份验证中间件
app.UseAuthentication();
app.UseAuthorization();

app.UseYukiErrorHandler();

app.MapControllers();

app.Run();