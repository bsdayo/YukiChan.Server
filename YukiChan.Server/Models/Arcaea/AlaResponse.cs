using System.Text.Json.Serialization;

namespace YukiChan.Server.Models.Arcaea;

public sealed class AlaResponse<T>
{
    [JsonPropertyName("data")]
    public T Data { get; set; } = default!;

    [JsonPropertyName("message")]
    public string? Message { get; set; }
}