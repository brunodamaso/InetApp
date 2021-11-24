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
        Task<CategoryDto> GetCategoryFromApi(string usuario, string password);
        Task<MessageDto> GetMessageFromApi(string usuario, string password);


        //Task<TDto> GetDatos<TDto, TResponse>(string Tabla) where TResponse : Response where TDto : BaseDto, new();

    }
}
