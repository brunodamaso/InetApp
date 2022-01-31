using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using INetApp.Resources;
using INetApp.Services.NFC;
using INetApp.ViewModels.Base;
using Xamarin.Forms;

namespace INetApp.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private bool ispermissionApp;

        #region Properties
        public bool IspermissionApp
        {
            get => ispermissionApp;
            set
            {
                ispermissionApp = value;
                RaisePropertyChanged(() => IspermissionApp);
            }
        }

        #endregion
        public ICommand ControlAccesoCommand => new Command(async () => await ControlAcceso());
        public ICommand BandejaEntradaCommand => new Command(async () => await BandejaEntrada());
        public ICommand PartesCommand => new Command(async () => await Partes());
        public ICommand FormacionCommand => new Command(async () => await Formacion());
        public ICommand InfoCommand => new Command(OnInfoCommand);

        public MainViewModel()
        {
        }
        public override Task InitializeAsync(IDictionary<string, string> query)
        {
            IsBusy = true;

            Device.BeginInvokeOnMainThread(async () =>
            {
                await InetAppNFC.ActivateNFC();
                await InetAppNFC.StartListeningIfNotiOS();
            });

            //todo buscar fecha ult actualziacion
            Text_last_update = string.Format(Literales.view_text_last_updated, "xxx");
            IspermissionApp = settingsService.Permission;

            IsBusy = false;
            return base.InitializeAsync(query);
        }

        private async Task ControlAcceso()
        {
            await NavigationService.NavigateToAsync("LectorQR");
        }
        private async Task BandejaEntrada()
        {
            await NavigationService.NavigateToAsync("Category");
        }

        private async Task Partes()
        {
            await NavigationService.NavigateToAsync("Settings");
        }

        private async Task Formacion()
        {
            await NavigationService.NavigateToAsync("Settings");
        }
        private async void OnInfoCommand()
        {
            IsBusy = true;
            Dictionary<string, string> Parametro = new Dictionary<string, string>
                {
                    { "Formulario", "//MainView"}
                };

            await NavigationService.NavigateToAsync("InfoView", Parametro);

            IsBusy = false;
        }
    }
}