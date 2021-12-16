using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using INetApp.APIWebServices.Dtos;
using INetApp.Models;
using INetApp.ViewModels.Base;

namespace INetApp.Services
{
    public class MessageService : IMessageService
    {
        private readonly IRepositoryWebService repositoryWebService;
        private readonly IRepositoryService repositoryService;
        private protected readonly IUserService userService;

        public MessageService(IRepositoryWebService _repositoryWebService, IRepositoryService _repositoryService)
        {
            repositoryWebService = _repositoryWebService;
            repositoryService = _repositoryService;
            userService = ViewModelLocator.Resolve<IUserService>();
        }

        public async Task<MessagesDto> GetMessageAsync(int categoryId)
        {
            MessagesDto messagesDto = await repositoryWebService.GetMessages(categoryId);
            if (messagesDto.IsOk)
            {
                List<MessageModel> messagemodelapi = messagesDto.MessagesModel;
                MarkFavorite(categoryId, ref messagemodelapi);
            }
            return messagesDto;
        }

        public void MarkFavorite(int categoryId, ref List<MessageModel> messagesModelApi)
        {
            List<MessageModel> messagesModel = repositoryService.GetItemsWhere<MessageModel>(a => a.categoryId == categoryId).Result;
            if (messagesModel.Count > 0)
            {
                foreach (MessageModel item in messagesModelApi)
                {
                    MessageModel messageModel = messagesModel.FirstOrDefault(a => a.messageId == item.messageId);
                    if (messageModel != null)
                    {
                        item.favorite = messageModel.favorite;
                    }
                }
            }
        }

        public async Task<MessageDto> GetMessageDetailsAsync(int categoryId, int messageId)
        {
            MessageDto messageDto = await repositoryWebService.GetMessageDetails(categoryId, messageId);
            if (messageDto.IsOk)
            {

            }
            return messageDto;
        }
        public async Task<bool?> MarkMessageFavoriteAsync(MessageModel messageModel, bool IsFavorite)
        {
            return await repositoryService.MarkMessageFavoriteAsync(messageModel, IsFavorite);

            //return resultado;
        }

    }
}