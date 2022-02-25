using System;
using System.Collections.Generic;
using INetApp.Models;

using System.Collections.ObjectModel;
using System.Threading.Tasks;
using INetApp.APIWebServices.Dtos;
using INetApp.APIWebServices.Responses;


namespace INetApp.Services
{
    public interface IRepositoryWebService
    {
        Task<UserLoggedDto> GetUserLogged(string usuario, string password);
        Task<CategorysDto> GetCategory();
        Task<MessagesDto> GetMessages(int categoryId);
        Task<MessageDto> GetMessageDetails(int categoryId, int messageId);
        Task<bool> ApproveMessage(MessageModel messageModel, bool isList = false);
        Task<bool> ApproveMessages(List<MessageModel> messageModels);
        Task<bool> RefuseMessage(MessageModel messageModel, string cause, bool isList = false);
        Task<bool> RefuseMessages(List<MessageModel> messageModels, string cause);
        Task<OptionsDto> GetOptions();
        Task<bool> MarkOptions(List<OptionsModel> optionsModels);
        Task<UserAccessDto> GetAccesoQR(string QR);
        Task<UserAccessDto> GetAccesoNFC(string NFC);
        Task<bool> RegisterTokenPush(string Token);
        Task<bool> UnRegisterTokenPush();
        Task<WorkPartsDto> GetWorkParts(string fechaini = null, string fechafin = null, int? semanaid = null);

    }
}
