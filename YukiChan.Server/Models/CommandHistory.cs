using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using YukiChan.Shared.Data.Console;
using YukiChan.Shared.Models;

// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace YukiChan.Server.Models;

[Table("command_history")]
public class CommandHistory
{
    [Column("id")]
    [Key]
    public int Id { get; set; }

    [Column("time")]
    [Required]
    public required DateTime Time { get; set; }

    [Column("platform")]
    [Required]
    public required string Platform { get; set; }

    [Column("guild_id")]
    public required string? GuildId { get; set; }

    [Column("channel_id")]
    public required string? ChannelId { get; set; }

    [Column("user_id")]
    [Required]
    public required string UserId { get; set; }

    [Column("user_authority")]
    [Required]
    public required YukiUserAuthority UserAuthority { get; set; }

    [Column("assignee_id")]
    [Required]
    public required string AssigneeId { get; set; }

    [Column("environment")]
    [Required]
    public required YukiEnvironment Environment { get; set; }

    [Column("command")]
    [Required]
    public required string Command { get; set; }

    [Column("command_text")]
    [Required]
    public required string CommandText { get; set; }
}