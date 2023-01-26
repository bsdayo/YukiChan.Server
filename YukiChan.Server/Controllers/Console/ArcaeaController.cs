using ArcaeaUnlimitedAPI.Lib.Utils;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YukiChan.Server.Services.Console.Arcaea;
using YukiChan.Server.Utils;
using YukiChan.Shared.Data;
using YukiChan.Shared.Data.Console.Arcaea;

namespace YukiChan.Server.Controllers.Console;

[ApiController]
[Authorize]
[ApiVersion(1)]
[Route("console/v{version:apiVersion}/arcaea")]
public sealed class ArcaeaController : YukiController
{
    private readonly ArcaeaService _service;

    public ArcaeaController(ArcaeaService service)
    {
        _service = service;
    }

    #region 用户相关

    [HttpGet("users/{platform}/{userId}")]
    public async Task<IActionResult> OnGetUser(string platform, string userId)
    {
        var user = await _service.GetUser(platform, userId);
        if (user is null)
            return NotFoundResp(YukiErrorCode.Arcaea_NotBound);
        return OkResp(new ArcaeaUserResponse
        {
            ArcaeaName = user.ArcaeaName,
            ArcaeaId = user.ArcaeaId
        });
    }

    [HttpPut("users/{platform}/{userId}")]
    public async Task<IActionResult> OnBindUser(string platform, string userId,
        [FromBody] ArcaeaBindRequest bindRequest)
    {
        try
        {
            var resp = await _service.BindUser(platform, userId, bindRequest);
            return OkResp(resp);
        }
        catch (AuaException auaEx)
        {
            return NotFound(auaEx.ToYukiResponse());
        }
    }

    #endregion

    #region 偏好相关

    [HttpGet("preferences/{platform}/{userId}")]
    public async Task<IActionResult> OnGetPreferences(string platform, string userId)
    {
        var pref = await _service.GetPreferences(platform, userId)
                   ?? await _service.AddNewPreferences(platform, userId);
        return OkResp(new ArcaeaPreferencesResponse { Preferences = pref });
    }

    /// <remarks>
    /// 更新前需要先从服务器获取，因此提交更新的 pref 对象一定包含 Id
    /// </remarks>
    [HttpPut("preferences/{platform}/{userId}")]
    public async Task<IActionResult> OnUpdatePreferences(string platform, string userId,
        [FromBody] ArcaeaPreferencesRequest prefReq)
    {
        var pref = prefReq.Preferences;
        if (pref.Platform != platform || pref.UserId != userId)
            return BadRequestResp();
        return await _service.UpdatePreferences(pref) ? OkResp() : NotFoundResp();
    }

    #endregion

    #region 查分相关

    [HttpGet("best30/{target}")]
    public async Task<IActionResult> OnGetBest30(string target, bool official)
    {
        try
        {
            var (b30, errCode) = await _service.GetBest30(target, official);
            return b30 is not null
                ? OkResp(new ArcaeaBest30Response { Best30 = b30 })
                : BadRequestResp(errCode);
        }
        catch (AuaException auaEx)
        {
            return NotFound(auaEx.ToYukiResponse());
        }
    }

    [HttpGet("best/{target}/{song}/{difficulty:int}")]
    public async Task<IActionResult> OnGetBest(string target, string song, int difficulty)
    {
        var songId = await _service.QuerySongId(song);
        if (songId is null) return NotFoundResp(YukiErrorCode.Arcaea_SongNotFound);
        try
        {
            var (user, best) = await _service.GetBest(target, songId, difficulty);
            return OkResp(new ArcaeaBestResponse
            {
                User = user,
                BestRecord = best
            });
        }
        catch (AuaException auaEx)
        {
            return NotFound(auaEx.ToYukiResponse());
        }
    }

    [HttpGet("recent/{target}")]
    public async Task<IActionResult> OnGetRecent(string target)
    {
        try
        {
            var (user, recent) = await _service.GetRecent(target);
            return OkResp(new ArcaeaRecentResponse
            {
                User = user,
                RecentRecord = recent
            });
        }
        catch (AuaException auaEx)
        {
            return NotFound(auaEx.ToYukiResponse());
        }
    }

    #endregion

    #region 信息相关

    [HttpGet("songs/{query}/id")]
    public async Task<IActionResult> OnQuerySongId(string query)
    {
        var songId = await _service.QuerySongId(query);
        return songId is null
            ? NotFoundResp(YukiErrorCode.Arcaea_SongNotFound)
            : OkResp(new ArcaeaSongIdResponse { SongId = songId });
    }

    [HttpGet("songs/{query}/aliases")]
    public async Task<IActionResult> OnQuerySongAliases(string query)
    {
        var songId = await _service.QuerySongId(query);
        if (songId is null) return NotFoundResp(YukiErrorCode.Arcaea_SongNotFound);
        var aliases = await _service.GetSongAliases(songId);
        var chart = await _service.GetChartInfo(songId);
        return OkResp(new ArcaeaSongAliasesResponse
        {
            Aliases = aliases,
            Name = chart.NameEn,
            Artist = chart.Artist
        });
    }

    [HttpPost("songs/{query}/aliases")]
    public async Task<IActionResult> OnAddSongAlias(string query, [FromBody] ArcaeaSongAddAliasRequest req)
    {
        var songId = await _service.QuerySongId(query);
        if (songId is null) return NotFoundResp(YukiErrorCode.Arcaea_SongNotFound);
        var err = await _service.AddSongAlias(songId, req.Alias);
        return err switch
        {
            YukiErrorCode.Ok => OkResp(),
            YukiErrorCode.Arcaea_SongNotFound => NotFoundResp(err),
            _ => BadRequestResp(err)
        };
    }

    #endregion
}