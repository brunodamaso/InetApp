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
using Xamarin.Forms;

namespace INetApp.ViewModels
{
    public class MessageViewModel : ViewModelBase
    {
        private ObservableCollection<MessageModel> _MessageItems;
        private string _mensajeListView;
        private readonly IMessageService MessageService;
        private int _selectecTab;
        private bool _SelectAll, IsChangeTab;
        private string _Title;
        private int CategoryID;
        private bool _RowChecked =false;
        #region Properties

        public List<MessageModel> MessageList;

        public ObservableCollection<MessageModel> MessageItems
        {
            get => _MessageItems;
            set
            {
                _MessageItems = value;
                RaisePropertyChanged(() => this.MessageItems);
            }
        }

        public string MensajeListView
        {
            get => _mensajeListView;
            set
            {
                _mensajeListView = value;
                RaisePropertyChanged(() => this.MensajeListView);
            }
        }
        public string Title
        {
            get => _Title;
            set
            {
                _Title = value;
                RaisePropertyChanged(() => this.Title);
            }
        }
        public bool SelectAll
        {
            get => _SelectAll;
            set
            {
                _SelectAll = value;
                RaisePropertyChanged(() => this.SelectAll);
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
                RaisePropertyChanged(() => this.SelectecTab);
                OnSelectTab(value);
                IsChangeTab = true;
                this.SelectAll = MessageList.Where(a => a.checkeado).Count() == this.MessageItems.Count;
                IsChangeTab = false;

            }
        }
        public bool IsRowChecked
        {
            get => _RowChecked;
            set
            {
                _RowChecked = value;
                RaisePropertyChanged(() => this.IsRowChecked);
            }
        }

        #endregion

        public ICommand SelectMessageCommand => new Command<MessageModel>(OnSelectMessage);

        public MessageViewModel()
        {
            MessageService = DependencyService.Get<IMessageService>();
        }

        public override async Task InitializeAsync(IDictionary<string, string> query)
        {
            if (query.TryGetValue("Name", out string title))
            {
                this.Title = title;
            }
            if (query.TryGetValue("CategoryId", out string category))
            {
                CategoryID = int.Parse(category);
            }

            await Sincroniza();
        }
        private async Task Sincroniza()
        {
            this.IsBusy = true;

            this.MensajeListView = "Cargando Datos";
            MessageDto messageDto = await MessageService.GetMessageAsync(CategoryID);

            MessageList = messageDto.IsOk ? messageDto.MessageModels : new List<MessageModel>();

            OnSelectTab(_selectecTab);

            this.MensajeListView = Literales.empty_categories;
            this.Text_last_update = string.Format(Literales.view_text_last_updated, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));

            this.IsBusy = false;
        }

        private async void OnSelectMessage(MessageModel messageModel)
        {
            this.IsBusy = true;
            await Sincroniza();
            this.IsBusy = false;
        }

        private void OnSelectAll(bool TrueFalse)
        {
            foreach (MessageModel item in MessageList.Where(a => _selectecTab != 1 || a.checkeado))
            {
                item.checkeado = TrueFalse;
            }
            OnSelectTab(_selectecTab);
        }
        private void OnSelectTab(int selectedTab)
        {
            this.MessageItems = selectedTab == 0
                ? new ObservableCollection<MessageModel>(MessageList)
                : new ObservableCollection<MessageModel>(MessageList.Where(a => a.checkeado));
        }

        public bool IsRowSelect()
        {
            this.IsRowChecked = this.MessageItems.Any(a => a.checkeado);
            return this.IsRowChecked;
        }
    }
}