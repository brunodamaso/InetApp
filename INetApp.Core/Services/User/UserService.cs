using System.Collections.Generic;
using System.Threading.Tasks;
using INetApp.APIWebServices.Dtos;
using INetApp.Services.Identity;
using INetApp.Services.Settings;
using INetApp.ViewModels.Base;

namespace INetApp.Services
{
    public class UserService : IUserService
    {
        private readonly ISettingsService settingsService;
        private readonly IRepositoryWebService repositoryWebService;
        private protected readonly IIdentityService identityService;

        public UserService(IRepositoryWebService _repositoryWebService)
        {
            settingsService = ViewModelLocator.Resolve<ISettingsService>();
            repositoryWebService = _repositoryWebService;
            identityService = ViewModelLocator.Resolve<IIdentityService>();
        }

        public async Task<UserLoggedDto> GetUserLoggedDto(string userName, string userPass)
        {
            UserLoggedDto userLoggedDto = await repositoryWebService.GetUserLogged(userName, userPass);
            if (userLoggedDto.IsOk)
            {
                settingsService.AuthAccessToken = userName;
                settingsService.NameInitial = userLoggedDto.UserLoggedModel.nameInitial + userLoggedDto.UserLoggedModel.lastNameInitial;
                settingsService.NameFull = userLoggedDto.UserLoggedModel.fullName;
                settingsService.Permission = userLoggedDto.UserLoggedModel.permission;
            }
            return userLoggedDto;
        }
        public async Task<UserLoggedDto> GetUserLoggedDto()
        {
            KeyValuePair<string, object> credenciales = identityService.GetCredentialsFromPrefs();

            string userName = credenciales.Key.ToString();
            string userPass = credenciales.Value.ToString();
            return await GetUserLoggedDto(userName, userPass);
        }
    }
}