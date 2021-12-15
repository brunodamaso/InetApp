using System.Threading.Tasks;
using INetApp.APIWebServices.Dtos;
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

            }
            return messagesDto;
        }

        public async Task<MessageDto> GetMessageDetailsAsync(int categoryId, int messageId)
        {
            MessageDto messageDto = await repositoryWebService.GetMessageDetails(categoryId, messageId);
            if (messageDto.IsOk)
            {

            }
            return messageDto;
        }
        public async Task<bool?> MarkMessageFavoriteAsync(int categoryId, int messageId, bool IsFavorite)
        {
            return await repositoryService.MarkMessageFavoriteAsync(categoryId, messageId, !IsFavorite);

            //return resultado;
        }

    }
}