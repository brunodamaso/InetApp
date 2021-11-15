using INetApp.Models.Token;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace INetApp.Services.Identity
{
    public interface IIdentityService
    {
        string CreateAuthorizationRequest();
        string CreateLogoutRequest(string token);
        Task<UserToken> GetTokenAsync(string code);
        KeyValuePair<string, object> GetCredentialsFromPrefs();
        void PutCredentialsFromPrefs(string user, string pass);
    }
}