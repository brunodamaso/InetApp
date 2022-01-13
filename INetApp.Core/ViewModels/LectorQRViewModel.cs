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
        public ICommand AproveCommand => new Command(OnAproveOptions);

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

            OptionsDto optionsDto = await optionsService.GetOptionsAsync();

            OptionsItems = optionsDto.IsOk ? new ObservableCollection<OptionsModel>(optionsDto.OptionsModel) : new ObservableCollection<OptionsModel>();

            IsBusy = false;
        }

        //private void OnSelectMessage(OptionsModel optionsModel)
        //{
        //    IsBusy = true;
        //    foreach (OptionsModel item in OptionsItems.Where(a => a.name == optionsModel.name))
        //    {
        //        item.checkeado = !optionsModel.checkeado;
        //    }

        //    OptionsItems = new ObservableCollection<OptionsModel>(OptionsItems);
        //    IsBusy = false;
        //}

        private async void OnAproveOptions()
        {
            IsBusy = true;

            if (await optionsService.MarkOptionsAsync(OptionsItems.Where(a => a.checkeado).ToList()))
            {
                await DialogService.ShowAlertAsync(Literales.toast_approve_options, "", Literales.btn_text_accept);
            }
            await NavigationService.NavigateToAsync("//MainView");

            IsBusy = false;
        }
    }
}