using System.Collections.Generic;
using System.Threading.Tasks;
using INetApp.NFC;
using INetApp.Services;
using INetApp.Services.NFC;
using INetApp.Services.Settings;
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

        private string LecturaNFC;
        private bool _isInitialized;
        private string text_last_update;
        private bool _isBusy;
        private bool _multipleInitialization;
        private NFCService NFCService;
        
        public string Text_last_update
        {
            get => text_last_update;
            set
            {
                text_last_update = value;
                RaisePropertyChanged(() => Text_last_update);
            }
        }
        public bool IsInitialized
        {
            get => _isInitialized;

            set
            {
                _isInitialized = value;
                OnPropertyChanged(nameof(IsInitialized));
            }
        }
        public bool MultipleInitialization
        {
            get => _multipleInitialization;

            set
            {
                _multipleInitialization = value;
                OnPropertyChanged(nameof(MultipleInitialization));
            }
        }
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
            this.NFCService = new NFCService();
            Device.BeginInvokeOnMainThread(async () =>
            {
                await this.NFCService.ActivateNFC();
                CrossNFC.Current.OnMessageReceived += VM_OnMessageReceived;
                await this.NFCService.BeginListening();
            });

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

        private void VM_OnMessageReceived(ITagInfo tagInfo)
        {
            LecturaNFC = this.NFCService.Current_OnMessageReceived(tagInfo);
            if (!string.IsNullOrEmpty(LecturaNFC))
            {
                //RepositoryWebService.GetAccesoNFC(LecturaNFC);
            }
        }
    }
}