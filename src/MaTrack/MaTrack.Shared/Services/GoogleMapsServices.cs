using MaTrack.Shared.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MaTrack.Shared.Services
{
    public class GoogleMapsServices:IGoogleMapsApiService
    {
        static string _googleMapsKey;

        private const string ApiBaseAddress = "https://maps.googleapis.com/maps/";
        private HttpClientHandler _httpClientHandler;
        private HttpClient _httpClient;
        public GoogleMapsServices()
        {
#if __ANDROID__

            _httpClientHandler = new Xamarin.Android.Net.AndroidClientHandler()
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; }
            };
            _httpClient = new HttpClient(_httpClientHandler);
#else
            _httpClientHandler = new HttpClientHandler()
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; }
            };
            _httpClient = new HttpClient(_httpClientHandler);
#endif
            _httpClient.BaseAddress = new Uri(ApiBaseAddress);

            //_httpClient.DefaultRequestHeaders.Accept.Clear();
            //_httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _googleMapsKey = "AIzaSyAPbw5REd81rKh2BE3XbLoazyzKU859qHA";
        }
        private HttpClient CreateClient()
        {
#if __ANDROID__

            _httpClientHandler = new Xamarin.Android.Net.AndroidClientHandler()
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; }
            };
            _httpClient = new HttpClient(_httpClientHandler);
#else
            _httpClientHandler = new HttpClientHandler()
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; }
            };
            _httpClient = new HttpClient(_httpClientHandler);
#endif
            _httpClient.BaseAddress = new Uri(ApiBaseAddress);

            //_httpClient.DefaultRequestHeaders.Accept.Clear();
            //_httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _googleMapsKey = "AIzaSyAPbw5REd81rKh2BE3XbLoazyzKU859qHA";
            return _httpClient;
        }
        public static void Initialize(string googleMapsKey)
        {
            _googleMapsKey = googleMapsKey;
        }


        public async Task<GooglePlaceAutoCompleteResult> GetPlaces(string text)
        {
            GooglePlaceAutoCompleteResult results = null;
            try
            {
                var query = $"api/place/autocomplete/json?input={Uri.EscapeUriString(text)}&key={_googleMapsKey}";
                var response = await _httpClient.GetAsync(query);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    if (!string.IsNullOrWhiteSpace(json) && json != "ERROR")
                    {
                        results = await Task.Run(() =>
                           JsonConvert.DeserializeObject<GooglePlaceAutoCompleteResult>(json)
                        ).ConfigureAwait(false);

                    }
                }
            }
            catch(Exception ex)
            {
                var message = ex;
            }

            return results;
        }

        public async Task<GooglePlace> GetPlaceDetails(string placeId)
        {
            GooglePlace result = null;
            using (var httpClient = CreateClient())
            {
                var response = await httpClient.GetAsync($"api/place/details/json?placeid={Uri.EscapeUriString(placeId)}&key={_googleMapsKey}").ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    if (!string.IsNullOrWhiteSpace(json) && json != "ERROR")
                    {
                        result = new GooglePlace(JObject.Parse(json));
                    }
                }
            }

            return result;
        }
    }
}
