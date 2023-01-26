using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YukiChan.Server.Models;

[Table("guilds")]
public class GuildData
{
    [Key]
    [Column("id")]
    [Required]
    public int Id { get; set; }

    [Column("platform")]
    [Required]
    public required string Platform { get; set; }

    [Column("guild_id")]
    [Required]
    public required string GuildId { get; set; }

    [Column("assignee")]
    [Required]
    public required string Assignee { get; set; }
}