using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YukiChan.Server.Models.Arcaea;

[Table("arcaea_alias_submissions")]
public sealed class ArcaeaAliasSubmission
{
    [Key, Column("id")]
    public int Id { get; init; }

    [Column("platform"), Required]
    public required string Platform { get; init; }

    [Column("user_id"), Required]
    public required string UserId { get; init; }

    [Column("song_id"), Required]
    public required string SongId { get; init; }

    [Column("alias"), Required]
    public required string Alias { get; init; }

    [Column("submit_time"), Required]
    public required DateTime SubmitTime { get; init; }
}