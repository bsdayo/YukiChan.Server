using Microsoft.EntityFrameworkCore;
using YukiChan.Server.Databases;
using YukiChan.Server.Models;
using YukiChan.Shared.Data.Console;
using YukiChan.Shared.Models;

namespace YukiChan.Server.Services.Console;

public sealed class PrecheckService
{
    private readonly GuildsDbContext _guildData;
    private readonly UsersDbContext _userData;
    private readonly CommandHistoryDbContext _cmdHistoryData;

    public PrecheckService(GuildsDbContext guildData, UsersDbContext userData, CommandHistoryDbContext cmdHistoryData)
    {
        _guildData = guildData;
        _userData = userData;
        _cmdHistoryData = cmdHistoryData;
    }

    public async Task<bool> CheckAssignee(string platform, string guildId, string botId)
    {
        var guild = await _guildData.Guilds
            .AsNoTracking()
            .FirstOrDefaultAsync(guild => guild.Platform == platform &&
                                          guild.GuildId == guildId);

        if (guild is not null)
            return guild.Assignee == botId;

        _guildData.Guilds.Add(new GuildData
        {
            Platform = platform,
            GuildId = guildId,
            Assignee = botId
        });
        await _guildData.SaveChangesAsync();
        return true;
    }

    public async Task<YukiUserAuthority> GetUserAuthority(string platform, string userId)
    {
        var user = await _userData.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(user => user.Platform == platform &&
                                         user.UserId == userId);

        if (user is null)
        {
            user = new UserData
            {
                Platform = platform,
                UserId = userId,
                UserAuthority = YukiUserAuthority.User
            };
            _userData.Users.Add(user);
            await _userData.SaveChangesAsync();
        }

        return user.UserAuthority;
    }

    public Task SaveCommandHistory(PrecheckRequest req, YukiUserAuthority authority)
    {
        _cmdHistoryData.Histories.Add(new CommandHistory
        {
            Time = DateTime.UtcNow,
            Platform = req.Platform,
            GuildId = req.GuildId,
            ChannelId = req.ChannelId,
            UserId = req.UserId,
            UserAuthority = authority,
            AssigneeId = req.SelfId,
            Environment = req.Environment,
            Command = req.Command,
            CommandText = req.CommandText
        });
        return _cmdHistoryData.SaveChangesAsync();
    }
}