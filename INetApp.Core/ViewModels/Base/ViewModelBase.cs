using System.Collections.Generic;
using System.Threading.Tasks;
using INetApp.NFC;
using INetApp.Resources;
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
        protected readonly INFCService nfcService;
        private string LecturaNFC;
        private bool _isInitialized;
        private string text_last_update;
        private bool _isBusy;
        private bool _multipleInitialization;
        
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
            nfcService = DependencyService.Get<INFCService>();
        }

        public virtual Task InitializeAsync(IDictionary<string, string> query)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await nfcService.ActivateNFC();
                if (nfcService.NfcIsEnabled)
                {
                    CrossNFC.Current.OnMessageReceived += VM_OnMessageReceived;
                    await nfcService.BeginListening();
                }
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

        private async void VM_OnMessageReceived(ITagInfo tagInfo)
        {
            LecturaNFC = nfcService.Current_OnMessageReceived(tagInfo);
            //await DialogService.ShowAlertAsync(LecturaNFC, LecturaNFC ,"OK");
            //LecturaNFC = "Tag [04:51:17:2A:5A:1E:80]";
            if (!string.IsNullOrEmpty(LecturaNFC))
            {
                APIWebServices.Dtos.UserAccessDto userAccessDto = await nfcService.GetAccesoAsync(LecturaNFC);
                if (userAccessDto.IsOk)
                {
                    await DialogService.ShowAlertAsync(userAccessDto.UserAccessModel.mensaje, "", Literales.btn_text_accept);
                }
                else
                {
                    if (!userAccessDto.IsConnected)
                    {
                        await DialogService.ShowAlertAsync(Literales.exception_message_no_connection, "", Literales.btn_text_accept);
                    }
                    else if (userAccessDto.UserAccessModel == null)
                    {
                        await DialogService.ShowAlertAsync(Literales.exception_message_message_not_found, "", Literales.btn_text_accept);
                    }
                    else
                    {
                        await DialogService.ShowAlertAsync(userAccessDto.ErrorDescription, "", Literales.btn_text_accept);
                    }
                }
            }
        }
    }
}