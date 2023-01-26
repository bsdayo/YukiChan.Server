using System.Text.Json.Serialization;

namespace YukiChan.Server.Models.Arcaea;

public sealed class AlaUser
{
    [JsonPropertyName("display_name")]
    public string DisplayName { get; set; } = null!;

    [JsonPropertyName("potential")]
    public double Potential { get; set; }

    [JsonPropertyName("partner")]
    public PartnerInfo Partner { get; set; } = null!;

    [JsonPropertyName("last_played_song")]
    public AlaRecord LastPlayedSong { get; set; } = null!;

    public sealed class PartnerInfo
    {
        [JsonPropertyName("partner_id")]
        public int PartnerId { get; set; }

        [JsonPropertyName("is_awakened")]
        public bool IsAwakened { get; set; }
    }
}