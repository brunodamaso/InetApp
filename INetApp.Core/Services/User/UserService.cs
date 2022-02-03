using System.Collections.Generic;
using System.Threading.Tasks;
using INetApp.APIWebServices.Dtos;
using INetApp.Models;
using INetApp.Resources;
using INetApp.Services.Identity;
using INetApp.Services.Settings;
using INetApp.ViewModels.Base;
using Xamarin.Essentials;

namespace INetApp.Services
{
    public class UserService : IUserService
    {
        private readonly ISettingsService settingsService;
        private readonly IRepositoryWebService repositoryWebService;
        private readonly IDialogService DialogService;
        private readonly INavigationService NavigationService;
        private protected readonly IIdentityService identityService;

        public UserService(IRepositoryWebService _repositoryWebService)
        {
            settingsService = ViewModelLocator.Resolve<ISettingsService>();
            repositoryWebService = _repositoryWebService;
            identityService = ViewModelLocator.Resolve<IIdentityService>();
            DialogService = ViewModelLocator.Resolve<IDialogService>();
            NavigationService = ViewModelLocator.Resolve<INavigationService>();
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

        public async Task<bool> CheckVersion()
        {
            bool actualiza = false;
            if (VersionTracking.CurrentVersion != settingsService.Version)
            {
                if (settingsService.Requerido)
                {
                    await DialogService.ShowAlertAsync(Literales.dialog_approve_install,
                        Literales.dialog_approve_title_search,
                        Literales.dialog_approve_positive_install);
                    actualiza = true;
                }
                else
                {
                    actualiza = await DialogService.ShowAlertAsync(Literales.dialog_approve_install,
                        Literales.dialog_approve_title_search,
                        Literales.dialog_approve_positive_install,
                        Literales.cancel);
                }
            }
            return actualiza;
        }
    }
}