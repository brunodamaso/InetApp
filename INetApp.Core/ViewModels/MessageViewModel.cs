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
        private bool _RowChecked = false;
        #region Properties

        public List<MessageModel> MessageList;

        public ObservableCollection<MessageModel> MessageItems
        {
            get => _MessageItems;
            set
            {
                _MessageItems = value;
                RaisePropertyChanged(() => MessageItems);
            }
        }

        public string MensajeListView
        {
            get => _mensajeListView;
            set
            {
                _mensajeListView = value;
                RaisePropertyChanged(() => MensajeListView);
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

        public MessageViewModel()
        {
            MessageService = DependencyService.Get<IMessageService>();
        }

        public override async Task InitializeAsync(IDictionary<string, string> query)
        {
            if (query.TryGetValue("Name", out string title))
            {
                Title = Uri.UnescapeDataString(title);
            }
            if (query.TryGetValue("CategoryId", out string category))
            {
                CategoryID = int.Parse(category);
            }

            await Sincroniza();
            await base.InitializeAsync(query);
        }
        private async Task Sincroniza()
        {
            IsBusy = true;

            MensajeListView = "Cargando Datos";
            MessageDto messageDto = await MessageService.GetMessageAsync(CategoryID);

            MessageList = messageDto.IsOk ? messageDto.MessageModels : new List<MessageModel>();

            OnSelectTab(_selectecTab);

            MensajeListView = Literales.empty_categories;
            Text_last_update = string.Format(Literales.view_text_last_updated, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));

            IsBusy = false;
        }

        private async void OnSelectMessage(MessageModel messageModel)
        {
            IsBusy = true;
            await Sincroniza();
            IsBusy = false;
        }

        private void OnSelectAll(bool TrueFalse)
        {
            //todo cambiar a favoritos en vez de marcados @-)
            IsBusy = true;
            foreach (MessageModel item in MessageList.Where(a => _selectecTab != 1 || a.checkeado))
            {
                item.checkeado = TrueFalse;
            }
            OnSelectTab(_selectecTab);

            IsRowChecked = TrueFalse;
            IsBusy = false;
        }
        private void OnSelectTab(int selectedTab)
        {
            MessageItems = selectedTab == 0
                ? new ObservableCollection<MessageModel>(MessageList)
                : new ObservableCollection<MessageModel>(MessageList.Where(a => a.checkeado));
        }

        public bool IsRowSelect()
        {
            IsChangeTab = true;
            SelectAll = MessageItems.Count == MessageItems.Count(a => a.checkeado);
            IsChangeTab = false;

            IsRowChecked = MessageItems.Any(a => a.checkeado);
            return IsRowChecked;
        }
    }
}