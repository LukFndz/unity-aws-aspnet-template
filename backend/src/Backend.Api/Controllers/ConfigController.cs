using Amazon.S3;
using Amazon.S3.Model;
using Backend.Api.Configuration;
using Backend.Api.Models;
using Backend.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net;

namespace Backend.Api.Controllers;

[ApiController]
[Route("api/config")]
public sealed class ConfigController : ControllerBase
{
    private readonly IUnityConfigService _unityConfigService;
    private readonly IAmazonS3 _s3;
    private readonly IOptionsMonitor<AwsOptions> _awsOptions;

    public ConfigController(IUnityConfigService unityConfigService, IAmazonS3 s3, IOptionsMonitor<AwsOptions> awsOptions)
    {
        _unityConfigService = unityConfigService;
        _s3 = s3;
        _awsOptions = awsOptions;
    }

    [HttpGet]
    public ActionResult<UnityClientConfig> GetClientConfig()
    {
        return Ok(_unityConfigService.BuildClientConfig());
    }

    [HttpGet("aws/status")]
    public async Task<IActionResult> GetAwsStatus(CancellationToken cancellationToken)
    {
        var region = _s3.Config.RegionEndpoint?.SystemName ?? _awsOptions.CurrentValue.Region;

        try
        {
            var response = await _s3.ListBucketsAsync(cancellationToken);
            var buckets = response.Buckets.Select(b => b.BucketName).OrderBy(n => n).ToArray();

            return Ok(new
            {
                region,
                credentialStatus = "ok",
                buckets
            });
        }
        catch (AmazonS3Exception ex) when (ex.StatusCode is HttpStatusCode.Forbidden or HttpStatusCode.Unauthorized)
        {
            return StatusCode(StatusCodes.Status403Forbidden, new
            {
                region,
                credentialStatus = "unauthorized",
                message = "Check AWS credentials or IAM permissions for S3."
            });
        }
        catch (AmazonS3Exception ex) when (ex.StatusCode == 0)
        {
            return StatusCode(StatusCodes.Status503ServiceUnavailable, new
            {
                region,
                credentialStatus = "missing",
                message = "Unable to reach AWS. Ensure credentials/region are configured."
            });
        }
    }
}
