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
    public class MessageDetailsViewModel : ViewModelBase
    {
        private MessageModel _MessageDetails;
        private readonly IMessageService MessageService;
        private int CategoryID, MessageId;
        private string _Date,_Name;
        private bool _Favorite;
        #region Properties

        public MessageModel MessageDetail
        {
            get => _MessageDetails;
            set
            {
                _MessageDetails = value;
                RaisePropertyChanged(() => MessageDetail);
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
        public string Name
        {
            get => _Name;
            set
            {
                _Name = value;
                RaisePropertyChanged(() => Name);
            }
        }
        public bool Favorite
        {
            get => _Favorite;
            set
            {
                _Favorite = value;
                RaisePropertyChanged(() => Favorite);
            }
        }

        #endregion

        public ICommand SelectMessageCommand => new Command<MessageModel>(OnSelectMessage);

        public MessageDetailsViewModel()
        {
            MessageService = DependencyService.Get<IMessageService>();
        }

        public override async Task InitializeAsync(IDictionary<string, string> query)
        {
            if (query.TryGetValue("MessageId", out string messageid))
            {
                MessageId = int.Parse(messageid);
            }
            if (query.TryGetValue("CategoryId", out string category))
            {
                CategoryID = int.Parse(category);
            }
            if (query.TryGetValue("Date", out string date))
            {
                Date = Uri.UnescapeDataString(date);
            }
            if (query.TryGetValue("Name", out string name))
            {
                Name = Uri.UnescapeDataString(name);
            }
            if (query.TryGetValue("Favorite", out string favorite))
            {
                Favorite = favorite == "True";
            }

            await Sincroniza();
            await base.InitializeAsync(query);
        }
        private async Task Sincroniza()
        {
            IsBusy = true;

            MessageDto messageDto = await MessageService.GetMessageDetailsAsync(CategoryID, MessageId);

            MessageDetail = messageDto.IsOk ? messageDto.MessageModel : new MessageModel();

            Text_last_update = string.Format(Literales.view_text_last_updated, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));

            IsBusy = false;
        }

        private async void OnSelectMessage(MessageModel messageModel)
        {
            IsBusy = true;
            Dictionary<string, string> Parametro = new Dictionary<string, string>
                {
                    { "CategoryId", messageModel.categoryId.ToString() },
                    { "MessageId", messageModel.messageId.ToString() }
                };
            await NavigationService.NavigateToAsync("MessageDetails", Parametro);
            IsBusy = false;
        }
    }
}