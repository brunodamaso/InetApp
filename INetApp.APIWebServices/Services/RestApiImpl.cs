using System.Collections.Generic;
using System.Threading.Tasks;
using INetApp.APIWebServices.Dtos;
using INetApp.APIWebServices.Entity;
//using INetApp.Models;
using INetApp.APIWebServices.Helpers;
using INetApp.APIWebServices.Responses;

namespace INetApp.APIWebServices
{
    public class RestApiImpl : APIWebService, IRestApi
    {
        public RestApiImpl()
        {
            Mappers.ServiceMapster.CreateMapper();
        }

        #region EndPoint

        private static readonly string https = "https://";
        //pre
        private static readonly string restService = "inet-pre.ineco.es/WS/WSAppMovilAprobacion/WSAppMovilAprobacion.svc/rest/";
        //local
        // string restService = "192.168.1.33/WSAppMovilAprobacion/WSAppMovilAprobacion.svc/rest/";
        //private static readonly string restService = "localhost:62173/WSAppMovilAprobacion.svc/rest/";
        //pro
        //private static readonly string restService = "inet.ineco.es/WebSvc/WSAppMovilAprobacion/WSAppMovilAprobacion.svc/rest/";

        private static readonly string API_BASE_URL = https + restService;

        /**
         * Api url for getting a user profile: Remember to concatenate id + 'json'
         */
        private readonly string API_URL_GET_USER_LOGGED = API_BASE_URL + "getuser/";

        /**
         * Api url for getting messages by category: Remember to concatenate id'
         */
        private readonly string API_URL_GET_MESSAGE_LIST_BY_CATEGORY = API_BASE_URL + "detalleBandejas/";

        /**
         * Api url for getting a message details: Remember to concatenate: categoryId/messageId
         */
        private readonly string API_URL_GET_MESSAGE_DETAILS = API_BASE_URL + "detallesolicitud/";

        /**
         * Api url for getting a message details: Remember to concatenate: categoryId/messageId
         */
        private readonly string API_URL_REFUSE_MESSAGE = API_BASE_URL + "rechazarsolicitud/";

        /**
         * Api url for getting a message details: Remember to concatenate: categoryId/messageId
         */
        private readonly string API_URL_APPROVE_MESSAGE = API_BASE_URL + "AprobarSolicitud/";

        /**
         * Api url for getting all messages
         */
        private readonly string API_URL_GET_CATEGORY_LIST = API_BASE_URL + "getBandejas/";

        /**
         * Api for register token user
         */
        private readonly string API_URL_GET_REGISTERTOKEN = API_BASE_URL + "registrartoken/";

        private readonly string API_URL_GET_UNREGISTERTOKEN = API_BASE_URL + "eliminartoken/";


        //    PARTES
        /**
         * Api url for getting all options
         */
        private readonly string API_URL_GET_OPTIONS_LIST = API_BASE_URL + "GetAplicaciones/";

        /**
         * Api url for getting all options
         */
        private readonly string API_URL_GET_USER_PERMISSION = API_BASE_URL + "tienepermisobandeja/";

        /**
         * Api url for getting all projects
         */
        private readonly string API_URL_GET_INECO_PROJECTS_LIST = API_BASE_URL + "GetProyectosIneco/";

        /**
         * Api url for getting search projects
         */
        private readonly string API_URL_GET_SEARCH_PROJECTS = API_BASE_URL + "getproyectosbuscador/";

        /**
         * Api url for getting all options
         */
        private readonly string API_URL_SET_OPTIONS_LIST = API_BASE_URL + "SetAplicaciones/";

        /**
         * Api url for getting current part
         */
        private readonly string API_URL_GET_CURRENT_PART = API_BASE_URL + "getparteactual/";

        /**
         * Api url for getting calendar part
         */
        private readonly string API_URL_GET_CALENDAR_PART = API_BASE_URL + "GetPartePorFechas/";

        /**
         * Api url for getting week part
         */
        private readonly string API_URL_GET_WEEK_PART = API_BASE_URL + "GetParteIdSemana/";

        /**
         * Api url for save part
         */
        private readonly string API_URL_SAVE_PART = API_BASE_URL + "GuardarParte/";


        //BIGAALCA-2018/05/02
        /**
         * Api url for getting version
         */
        private readonly string API_URL_GET_VERSION = API_BASE_URL + "getversion/android";
        //FIN BIGAALCA-2018/05/02

        //BIGAALCA-2020/05/14
        /**
         * Api url for getting periodo activo
         */
        private readonly string API_URL_GET_PERIODO_ACTIVO = API_BASE_URL + "getperiodoactivo/";
        //FIN BIGAALCA-2020/05/14

