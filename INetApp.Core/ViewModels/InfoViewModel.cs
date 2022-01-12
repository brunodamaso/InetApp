using INetApp.Resources;
using INetApp.Services;
using INetApp.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace INetApp.ViewModels
{
    public class InfoViewModel : ViewModelBase
    {

        #region Properties
        public ICommand AproveCommand => new Command(OnAproveOptions);

        #endregion


        public InfoViewModel()
        {
        }

        public override async Task InitializeAsync(IDictionary<string, string> query)
        {
        }
        private async void OnAproveOptions()
        {
            IsBusy = true;

            await NavigationService.NavigateToAsync("//MainView");

            IsBusy = false;
        }
    }
}

