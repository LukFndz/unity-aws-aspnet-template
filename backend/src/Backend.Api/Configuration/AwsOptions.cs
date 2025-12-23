namespace Backend.Api.Configuration;

public sealed class AwsOptions
{
    public const string SectionName = "AWS";

    /// <summary>
    /// AWS region used by the SDK clients (e.g. us-east-1).
    /// </summary>
    public string Region { get; set; } = "us-east-1";

    /// <summary>
    /// Optional credentials profile name. Leave empty to rely on environment/instance roles.
    /// </summary>
    public string? Profile { get; set; }

    /// <summary>
    /// Example bucket for Unity asset delivery. Replace with your own bucket name.
    /// </summary>
    public string? AssetBucket { get; set; }
}
