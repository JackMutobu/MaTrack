using MaTrack.Core.Dtos;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MaTrack.Shared.Services
{
    public class HttpClientService:IHttpClientService
    {
        private HttpClient _httpClient;
        private HttpClientHandler _httpClientHandler;

        public HttpClientService()
        {
#if __ANDROID__

            _httpClient = new HttpClient();
#else
            _httpClientHandler = new HttpClientHandler()
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; }
            };
            _httpClient = new HttpClient(_httpClientHandler);
#endif
            var localUri = new Uri("https://localhost:44389/api/");
            _httpClient.BaseAddress = localUri;
        }
        public HttpClient HttpClient
        {
            get { return _httpClient; }
            set { _httpClient = value; }
        }
        public void SetAuthorizationHeaderToken(HttpClient client, string token)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
        public async Task<string> GetAsync(string uri)
        {
            try
            {
                var result = await _httpClient.GetStringAsync(uri);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<UserAuthDto> AuthenticateUser(UserAuthDto userDto)
        {
            var data = JsonConvert.SerializeObject(userDto);
            var content = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("users/authenticate", content);
            var result = JsonConvert.DeserializeObject<UserAuthDto>(response.Content.ReadAsStringAsync().Result);
            if (response.IsSuccessStatusCode)
            {
                return result;
            }
            throw new Exception(response.Content.ReadAsStringAsync().Result);
        }
        public async Task<HttpResponseMessage> PostAsync(object content, string uri)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Post, uri))
            {
                var json = JsonConvert.SerializeObject(content);
                using (var stringContent = new StringContent(json, Encoding.UTF8, "application/json"))
                {
                    request.Content = stringContent;

                    using (var response = await _httpClient
                        .SendAsync(request, HttpCompletionOption.ResponseHeadersRead)
                        .ConfigureAwait(false))
                    {
                        response.EnsureSuccessStatusCode();
                        return response;
                    }
                }
            }
        }
        public async Task<HttpResponseMessage> PutAsync(object content, string uri)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Put, uri))
            {
                var json = JsonConvert.SerializeObject(content);
                using (var stringContent = new StringContent(json, Encoding.UTF8, "application/json"))
                {
                    request.Content = stringContent;

                    using (var response = await _httpClient
                        .SendAsync(request, HttpCompletionOption.ResponseHeadersRead)
                        .ConfigureAwait(false))
                    {
                        response.EnsureSuccessStatusCode();
                        return response;
                    }
                }
            }
        }
    }
}
