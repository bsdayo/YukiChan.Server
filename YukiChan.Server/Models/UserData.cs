using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using YukiChan.Shared.Models;

namespace YukiChan.Server.Models;

[Table("users")]
public class UserData
{
    [Key]
    [Column("id")]
    [Required]
    public int Id { get; set; }

    [Column("platform")]
    [Required]
    public required string Platform { get; set; }

    [Column("user_id")]
    [Required]
    public required string UserId { get; set; }

    [Column("user_authority")]
    public YukiUserAuthority UserAuthority { get; set; } = YukiUserAuthority.User;
}