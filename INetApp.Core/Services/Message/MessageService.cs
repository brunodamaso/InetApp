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

        public async Task<List<MessageModel>> GetMessageLocalAsync()
        {
            List<MessageModel> messages = await repositoryService.GetAll<MessageModel>();
           
            return messages;
        }

        //public void MarkFavorite(int categoryId, ref List<MessageModel> _messagesModelApi)
        //{
        //    messageModelApi = _messagesModelApi;
        //    MarkFavoriteAsync(categoryId);
        //}
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

        public async Task<bool> ApproveMessageAsync(MessageModel messageModel)
        {
            bool retorno = await repositoryWebService.ApproveMessage(messageModel);
            if (retorno)
            {
                await repositoryService.MarkMessageFavoriteAsync(messageModel, false);
            }
            return retorno;
        }

        public async Task<bool> ApproveMessagesAsync(List<MessageModel> messageModels)
        {
            bool retorno = await repositoryWebService.ApproveMessages(messageModels);
            if (retorno)
            {
                foreach (var item in messageModels)
                {
                    await repositoryService.MarkMessageFavoriteAsync(item, false);
                }
            }
            return retorno;
        }

        public async Task<bool> RefuseMessageAsync(MessageModel messageModel, string cause)
        {
            bool retorno = await repositoryWebService.RefuseMessage(messageModel, cause);
            if (retorno)
            {
                await repositoryService.MarkMessageFavoriteAsync(messageModel, false);
            }
            return retorno;
        }

        public async Task<bool> RefuseMessagesAsync(List<MessageModel> messageModels, string cause)
        {
            bool retorno = await repositoryWebService.RefuseMessages(messageModels, cause);
            if (retorno)
            {
                foreach (var item in messageModels)
                {
                    await repositoryService.MarkMessageFavoriteAsync(item, false);
                }
            }
            return retorno;
        }
    }
}