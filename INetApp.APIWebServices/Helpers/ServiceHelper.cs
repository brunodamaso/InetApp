using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Web;
//using Newtonsoft.Json;
using INetApp.APIWebServices.Responses;

namespace INetApp.APIWebServices.Helpers
{
    public static class ServiceHelper
    {
        private static readonly JsonSerializerOptions Settings = new JsonSerializerOptions()
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            IncludeFields = true,
            NumberHandling = JsonNumberHandling.AllowReadingFromString,
            PropertyNameCaseInsensitive = true, 
        };

        public static ServiceResponse<T> CreateResponse<T>(HttpResponse response) where T : Response
        {
            ServiceResponse<T> result = null;

            T dto = DeserializeJSON<T>(response, response.IsOk);

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

        private static T DeserializeJSON<T>(HttpResponse response, bool isOk) where T : Response
        {
            try
            {
                string json = string.Empty;

                json = isOk ? HttpUtility.HtmlDecode(response.Resultado) : HttpUtility.HtmlDecode(response.Description);

                T value = JsonSerializer.Deserialize<T>(json, Settings);

                return value;
            }
            catch (Exception ex)
            {
                Console.WriteLine("error al Deserializar " + ex.Message);
                return null;
            }

        }

        public static string ToJson<T>(T request, bool ignoreNull = false) where T : new()
        {
            string stringRequest = JsonSerializer.Serialize(request, Settings);

            return stringRequest;
        }
    }
}
