using Backend.Api.Models;

namespace Backend.Api.Services;

public interface IUnityConfigService
{
    UnityClientConfig BuildClientConfig();
}
