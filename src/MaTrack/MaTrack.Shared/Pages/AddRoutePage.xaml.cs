using MaTrack.Core.Dtos;
using MaTrack.Core.Entities;
using MaTrack.Shared.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace MaTrack.Shared.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddRoutePage : Page
    {
        private UserAuthDto _userAuthDto;
        private string _userAuthDtoJson;
        private IHttpClientService _httpClientService;
        private List<VehicleEntity> _vehicles;
        private List<StageEntity> _stages;

        public AddRoutePage()
        {
            this.InitializeComponent();
            _httpClientService = new HttpClientService();
            BtnNewSatge.Click += BtnNewSatge_Click;
            BtnNewVehicle.Click += BtnNewVehicle_Click;
            BtnAddStage.Click += BtnAddStage_Click;
            BtnAddVehicle.Click += BtnAddVehicle_Click;
            BtnAddRoute.Click += BtnAddRoute_Click;
            _vehicles = new List<VehicleEntity>();
            _stages = new List<StageEntity>();
        }

        private async void BtnAddRoute_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var route = new RouteEntity
                {
                    RouteName = txtName.Text,
                    UploadDate = DateTime.Now,
                  
                };
                foreach (var stage in _stages)
                {
                    route.RouteStages.Add(new RouteStageEntity
                    {
                        StageId = stage.Id
                    });
                }
                foreach (var vehicle in _vehicles)
                {
                    route.VehicleRoutes.Add(new VehicleRouteEntity
                    {
                        VeicleId = vehicle.Id
                    });
                }
                var response = await _httpClientService.PostAsync(route, "Route/add");
                if (response.IsSuccessStatusCode)
                {
                    MessageDialog messageDialog = new MessageDialog($"{route.RouteName} has been registered");
                    await messageDialog.ShowAsync();
                }
            }
            catch(Exception ex)
            {
                txtError.Text = ex.Message;
            }
        }

        private void BtnAddVehicle_Click(object sender, RoutedEventArgs e)
        {
            var selectedVehicule = ComboVehicles.SelectedItem as VehicleEntity;
            _vehicles.Add(selectedVehicule);
            listVehicles.ItemsSource = _vehicles;
        }

        private void BtnAddStage_Click(object sender, RoutedEventArgs e)
        {
            var selectedStage = ComboStages.SelectedItem as StageEntity;
            _stages.Add(selectedStage);
            listStages.ItemsSource = _stages;
        }

        private void BtnNewVehicle_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AddVehiclePage), _userAuthDtoJson);
        }

        private void BtnNewSatge_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AddStagePage), _userAuthDtoJson);
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            _userAuthDto = JsonConvert.DeserializeObject<UserAuthDto>(e.Parameter as string);
            _userAuthDtoJson = JsonConvert.SerializeObject(_userAuthDto);
            try
            {
                _httpClientService.SetAuthorizationHeaderToken(_httpClientService.HttpClient, _userAuthDto.Token);
                var vehicles = JsonConvert.DeserializeObject<List<VehicleEntity>>(await _httpClientService.GetAsync("Vehicles/all"));
                ComboVehicles.ItemsSource = vehicles;
                var stages = JsonConvert.DeserializeObject<List<StageEntity>>(await _httpClientService.GetAsync("Stages/all"));
                ComboStages.ItemsSource = stages;
            }
            catch (Exception ex)
            {
                txtError.Text = ex.Message;
            }
        }

    }
}
