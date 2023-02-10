using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#pragma warning disable CS8618

namespace YukiChan.Server.Models.Arcaea;

[Table("arcaea_users")]
public class ArcaeaDatabaseUser
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("platform")]
    [Required]
    public required string Platform { get; set; }

    [Column("user_id")]
    [Required]
    public required string UserId { get; set; }

    [Column("arcaea_id")]
    [Required]
    public required string ArcaeaId { get; set; }

    [Column("arcaea_name")]
    public string ArcaeaName { get; set; } = string.Empty;
}