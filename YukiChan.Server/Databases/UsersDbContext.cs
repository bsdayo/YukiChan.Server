using Microsoft.EntityFrameworkCore;
using YukiChan.Server.Models;
using YukiChan.Server.Utils;

namespace YukiChan.Server.Databases;

public sealed class UsersDbContext : DbContext
{
    private static readonly string DbPath = Path.Combine(YukiServerDir.Databases, "users.db");

    public DbSet<UserData> Users => Set<UserData>();

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"DataSource={DbPath}");
}