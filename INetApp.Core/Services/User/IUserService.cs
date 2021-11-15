using INetApp.Models.User;
using System.Threading.Tasks;

namespace INetApp.Services.User
{
    public interface IUserService
    {
        Task<UserInfo> GetUserInfoAsync(string authToken);
    }
}
