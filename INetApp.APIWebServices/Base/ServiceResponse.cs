namespace INetApp.APIWebServices.Responses
{
    public class ServiceResponse<T> : BaseResponse<T> where T : Response
    {
        public ServiceResponse(T value) : base(value) { }

        public ServiceResponse(string errorCode, string errorDescription) : base(errorCode, errorDescription) { }

        public ServiceResponse(string errorCode, string errorDescription, bool isConnected) : base(errorCode, errorDescription, isConnected) { }

        internal static ServiceResponse<T> CreateErr(string errorCode, string errorDescription) => new ServiceResponse<T>(errorCode, errorDescription);

        internal static ServiceResponse<T> CreateErr(string errorCode, string errorDescription, bool isConnected) => new ServiceResponse<T>(errorCode, errorDescription, isConnected);

        internal static ServiceResponse<T> CreateOk(T value) => new ServiceResponse<T>(value);
    }
}
