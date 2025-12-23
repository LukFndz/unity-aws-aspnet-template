using UnityEngine;

namespace Assets.Scripts.Networking
{
    [CreateAssetMenu(
        fileName = "ApiConfig",
        menuName = "Networking/Api Config"
    )]
    public sealed class ApiConfig : ScriptableObject
    {
        [SerializeField] private string baseUrl;

        public string BaseUrl => baseUrl;
    }
}
