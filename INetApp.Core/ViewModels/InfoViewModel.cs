using INetApp.Models;
using INetApp.Resources;
using INetApp.Services;
using INetApp.ViewModels.Base;
//using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace INetApp.ViewModels
{
    public class InfoViewModel : ViewModelBase
    {
        private string formulario;
        #region Properties
        public ICommand AproveCommand => new Command(OnAproveOptions);

        #endregion


        public InfoViewModel()
        {
        }

        public override async Task InitializeAsync(IDictionary<string, string> query)
        {
            formulario = Uri.UnescapeDataString(query["Formulario"]);
        }
        private async void OnAproveOptions()
        {
            IsBusy = true;

            await NavigationService.NavigateToAsync(formulario);

            IsBusy = false;
        }
    }
}

