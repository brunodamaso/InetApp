using System;
using System.Collections.Generic;
using System.Text;
using INetApp.Services.Identity;
using INetApp.Services.Settings;
using INetApp.ViewModels.Base;

namespace INetApp.Services
{
    public class PushService : IPushService
    {
        private readonly ISettingsService settingsService;
        private readonly IRepositoryWebService repositoryWebService;
        private readonly IDialogService DialogService;
        private readonly INavigationService NavigationService;
        private protected readonly IIdentityService identityService;

        public PushService()
        {
        }

        public PushService(IRepositoryWebService _repositoryWebService)
        {
            settingsService = ViewModelLocator.Resolve<ISettingsService>();
            identityService = ViewModelLocator.Resolve<IIdentityService>();
            repositoryWebService = _repositoryWebService;
            DialogService = ViewModelLocator.Resolve<IDialogService>();
            NavigationService = ViewModelLocator.Resolve<INavigationService>();
        }

        public string GetToken()
        {
            return settingsService.PushToken;
        }
        public bool RegistrarToken(string token)
        {
            return true;
        }
        public virtual bool OnPushAction(IDictionary<string, string> token)
        {
            return true;
        }
    }
}
