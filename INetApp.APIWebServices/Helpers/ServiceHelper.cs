using System;
using System.Web;
using INetApp.APIWebServices;
using INetApp.APIWebServices.Responses;

namespace INetApp.APIWebServices.Helpers
{
    public static class ServiceHelper
    {
        public static ServiceResponse<T> CreateResponse<T>(HttpResponse response) where T : Response
        {
            ServiceResponse<T> result = null;

            T dto = DeserializeJSON<T>(response, response.IsOk);

            if (response.IsOk)                
            {
                if (dto != null || response.Resultado.ToLower().Equals("true"))
                {
                    result = ServiceResponse<T>.CreateOk(dto);
                }
            }
            else
            {
                result = ServiceResponse<T>.CreateErr(dto != null && !string.IsNullOrEmpty(dto.Code) ? dto.Code : response.StatusCode.ToString(),
                                                      dto != null && !string.IsNullOrEmpty(dto.Description) ? dto.Description : response.Description,
                                                      response.IsConnected);
            }

            return result;
        }

        private static T DeserializeJSON<T>(HttpResponse response, bool isOk) where T : Response
        {
            try
            {
                string json = string.Empty;

                json = isOk ? HttpUtility.HtmlDecode(response.Resultado) : HttpUtility.HtmlDecode(response.Description);

                T value = JsonService.Deserialize<T>(json);

                return value;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public static string ToJson<T>(T request, bool ignoreNull = false) where T : new()
        {
            string stringRequest = JsonService.Serialize(request);

            return stringRequest;
        }
    }
}
