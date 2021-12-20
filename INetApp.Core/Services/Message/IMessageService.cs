using INetApp.APIWebServices.Dtos;
using INetApp.Models;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace INetApp.Services
{
    public interface IMessageService
    {
        Task<MessagesDto> GetMessageAsync(int categoryId);
        Task<MessageDto> GetMessageDetailsAsync(int categoryId, int messageId);
        void MarkFavorite(int categoryId, ref List<MessageModel> messagesModelApi);
        Task<bool?> MarkMessageFavoriteAsync(MessageModel messageModel, bool IsFavorite);
        Task<bool> ApproveMessageAsync(MessageModel messageModel);
        Task<bool> ApproveMessagesAsync(List<MessageModel> messageModels);
        Task<bool> RefuseMessageAsync(MessageModel messageModel, string cause);
        Task<bool> RefuseMessagesAsync(List<MessageModel> messageModels, string cause);
    }
}