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
    public sealed partial class ReportPage : Page
    {
        private UserAuthDto _userAuthDto;
        private string _userAuthDtoJson;
        private IHttpClientService _httpClientService;
        private List<TripEntity> _trips;

        public ReportPage()
        {
            this.InitializeComponent();
            _httpClientService = new HttpClientService();
            _trips = new List<TripEntity>();
            btnToday.Click += BtnToday_Click;
            btnYesterday.Click += BtnYesterday_Click;
            btnAll.Click += BtnAll_Click;
        }

        private void BtnAll_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            listReports.ItemsSource = _trips;
        }

        private void BtnYesterday_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            listReports.ItemsSource = _trips.Where(t => t.UploadDate == DateTime.Today.Subtract(TimeSpan.FromHours(24)));
        }

        private void BtnToday_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            listReports.ItemsSource = _trips.Where(t => t.UploadDate == DateTime.Today);
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            _userAuthDto = JsonConvert.DeserializeObject<UserAuthDto>(e.Parameter as string);
            _userAuthDtoJson = JsonConvert.SerializeObject(_userAuthDto);
            try
            {
                _httpClientService.SetAuthorizationHeaderToken(_httpClientService.HttpClient, _userAuthDto.Token);
                _trips = JsonConvert.DeserializeObject<List<TripEntity>>(await _httpClientService.GetAsync("Trip/getallwith"));
                listReports.ItemsSource = _trips;
            }
            catch (Exception ex)
            {
                MessageDialog messageDialog = new MessageDialog(ex.Message);
                await messageDialog.ShowAsync();
            }
        }
    }
}
