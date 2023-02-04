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

    public DbSet<ArcaeaAliasSubmission> AliasSubmissions => Set<ArcaeaAliasSubmission>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        // Alias submission
        builder.Entity<ArcaeaAliasSubmission>(b =>
        {
            b.ToTable("arcaea_alias_submissions");

            b.HasKey(m => m.Id);
            b.Property(m => m.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            b.Property(m => m.Platform)
                .HasColumnName("platform")
                .IsRequired();

            b.Property(m => m.UserId)
                .HasColumnName("user_id")
                .IsRequired();

            b.Property(m => m.SongId)
                .HasColumnName("song_id")
                .IsRequired();

            b.Property(m => m.Alias)
                .HasColumnName("alias")
                .IsRequired();

            b.Property(m => m.SubmitTime)
                .HasColumnName("submit_time")
                .IsRequired();

            b.Property(m => m.Status)
                .HasColumnName("status")
                .IsRequired();
        });
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"DataSource={DbPath}");
}