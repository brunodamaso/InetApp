using INetApp.APIWebServices.Dtos;
using INetApp.APIWebServices.Responses;
using INetApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace INetApp.APIWebServices
{
    public interface IRestApi
    {
        Task<UserLoggedDto> GetUserLoggedFromApi(string Usuario, string Password);
        Task<string> GetUserLoggedPermission(string Usuario, string Password);
        Task<UserLoggedDto> GetVersion(string Usuario, string Password);
        Task<CategorysDto> GetCategoriesFromApi(string Usuario, string Password);
        Task<MessagesDto> GetMessagesFromApi(string Usuario, string Password, int CategoryId);
        Task<MessageDto> GetMessageDetailsFromApi(string Usuario, string Password, int CategoryId, int MessageId);
        Task<bool> ApproveMessageFromApi(string Usuario, string Password, int CategoryId, int MessageId);
        Task<bool> RefuseMessageFromApi(string Usuario, string Password, int CategoryId, int MessageId, string cause);
        Task<OptionsDto> GetOptionsEntitiesFromApi(string Usuario, string Password);
        Task<bool> MarkOptionsFromApi(string Usuario, string Password, string strOptionlist);
        Task<UserAccessDto> GetAccesoQRFromAPI(string Usuario, string Password, string QR);
        Task<UserAccessDto> GetAccesoNFCFromAPI(string Usuario, string Password, string NFC);
        Task<bool> RegisterToken(string Usuario, string Password, string Token);
        Task<bool> UnRegisterToken(string Usuario, string Password);

        //Task<TDto> GetDatos<TDto, TResponse>(string Tabla) where TResponse : Response where TDto : BaseDto, new();

    }
}
