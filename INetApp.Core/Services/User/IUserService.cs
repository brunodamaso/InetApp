using INetApp.APIWebServices.Dtos;
using INetApp.Models.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace INetApp.Services
{
    public interface IUserService
    {
        Task<UserInfo> GetUserInfoAsync(string authToken);
        Task<UserLoggedDto> GetUserLoggedDto(string userName, string userPass);
        Task<UserLoggedDto> GetUserLoggedDto();
    }
}
