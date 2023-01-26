using System.Text.Json;
using Microsoft.Extensions.Options;
using YukiChan.Server.Models.Arcaea;

namespace YukiChan.Server.Services.Console.Arcaea;

public sealed class ArcaeaAlaService : IDisposable
{
    private const string ApiUrl = "https://arcaea-limitedapi.lowiro.com/api/v0/";

    private readonly HttpClient _httpClient = new();

    public ArcaeaAlaService(IOptions<ArcaeaServiceOptions> options)
    {
        _httpClient.BaseAddress = new Uri(ApiUrl);
        _httpClient.Timeout = new TimeSpan(0, 0, 0, options.Value.AlaTimeout);
        _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {options.Value.AlaToken}");
    }

    public async Task<AlaRecord[]> GetBest30(string usercode)
    {
        try
        {
            var json = await _httpClient.GetStringAsync($"user/{usercode}/best");
            var resp = JsonSerializer.Deserialize<AlaResponse<AlaRecord[]>>(json)!;
            if (resp.Message is not null)
                throw new AlaException(resp.Message);
            return resp.Data;
        }
        catch (Exception e)
        {
            throw new AlaException(e.Message);
        }
    }

    public async Task<AlaUser> GetUser(string usercode)
    {
        try
        {
            var json = await _httpClient.GetStringAsync($"user/{usercode}");
            var resp = JsonSerializer.Deserialize<AlaResponse<AlaUser>>(json)!;
            if (resp.Message is not null)
                throw new AlaException(resp.Message);
            return resp.Data;
        }
        catch (Exception e)
        {
            throw new AlaException(e.Message);
        }
    }

    public void Dispose() => _httpClient.Dispose();
}