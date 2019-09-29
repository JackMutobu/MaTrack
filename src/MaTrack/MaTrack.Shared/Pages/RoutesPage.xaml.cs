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
    public sealed partial class RoutesPage : Page
    {
        private IHttpClientService _httpClientService;
        private UserAuthDto _userAuthDto;
        private string _userAuthDtoJson;

        public RoutesPage()
        {
            this.InitializeComponent();
            _httpClientService = new HttpClientService();
            btnAddRoute.Click += BtnAddRoute_Click;
        }

        private void BtnAddRoute_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AddRoutePage), _userAuthDtoJson);
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            _userAuthDto = JsonConvert.DeserializeObject<UserAuthDto>(e.Parameter as string);
            _userAuthDtoJson = JsonConvert.SerializeObject(_userAuthDto);
            try
            {
                _httpClientService.SetAuthorizationHeaderToken(_httpClientService.HttpClient, _userAuthDto.Token);
                var routes = JsonConvert.DeserializeObject<List<RouteEntity>>(await _httpClientService.GetAsync("Route/all"));
                //var admins = JsonConvert.DeserializeObject<List<AdminEntity>>(await _httpClientService.GetAsync("Drivers/all"));
                //var admin = admins.SingleOrDefault(x => x.Phone == _userAuthDto.Phone);
                //listAdmins.ItemsSource = admins.Where(x => x.SACCO == admin?.SACCO);
                listRoutes.ItemsSource = routes;
            }
            catch (Exception ex)
            {
                MessageDialog messageDialog = new MessageDialog(ex.Message);
                await messageDialog.ShowAsync();
            }
        }
    }
}
