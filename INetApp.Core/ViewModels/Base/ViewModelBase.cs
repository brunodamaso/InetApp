using INetApp.Services;
using INetApp.Services.Settings;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace INetApp.ViewModels.Base
{
    public abstract class ViewModelBase : ExtendedBindableObject, IQueryAttributable
    {
        protected readonly IDialogService DialogService;
        protected readonly INavigationService NavigationService;
        protected readonly IRepositoryWebService RepositoryWebService;
        protected readonly IRepositoryService RepositoryService;
        protected readonly IDBService DBService;
        protected readonly ISettingsService settingsService;

        private string text_last_update;
        public string Text_last_update
        {
            get => text_last_update;
            set
            {
                text_last_update = value;
                RaisePropertyChanged(() => Text_last_update);
            }
        }

        private bool _isInitialized;
        public bool IsInitialized
        {
            get => _isInitialized;

            set
            {
                _isInitialized = value;
                OnPropertyChanged(nameof(IsInitialized));
            }
        }

        private bool _multipleInitialization;

        public bool MultipleInitialization
        {
            get => _multipleInitialization;

            set
            {
                _multipleInitialization = value;
                OnPropertyChanged(nameof(MultipleInitialization));
            }
        }

        private bool _isBusy;

        public bool IsBusy
        {
            get => _isBusy;

            set
            {
                _isBusy = value;
                OnPropertyChanged(nameof(IsBusy));
            }
        }

        public ViewModelBase()
        {
            DialogService = ViewModelLocator.Resolve<IDialogService>();
            NavigationService = ViewModelLocator.Resolve<INavigationService>();
            RepositoryWebService = ViewModelLocator.Resolve<IRepositoryWebService>();
            DBService = ViewModelLocator.Resolve<IDBService>();
            RepositoryService = ViewModelLocator.Resolve<IRepositoryService>();
            settingsService = ViewModelLocator.Resolve<ISettingsService>();
        }

        public virtual Task InitializeAsync(IDictionary<string, string> query)
        {
            return Task.FromResult(false);
        }

        public async void ApplyQueryAttributes(IDictionary<string, string> query)
        {
            if (!IsInitialized)
            {
                IsInitialized = true;
                await InitializeAsync(query);
            }
        }
        public virtual Task OnPageBack()
        {
            return Task.FromResult(true);
        }
    }
}