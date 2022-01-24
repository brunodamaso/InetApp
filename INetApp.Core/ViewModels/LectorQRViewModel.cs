using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using IdentityModel.Client;
using INetApp.APIWebServices.Dtos;
using INetApp.Models;
using INetApp.Resources;
using INetApp.Services;
using INetApp.ViewModels.Base;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace INetApp.ViewModels
{
    public class LectorQRViewModel : ViewModelBase
    {
        private ObservableCollection<OptionsModel> _OptionsItems;
        private readonly IOptionsService optionsService;
        
        #region Properties

        public ObservableCollection<OptionsModel> OptionsItems
        {
            get => _OptionsItems;
            set
            {
                _OptionsItems = value;
                RaisePropertyChanged(() => OptionsItems);
            }
        }

        #endregion

        //public ICommand SelectOptionsCommand => new Command<OptionsModel>(OnSelectMessage);
        public ICommand ScanCommand => new Command<ZXing.Result>(OnScanExecute);

        public LectorQRViewModel()
        {
            optionsService = DependencyService.Get<IOptionsService>();
        }

        public override async Task InitializeAsync(IDictionary<string, string> query)
        {
            await Sincroniza();
            //await base.InitializeAsync(query);
        }

        private async Task Sincroniza()
        {
            IsBusy = true;

            IsBusy = false;
       }

        

        private async void OnScanExecute (ZXing.Result result)
        {
            IsBusy = true;

            //await NavigationService.NavigateToAsync("//MainView?scanresult="+ result.Text);

            IsBusy = false;
        }
    }
}