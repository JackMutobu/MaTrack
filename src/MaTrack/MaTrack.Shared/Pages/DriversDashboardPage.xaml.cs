using MaTrack.Core.Dtos;
using MaTrack.Shared.Services;
using Newtonsoft.Json;
using System;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Microsoft.AspNetCore.SignalR.Client;
using Xamarin.Essentials;
using System.Threading.Tasks;
using MaTrack.Shared.Helpers;
using MaTrack.Core.Entities;
using Windows.UI.Xaml;
using MaTrack.Shared.Models;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace MaTrack.Shared.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DriversDashboardPage : Page
    {
        private UserAuthDto _userAuthDto;
        private string _userAuthDtoJson;
        private DriverEntity _driverEntity;
        private IHttpClientService _httpClientService;
        private ISignalRClientService _signalRClientService;
        private int? _departureStageId;
        
        public DriversDashboardPage()
        {
            this.InitializeComponent();
            BtnTracking.Toggled += BtnTracking_Toggled;
            _httpClientService = new HttpClientService();
            _signalRClientService = new SignalRClientService();
            GlobalHelpers.HubConnection.On<LocationDto,string>("ReceiveLocation", async (locationDto,connectionId) =>
            {
                var request = new GeolocationRequest(GeolocationAccuracy.High);
                var location = await Geolocation.GetLocationAsync(request);
                if (location != null)
                {
                    var gpsLocation = new LocationDto(location.Latitude.ToString(),
                        location.Longitude.ToString())
                    {
                        Altitude = location.Altitude.ToString(),
                    };

                }
                var requestResponse = new RequestMatatuResponse
                {
                    Driver = _driverEntity,
                    LocationDto = new LocationDto(location.Latitude.ToString(), location.Longitude.ToString()),
                    Vehicle = _driverEntity.Vehicle
                };
                if (GlobalHelpers.HubConnection.State == HubConnectionState.Disconnected)
                {
                    await GlobalHelpers.HubConnection.StartAsync();
                }
                await GlobalHelpers.HubConnection.InvokeAsync("SendLocation",
               requestResponse,connectionId);
            });

        }

        private async void BtnTracking_Toggled(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            while(BtnTracking.IsOn)
            {
                
                try
                {
                    var trip = JsonConvert.DeserializeObject<TripEntity>(await _httpClientService.GetAsync($"Trip/tripStateFor/{ _driverEntity?.VehicleId}"));
                    if (trip != null && trip.TripState == Core.Enumerations.TripState.Arrival)
                    {
                        var stageName = trip.DepartureStageId == 1 ? "Madaraka" : "Nairobi CBD";
                        _departureStageId = trip.DepartureStageId;
                        if (GlobalHelpers.HubConnection.State == HubConnectionState.Disconnected)
                        {
                            await GlobalHelpers.HubConnection.StartAsync();
                        }
                        await GlobalHelpers.HubConnection.InvokeAsync("AddToGroup",
                       "Nairobi CBD");
                        await Task.Delay(5000);
                    }                   
                }
                catch (Exception ex)
                {
                    MessageDialog messageDialog = new MessageDialog(ex.Message);
                    await messageDialog.ShowAsync();
                }
            }
        }
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            _userAuthDto = JsonConvert.DeserializeObject<UserAuthDto>(e.Parameter as string);
            _userAuthDtoJson = JsonConvert.SerializeObject(_userAuthDto);
            GlobalHelpers.HubConnection.On<RequestMatatuResponse>("RequestResponse", (requestReponse) =>
            {
                var res = requestReponse;
            });
            try
            {
                _httpClientService.SetAuthorizationHeaderToken(_httpClientService.HttpClient, _userAuthDto.Token);
                _driverEntity = JsonConvert.DeserializeObject<DriverEntity>(await _httpClientService.GetAsync($"Drivers/getbyphone/{_userAuthDto.Phone}"));
            }
            catch (Exception ex)
            {
                MessageDialog messageDialog = new MessageDialog(ex.Message);
                await messageDialog.ShowAsync();
            }
        }
        private async void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            MatrackPlace _from = null;
            MatrackPlace _to = null;
            var locationFrom = new LocationDto();
            var locationTo = new LocationDto();
            try
            {
                if (_from == null)
                {
                    var request = new GeolocationRequest(GeolocationAccuracy.High);
                    var location = await Geolocation.GetLocationAsync(request);
                    if (location != null)
                    {
                        var gpsLocation = new LocationDto
                        {
                            Altitude = location.Altitude.ToString(),
                            Latitude = location.Latitude.ToString(),
                            Longitude = location.Longitude.ToString()
                        };
                        locationFrom = gpsLocation;
                    }
                    if (_to == null)
                    {
                        _to = new MatrackPlace
                        {
                            Name = "Nairobi CBD",
                            LocationDto = new LocationDto
                            {
                                Latitude = "-1.292066",
                                Longitude = "36.821945"
                            }
                        };
                    }
                }
                else
                {
                    locationFrom = _from.LocationDto;
                }
                if (GlobalHelpers.HubConnection.State == HubConnectionState.Disconnected)
                {
                    await GlobalHelpers.HubConnection.StartAsync();
                }
                await GlobalHelpers.HubConnection.InvokeAsync("GetLocation",
                _from, _to);
            }
            catch (Exception ex)
            {
                MessageDialog messageDialog = new MessageDialog(ex.Message);
                await messageDialog.ShowAsync();
            }
        }
    }
}
