﻿using System.Collections.Generic;
using System.Threading.Tasks;
using INetApp.Services;
using INetApp.Services.Settings;
using Xamarin.Forms;

namespace INetApp.ViewModels.Base
{
    public abstract class ViewModelBase : ExtendedBindableObject, IQueryAttributable
    {
        protected readonly IDialogService DialogService;
        protected readonly INavigationService NavigationService;
        protected readonly IRepositoryWebService RepositoryWebService;
        protected readonly ISettingsService settingsService;

        private string text_last_update;
        public string Text_last_update
        {
            get => text_last_update;
            set
            {
                text_last_update = value;
                RaisePropertyChanged(() => this.Text_last_update);
            }
        }

        private bool _isInitialized;
        public bool IsInitialized
        {
            get => _isInitialized;

            set
            {
                _isInitialized = value;
                OnPropertyChanged(nameof(this.IsInitialized));
            }
        }

        private bool _multipleInitialization;

        public bool MultipleInitialization
        {
            get => _multipleInitialization;

            set
            {
                _multipleInitialization = value;
                OnPropertyChanged(nameof(this.MultipleInitialization));
            }
        }

        private bool _isBusy;

        public bool IsBusy
        {
            get => _isBusy;

            set
            {
                _isBusy = value;
                OnPropertyChanged(nameof(this.IsBusy));
            }
        }

        public ViewModelBase()
        {
            DialogService = ViewModelLocator.Resolve<IDialogService>();
            NavigationService = ViewModelLocator.Resolve<INavigationService>();
            RepositoryWebService = ViewModelLocator.Resolve<IRepositoryWebService>();
            settingsService = ViewModelLocator.Resolve<ISettingsService>();

            GlobalSetting.Instance.BaseIdentityEndpoint = settingsService.IdentityEndpointBase;
            GlobalSetting.Instance.BaseGatewayShoppingEndpoint = settingsService.GatewayShoppingEndpointBase;
            GlobalSetting.Instance.BaseGatewayMarketingEndpoint = settingsService.GatewayMarketingEndpointBase;
        }

        public virtual Task InitializeAsync(IDictionary<string, string> query)
        {
            return Task.FromResult(false);
        }

        public async void ApplyQueryAttributes(IDictionary<string, string> query)
        {
            if (!this.IsInitialized)
            {
                this.IsInitialized = true;
                await InitializeAsync(query);
            }
        }
    }
}