using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using INetApp.APIWebServices;
using INetApp.APIWebServices.Dtos;
using INetApp.Services.Identity;

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
                            //responseUserLogged.Result = responseUserLogged.Result.Replace("}", "," + '\u0022' + "permiso" + '\u0022' + ":" + responseUserPermission + ",");
                            //responseVersion.Result = responseVersion.Replace("{", responseUserLogged.Result);
                            //Console.WriteLine("PRUEBAAAAAA", responseUserLogged);
                            //Console.WriteLine("PRUEBAAAAAA", responseVersion);
                            //subscriber.OnNext(userLoggedEntityJsonMapper.transformUserLoggedEntity(responseVersion));
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
        public async Task<CategoryDto> GetCategory()
        {
            CategoryDto categoryDto = new CategoryDto();
            try
            {
                if (connectivityService.CheckConnectivity())
                {
                    GetUser();
                    categoryDto = await RestApiImpl.GetCategoryFromApi(userName, userPass);

                    if (categoryDto.IsOk)
                    {
                    }
                }
                else
                {
                    categoryDto.IsOk = false;
                    categoryDto.IsConnected = false;
                    //subscriber.OnError(new NetworkConnectionException());
                }
            }
            catch (Exception)
            {
                categoryDto.IsOk = false;
            }

            return categoryDto;
        }
        #endregion
        #region Message
        public async Task<MessageDto> GetMessage(int categoryId)
        {
            MessageDto messageDto = new MessageDto();
            try
            {
                if (connectivityService.CheckConnectivity())
                {
                    GetUser();
                    messageDto = await RestApiImpl.GetMessageFromApi(userName, userPass, categoryId);

                    if (messageDto.IsOk)
                    {
                    }
                }
                else
                {
                    messageDto.IsOk = false;
                    messageDto.IsConnected = false;
                    //subscriber.OnError(new NetworkConnectionException());
                }
            }
            catch (Exception)
            {
                messageDto.IsOk = false;
            }

            return messageDto;
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