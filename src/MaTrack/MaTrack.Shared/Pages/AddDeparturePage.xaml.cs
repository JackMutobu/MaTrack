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
    public sealed partial class AddDeparturePage : Page
    {
        private UserAuthDto _userAuthDto;
        private string _userAuthDtoJson;
        private IHttpClientService _httpClientService;
        private List<DriverEntity> _drivers;
        private List<VehicleEntity> _vehicles;
        private VehicleEntity _selectedVehicle;

        public AddDeparturePage()
        {
            this.InitializeComponent();
            _httpClientService = new HttpClientService();
            _drivers = new List<DriverEntity>();
            _vehicles = new List<VehicleEntity>();
            progressBar.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            ComboDrivers.SelectionChanged += ComboDrivers_SelectionChanged;
            ComboVehicles.SelectionChanged += ComboVehicles_SelectionChanged;
            BtnAddDeparture.Click += BtnAddDeparture_Click;
        }

        private async void BtnAddDeparture_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            progressBar.Visibility = Windows.UI.Xaml.Visibility.Visible;
            var trip = new TripEntity
            {
                LastTripStateTime = DateTime.Now,
                NumberOfTrip = 0,
                TripState = Core.Enumerations.TripState.Departure,
                UploadDate = DateTime.Now,
                VehicleId = _selectedVehicle.Id,               
            };
            var response = await _httpClientService.PostAsync(trip, "Trip/add");
            if (response.IsSuccessStatusCode)
            {
                MessageDialog messageDialog = new MessageDialog($"This trip has been registered");
                await messageDialog.ShowAsync();
            }
            else
            {
                txtError.Text = await response.Content.ReadAsStringAsync();
            }
            progressBar.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }

        private void ComboVehicles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedVehicle = e.AddedItems.Last() as VehicleEntity;
            var selectedDriver = _vehicles.Single(v => v.Id == _selectedVehicle.Id).Driver;
            ComboDrivers.SelectedValue = selectedDriver;
        }

        private void ComboDrivers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedDriver = e.AddedItems.Last() as DriverEntity;
            _selectedVehicle = _vehicles.Single(v => v.Id == selectedDriver?.VehicleId);
            ComboVehicles.SelectedItem = _selectedVehicle;
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            _userAuthDto = JsonConvert.DeserializeObject<UserAuthDto>(e.Parameter as string);
            _userAuthDtoJson = e.Parameter as string;
            try
            {
                _httpClientService.SetAuthorizationHeaderToken(_httpClientService.HttpClient, _userAuthDto.Token);               
                _vehicles = JsonConvert.DeserializeObject<List<VehicleEntity>>(await _httpClientService.GetAsync("Vehicles/getallwith"));
                ComboVehicles.ItemsSource = _vehicles;
                _drivers = _vehicles.Select(v => v.Driver).ToList();
                ComboDrivers.ItemsSource = _drivers;
            }
            catch (Exception ex)
            {
                txtError.Text = ex.Message;
            }
        }
    }
}