        //BIGAALCA-2018/05/02
        /**
         * Api url for getting acceso
         */
        private readonly string API_URL_GET_ACCESO = API_BASE_URL + "getaccesopermitido/";
        //FIN BIGAALCA-2018/05/02

        /**
         * Api url for getting acceso by nfc
         */
        private readonly string API_URL_GET_ACCESO_BY_NFC = API_BASE_URL + "getaccesopermitidonfc/";

        #endregion

        #region user
        public Task<UserLoggedDto> GetUserLoggedFromApi(string Usuario, string Password)
        {
            return Task.Factory.StartNew(() =>
            {
                //var json = ServiceHelper.ToJson(request);

                HttpResponse httpResponse = Get(API_URL_GET_USER_LOGGED, Usuario, Password).Result;

                ServiceResponse<UserLoggedEntity> response = ServiceHelper.CreateResponse<UserLoggedEntity>(httpResponse);

                return Mappers.ServiceMapper.ConvertToBusiness<UserLoggedDto, UserLoggedEntity>(response);
            });
        }

        public Task<string> GetUserLoggedPermission(string Usuario, string Password)
        {
            return Task.Factory.StartNew(() =>
            {
                HttpResponse httpResponse = Get(API_URL_GET_USER_PERMISSION, Usuario, Password).Result;
                if (httpResponse.IsOk)
                {
                    return httpResponse.Resultado;
                }
                else
                {
                    return "false";
                }
            });
        }

        public Task<UserLoggedDto> GetVersion(string Usuario, string Password)
        {
            return Task.Factory.StartNew(() =>
            {
                HttpResponse httpResponse = Get(API_URL_GET_VERSION, Usuario, Password).Result;

                ServiceResponse<UserLoggedEntity> response = ServiceHelper.CreateResponse<UserLoggedEntity>(httpResponse);

                return Mappers.ServiceMapper.ConvertToBusiness<UserLoggedDto, UserLoggedEntity>(response);

            });
        }
        #endregion

        #region Category
        public Task<CategorysDto> GetCategoriesFromApi(string Usuario, string Password)
        {
            return Task.Factory.StartNew(() =>
            {
                HttpResponse httpResponse = Get(API_URL_GET_CATEGORY_LIST, Usuario, Password).Result;

                httpResponse.Resultado = $"{{CategoryEntity:{httpResponse.Resultado}}}";

                ServiceResponse<CategorysEntity> response = ServiceHelper.CreateResponse<CategorysEntity>(httpResponse);

                return Mappers.ServiceMapper.ConvertToBusiness<CategorysDto, CategorysEntity>(response);

            });
        }
        #endregion
        #region message
        public Task<MessagesDto> GetMessagesFromApi(string Usuario, string Password, int categoryId)
        {
            return Task.Factory.StartNew(() =>
            {
                HttpResponse httpResponse = Get(API_URL_GET_MESSAGE_LIST_BY_CATEGORY + categoryId, Usuario, Password).Result;

                httpResponse.Resultado = $"{{MessagesEntitys:{httpResponse.Resultado}}}";

                ServiceResponse<MessagesEntity> response = ServiceHelper.CreateResponse<MessagesEntity>(httpResponse);

                return Mappers.ServiceMapper.ConvertToBusiness<MessagesDto, MessagesEntity>(response);

            });
        }

        public Task<MessageDto> GetMessageDetailsFromApi(string Usuario, string Password, int categoryId, int messageId)
        {
            return Task.Factory.StartNew(() =>
            {
                HttpResponse httpResponse = Get(API_URL_GET_MESSAGE_DETAILS + categoryId + "/" + messageId, Usuario, Password).Result;

                httpResponse.Resultado = $"{{MessagesEntitys:{httpResponse.Resultado}}}";

                ServiceResponse<MessageEntity> response = ServiceHelper.CreateResponse<MessageEntity>(httpResponse);

                return Mappers.ServiceMapper.ConvertToBusiness<MessageDto, MessageEntity>(response);

            });
        }
        #endregion

        //public Task<CategoriasDto> ResetPassword(string Mail, string Usuario, string Password)
        //{
        //    RstPasswordRequest request = new RstPasswordRequest
        //    {
        //        mail = Mail,
        //        user = Usuario,
        //        pass = Password
        //    };
        //    return Task.Factory.StartNew(() =>
        //    {
        //        string json = ServiceHelper.ToJson(request);

        //        HttpResponse httpResponse = Post(API_URL_GET_USER_LOGGED, json);

        //        // httpResponse.Content = $"{{Result:'{httpResponse.Content}'}}";

        //        ServiceResponse<RstPasswordResponse> response = ServiceHelper.CreateResponse<RstPasswordResponse>(httpResponse);

        //        return Mappers.ServiceMapper.ConvertToBusiness(response);
        //    });
        //}              
    }
}
