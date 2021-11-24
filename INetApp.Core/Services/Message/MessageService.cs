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

        public async Task<MessageDto> GetMessageAsync()
        {
            MessageDto messageDto = await repositoryWebService.GetMessage();
            if (messageDto.IsOk)
            {
              
            }
            return messageDto;
        }
    }
}