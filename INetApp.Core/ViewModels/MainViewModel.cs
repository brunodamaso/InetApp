using INetApp.Models.Navigation;
using INetApp.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace INetApp.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public ICommand SettingsCommand => new Command(async () => await SettingsAsync());
        public ICommand ControlAccesoCommand => new Command(async () => await ControlAcceso());
        public ICommand BandejaEntradaCommand => new Command(async () => await BandejaEntrada());
        public ICommand PartesCommand => new Command(async () => await Partes());
        public ICommand FormacionCommand => new Command(async () => await Formacion());

        public MainViewModel()
        {

        }
        public override Task InitializeAsync(IDictionary<string, string> query)
        {
            IsBusy = true;

            return base.InitializeAsync(query);
        }

        private async Task SettingsAsync()
        {
            await NavigationService.NavigateToAsync("Settings");
        }       

        private async Task ControlAcceso()
        {
            throw new NotImplementedException();
        }
        private async Task BandejaEntrada()
        {
            throw new NotImplementedException();
        }


        private async Task Partes()
        {
            throw new NotImplementedException();
        }


        private async Task Formacion()
        {
            throw new NotImplementedException();
        }

    }
}