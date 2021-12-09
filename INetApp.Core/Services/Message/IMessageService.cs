using INetApp.APIWebServices.Dtos;
using INetApp.Models.Basket;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace INetApp.Services
{
    public interface IMessageService
    {
        Task<MessagesDto> GetMessageAsync(int categoryId);
        Task<MessageDto> GetMessageDetailsAsync(int categoryId, int messageId);
    }
}