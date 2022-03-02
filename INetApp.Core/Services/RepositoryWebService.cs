using INetApp.APIWebServices;
using INetApp.APIWebServices.Dtos;
using INetApp.APIWebServices.Entity;
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
                            string responseUserPermission = await RestApiImpl.GetUserLoggedPermission(Usuario, Password);
                            UserLoggedDto responseVersion = await RestApiImpl.GetVersion(Usuario, Password);
                            userLoggedDto.UserLoggedModel.permission = responseUserPermission == "true";
                            userLoggedDto.UserLoggedModel.version = responseVersion.UserLoggedModel.version;
                            userLoggedDto.UserLoggedModel.url = responseVersion.UserLoggedModel.url;
                            userLoggedDto.UserLoggedModel.requerido = responseVersion.UserLoggedModel.requerido;
                            identityService.PutCredentialsFromPrefs(Usuario, Password , userLoggedDto.UserLoggedModel);
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

        #region Options

        public async Task<OptionsDto> GetOptions()
        {
            OptionsDto optionsDto = new OptionsDto();
            try
            {
                if (connectivityService.CheckConnectivity())
                {
                    GetUser();
                    optionsDto = await RestApiImpl.GetOptionsEntitiesFromApi(userName, userPass);

                    if (optionsDto.IsOk)
                    { }
                }
                else
                {
                    optionsDto.IsOk = false;
                    optionsDto.IsConnected = false;
                }
            }
            catch (Exception)
            {
                optionsDto.IsOk = false;
            }

            return optionsDto;
        }

        public async Task<bool> MarkOptions(List<OptionsModel> optionList)
        {
            bool resultado = true;
            try
            {
                if (connectivityService.CheckConnectivity())
                {
                    string strOptionlist = "";
                    foreach (OptionsModel optionId in optionList)
                    {
                        if (!strOptionlist.Equals(""))
                        {
                            strOptionlist += ",";
                        }

                        strOptionlist += optionId.optionsId.ToString();
                    }

                    GetUser();
                    resultado = await RestApiImpl.MarkOptionsFromApi(userName, userPass, strOptionlist);
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
        #endregion

        #region AccesoQR
        public async Task<UserAccessDto> GetAccesoQR(string QR)
        {
            UserAccessDto userAccessDto = new UserAccessDto();
            try
            {
                if (connectivityService.CheckConnectivity())
                {
                    GetUser();
                    userAccessDto = await RestApiImpl.GetAccesoQRFromAPI(userName, userPass, QR);
                    if (userAccessDto.IsOk)
                    {
                        if (!userAccessDto.UserAccessModel.respuesta)
                        {
                            userAccessDto.IsOk = false;
                            userAccessDto.ErrorDescription = userAccessDto.UserAccessModel.mensaje;
                        }
                    }
                }
                else
                {
                    userAccessDto.IsOk = false;
                    userAccessDto.IsConnected = false;
                }

            }
            catch (Exception)
            {
                userAccessDto.IsOk = false;
            }

            return userAccessDto;
        }
        #endregion

        #region AccesoNFC
        public async Task<UserAccessDto> GetAccesoNFC(string NFC)
        {
            UserAccessDto userAccessDto = new UserAccessDto();
            try
            {
                if (connectivityService.CheckConnectivity())
                {
                    GetUser();
                    userAccessDto = await RestApiImpl.GetAccesoNFCFromAPI(userName, userPass, NFC);
                    if (userAccessDto.IsOk)
                    {
                        if (!userAccessDto.UserAccessModel.respuesta)
                        {
                            userAccessDto.IsOk = false;
                            userAccessDto.ErrorDescription = userAccessDto.UserAccessModel.mensaje;
                        }
                    }
                }
                else
                {
                    userAccessDto.IsOk = false;
                    userAccessDto.IsConnected = false;
                }

            }
            catch (Exception)
            {
                userAccessDto.IsOk = false;
            }

            return userAccessDto;
        }
        #endregion

        #region TokenPush
        public async Task<bool> RegisterTokenPush(string Token)
        {
            bool result = true;
            try
            {
                if (connectivityService.CheckConnectivity())
{
                    byte[] tokeb64 = System.Text.Encoding.UTF8.GetBytes(Token);
                    string Token64 = Convert.ToBase64String(tokeb64,Base64FormattingOptions.None);

                    result = await RestApiImpl.RegisterToken(userName, userPass, Token64);

                    if (result)
                    {

                    }
                }
            }
            catch (Exception)
            {
                result = false;
            }

            return result;
        }

        public async Task<bool> UnRegisterTokenPush()
        {
            bool result = true;
            try
            {
                if (connectivityService.CheckConnectivity())
                {
                    result = await RestApiImpl.UnRegisterToken(userName, userPass);

                    if (result)
                    {

                    }
                }
            }
            catch (Exception)
            {
                result = false;
            }

            return result;
        }
        #endregion

        #region WorkParts
        public async Task<WorkPartsDto> GetWorkParts(string FechaIni = null, string FechaFin = null, int? IdSemana = null)
        {
            WorkPartsDto workPartsDto = new WorkPartsDto();
            try
            {
                if (connectivityService.CheckConnectivity())
                {
                    GetUser();
                    workPartsDto = await RestApiImpl.GetWorkPartsEntitiesFromApi(userName, userPass, FechaIni, FechaFin, IdSemana);

                    if (workPartsDto.IsOk)
                    { }
                }
                else
                {
                    workPartsDto.IsOk = false;
                    workPartsDto.IsConnected = false;
                }
            }
            catch (Exception)
            {
                workPartsDto.IsOk = false;
            }

            return workPartsDto;
        }

        public async Task<InecoProjectsDto> GetInecoProjects(bool ineco, string pronumero, string titulo)
        {
            InecoProjectsDto inecoProjectsDto = new InecoProjectsDto();
            try
            {
                if (connectivityService.CheckConnectivity())
                {
                    GetUser();
                    inecoProjectsDto = await RestApiImpl.GetInecoProjecsEntitiesFromApi(userName, userPass, ineco, pronumero, titulo);

                    if (inecoProjectsDto.IsOk)
                    { }
                }
                else
                {
                    inecoProjectsDto.IsOk = false;
                    inecoProjectsDto.IsConnected = false;
                }
            }
            catch (Exception)
            {
                inecoProjectsDto.IsOk = false;
            }

            return inecoProjectsDto;
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