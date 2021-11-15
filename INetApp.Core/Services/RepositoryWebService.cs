using System;
using System.Threading.Tasks;
using INetApp.APIWebServices;
using INetApp.APIWebServices.Dtos;
using INetApp.APIWebServices.Responses;
using INetApp.Services.Identity;
using INetApp.Services.Settings;
using INetApp.Models;
using INetApp.Services;

namespace INetApp.Services
{

    public class RepositoryWebService : IRepositoryWebService
    {
        #region Variables
        protected readonly IAPIWebService apiWebService;
        //protected readonly AppNavigationService Navigation;
        //protected readonly AppSettingsService settings;
        protected readonly ConnectivityService connectivityService;
        //protected readonly IDeviceService deviceService;
        protected readonly IIdentityService identityService;

        #endregion
        public RepositoryWebService(IAPIWebService _apiWebService , IIdentityService _identityService)
        {
            connectivityService = new ConnectivityService();
            apiWebService = _apiWebService;
            identityService = _identityService;
        }
        public async Task<UserLoggedDto> GetUserLogged(string Usuario, string Password)
        {
            UserLoggedDto userLoggedDto = new UserLoggedDto();
            try
            {
                if (connectivityService.CheckConnectivity())
                {
                    userLoggedDto = await apiWebService.GetUserLoggedFromApi(Usuario, Password);

                    if (userLoggedDto.IsOk)
                    {
                        if (!userLoggedDto.UserLoggedModel.username.Equals(""))
{
                            string responseUserPermission = await apiWebService.GetUserLoggedPermission(Usuario, Password);
                            UserLoggedDto responseVersion = await apiWebService.GetVersion(Usuario, Password);
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
                    //subscriber.OnError(new NetworkConnectionException());
                }

            }
            catch (Exception)
            {
                userLoggedDto.IsOk = false;
            }

            return userLoggedDto;
        }

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

        #endregion
    }


}