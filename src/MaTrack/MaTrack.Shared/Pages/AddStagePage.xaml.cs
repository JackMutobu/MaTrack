﻿using MaTrack.Core.Entities;
using MaTrack.Shared.Services;
using System;
using Newtonsoft.Json;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Xamarin.Essentials;
using MaTrack.Core.Dtos;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace MaTrack.Shared.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddStagePage : Page
    {
        private IHttpClientService _httpClientService;
        private UserAuthDto _userAuthDto;
        private string _userAuthDtoJson;

        public AddStagePage()
        {
            this.InitializeComponent();
            _httpClientService = new HttpClientService();
            BtnMyLocation.Click += BtnMyLocation_Click;
            BtnAddStage.Click += BtnAddStage_Click;
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _userAuthDto = JsonConvert.DeserializeObject<UserAuthDto>(e.Parameter as string);
            _userAuthDtoJson = e.Parameter as string;
            _httpClientService.SetAuthorizationHeaderToken(_httpClientService.HttpClient, _userAuthDto.Token);
        }
        private async void BtnAddStage_Click(object sender, RoutedEventArgs e)
        {
            var stage = new StageEntity
            {
                Name = txtName.Text,
                GPSCoordinate = txtLocation.Text,
                UploadDate = DateTime.Now
            };
            if(!string.IsNullOrEmpty(stage.Name) || !string.IsNullOrEmpty(stage.GPSCoordinate))
            {
                var response = await _httpClientService.PostAsync(stage,"Stages/add");
                if (response.IsSuccessStatusCode)
                {
                    MessageDialog messageDialog = new MessageDialog($"{stage.Name}  has been registered");
                    await messageDialog.ShowAsync();
                    return;
                }
                else
                {
                    txtError.Text = await response.Content.ReadAsStringAsync();
                    return;
                }
            }
            txtError.Text = "Invalid input";
        }

        private async void BtnMyLocation_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.High);
                var location = await Geolocation.GetLocationAsync(request);

                if (location != null)
                {
                    txtLocation.Text = $"{ location.Altitude}m A {location.Latitude}° N {location.Longitude}° W";
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
            }
            catch (Exception ex)
            {
                // Unable to get location
            }
        }


    }
}