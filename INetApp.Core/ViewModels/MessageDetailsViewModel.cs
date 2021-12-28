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
    public class MessageDetailsViewModel : ViewModelBase
    {
        private MessageDetails _MessageDetails;
        private MessageModel _messageModel;
        private readonly IMessageService MessageService;
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

        public ICommand SelectFavoriteCommand => new Command<bool>(OnSelectFavorite);
        public ICommand AproveCommand => new Command(OnAproveMessage);
        public ICommand RefuseCommand => new Command(OnRefuseMessage);
        public ICommand ButtonUrlCommand => new Command<Detail>(OnButtonUrl);
                
        public MessageDetailsViewModel()
        {
            MessageService = DependencyService.Get<IMessageService>();
        }

        public override async Task InitializeAsync(IDictionary<string, string> query)
        {
            MessageModel = JsonConvert.DeserializeObject<MessageModel>(Uri.UnescapeDataString(query["MessageModel"]));
            Date = MessageModel.date.Day + " de " + MessageModel.date.ToString("MMMM") + " de " + MessageModel.date.Year;

            await Sincroniza();
            //await base.InitializeAsync(query);
        }
        private async Task Sincroniza()
        {
            IsBusy = true;

            MessageDto messageDto = await MessageService.GetMessageDetailsAsync(MessageModel.categoryId, MessageModel.messageId);

            MessageDetail = messageDto.IsOk ? messageDto.MessageModel.fields : new MessageDetails();
            Text_last_update = string.Format(Literales.view_text_last_updated, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));

            IsBusy = false;
        }

        private async void OnSelectFavorite(bool IsFavorite)
        {
            IsBusy = true;
            bool? resultado = await MessageService.MarkMessageFavoriteAsync(MessageModel, !IsFavorite);
            if (resultado != null)
            {
                MessageModel.favorite = (bool)resultado;
                RaisePropertyChanged(() => MessageModel);
            }
            IsBusy = false;
        }

        private async void OnAproveMessage()
        {
            IsBusy = true;
            if (await DialogService.ShowAlertAsync(Literales.dialog_approve_message, Literales.dialog_approve_title, Literales.dialog_approve_positive, Literales.cancel))
            {
                if (await MessageService.ApproveMessageAsync(MessageModel))
                {
                    await DialogService.ShowAlertAsync(Literales.toast_approve_message, "", Literales.btn_text_accept);
                    IsInitialized = false;
                    await NavigationService.NavigateToAsync("..");
                }
                else
                {
                    await DialogService.ShowAlertAsync(Literales.toast_approve_message_error, "", Literales.btn_text_accept);
                }
            }
            IsBusy = false;
        }

        private async void OnRefuseMessage()
        {
            IsBusy = true;
            if (await DialogService.ShowPromptAsync(Literales.dialog_refuse_message + "\n\r" + Literales.dialog_refuse_reason, Literales.dialog_refuse_title, Literales.dialog_refuse_positive, Literales.cancel) is string cause
                             && !string.IsNullOrEmpty(cause))
            {
                if (await MessageService.RefuseMessageAsync(MessageModel, cause))
                {
                    await DialogService.ShowAlertAsync(Literales.toast_refuse_message, "", Literales.btn_text_accept);
                    IsInitialized = false;
                    await NavigationService.NavigateToAsync("..");
                }
                else
                {
                    await DialogService.ShowAlertAsync(Literales.toast_refuse_message_error, "", Literales.btn_text_accept);
                }
            }
            IsBusy = true;
        }

        private async void OnButtonUrl(Detail detail)
        {
            await NavigationService.NavigateToAsync("WebView?Ruta=" + detail.Campo);
        }
    }
}