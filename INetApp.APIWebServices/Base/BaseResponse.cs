namespace INetApp.APIWebServices.Responses
{
    public class BaseResponse<T>
    {
        protected BaseResponse()
        {
            this.IsOk = true;
            this.ErrorCode = string.Empty;
            this.Description = string.Empty;
        }

        protected BaseResponse(T value)
        {
            this.IsOk = true;
            this.IsConnected = true;
            this.ErrorCode = string.Empty;
            this.Description = string.Empty;
            this.Resultado = value;
        }

        protected BaseResponse(string errorCode, string description)
        {
            this.IsOk = false;
            this.IsConnected = true;
            this.ErrorCode = errorCode;
            this.Description = description;
        }

        protected BaseResponse(string errorCode, string description, bool isConnected)
        {
            this.IsOk = false;
            this.IsConnected = isConnected;
            this.ErrorCode = errorCode;
            this.Description = description;
        }

        public bool IsOk { get; set; }
        public bool IsConnected { get; set; }
        public string ErrorCode { get; set; }
        public string Description { get; set; }
        public T Resultado { get; set; }
    }
}
