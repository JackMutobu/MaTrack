using MaTrack.Core.Dtos;
using System.Net.Http;
using System.Threading.Tasks;

namespace MaTrack.Shared.Services
{
    public interface IHttpClientService
    {
        HttpClient HttpClient { get; set; }
        Task<UserAuthDto> AuthenticateUser(UserAuthDto userDto);
        Task<string> GetAsync(string uri);
        void SetAuthorizationHeaderToken(HttpClient client, string token);
        Task<HttpResponseMessage> PostAsync(object content, string uri);
        Task<HttpResponseMessage> PutAsync(object content, string uri);
    }
}
