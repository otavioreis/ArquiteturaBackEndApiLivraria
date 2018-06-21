using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Livraria.Api.ApiClient
{
    public class ApiHttpClient
    {
        protected string ApiKey { get; private set; }
        public string BaseUrl { get; protected set; }
        public HttpRequestClient HttpClient { get; set; }


        public ApiHttpClient(string apiKey, string baseUrl, HttpClient httpClient = null)
        {
            this.ApiKey = apiKey;
            this.BaseUrl = baseUrl;

            httpClient = httpClient ?? new HttpClient();

            if (httpClient.BaseAddress == null)
            {
                httpClient.BaseAddress = new Uri(baseUrl);
                this.AddDefaultHeaders(httpClient.DefaultRequestHeaders);
            }

            this.HttpClient = new HttpRequestClient(httpClient);
        }

        protected virtual void AddDefaultHeaders(HttpRequestHeaders defaultHeaders)
        {
            defaultHeaders.Clear();
            defaultHeaders.ExpectContinue = false;
            defaultHeaders.ConnectionClose = false;
            defaultHeaders.Add("Connection", "keep-alive");

            defaultHeaders.TryAddWithoutValidation("Content-type", "application/json; charset=utf-8");
            defaultHeaders.TryAddWithoutValidation("Accept-Charset", "utf-8");
            defaultHeaders.TryAddWithoutValidation("Accept", "application/json");
            defaultHeaders.TryAddWithoutValidation("Accept-Language", "pt-BR");

            if(this.ApiKey != null)
                defaultHeaders.TryAddWithoutValidation("Authorization", string.Format("Bearer {0}", this.ApiKey));
        }
    }
}
