namespace Assets.Scripts.Networking
{
    public sealed class ApiResult<T>
    {
        public bool IsSuccess { get; }
        public T Data { get; }
        public string Error { get; }

        private ApiResult(bool isSuccess, T data, string error)
        {
            IsSuccess = isSuccess;
            Data = data;
            Error = error;
        }

        public static ApiResult<T> Success(T data)
            => new ApiResult<T>(true, data, null);

        public static ApiResult<T> Failure(string error)
            => new ApiResult<T>(false, default, error);
    }
}
