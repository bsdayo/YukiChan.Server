using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using YukiChan.Server.Services.Console;
using YukiChan.Shared.Data;

namespace YukiChan.Server.Core;

public class TokenAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    public const string SchemeName = "TokenAuthentication";

    private readonly TokenService _tokenService;

    public TokenAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger,
        UrlEncoder encoder, ISystemClock clock, TokenService service) : base(options, logger, encoder, clock)
    {
        _tokenService = service;
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var authHeader = Request.Headers.Authorization.ToString();
        if (!authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            return Task.FromResult(AuthenticateResult.NoResult());

        var token = authHeader[7..];
        if (!_tokenService.Authenticate(token))
            return Task.FromResult(AuthenticateResult.NoResult());

        var identity = new ClaimsIdentity(new[]
        {
            new Claim("Token", token)
        }, nameof(TokenAuthenticationHandler)); // 必须指定 Handler 类型名称，否则将不起作用

        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, SchemeName);
        return Task.FromResult(AuthenticateResult.Success(ticket));
    }

    protected override async Task HandleChallengeAsync(AuthenticationProperties properties)
    {
        await base.HandleChallengeAsync(properties);
        await Response.WriteAsJsonAsync(new YukiResponse
        {
            Code = YukiErrorCode.Unauthorized
        }, YukiResponse.JsonSerializerOptions);
    }
}