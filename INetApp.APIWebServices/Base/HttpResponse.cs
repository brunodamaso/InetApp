using System;
using System.Net;

namespace INetApp.APIWebServices.Responses
{ 
    public class HttpResponse
    {
        private HttpResponse()
        {
            StatusCode = HttpStatusCode.OK;
            Content = string.Empty;
            Description = string.Empty;
        }

        public HttpStatusCode StatusCode { get; set; }
        public bool IsOk => StatusCode == HttpStatusCode.OK && string.IsNullOrEmpty(Description);
        public bool IsConnected => StatusCode != HttpStatusCode.NotFound;
        public string Description { get; set; }
        public string Content { get; set; }

        internal static HttpResponse CreateOk(string content)
        {
            return new HttpResponse
            {
                StatusCode = HttpStatusCode.OK,
                Content = content,
                Description = string.Empty
            };
        }

        internal static HttpResponse CreateErr(HttpStatusCode statusCode, string description)
        {
            return new HttpResponse
            {
                StatusCode = statusCode,
                Content = string.Empty,
                Description = description
            };
        }
    }
}
