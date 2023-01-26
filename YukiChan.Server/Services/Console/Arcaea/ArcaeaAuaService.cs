using ArcaeaUnlimitedAPI.Lib;
using Microsoft.Extensions.Options;

namespace YukiChan.Server.Services.Console.Arcaea;

public sealed class ArcaeaAuaService
{
    public AuaClient AuaClient { get; }

    public ArcaeaAuaService(IOptions<ArcaeaServiceOptions> options)
    {
        AuaClient = new AuaClient
        {
            ApiUrl = options.Value.AuaApiUrl,
            Token = options.Value.AuaToken,
            UserAgent = "YukiChan",
            Timeout = options.Value.AuaTimeout
        }.Initialize();
    }
}