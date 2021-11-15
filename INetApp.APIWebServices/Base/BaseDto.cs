namespace INetApp.APIWebServices.Dtos
{
    public class BaseDto
    {
        public BaseDto()
        {
            IsOk = true;
            IsConnected = true;
            ErrorCode = string.Empty;
            ErrorDescription = string.Empty;
        }

        public BaseDto(bool isOk, string errorCode, string errorDescription)
        {
            IsOk = isOk;
            IsConnected = true;
            ErrorCode = errorCode;
            ErrorDescription = errorDescription;
        }

        public BaseDto(bool isOk, string errorCode, string errorDescription, bool isConnected)
        {
            IsOk = isOk;
            IsConnected = isConnected;
            ErrorCode = errorCode;
            ErrorDescription = errorDescription;
        }

        public bool IsOk { get; set; }
        public bool IsConnected { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorDescription { get; set; }
    }
}
