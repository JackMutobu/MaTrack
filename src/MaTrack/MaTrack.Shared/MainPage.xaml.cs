using MaTrack.Shared.Pages;
using MaTrack.Shared.Services;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Microsoft.AspNetCore.SignalR.Client;
using Xamarin.Essentials;
using MaTrack.Core.Dtos;
using Windows.UI.Xaml.Navigation;
using System;
using Windows.UI.Popups;
using MaTrack.Core.Entities;
using MaTrack.Shared.Helpers;
using MaTrack.Shared.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MaTrack
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        IGoogleMapsApiService _googleMapsApi;
        IPlacesService _placesService;
        List<RequestMatatuResponse> _requestMatatuResponses;
        MatrackPlace _from;
        MatrackPlace _to;
        string _locationFrom;
        string _locationTo;
        public MainPage()
        {
            this.InitializeComponent();
            BtnUser.Click += BtnUser_Click;
            GlobalHelpers globalHelpers = new GlobalHelpers();
            _googleMapsApi = new GoogleMapsServices();
            _placesService = new PlacesService();
            //GoogleMapsServices.Initialize("AIzaSyAPbw5REd81rKh2BE3XbLoazyzKU859qHA");
            Suggestions = new ObservableCollection<string>(_placesService.GetMatrackPlaces().Select(p=> p.Name));
            _requestMatatuResponses = new List<RequestMatatuResponse>();
            listResponse.SelectionChanged 
        }
        public ObservableCollection<GooglePlaceAutoCompletePrediction> Places { get; set; }
        
        public ObservableCollection<string> Suggestions { get; set; }
        public ObservableCollection<GooglePlaceAutoCompletePrediction> RecentPlaces { get; set; } = new ObservableCollection<GooglePlaceAutoCompletePrediction>();
        private void BtnUser_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(LoginPage));
        }
        private async void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            var locationFrom = new LocationDto();
            var locationTo = new LocationDto();
            try
            {
                if(_from == null)
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
                    if(_to == null)
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
        private  void AutoSuggestBoxFrom_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                var suggestions = new ObservableCollection<string>(Suggestions.ToList());
                Suggestions.Clear();
                foreach (var place in suggestions)
                {
                    if (place.Contains(sender.Text))
                    {
                        Suggestions.Add(place);
                    }
                }
                sender.ItemsSource = Suggestions;
            }
        }
        private void AutoSuggestBoxFrom_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            // Set sender.Text. You can use args.SelectedItem to build your text string.
            var selectedText = args.SelectedItem as string;
            _from = _placesService.GetMatrackPlaces().FirstOrDefault(x => x.Name == selectedText);
            Suggestions = new ObservableCollection<string>(_placesService.GetMatrackPlaces().Select(p => p.Name));
        }
        private  void AutoSuggestBoxTo_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                var suggestions = new ObservableCollection<string>(Suggestions.ToList());
                Suggestions.Clear();
                foreach (var place in suggestions)
                {
                    if (place.Contains(sender.Text))
                    {
                        Suggestions.Add(place);
                    }
                }
                sender.ItemsSource = Suggestions;
            }
            
        }
        private void AutoSuggestBoxTo_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            // Set sender.Text. You can use args.SelectedItem to build your text string.
            var selectedText = args.SelectedItem as string;
            _to = _placesService.GetMatrackPlaces().FirstOrDefault(x => x.Name == selectedText);
            Suggestions = new ObservableCollection<string>(_placesService.GetMatrackPlaces().Select(p => p.Name));
        }
        public async Task GetPlacesByName(string placeText)
        {
            var places = await _googleMapsApi.GetPlaces(placeText);
            var placeResult = places?.AutoCompletePlaces;
            if (placeResult != null && placeResult.Count > 0)
            {
                Places = new ObservableCollection<GooglePlaceAutoCompletePrediction>(placeResult);
            }

            Suggestions?.Clear();
            foreach(var place in Places)
            {
                Suggestions?.Add($"{place.StructuredFormatting.MainText} - {place.StructuredFormatting.SecondaryText}");
            }
            
        }
        
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            try
            {
                GlobalHelpers.HubConnection.On<RequestMatatuResponse>("RequestResponse", (requestReponse) =>
                {
                    _requestMatatuResponses.Add(requestReponse);
                    listResponse.ItemsSource = _requestMatatuResponses.OrderBy(x => x.Distance).Distinct();
                });
            }
            catch (Exception ex)
            {
                MessageDialog messageDialog = new MessageDialog(ex.Message);
                await messageDialog.ShowAsync();
            }
        }
    }
}
