using MaTrack.Core.Dtos;
using MaTrack.Core.Entities;
using MaTrack.Shared.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace MaTrack.Shared.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DepartPage : Page
    {
        private UserAuthDto _userAuthDto;
        private string _userAuthDtoJson;
        private List<TripEntity> _trips;
        private IHttpClientService _httpClientService;
        public DepartPage()
        {
            this.InitializeComponent();
            _httpClientService = new HttpClientService();
            _trips = new List<TripEntity>();
            btnVehicleAdd.Click += BtnVehicleAdd_Click;
            listDepartures.SelectionChanged += ListDepartures_SelectionChanged;
        }

        private async void ListDepartures_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var selectedDeparture = e.AddedItems.Last() as TripEntity;
                selectedDeparture.TripState = Core.Enumerations.TripState.Arrival;
                selectedDeparture.Vehicle = null;
                selectedDeparture.LastTripStateTime = DateTime.Now;
                var reponse = await _httpClientService.PutAsync(selectedDeparture, "Trip/update");
                if (reponse.IsSuccessStatusCode)
                {
                    _trips.Remove(selectedDeparture);
                    listDepartures.ItemsSource = _trips;
                }
            }
            catch(Exception ex)
            {
                listDepartures.ItemsSource = _trips.Where(x => x.TripState == Core.Enumerations.TripState.Departure && x.UploadDate == DateTime.Today); ;
            }

        }

        private void BtnVehicleAdd_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AddDeparturePage),_userAuthDtoJson);
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            _userAuthDto = JsonConvert.DeserializeObject<UserAuthDto>(e.Parameter as string);
            _userAuthDtoJson = JsonConvert.SerializeObject(_userAuthDto);
            try
            {
                _httpClientService.SetAuthorizationHeaderToken(_httpClientService.HttpClient, _userAuthDto.Token);
                _trips = JsonConvert.DeserializeObject<List<TripEntity>>(await _httpClientService.GetAsync("Trip/getallwith"));
                listDepartures.ItemsSource = _trips.Where(x=> x.TripState == Core.Enumerations.TripState.Departure && x.UploadDate == DateTime.Today);
            }
            catch (Exception ex)
            {
                MessageDialog messageDialog = new MessageDialog(ex.Message);
                await messageDialog.ShowAsync();
            }
        }
    }
}
