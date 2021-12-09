using INetApp.APIWebServices.Dtos;
using INetApp.APIWebServices.Responses;
using System.Threading.Tasks;

namespace INetApp.APIWebServices
{
    public interface IRestApi
    {
        Task<UserLoggedDto> GetUserLoggedFromApi(string usuario, string password);
        Task<string> GetUserLoggedPermission(string usuario, string password);
        Task<UserLoggedDto> GetVersion(string usuario, string password);
        Task<CategorysDto> GetCategoriesFromApi(string usuario, string password);
        Task<MessagesDto> GetMessagesFromApi(string usuario, string password, int categoryId);
        Task<MessageDto> GetMessageDetailsFromApi(string usuario, string password, int categoryId, int messageId);


        //Task<TDto> GetDatos<TDto, TResponse>(string Tabla) where TResponse : Response where TDto : BaseDto, new();

    }
}
