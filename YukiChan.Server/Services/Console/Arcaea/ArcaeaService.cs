using ArcaeaUnlimitedAPI.Lib.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using YukiChan.Server.Databases;
using YukiChan.Server.Models.Arcaea;
using YukiChan.Server.Models.Arcaea.Factories;
using YukiChan.Shared.Data;
using YukiChan.Shared.Data.Console.Arcaea;
using YukiChan.Shared.Models.Arcaea;
using YukiChan.Shared.Utils;
using ArcaeaDifficulty = ArcaeaUnlimitedAPI.Lib.Models.ArcaeaDifficulty;

namespace YukiChan.Server.Services.Console.Arcaea;

public sealed class ArcaeaService
{
    private readonly ArcaeaServiceOptions _options;
    private readonly ILogger<ArcaeaService> _logger;

    private readonly ArcaeaAuaService _auaService;
    private readonly ArcaeaAlaService _alaService;
    private readonly ArcaeaDbContext _database;
    private readonly ArcaeaSongDbContext _songDb;

    public ArcaeaService(IOptions<ArcaeaServiceOptions> options, ILogger<ArcaeaService> logger,
        ArcaeaAuaService auaService, ArcaeaAlaService alaService, ArcaeaDbContext database, ArcaeaSongDbContext songDb)
    {
        _options = options.Value;
        _logger = logger;
        _auaService = auaService;
        _alaService = alaService;
        _database = database;
        _songDb = songDb;
    }

    #region 用户相关

    public Task<ArcaeaDatabaseUser?> GetUser(string platform, string userId)
    {
        return _database.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(user => user.Platform == platform && user.UserId == userId);
    }

    public async Task<ArcaeaBindResponse> BindUser(string platform, string userId, ArcaeaBindRequest bindRequest)
    {
        var auaUser = await _auaService.AuaClient.User.Info(bindRequest.BindTarget);

        var previous = await GetUser(platform, userId);
        _database.Users.Update(new ArcaeaDatabaseUser
        {
            Id = previous?.Id ?? default,
            Platform = platform,
            UserId = userId,
            ArcaeaId = auaUser.AccountInfo.Code,
            ArcaeaName = auaUser.AccountInfo.Name
        });
        await _database.SaveChangesAsync();

        return new ArcaeaBindResponse
        {
            Name = auaUser.AccountInfo.Name,
            Potential = auaUser.AccountInfo.Rating / 100d
        };
    }

    #endregion

    #region 偏好相关

    public async Task<ArcaeaUserPreferences?> GetPreferences(string platform, string userId)
    {
        var pref = await _database.Preferences
            .AsNoTracking()
            .FirstOrDefaultAsync(pref => pref.Platform == platform && pref.UserId == userId);

        return pref;
    }

    public async Task<ArcaeaUserPreferences> AddNewPreferences(string platform, string userId)
    {
        var pref = new ArcaeaUserPreferences
        {
            Platform = platform,
            UserId = userId
        };
        _database.Add(pref);
        await _database.SaveChangesAsync();
        return pref;
    }

    public async Task<bool> UpdatePreferences(ArcaeaUserPreferences pref)
    {
        var previous = await GetPreferences(pref.Platform, pref.UserId);
        if (previous is null || previous.Id != pref.Id) return false;
        _database.Preferences.Update(pref);
        await _database.SaveChangesAsync();
        return true;
    }

    #endregion

    #region 查分相关

    public async Task<(ArcaeaBest30?, YukiErrorCode)> GetBest30(string target, bool official)
    {
        ArcaeaBest30 b30;

        if (official)
        {
            // 仅允许通过好友码查询
            if (!int.TryParse(target, out _))
                return (null, YukiErrorCode.Arcaea_InvalidUserCode);
            var alaUser = await _alaService.GetUser(target);
            var alaB30 = await _alaService.GetBest30(target);
            b30 = ArcaeaBest30Factory.FromAla(alaUser, alaB30, target, _songDb.Charts);
        }
        else
        {
            if (int.TryParse(target, out _))
                target = target.PadLeft(9, '0');
            var auaB30 = await _auaService.AuaClient.User.Best30(target, 9, AuaReplyWith.SongInfo);
            b30 = ArcaeaBest30Factory.FromAua(auaB30);
        }

        return (b30, YukiErrorCode.Ok);
    }

    public async Task<(ArcaeaUser, ArcaeaRecord)> GetBest(string target, string songId, int difficulty)
    {
        if (int.TryParse(target, out _))
            target = target.PadLeft(9, '0');
        var best = await _auaService.AuaClient.User.Best(target, songId, AuaSongQueryType.SongId,
            (ArcaeaDifficulty)difficulty, AuaReplyWith.SongInfo);
        var user = ArcaeaUserFactory.FromAua(best.AccountInfo);
        var record = ArcaeaRecordFactory.FromAua(best.Record, best.SongInfo![0]);
        return (user, record);
    }

    public async Task<(ArcaeaUser, ArcaeaRecord)> GetRecent(string target)
    {
        if (int.TryParse(target, out _))
            target = target.PadLeft(9, '0');
        var info = await _auaService.AuaClient.User.Info(target, 1, AuaReplyWith.SongInfo);
        var user = ArcaeaUserFactory.FromAua(info.AccountInfo);
        var recent = ArcaeaRecordFactory.FromAua(info.RecentScore![0], info.SongInfo![0]);
        return (user, recent);
    }

    #endregion

    #region 信息相关

    public Task<string?> QuerySongId(string query)
    {
        return _songDb.FuzzySearchId(query);
    }

    public async Task<ArcaeaSongDbChart> GetChartInfo(string songId, int difficulty = 2)
    {
        return await _songDb.Charts
            .AsNoTracking()
            .FirstAsync(chart => chart.SongId == songId && chart.RatingClass == difficulty);
    }

    public async Task<string[]> GetSongAliases(string songId)
    {
        return await _songDb.Aliases
            .AsNoTracking()
            .Where(alias => alias.SongId == songId)
            .Select(alias => alias.Alias)
            .ToArrayAsync();
    }

    public async Task<YukiErrorCode> AddSongAlias(string songId, string alias)
    {
        var previous = await _songDb.Aliases
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.Alias == alias);
        if (previous is not null)
            return YukiErrorCode.Arcaea_AliasAlreadyExists;

        _songDb.Aliases
            .Add(new ArcaeaSongDbAlias
            {
                SongId = songId,
                Alias = alias
            });
        await _songDb.SaveChangesAsync();

        return YukiErrorCode.Ok;
    }

    public Task<bool> CheckSongIdExists(string songId)
    {
        return _songDb.Charts
            .AsNoTracking()
            .AnyAsync(chart => chart.SongId == songId);
    }

    public async Task<bool> AddAliasSubmission(ArcaeaAliasSubmission submission)
    {
        if (await _database.AliasSubmissions
                .AsNoTracking()
                .ContainsAsync(submission))
            return false;

        _database.AliasSubmissions.Add(submission);
        await _database.SaveChangesAsync();
        return true;
    }

    #endregion
}