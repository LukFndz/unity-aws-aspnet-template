using Backend.Api.Configuration;
using Backend.Api.Models;
using Microsoft.Extensions.Options;

namespace Backend.Api.Services;

public sealed class UnityConfigService : IUnityConfigService
{
    private readonly IOptionsMonitor<AwsOptions> _awsOptions;
    private readonly IOptionsMonitor<UnityClientOptions> _unityOptions;

    public UnityConfigService(IOptionsMonitor<AwsOptions> awsOptions, IOptionsMonitor<UnityClientOptions> unityOptions)
    {
        _awsOptions = awsOptions;
        _unityOptions = unityOptions;
    }

    public UnityClientConfig BuildClientConfig()
    {
        var aws = _awsOptions.CurrentValue;
        var unity = _unityOptions.CurrentValue;

        return new UnityClientConfig
        {
            Environment = unity.Environment,
            ApiBaseUrl = unity.ApiBaseUrl,
            AssetBucket = aws.AssetBucket
        };
    }
}
