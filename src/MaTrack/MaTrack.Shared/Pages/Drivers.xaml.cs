﻿using MaTrack.Core.Dtos;
using MaTrack.Core.Entities;
using MaTrack.Shared.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace MaTrack.Shared.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Drivers : Page
    {
        private UserAuthDto _userAuthDto;
        private string _userAuthDtoJson;
        private IHttpClientService _httpClientService;

        public Drivers()
        {
            this.InitializeComponent();
            _httpClientService = new HttpClientService();
            btnAddDriver.Click += BtnAddDriver_Click;
        }
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            _userAuthDto = JsonConvert.DeserializeObject<UserAuthDto>(e.Parameter as string);
            _userAuthDtoJson = e.Parameter as string;
            try
            {
                _httpClientService.SetAuthorizationHeaderToken(_httpClientService.HttpClient, _userAuthDto.Token);
                var dr = await _httpClientService.GetAsync("Drivers/all");
                var drivers = JsonConvert.DeserializeObject<List<DriverEntity>>(await _httpClientService.GetAsync("Drivers/all"));
                await App.RunOnDispatcher(() =>
                {
                    listDrivers.ItemsSource = drivers;
                });
            }
            catch (Exception ex)
            {
                MessageDialog messageDialog = new MessageDialog(ex.Message);
                await messageDialog.ShowAsync();
            }
        }
        private void BtnAddDriver_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AddDriverPage),_userAuthDtoJson);
        }
    }
}
