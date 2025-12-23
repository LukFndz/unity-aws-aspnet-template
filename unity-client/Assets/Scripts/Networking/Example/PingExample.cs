using UnityEngine;

namespace Assets.Scripts.Networking.Example
{
    public sealed class PingExample : MonoBehaviour
    {
        [SerializeField] private ApiConfig apiConfig;

        private async void Start()
        {
            var client = new ApiClient(apiConfig);
            var result = await client.GetAsync<PingResponse>("ping");

            if (result.IsSuccess)
                Debug.Log($"Ping OK: {result.Data.status}");
            else
                Debug.LogError(result.Error);
        }
    }
}