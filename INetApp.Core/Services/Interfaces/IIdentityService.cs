using System.Collections.Generic;
using System.Threading.Tasks;

namespace INetApp.Services.Identity
{
    public interface IIdentityService
    {
        KeyValuePair<string, object> GetCredentialsFromPrefs();
        void PutCredentialsFromPrefs(string user, string pass ,Models.UserLoggedModel userLoggedModel);
    }
}