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
    public class MessageFavoriteViewModel : ViewModelBase
    {
        private ObservableCollection<MessageModel> _MessageItems;
        private readonly IMessageService MessageService;
        private bool _SelectAll;
        private int CategoryID;
        private bool _RowChecked = false;
        public List<MessageModel> MessageList;

        #region Properties

        public ObservableCollection<MessageModel> MessageItems
        {
            get => _MessageItems;
            set
            {
                _MessageItems = value;
                RaisePropertyChanged(() => MessageItems);
            }
        }
        
        public bool SelectAll
        {
            get => _SelectAll;
            set
            {
                _SelectAll = value;
                RaisePropertyChanged(() => SelectAll);
                OnSelectAll(value);
            }
        }
        public bool IsRowChecked
        {
            get => _RowChecked;
            set
            {
                _RowChecked = value;
                RaisePropertyChanged(() => IsRowChecked);
            }
        }

        #endregion

        public ICommand SelectMessageCommand => new Command<MessageModel>(OnSelectMessage);
        public ICommand AproveCommand => new Command(OnAproveMessages);
        public ICommand RefuseCommand => new Command(OnRefuseMessages);

        public MessageFavoriteViewModel()
        {
            MessageService = DependencyService.Get<IMessageService>();
        }

        public override async Task InitializeAsync(IDictionary<string, string> query)
        {
            await Sincroniza();
        }

        private async Task Sincroniza()
        {
            IsBusy = true;

            MessageList = await MessageService.GetMessageLocalAsync();

            MessageItems = new ObservableCollection<MessageModel>(MessageList);

            Text_last_update = string.Format(Literales.view_text_last_updated, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));

            IsBusy = false;
        }

        private async void OnSelectMessage(MessageModel messageModel)
        {
            IsBusy = true;
            string StringParametro = JsonConvert.SerializeObject(messageModel);

            Dictionary<string, string> Parametro = new Dictionary<string, string>
                {
                    { "MessageModel", StringParametro }
                };
            await NavigationService.NavigateToAsync("MessageDetails", Parametro);
            //Todo obliga a refrescar la pantalla al regresar
            //pudiera optimizarse llenando la tabla localmente y hacerle acciones a la misma
            //borrar o cambiar favorite
            //https://theconfuzedsourcecode.wordpress.com/2020/06/09/overriding-back-button-in-xamarin-forms-shell/
            IsInitialized = false;
            IsBusy = false;
        }

        private void OnSelectAll(bool TrueFalse)
        {
            IsBusy = true;
            MessageList.ForEach(a => a.checkeado = TrueFalse);
            MessageItems = new ObservableCollection<MessageModel>(MessageList);

            IsRowChecked = TrueFalse;
            IsBusy = false;
        }

        public bool IsRowSelect()
        {
            int canti = MessageItems.Count(a => a.checkeado);
            SelectAll = MessageItems.Count == canti;
            IsRowChecked = canti > 0;
            return IsRowChecked;
        }
        private async void OnAproveMessages()
        {
            IsBusy = true;
            
            if (await DialogService.ShowAlertAsync(Literales.dialog_approve_messages, Literales.dialog_approve_title, Literales.dialog_approve_positive, Literales.cancel))
            {
                List<MessageModel> messageModels = MessageItems.Where(a => a.checkeado).ToList();
            
                if (await MessageService.ApproveMessagesAsync(messageModels))
                {
                    IsRowChecked = false;
                    await Sincroniza();
                    await DialogService.ShowAlertAsync(Literales.toast_approve_messages, "", Literales.btn_text_accept);
                }
                else
                {
                    await DialogService.ShowAlertAsync(Literales.toast_not_all_messages_approved, "", Literales.btn_text_accept);
                }
            }
            IsBusy = false;
        }
        private async void OnRefuseMessages()
        {
            IsBusy = true;
            if (await DialogService.ShowPromptAsync(Literales.dialog_refuse_messages + "\n\r" + Literales.dialog_refuse_reason, Literales.dialog_refuse_title, Literales.dialog_refuse_positive, Literales.cancel) is string cause 
                    && !string.IsNullOrEmpty(cause))
            {
                List<MessageModel> messageModels = MessageItems.Where(a => a.checkeado).ToList();
                if (await MessageService.RefuseMessagesAsync(messageModels, cause))
                {
                    await Sincroniza();
                    IsRowChecked = false;
                    await DialogService.ShowAlertAsync(Literales.toast_refuse_messages, "", Literales.btn_text_accept);
                }
                else
                {
                    await DialogService.ShowAlertAsync(Literales.toast_not_all_messages_refused, "", Literales.btn_text_accept);
                }
            }
            IsBusy = false;
        }
    }
}