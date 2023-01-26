using System.Text.Json.Serialization;

namespace YukiChan.Server.Models.Arcaea;

public sealed class AlaRecord
{
    [JsonPropertyName("song_id")]
    public string SongId { get; set; } = null!;

    [JsonPropertyName("difficulty")]
    public int Difficulty { get; set; }

    [JsonPropertyName("score")]
    public int Score { get; set; }

    [JsonPropertyName("shiny_pure_count")]
    public int ShinyPureCount { get; set; }

    [JsonPropertyName("pure_count")]
    public int PureCount { get; set; }

    [JsonPropertyName("far_count")]
    public int FarCount { get; set; }

    [JsonPropertyName("lost_count")]
    public int LostCount { get; set; }

    [JsonPropertyName("recollection_rate")]
    public int RecollectionRate { get; set; }

    [JsonPropertyName("time_played")]
    public long TimePlayed { get; set; }

    [JsonPropertyName("gauge_type")]
    public int GaugeType { get; set; }
}