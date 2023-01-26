using Microsoft.EntityFrameworkCore;
using YukiChan.Server.Models.Arcaea;
using YukiChan.Server.Utils;
using YukiChan.Shared.Models.Arcaea;

namespace YukiChan.Server.Databases;

public sealed class ArcaeaDbContext : DbContext
{
    private static readonly string DbPath = Path.Combine(YukiServerDir.Databases, "arcaea.db");

    public DbSet<ArcaeaDatabaseUser> Users => Set<ArcaeaDatabaseUser>();

    public DbSet<ArcaeaUserPreferences> Preferences => Set<ArcaeaUserPreferences>();

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"DataSource={DbPath}");
}