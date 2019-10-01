using MaTrack.Core.Dtos;
using MaTrack.Core.Entities;
using MaTrack.Shared.Dialogs;
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
    public sealed partial class ArrivalsPage : Page
    {
        private UserAuthDto _userAuthDto;
        private string _userAuthDtoJson;
        private IHttpClientService _httpClientService;
        private List<TripEntity> _trips;

        public ArrivalsPage()
        {
            this.InitializeComponent();
            _httpClientService = new HttpClientService();
            _trips = new List<TripEntity>();
            listArrivals.SelectionChanged += ListArrivals_SelectionChanged;
        }

        private async void ListArrivals_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var selectedTrip = e.AddedItems.Last() as TripEntity;
                var dialog = new DepartureDialog();
                var result = await dialog.ShowAsync();
                if (result == ContentDialogResult.Primary)
                {
                    if (dialog.CanAddToDeparture)
                    {
                        selectedTrip.TripState = Core.Enumerations.TripState.Departure;
                        selectedTrip.NumberOfTrip += 1;
                    }
                    else
                    {
                        selectedTrip.TripState = Core.Enumerations.TripState.None;
                    }
                    selectedTrip.LastTripStateTime = DateTime.Now;
                    selectedTrip.Revenue += decimal.Parse(dialog.Revenue);
                    var reponse = await _httpClientService.PutAsync(selectedTrip, "Trip/update");
                    if (reponse.IsSuccessStatusCode)
                    {
                        _trips.Remove(selectedTrip);
                        listArrivals.ItemsSource = _trips;
                    }
                }
            }
            catch(Exception ex)
            {
                listArrivals.ItemsSource = _trips.Where(x => x.TripState == Core.Enumerations.TripState.Arrival && x.UploadDate == DateTime.Today); 
            }
            
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            _userAuthDto = JsonConvert.DeserializeObject<UserAuthDto>(e.Parameter as string);
            _userAuthDtoJson = JsonConvert.SerializeObject(_userAuthDto);
            try
            {
                _httpClientService.SetAuthorizationHeaderToken(_httpClientService.HttpClient, _userAuthDto.Token);
                _trips = JsonConvert.DeserializeObject<List<TripEntity>>(await _httpClientService.GetAsync("Trip/getallwith"));
                listArrivals.ItemsSource = _trips.Where(x => x.TripState == Core.Enumerations.TripState.Arrival && x.UploadDate ==  DateTime.Today);
            }
            catch (Exception ex)
            {
                MessageDialog messageDialog = new MessageDialog(ex.Message);
                await messageDialog.ShowAsync();
            }
        }
    }
}
