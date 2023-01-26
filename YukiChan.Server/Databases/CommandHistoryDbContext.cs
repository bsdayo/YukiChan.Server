// using Microsoft.EntityFrameworkCore;
// using YukiChan.Server.Models;
// using YukiChan.Server.Utils;
//
// namespace YukiChan.Server.Databases;
//
// public class CommandHistoryDbContext : DbContext
// {
//     private static readonly string DbPath = Path.Combine(YukiServerDir.Databases, "command-history.db");
//     
//     public DbSet<CommandHistory> Histories => Set<CommandHistory>();
//
//     protected override void OnConfiguring(DbContextOptionsBuilder options)
//         => options.UseSqlite($"DataSource={DbPath}");
// }

