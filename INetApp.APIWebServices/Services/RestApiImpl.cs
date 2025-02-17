using System.Collections.Generic;
using System.Threading.Tasks;
using INetApp.APIWebServices.Dtos;
using INetApp.APIWebServices.Entity;
using INetApp.APIWebServices.Helpers;
using INetApp.APIWebServices.Responses;
using System.Web;
using INetApp.Models;
using Xamarin.Forms;

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
        //private static readonly string restService = "inet-pre.ineco.es/WS/WSAppMovilAprobacion/WSAppMovilAprobacion.svc/rest/";
        ///local
        /// string restService = "192.168.1.33/WSAppMovilAprobacion/WSAppMovilAprobacion.svc/rest/";
        ///private static readonly string restService = "localhost:62173/WSAppMovilAprobacion.svc/rest/";
        //pro
        private static readonly string restService = "inet.ineco.es/WebSvc/WSAppMovilAprobacion/WSAppMovilAprobacion.svc/rest/";

        private static readonly string API_BASE_URL = https + restService;

        /**
         * Api url for getting a user profile: Remember to concatenate id + 'json'
         */
        private readonly string API_URL_GET_USER_LOGGED = API_BASE_URL + "getuser/";

        /**
         * Api url for getting all options
         */
        private readonly string API_URL_GET_USER_PERMISSION = API_BASE_URL + "tienepermisobandeja/";
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

        /**
         * Api url for getting all options
         */
        private readonly string API_URL_GET_OPTIONS_LIST = API_BASE_URL + "GetAplicaciones/";
        /**
         * Api url for getting all options
         */
        private readonly string API_URL_SET_OPTIONS_LIST = API_BASE_URL + "SetAplicaciones/";
        /**
         * Api url for getting version
         */
        private readonly string API_URL_GET_VERSION = API_BASE_URL + "getversion/";

        /**
         * Api url for getting acceso
         */
        private readonly string API_URL_GET_ACCESO = API_BASE_URL + "getaccesopermitido/";

        /**
         * Api url for getting acceso by nfc
         */
        private readonly string API_URL_GET_ACCESO_BY_NFC = API_BASE_URL + "getaccesopermitidonfc/";

        /**
	   * Delimitador
	   */
		private readonly string DELIMITER = "/";

		#endregion

		#region user
		public Task<UserLoggedDto> GetUserLoggedFromApi(string Usuario, string Password)
        {
            return Task.Factory.StartNew(() =>
            {
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
                return httpResponse.IsOk ? httpResponse.Resultado : "false";
            });
        }

        public Task<UserLoggedDto> GetVersion(string Usuario, string Password)
        {
            return Task.Factory.StartNew(() =>
            {
                HttpResponse httpResponse = Get(API_URL_GET_VERSION + Device.RuntimePlatform, Usuario, Password).Result;

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

                httpResponse.Resultado = string.Concat("{\"CategorysEntities\":", httpResponse.Resultado, "}");

                ServiceResponse<CategorysEntity> response = ServiceHelper.CreateResponse<CategorysEntity>(httpResponse);

                return Mappers.ServiceMapper.ConvertToBusiness<CategorysDto, CategorysEntity>(response);

            });
        }
        #endregion

        #region message
        public Task<MessagesDto> GetMessagesFromApi(string Usuario, string Password, int CategoryId)
        {
            return Task.Factory.StartNew(() =>
            {
                HttpResponse httpResponse = Get(API_URL_GET_MESSAGE_LIST_BY_CATEGORY + CategoryId, Usuario, Password).Result;

                httpResponse.Resultado = string.Concat("{\"MessagesEntities\":", httpResponse.Resultado, "}");

                ServiceResponse<MessagesEntity> response = ServiceHelper.CreateResponse<MessagesEntity>(httpResponse);
                return Mappers.ServiceMapper.ConvertToBusiness(response);
            });
        }

        public Task<MessageDto> GetMessageDetailsFromApi(string Usuario, string Password, int CategoryId, int MessageId)
        {
            return Task.Factory.StartNew(() =>
            {
                HttpResponse httpResponse = Get(API_URL_GET_MESSAGE_DETAILS + CategoryId + DELIMITER + MessageId, Usuario, Password).Result;

                httpResponse.Resultado = string.Concat("{\"data\":", httpResponse.Resultado, "}");

                ServiceResponse<MessageEntity> response = ServiceHelper.CreateResponse<MessageEntity>(httpResponse);

                return Mappers.ServiceMapper.ConvertToBusiness(response);

            });
        }

        public Task<bool> ApproveMessageFromApi(string Usuario, string Password, int CategoryId, int MessageId)
        {
            return Task.Factory.StartNew(() =>
            {
                HttpResponse httpResponse = Get(API_URL_APPROVE_MESSAGE + CategoryId + DELIMITER + MessageId, Usuario, Password).Result;
                return httpResponse.IsOk ? httpResponse.Resultado.Contains("0") : httpResponse.IsOk;

            });
        }
        public Task<bool> RefuseMessageFromApi(string Usuario, string Password, int CategoryId, int MessageId, string cause)
        {
            return Task.Factory.StartNew(() =>
            {
                string causeEncoded = HttpUtility.UrlEncode(cause, System.Text.Encoding.UTF8);
                causeEncoded = causeEncoded.Replace("+", "%20");

                HttpResponse httpResponse = Get(API_URL_REFUSE_MESSAGE + CategoryId + DELIMITER + MessageId + DELIMITER + causeEncoded, Usuario, Password).Result;

                return httpResponse.IsOk ? httpResponse.Resultado.Contains("0") : httpResponse.IsOk;
            });
        }

        #endregion

        #region Options
        public Task<OptionsDto> GetOptionsEntitiesFromApi(string Usuario, string Password)
        {
            return Task.Factory.StartNew(() =>
            {
                HttpResponse httpResponse = Get(API_URL_GET_OPTIONS_LIST, Usuario, Password).Result;

                httpResponse.Resultado = string.Concat("{\"OptionsEntities\":", httpResponse.Resultado, "}");

                ServiceResponse<OptionsEntity> response = ServiceHelper.CreateResponse<OptionsEntity>(httpResponse);

                return Mappers.ServiceMapper.ConvertToBusiness<OptionsDto, OptionsEntity>(response);
            });
        }
        public Task<bool> MarkOptionsFromApi(string Usuario, string Password, string strOptionlist)
        {
            return Task.Factory.StartNew(() =>
            {
                HttpResponse httpResponse = Get(API_URL_SET_OPTIONS_LIST + strOptionlist, Usuario, Password).Result;

                return httpResponse.IsOk ? httpResponse.Resultado.ToLower().Contains("true") : httpResponse.IsOk;
            });
        }

        #endregion

        #region GetAccesoQRFromAPI
        public Task<UserAccessDto> GetAccesoQRFromAPI(string Usuario, string Password, string QR)
        {
            return Task.Factory.StartNew(() =>
            {
                HttpResponse httpResponse = Get(API_URL_GET_ACCESO + QR, Usuario, Password).Result;

                ServiceResponse<UserAccessEntity> response = ServiceHelper.CreateResponse<UserAccessEntity>(httpResponse);

                return Mappers.ServiceMapper.ConvertToBusiness<UserAccessDto, UserAccessEntity>(response);
            });
        }

        #endregion

        #region GetAccesoNFCFromAPI
        public Task<UserAccessDto> GetAccesoNFCFromAPI(string Usuario, string Password, string NFC)
        {
            return Task.Factory.StartNew(() =>
            {
                HttpResponse httpResponse = Get(API_URL_GET_ACCESO_BY_NFC + NFC, Usuario, Password).Result;

                ServiceResponse<UserAccessEntity> response = ServiceHelper.CreateResponse<UserAccessEntity>(httpResponse);

                return Mappers.ServiceMapper.ConvertToBusiness<UserAccessDto, UserAccessEntity>(response);
            });
        }

        #endregion

        #region TokenPush
        public Task<bool> RegisterToken(string Usuario, string Password, string Token)
        {
            return Task.Factory.StartNew(() =>
            {
                int isIos = Device.RuntimePlatform == Device.iOS ? 1 : 0;

                HttpResponse httpResponse = Get(API_URL_GET_REGISTERTOKEN + Token + DELIMITER + isIos, Usuario, Password).Result;

                return httpResponse.IsOk;

            });
        }
        public Task<bool> UnRegisterToken(string Usuario, string Password)
        {
            return Task.Factory.StartNew(() =>
            {
                HttpResponse httpResponse = Get(API_URL_GET_UNREGISTERTOKEN, Usuario, Password).Result;

                return httpResponse.IsOk;

            });
        }
        #endregion        
    }
}