namespace INetApp.APIWebServices.Responses
{
    public class BaseResponse<T>
    {
        protected BaseResponse()
        {
            IsOk = true;
            ErrorCode = string.Empty;
            Description = string.Empty;
        }

        protected BaseResponse(T value)
        {
            IsOk = true;
            IsConnected = true;
            ErrorCode = string.Empty;
            Description = string.Empty;
            Content = value;
        }

        protected BaseResponse(string errorCode, string description)
        {
            IsOk = false;
            IsConnected = true;
            ErrorCode = errorCode;
            Description = description;
        }

        protected BaseResponse(string errorCode, string description, bool isConnected)
        {
            IsOk = false;
            IsConnected = isConnected;
            ErrorCode = errorCode;
            Description = description;
        }

        public bool IsOk { get; set; }
        public bool IsConnected { get; set; }
        public string ErrorCode { get; set; }
        public string Description { get; set; }
        public T Content { get; set; }
    }
}
