using Microsoft.EntityFrameworkCore;
using YukiChan.Server.Databases;
using YukiChan.Server.Models;

namespace YukiChan.Server.Services.Console;

public sealed class GuildsService
{
    private readonly GuildDataDbContext _guildData;

    public GuildsService(GuildDataDbContext guildData)
    {
        _guildData = guildData;
    }

    public async Task UpdateGuildAssignee(string platform, string guildId, string botId)
    {
        var guild = await _guildData.Guilds
            .FirstOrDefaultAsync(guild => guild.Platform == platform &&
                                          guild.GuildId == guildId);
        if (guild is null)
        {
            guild = new GuildData
            {
                Platform = platform,
                GuildId = guildId,
                Assignee = botId,
            };
            _guildData.Guilds.Add(guild);
        }

        guild.Assignee = botId;
        await _guildData.SaveChangesAsync();
    }
}