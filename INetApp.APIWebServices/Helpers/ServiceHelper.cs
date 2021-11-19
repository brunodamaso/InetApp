using System;
using System.Web;
using Newtonsoft.Json;
using INetApp.APIWebServices.Dtos;
using INetApp.APIWebServices.Requests;
using INetApp.APIWebServices.Responses;

namespace INetApp.APIWebServices.Helpers
{
    public static class ServiceHelper
    {
        public static ServiceResponse<T> CreateResponse<T>(HttpResponse response) where T : Response
        {
            ServiceResponse<T> result = null;

            var dto = DeserializeJSON<T>(response, response.IsOk);

            if (response.IsOk && (dto != null || response.Resultado.ToLower().Equals("true")))
            {
                result = ServiceResponse<T>.CreateOk(dto);
            }
            else
            {
                result = ServiceResponse<T>.CreateErr(dto != null && !string.IsNullOrEmpty(dto.Code) ? dto.Code : response.StatusCode.ToString(),
                                                      dto != null && !string.IsNullOrEmpty(dto.Description) ? dto.Description : response.Description,
                                                      response.IsConnected);
            }

            return result;
        }

        private static T DeserializeJSON<T> (HttpResponse response, bool isOk) where T : Response
        {
            try
            {
                string json = string.Empty;

                if (isOk)
                {
                    json = HttpUtility.HtmlDecode(response.Resultado);
                }
                else
                {
                    json = HttpUtility.HtmlDecode(response.Description);
                }
            
                var value = JsonConvert.DeserializeObject<T>(json, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

                return value;
            }
            catch (Exception)
            {
                return null;
            }
            
        }

        public static string ToJson<T>(T request, bool ignoreNull = false) where T : new()
        {
            JsonSerializerSettings settings = null;

            if (ignoreNull)
                settings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };

            var stringRequest = JsonConvert.SerializeObject(request, settings);

            return stringRequest;
        }
    }
}
