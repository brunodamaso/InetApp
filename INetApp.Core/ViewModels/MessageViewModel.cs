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
//using Newtonsoft.Json;
using Xamarin.Forms;
using System.Text.Json;


namespace INetApp.ViewModels
{
    public class MessageViewModel : ViewModelBase
    {
        private ObservableCollection<MessageModel> _MessageItems;
        private readonly IMessageService messageService;
        private int _selectecTab;
        private bool _SelectAll, IsChangeTab;
        private string _Title;
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
        public string Title
        {
            get => _Title;
            set
            {
                _Title = value;
                RaisePropertyChanged(() => Title);
            }
        }
        public bool SelectAll
        {
            get => _SelectAll;
            set
            {
                _SelectAll = value;
                RaisePropertyChanged(() => SelectAll);
                if (!IsChangeTab)
                {
                    OnSelectAll(value);
                }

            }
        }
        public int SelectecTab
        {
            get => _selectecTab;
            set
            {
                _selectecTab = value;
                RaisePropertyChanged(() => SelectecTab);
                OnSelectTab(value);
                IsChangeTab = true;
                SelectAll = MessageList.Count(a => a.checkeado) == MessageItems.Count;
                IsChangeTab = false;

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

        public MessageViewModel()
        {
            messageService = DependencyService.Get<IMessageService>();
        }

        public override async Task InitializeAsync(IDictionary<string, string> query)
        {
            Title = Uri.UnescapeDataString(query["Name"]);
            CategoryID = int.Parse(query["CategoryId"]);

            await Sincroniza();
            await base.InitializeAsync(query);
        }

        private async Task Sincroniza()
        {
            IsBusy = true;

            MessagesDto messagesDto = await messageService.GetMessageAsync(CategoryID);

            MessageList = messagesDto.IsOk ? messagesDto.MessagesModel.OrderBy(a => a.messageId).ToList() : new List<MessageModel>();

            OnSelectTab(_selectecTab);

            Text_last_update = string.Format(Literales.view_text_last_updated, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));

            IsBusy = false;
        }

        private async void OnSelectMessage(MessageModel messageModel)
        {
            IsBusy = true;
            string StringParametro = JsonSerializer.Serialize(messageModel);

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
            foreach (MessageModel item in MessageList.Where(a => _selectecTab != 1 || a.checkeado))
            {
                item.checkeado = TrueFalse;
            }
            OnSelectTab(_selectecTab);

            IsRowChecked = TrueFalse && MessageList.Count(a => a.checkeado) > 0;
            IsBusy = false;
        }
        private void OnSelectTab(int selectedTab)
        {
            MessageItems = selectedTab == 0
                ? new ObservableCollection<MessageModel>(MessageList)
                : new ObservableCollection<MessageModel>(MessageList.Where(a => a.favorite));
        }

        public bool IsRowSelect()
        {
            IsChangeTab = true;
            int canti = MessageItems.Count(a => a.checkeado);
            SelectAll = MessageItems.Count == canti;
            IsChangeTab = false;

            IsRowChecked = canti > 0;
            return IsRowChecked;
        }
        private async void OnAproveMessages()
        {
            IsBusy = true;
            
            if (await DialogService.ShowAlertAsync(Literales.dialog_approve_messages, Literales.dialog_approve_title, Literales.dialog_approve_positive, Literales.cancel))
            {
                List<MessageModel> messageModels = MessageItems.Where(a => a.checkeado).ToList();
            
                if (await messageService.ApproveMessagesAsync(messageModels))
                {
                    IsRowChecked = false;
                    await Sincroniza();
                    await DialogService.ShowAlertAsync(Literales.toast_approve_messages, "", Literales.btn_text_accept);
                    //IsInitialized = false;
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
                if (await messageService.RefuseMessagesAsync(messageModels, cause))
                {
                    await Sincroniza();
                    IsRowChecked = false;
                    await DialogService.ShowAlertAsync(Literales.toast_refuse_messages, "", Literales.btn_text_accept);
                    //IsInitialized = false;
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