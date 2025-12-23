using System.Text.Json.Serialization;

namespace Backend.Api.Models;

public sealed class UnityClientConfig
{
    [JsonPropertyName("environment")]
    public required string Environment { get; init; }

    [JsonPropertyName("apiBaseUrl")]
    public required string ApiBaseUrl { get; init; }

    [JsonPropertyName("assetBucket")]
    public string? AssetBucket { get; init; }
}
