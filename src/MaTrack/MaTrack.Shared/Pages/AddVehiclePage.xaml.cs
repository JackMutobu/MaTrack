using MaTrack.Core.Dtos;
using MaTrack.Core.Entities;
using MaTrack.Shared.Services;
using Newtonsoft.Json;
using System;
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
    public sealed partial class AddVehiclePage : Page
    {
        private IHttpClientService _httpClientService;
        private UserAuthDto _userAuthDto;

        public AddVehiclePage()
        {
            this.InitializeComponent();
            _httpClientService = new HttpClientService();
            BtnAddVehicle.Click += BtnAddVehicle_Click;
            progressBar.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }

        private async void BtnAddVehicle_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            progressBar.Visibility = Windows.UI.Xaml.Visibility.Visible;
            try
            {
                var vehicle = new VehicleEntity
                {
                    Model = txtModel.Text,
                    Name = txtName.Text,
                    NumPlate = txtPlate.Text,
                    VehicleStatus = Core.Enumerations.EnumStatus.Active,
                    UploadDate = DateTime.Now
                };
                var response = await _httpClientService.PostAsync(vehicle, "Vehicles/add");
                if(response.IsSuccessStatusCode)
                {
                    MessageDialog messageDialog = new MessageDialog($"{vehicle.Name} has been saved");
                    await messageDialog.ShowAsync();
                }
                else
                {
                    txtError.Text = await response.Content.ReadAsStringAsync();
                }
            }
            catch(Exception ex)
            {
                txtError.Text = ex.Message;
            }
            progressBar.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _userAuthDto = JsonConvert.DeserializeObject<UserAuthDto>(e.Parameter as string);
            _httpClientService.SetAuthorizationHeaderToken(_httpClientService.HttpClient, _userAuthDto.Token);
        }
    }
}
