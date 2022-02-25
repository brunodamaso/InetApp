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
    public class WorkPartsViewModel : ViewModelBase
    {
        private MessageDetails _MessageDetails;
        private MessageModel _messageModel;
        private readonly IWorkPartsService WorkPartsService;
        private string _Date;
        #region Properties

        public MessageDetails MessageDetail
        {
            get => _MessageDetails;
            set
            {
                _MessageDetails = value;
                RaisePropertyChanged(() => MessageDetail);
            }
        }
        public MessageModel MessageModel
        {
            get => _messageModel;
            set
            {
                _messageModel = value;
                RaisePropertyChanged(() => MessageModel);
            }
        }
        public string Date
        {
            get => _Date;
            set
            {
                _Date = value;
                RaisePropertyChanged(() => Date);
            }
        }

        #endregion

        //public ICommand SelectFavoriteCommand => new Command<bool>(OnSelectFavorite);
        public ICommand AproveCommand => new Command(OnAproveMessage);

        private void OnAproveMessage(object obj)
        {
            throw new NotImplementedException();
        }

        public ICommand RefuseCommand => new Command(OnRefuseMessage);

        private void OnRefuseMessage(object obj)
        {
            throw new NotImplementedException();
        }

        //public ICommand ButtonUrlCommand => new Command<Detail>(OnButtonUrl);

        public WorkPartsViewModel()
        {
            WorkPartsService = DependencyService.Get<IWorkPartsService>();
        }

         public override async Task InitializeAsync(IDictionary<string, string> query)
        {
            await Sincroniza();
            //await base.InitializeAsync(query);
        }
        private async Task Sincroniza()
        {
            IsBusy = true;

            WorkPartsDto workPartsDto = await WorkPartsService.GetWorkPartsAsync();

            IsBusy = false;
        }

    }
}