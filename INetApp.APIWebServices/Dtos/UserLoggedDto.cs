using INetApp.Models;

namespace INetApp.APIWebServices.Dtos
{
    public class UserLoggedDto : BaseDto
    {
        public UserLoggedDto() : base() { }
        public UserLoggedDto(bool isOk, string errorCode, string errorDescription, bool isConnected) : base(isOk, errorCode, errorDescription, isConnected) { }
        public UserLoggedModel UserLoggedModel { get; set; }
    }
}
