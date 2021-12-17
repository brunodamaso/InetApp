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
        private List<MessageModel> messageModelApi;

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
                messageModelApi = messagesDto.MessagesModel;
                await MarkFavoriteAsync(categoryId);
            }
            return messagesDto;
        }

        public void MarkFavorite(int categoryId, ref List<MessageModel> _messagesModelApi)
        {
            messageModelApi = _messagesModelApi;
            MarkFavoriteAsync(categoryId);
        }
        public async Task MarkFavoriteAsync(int categoryId)
        {
            List<MessageModel> messagesModel = await repositoryService.GetItemsWhere<MessageModel>(a => a.categoryId == categoryId);
            if (messagesModel.Count > 0)
            {
                foreach (MessageModel item in messageModelApi)
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
        }

        //public async Task<bool> ApproveMessageAsync(int categoryId, int messageId)
        //{
        //    return await repositoryService.ApproveMessageAsync(categoryId, messageId);
        //}

        


    }
}