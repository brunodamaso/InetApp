using System;
using System.Threading.Tasks;
using INetApp.Services.RequestProvider;
using INetApp.Models.Basket;
using INetApp.Services.FixUri;
using INetApp.Helpers;
using System.Collections.Generic;
using INetApp.APIWebServices.Dtos;
using INetApp.Services.Settings;
using INetApp.ViewModels.Base;
using INetApp.Services.Identity;
using INetApp.Services;

namespace INetApp.Services
{
    public class MessageService : IMessageService
    {
        private readonly IRepositoryWebService repositoryWebService;
        private protected readonly IUserService userService;

        public MessageService(IRepositoryWebService _repositoryWebService)
        {           
            repositoryWebService = _repositoryWebService;
            userService = ViewModelLocator.Resolve<IUserService>();
        }

        public async Task<MessagesDto> GetMessageAsync(int categoryId)
        {
            MessagesDto messagesDto = await repositoryWebService.GetMessages(categoryId);
            if (messagesDto.IsOk)
            {

            }
            return messagesDto;
        }

        public async Task<MessageDto> GetMessageDetailsAsync(int categoryId, int messageId)
        {
            MessageDto messageDto = await repositoryWebService.GetMessageDetails(categoryId , messageId);
            if (messageDto.IsOk)
            {

            }
            return messageDto;
        }
    }
}