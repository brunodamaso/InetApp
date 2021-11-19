using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using INetApp.APIWebServices.Responses;

namespace INetApp.APIWebServices
{
    public class APIWebService
    {
        private const string CONTENT_TYPE_LABEL = "Content-Type";
        private const string CONTENT_TYPE = "application/json"; // charset=utf-8";
        private const string ERROR_NAME_RESOLUTION_FAILURE = "NameResolutionFailure";
        private const string ERROR_URL_DOMAIN = "NSURLErrorDomain";
        private const string ERROR_NO_HOST = "No such host is known";
        private const string AUTHORIZATION_LABEL = "Authorization";

        public APIWebService()
        {
        }

        #region Public


        private void CreateAppHeaders(ref HttpClient httpClient, string Usuario = null, string Password = null)
        {
            string authenticationString;          
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json;odata=verbose");
            //httpClient.DefaultRequestHeaders.Add(AUTHORIZATION_LABEL, base64EncodedAuthenticationString);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(CONTENT_TYPE));
            if (Usuario != null)
            {
                authenticationString = $"{Usuario}:{Password}";
                string base64EncodedAuthenticationString = Convert.ToBase64String(Encoding.ASCII.GetBytes(authenticationString));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64EncodedAuthenticationString);
            }


        }

        public async Task<HttpResponse> Get(string url, string Usuario = null, string Password = null)
        {
            //var handler = new WebRequestHandler();
            //handler.ClientCertificateOptions = ClientCertificateOption.Automatic;
            //handler.ClientCertificates.Add(GetClientCert());
            //handler.ServerCertificateValidationCallback += Validate;


            HttpResponse httpResponse;
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            HttpClient httpClient = new HttpClient(httpClientHandler)
            {
                Timeout = TimeSpan.FromSeconds(15000),
                BaseAddress = new Uri(url)
            };

            CreateAppHeaders(ref httpClient, Usuario, Password);

            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(new Uri(url));
                response.EnsureSuccessStatusCode();
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    //if (string.IsNullOrEmpty(content))
                    //    httpResponse = HttpResponse.CreateErr(response.StatusCode, "JsonDeserializeException");
                    //else
                    httpResponse = HttpResponse.CreateOk(content);
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    string description = "RequestException";

                    if (response.Content != null)
                    {
                        description = await response.Content.ReadAsStringAsync();
                    }

                    httpResponse = HttpResponse.CreateErr(response.StatusCode, description);
                }
                else
                {
                    httpResponse = HttpResponse.CreateErr(response.StatusCode, "ConnectionException");
                }
            }
            catch (Exception ex)
            {
                if (CheckConnectionError(ex))
                {
                    httpResponse = HttpResponse.CreateErr(System.Net.HttpStatusCode.NotFound, ex.InnerException.ToString());
                }
                else
                {
                    httpResponse = HttpResponse.CreateErr(System.Net.HttpStatusCode.BadRequest, ex.ToString());
                }
            }

            return httpResponse;
        }

        public HttpResponse Post(string url, string stringRequest)
        {
            HttpResponse httpResponse;
            HttpClient httpClient = new HttpClient { Timeout = TimeSpan.FromSeconds(15000) };

            CreateAppHeaders(ref httpClient);

            try
            {
                HttpResponseMessage response = httpClient.PostAsync(url, new StringContent(stringRequest, Encoding.UTF8, CONTENT_TYPE)).Result;
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string content = response.Content.ReadAsStringAsync().Result;

                    if (string.IsNullOrEmpty(content))
                    {
                        httpResponse = HttpResponse.CreateErr(response.StatusCode, "JsonDeserializeException");
                    }
                    else
                    {
                        httpResponse = HttpResponse.CreateOk(content);
                    }
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    string description = "RequestException";

                    if (response.Content != null)
                    {
                        description = response.Content.ReadAsStringAsync().Result;
                    }

                    httpResponse = HttpResponse.CreateErr(response.StatusCode, description);
                }
                else
                {
                    httpResponse = HttpResponse.CreateErr(response.StatusCode, "ConnectionException");
                }
            }
            catch (Exception ex)
            {
                if (CheckConnectionError(ex))
                {
                    httpResponse = HttpResponse.CreateErr(System.Net.HttpStatusCode.NotFound, ex.InnerException.ToString());
                }
                else
                {
                    httpResponse = HttpResponse.CreateErr(System.Net.HttpStatusCode.BadRequest, ex.ToString());
                }
            }

            return httpResponse;
        }

        public HttpResponse Put(string url, string stringRequest = null)
        {
            HttpResponse httpResponse;
            HttpClient httpClient = new HttpClient { Timeout = TimeSpan.FromSeconds(15000) };

            CreateAppHeaders(ref httpClient);

            try
            {
                HttpResponseMessage response = stringRequest != null
                    ? httpClient.PutAsync(url, new StringContent(stringRequest, Encoding.UTF8, CONTENT_TYPE)).Result
                    : httpClient.PutAsync(url, null).Result;

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string content = response.Content.ReadAsStringAsync().Result;

                    if (string.IsNullOrEmpty(content))
                    {
                        httpResponse = HttpResponse.CreateErr(response.StatusCode, "JsonDeserializeException");
                    }
                    else
                    {
                        httpResponse = HttpResponse.CreateOk(content);
                    }
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    string description = "RequestException";

                    if (response.Content != null)
                    {
                        description = response.Content.ReadAsStringAsync().Result;
                    }

                    httpResponse = HttpResponse.CreateErr(response.StatusCode, description);
                }
                else
                {
                    httpResponse = HttpResponse.CreateErr(response.StatusCode, "ConnectionException");
                }
            }
            catch (Exception ex)
            {
                if (CheckConnectionError(ex))
                {
                    httpResponse = HttpResponse.CreateErr(System.Net.HttpStatusCode.NotFound, ex.InnerException.ToString());
                }
                else
                {
                    httpResponse = HttpResponse.CreateErr(System.Net.HttpStatusCode.BadRequest, ex.ToString());
                }
            }

            return httpResponse;
        }

        #endregion

        #region Private

        private bool CheckConnectionError(Exception ex)
        {
            if (ex != null && ex.InnerException != null)
            {
                string error = ex.InnerException.ToString();

                return error.Contains(ERROR_NAME_RESOLUTION_FAILURE) ||
                       error.Contains(ERROR_URL_DOMAIN) ||
                       error.Contains(ERROR_NO_HOST);
            }

            return false;
        }

        #endregion
    }
}
