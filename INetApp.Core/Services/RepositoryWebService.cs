using INetApp.APIWebServices;
using INetApp.APIWebServices.Dtos;
using INetApp.Models;
using INetApp.Services.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace INetApp.Services
{

    public class RepositoryWebService : IRepositoryWebService
    {
        #region Variables
        protected readonly IRestApi RestApiImpl;
        //protected readonly AppNavigationService Navigation;
        //protected readonly AppSettingsService settings;
        protected readonly ConnectivityService connectivityService;
        //protected readonly IDeviceService deviceService;
        protected readonly IIdentityService identityService;
        private string userName;
        private string userPass;

        #endregion
        public RepositoryWebService(IRestApi _apiWebService, IIdentityService _identityService)
        {
            connectivityService = new ConnectivityService();
            RestApiImpl = _apiWebService;
            identityService = _identityService;
        }
        #region user
        public async Task<UserLoggedDto> GetUserLogged(string Usuario, string Password)
        {
            UserLoggedDto userLoggedDto = new UserLoggedDto();
            try
            {
                if (connectivityService.CheckConnectivity())
                {
                    userLoggedDto = await RestApiImpl.GetUserLoggedFromApi(Usuario, Password);

                    if (userLoggedDto.IsOk)
                    {
                        if (!userLoggedDto.UserLoggedModel.username.Equals(""))
                        {
                            //todo comparar version para pedir upgrade
                            //todo nfc
                            string responseUserPermission = await RestApiImpl.GetUserLoggedPermission(Usuario, Password);
                            UserLoggedDto responseVersion = await RestApiImpl.GetVersion(Usuario, Password);
                            userLoggedDto.UserLoggedModel.permission = responseUserPermission == "true";
                            userLoggedDto.UserLoggedModel.version = responseVersion.UserLoggedModel.version;
                            userLoggedDto.UserLoggedModel.url = responseVersion.UserLoggedModel.url;
                            userLoggedDto.UserLoggedModel.requerido = responseVersion.UserLoggedModel.requerido;
                            identityService.PutCredentialsFromPrefs(Usuario, Password);
                        }
                        else
                        {
                            userLoggedDto.IsOk = false;
                            userLoggedDto.ErrorCode = "UserNotFound";
                            //subscriber.OnError(new UserLoggedNotFoundException());
                        }
                    }
                    else
                    {
                        userLoggedDto.IsOk = false;
                    }
                }
                else
                {
                    userLoggedDto.IsOk = false;
                    userLoggedDto.IsConnected = false;
                    //subscriber.OnError(new NetworkConnectionException());
                }

            }
            catch (Exception)
            {
                userLoggedDto.IsOk = false;
            }

            return userLoggedDto;
        }
        #endregion

        #region Category
        public async Task<CategorysDto> GetCategory()
        {
            CategorysDto categorysDto = new CategorysDto();
            try
            {
                if (connectivityService.CheckConnectivity())
                {
                    GetUser();
                    categorysDto = await RestApiImpl.GetCategoriesFromApi(userName, userPass);

                    if (categorysDto.IsOk)
                    {
                    }
                }
                else
                {
                    categorysDto.IsOk = false;
                    categorysDto.IsConnected = false;
                    //subscriber.OnError(new NetworkConnectionException());
                }
            }
            catch (Exception)
            {
                categorysDto.IsOk = false;
            }

            return categorysDto;
        }
        #endregion

        #region Message
        public async Task<MessagesDto> GetMessages(int categoryId)
        {
            MessagesDto messagesDto = new MessagesDto();
            try
            {
                if (connectivityService.CheckConnectivity())
                {
                    GetUser();
                    messagesDto = await RestApiImpl.GetMessagesFromApi(userName, userPass, categoryId);

                    if (messagesDto.IsOk)
                    {
                        //BD viene categoryId vacio
                        messagesDto.MessagesModel.ForEach(a => a.categoryId = categoryId);
                    }
                }
                else
                {
                    messagesDto.IsOk = false;
                    messagesDto.IsConnected = false;
                }
            }
            catch (Exception)
            {
                messagesDto.IsOk = false;
            }

            return messagesDto;
        }

        public async Task<MessageDto> GetMessageDetails(int categoryId, int messageId)
        {
            MessageDto messageDto = new MessageDto();
            try
            {
                if (connectivityService.CheckConnectivity())
                {
                    GetUser();
                    messageDto = await RestApiImpl.GetMessageDetailsFromApi(userName, userPass, categoryId, messageId);

                    if (messageDto.IsOk)
                    {
                        int campo = 0;
                        messageDto.MessageModel.fields.Details = new List<Detail>();
                        foreach (Cabecera item in messageDto.MessageModel.fields.Cabecera)
                        {
                            Detail detail = new Detail();
                            switch (campo)
                            {
                                case 0:
                                    detail.Campo = messageDto.MessageModel.fields.Datos[0].Campo1;
                                    break;
                                case 1:
                                    detail.Campo = messageDto.MessageModel.fields.Datos[0].Campo2;
                                    break;
                                case 2:
                                    detail.Campo = messageDto.MessageModel.fields.Datos[0].Campo3;
                                    break;
                                case 3:
                                    detail.Campo = messageDto.MessageModel.fields.Datos[0].Campo4;
                                    break;
                                case 4:
                                    detail.Campo = messageDto.MessageModel.fields.Datos[0].Campo5;
                                    break;
                                case 5:
                                    detail.Campo = messageDto.MessageModel.fields.Datos[0].Campo6;
                                    break;
                                case 6:
                                    detail.Campo = messageDto.MessageModel.fields.Datos[0].Campo7;
                                    break;
                                case 7:
                                    detail.Campo = messageDto.MessageModel.fields.Datos[0].Campo8;
                                    break;
                                case 8:
                                    detail.Campo = messageDto.MessageModel.fields.Datos[0].Campo9;
                                    break;
                                case 9:
                                    detail.Campo = messageDto.MessageModel.fields.Datos[0].Campo10;
                                    break;
                                default:
                                    break;
                            }
                            detail.Nombre = item.Nombre;
                            detail.IsURL = item.Nombre == MessageModel.URL_LABEL;
                            campo++;
                            messageDto.MessageModel.fields.Details.Add(detail);
                        }
                        //messageDto.MessageModel.fields = JsonConvert.DeserializeObject<MessageDetails>(messageDto.MessageModel.data);
                    }
                }
                else
                {
                    messageDto.IsOk = false;
                    messageDto.IsConnected = false;
                }
            }
            catch (Exception)
            {
                messageDto.IsOk = false;
            }

            return messageDto;
        }

        public async Task<bool> ApproveMessage(MessageModel messageModel, bool isList = false)
        {
            bool resultado;
            try
            {
                if (isList || connectivityService.CheckConnectivity())
                {
                    if (!isList)
                    {
                        GetUser();
                    }
                    resultado = await RestApiImpl.ApproveMessageFromApi(userName, userPass, messageModel.categoryId, messageModel.messageId);

                    if (resultado)
                    {
                    }
                }
                else
                {
                    resultado = false;
                }
            }
            catch (Exception)
            {
                resultado = false;
            }

            return resultado;
        }

        public async Task<bool> ApproveMessages(List<MessageModel> messageModels)
        {
            bool resultado = true;
            try
            {
                if (connectivityService.CheckConnectivity())
                {
                    GetUser();
                    foreach (MessageModel item in messageModels)
                    {
                        if (!await ApproveMessage(item, true))
                        {
                            resultado = false;
                        }
                    }
                }
                else
                {
                    resultado = false;
                }
            }
            catch (Exception)
            {
                resultado = false;
            }

            return resultado;
        }

        public async Task<bool> RefuseMessage(MessageModel messageModel, string cause, bool isList = false)
        {
            bool resultado = true;
            try
            {
                if (isList || connectivityService.CheckConnectivity())
                {
                    if (!isList)
                    {
                        GetUser();
                    }
                    resultado = await RestApiImpl.RefuseMessageFromApi(userName, userPass, messageModel.categoryId, messageModel.messageId, cause);

                    if (resultado)
                    {
                    }
                }
                else
                {
                    resultado = false;
                }
            }
            catch (Exception)
            {
                resultado = false;
            }

            return resultado;
        }

        public async Task<bool> RefuseMessages(List<MessageModel> messageModels, string cause)
        {
            bool resultado = true;
            try
            {
                if (connectivityService.CheckConnectivity())
                {
                    GetUser();
                    foreach (MessageModel item in messageModels)
                    {
                        if (!await RefuseMessage(item, cause, true))
                        {
                            resultado = false;
                        }
                    }
                }
            }
            catch (Exception)
            {
                resultado = false;
            }

            return resultado;
        }
        #endregion

        //public async Task<TDto> GetDatos<TDto, TResponse>(string Tabla) where TResponse : Response where TDto : BaseDto, new()
        //{
        //    //BaseDto dto =new BaseDto(true, string.Empty, string.Empty, true);
        //    TDto dto = new TDto();

        //    if (connectivityService.CheckConnectivity())
        //    {
        //        dto = await apiWebService.GetDatos<TDto, TResponse>(Tabla);

        //        if (dto.IsOk)
        //        {
        //            return dto;
        //        }
        //    }
        //    dto.IsOk = false;

        //    return dto;
        //}


        #region private
        private void GetUser()
        {
            KeyValuePair<string, object> credenciales = identityService.GetCredentialsFromPrefs();
            userName = credenciales.Key.ToString();
            userPass = credenciales.Value.ToString();
        }

        #endregion
    }


}