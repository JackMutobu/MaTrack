using MaTrack.Core.Dtos;
using MaTrack.Core.Entities;
using MaTrack.Shared.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class VehiclesPage : Page
    {
        private UserAuthDto _userAuthDto;
        private string _userAuthDtoJson;
        private IHttpClientService _httpClientService;
        private List<VehicleEntity> _vehicles;

        public VehiclesPage()
        {
            this.InitializeComponent();
            _httpClientService = new HttpClientService();
            _vehicles = new List<VehicleEntity>();
            btnVehicleAdd.Click += BtnVehicleAdd_Click;
        }

        private void BtnVehicleAdd_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AddVehiclePage),_userAuthDtoJson);
        }

        protected async  override void OnNavigatedTo(NavigationEventArgs e)
        {
            _userAuthDto = JsonConvert.DeserializeObject<UserAuthDto>(e.Parameter as string);
            _userAuthDtoJson = e.Parameter as string;
            _httpClientService.SetAuthorizationHeaderToken(_httpClientService.HttpClient, _userAuthDto.Token);
            var vehicles = JsonConvert.DeserializeObject<List<VehicleEntity>>(await _httpClientService.GetAsync("Vehicles/all"));
            listVehicles.ItemsSource = vehicles;
        }
    }
}
