using Microsoft.EntityFrameworkCore;
using YukiChan.Server.Models;
using YukiChan.Server.Utils;

namespace YukiChan.Server.Databases;

public sealed class GuildDataDbContext : DbContext
{
    private static readonly string DbPath = Path.Combine(YukiServerDir.Databases, "guilds.db");

    public DbSet<GuildData> Guilds => Set<GuildData>();

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"DataSource={DbPath}");
}