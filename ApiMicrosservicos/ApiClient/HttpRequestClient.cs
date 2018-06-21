using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Livraria.Api.ApiClient
{
    public class HttpRequestClient : IDisposable
    {
        protected virtual HttpClient HttpClient { get; set; }

        public HttpRequestClient(HttpClient httpClient)
        {
            this.HttpClient = httpClient;
        }

        #region Public methods

        public virtual async Task<TContent> PostAsync<TContent>(string url, string body)
        {
            var responseContent = await this.PostAsync(url, body);

            return JsonConvert.DeserializeObject<TContent>(responseContent);
        }

        public virtual async Task<string> PostAsync(string url, string body)
        {
            return await this.PostInternalAsync(url, body);
        }

        #endregion

        #region Protected methods

        protected virtual async Task<string> PostInternalAsync(string url, string body)
        {
            var client = this.HttpClient;

            using (var content = new StringContent(body, Encoding.UTF8, "application/json"))
            {
                string rawContent;
                HttpResponseMessage response;

                try
                {
                    response = await client.PostAsync(url, content);
                    rawContent = await response.Content.ReadAsStringAsync();
                }
                catch (Exception ex)
                {
                    throw new Exception("Conexão não pode ser estabelecida");
                }

                if (response.IsSuccessStatusCode)
                {
                    return rawContent;
                }
                else
                {
                    throw new Exception("Algo deu errado");
                }
            }
        }



        #endregion

        public void Dispose()
        {
            this.HttpClient.CancelPendingRequests();
            this.HttpClient.Dispose();

            this.HttpClient = null;
        }
    }
}
