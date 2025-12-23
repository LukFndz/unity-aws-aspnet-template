namespace Backend.Api.Configuration;

public sealed class UnityClientOptions
{
    public const string SectionName = "UnityClient";

    /// <summary>
    /// Origins (semicolon separated) allowed to call the API from Unity (WebGL/editor).
    /// </summary>
    public string AllowedOrigins { get; set; } = "http://localhost:3000;http://127.0.0.1:3000";

    /// <summary>
    /// Logical environment name exposed to the Unity client (e.g., dev, stage, prod).
    /// </summary>
    public string Environment { get; set; } = "dev";

    /// <summary>
    /// Public base URL the Unity client should target.
    /// </summary>
    public string ApiBaseUrl { get; set; } = "http://localhost:5000";
}
