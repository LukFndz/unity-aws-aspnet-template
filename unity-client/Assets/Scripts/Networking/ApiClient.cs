using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts.Networking
{
    public sealed class ApiClient
    {
        private readonly string _baseUrl;

        public ApiClient(ApiConfig config)
        {
            _baseUrl = config.BaseUrl.TrimEnd('/');
        }

        public async Task<ApiResult<TResponse>> GetAsync<TResponse>(string path)
        {
            using var request = UnityWebRequest.Get(BuildUrl(path));
            return await SendAsync<TResponse>(request);
        }

        public async Task<ApiResult<TResponse>> PostAsync<TRequest, TResponse>(
            string path,
            TRequest payload
        )
        {
            var json = JsonUtility.ToJson(payload);
            var body = Encoding.UTF8.GetBytes(json);

            using var request = new UnityWebRequest(
                BuildUrl(path),
                UnityWebRequest.kHttpVerbPOST
            );

            request.uploadHandler = new UploadHandlerRaw(body);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            return await SendAsync<TResponse>(request);
        }

        private async Task<ApiResult<T>> SendAsync<T>(UnityWebRequest request)
        {
            var operation = request.SendWebRequest();

            while (!operation.isDone)
                await Task.Yield();

            if (request.result != UnityWebRequest.Result.Success)
            {
                return ApiResult<T>.Failure(request.error);
            }

            try
            {
                var data = JsonUtility.FromJson<T>(request.downloadHandler.text);
                return ApiResult<T>.Success(data);
            }
            catch
            {
                return ApiResult<T>.Failure("Failed to deserialize response");
            }
        }

        private string BuildUrl(string path)
        {
            path = path.TrimStart('/');
            return $"{_baseUrl}/{path}";
        }
    }
}
