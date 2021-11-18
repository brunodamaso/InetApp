using INetApp.APIWebServices.Dtos;
using INetApp.Helpers;
using INetApp.Models.User;
using INetApp.Services.Identity;
using INetApp.Services.RequestProvider;
using INetApp.Services.Settings;
using INetApp.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace INetApp.Services.User
{
    public class UserService : IUserService
    {
        private readonly IRequestProvider _requestProvider;
        private readonly ISettingsService settingsService;
        private readonly IRepositoryWebService repositoryWebService;
        protected readonly IIdentityService identityService;

        public UserService(IRequestProvider requestProvider)
        {
            _requestProvider = requestProvider;
            settingsService = ViewModelLocator.Resolve<ISettingsService>();
            repositoryWebService = ViewModelLocator.Resolve<IRepositoryWebService>();
            identityService = ViewModelLocator.Resolve<IIdentityService>();
        }

        public async Task<UserInfo> GetUserInfoAsync(string authToken)
        {
            var uri = UriHelper.CombineUri(GlobalSetting.Instance.UserInfoEndpoint);

            var userInfo = await _requestProvider.GetAsync<UserInfo>(uri, authToken);
            return userInfo;
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