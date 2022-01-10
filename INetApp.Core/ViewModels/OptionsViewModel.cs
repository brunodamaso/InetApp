using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using INetApp.APIWebServices.Dtos;
using INetApp.Models;
using INetApp.Resources;
using INetApp.Services;
using INetApp.ViewModels.Base;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace INetApp.ViewModels
{
    public class OptionsViewModel : ViewModelBase
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

        public OptionsViewModel()
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

            //if (await DialogService.ShowAlertAsync(Literales.dialog_approve_messages, Literales.dialog_approve_title, Literales.dialog_approve_positive, Literales.cancel))
            //{
            //    List<MessageModel> messageModels = MessageItems.Where(a => a.checkeado).ToList();

                if (await optionsService.MarkOptionsAsync(OptionsItems))
            //    {
            //        IsRowChecked = false;
            //        await Sincroniza();
            //        await DialogService.ShowAlertAsync(Literales.toast_approve_messages, "", Literales.btn_text_accept);
            //        //IsInitialized = false;
            //    }
            //    else
            //    {
            //        await DialogService.ShowAlertAsync(Literales.toast_not_all_messages_approved, "", Literales.btn_text_accept);
            //    }
            //}
            IsBusy = false;
        }
    }
}