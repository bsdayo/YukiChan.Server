using Microsoft.Extensions.Options;

namespace YukiChan.Server.Services.Console;

public sealed class TokenService
{
    private readonly IOptionsMonitor<TokenServiceOptions> _optionsMonitor;

    public TokenService(IOptionsMonitor<TokenServiceOptions> optionsMonitor)
        => _optionsMonitor = optionsMonitor;

    public IEnumerable<string> Tokens => _optionsMonitor.CurrentValue.Tokens;

    public bool Authenticate(string token)
    {
        return Tokens.Contains(token);
    }
}

public class TokenServiceOptions
{
    public string[] Tokens { get; set; } = Array.Empty<string>();
}