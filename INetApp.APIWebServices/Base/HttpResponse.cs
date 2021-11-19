using System.Net;

namespace INetApp.APIWebServices.Responses
{
    public class HttpResponse
    {
        private HttpResponse()
        {
            this.StatusCode = HttpStatusCode.OK;
            this.Resultado = string.Empty;
            this.Description = string.Empty;
        }

        public HttpStatusCode StatusCode { get; set; }
        public bool IsOk => this.StatusCode == HttpStatusCode.OK && string.IsNullOrEmpty(this.Description);
        public bool IsConnected => this.StatusCode != HttpStatusCode.NotFound;
        public string Description { get; set; }
        public string Resultado { get; set; }

        internal static HttpResponse CreateOk(string content)
        {
            return new HttpResponse
            {
                StatusCode = HttpStatusCode.OK,
                Resultado = content,
                Description = string.Empty
            };
        }

        internal static HttpResponse CreateErr(HttpStatusCode statusCode, string description)
        {
            return new HttpResponse
            {
                StatusCode = statusCode,
                Resultado = string.Empty,
                Description = description
            };
        }
    }
}
