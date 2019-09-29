using MaTrack.Core.Dtos;
using MaTrack.Core.Entities;
using MaTrack.Core.Helpers;
using MaTrack.Shared.Helpers;
using MaTrack.Shared.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace MaTrack.Shared.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddDriverPage : Page
    {
        private IHttpClientService _httpClientService;
        private UserAuthDto _userAuthDto;
        private string _userAuthDtoJson;
        private List<VehicleEntity> _vehicles;
        public AddDriverPage()
        {
            this.InitializeComponent();
            _httpClientService = new HttpClientService();
            _vehicles = new List<VehicleEntity>();
            BtnAddDriver.Click += BtnAddDriver_Click;
            BtnAddVehicle.Click += BtnAddVehicle_Click;
            progressBar.Visibility = Visibility.Collapsed;
        }

        private void BtnAddVehicle_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AddVehiclePage),_userAuthDtoJson);
        }

        private async void BtnAddDriver_Click(object sender, RoutedEventArgs e)
        {
            progressBar.Visibility = Visibility.Visible;
            try
            {
                var selectedVehicle = comboVehicles.SelectedItem as VehicleEntity;
                var driver = new DriverEntity
                {
                    Firstname = txtFirstname.Text,
                    Address = txtAddress.Text,
                    DoB = GlobalHelpers.GetDate(txtDob.Text),
                    Lastname = txtLastname.Text,
                    UploadDate = DateTime.Now,
                    Phone = txtPhone.Text,
                    DriverStatus = Core.Enumerations.EnumStatus.Active,
                    VehicleId = selectedVehicle.Id
                };
                var userDriver = new UserEntity
                {
                    Address = driver.Address,
                    DoB = driver.DoB,
                    UploadDate = driver.UploadDate,
                    Firstname = driver.Firstname,
                    Lastname = driver.Lastname,
                    Password = "Driver2019",
                    Role = Core.Enumerations.EnumRole.Driver,
                    Phone = driver.Phone
                };
                var response = await _httpClientService.PostAsync(userDriver, "Users/register");

                if (response.IsSuccessStatusCode)
                {
                    response = await _httpClientService.PostAsync(driver, "Drivers/add");
                    if (response.IsSuccessStatusCode)
                    {
                        MessageDialog messageDialog = new MessageDialog($"{driver.Firstname} {driver.Lastname} has been registered as driver");
                        await messageDialog.ShowAsync();
                    }
                    else
                    {
                        txtError.Text = await response.Content.ReadAsStringAsync();
                    }
                }
                else
                {
                    txtError.Text = await response.Content.ReadAsStringAsync();
                }
            }
            catch (Exception ex)
            {
                txtError.Text = ex.Message;
            }
            progressBar.Visibility = Visibility.Collapsed;
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            _userAuthDto = JsonConvert.DeserializeObject<UserAuthDto>(e.Parameter as string);
            _userAuthDtoJson = e.Parameter as string;
            try
            {
                _httpClientService.SetAuthorizationHeaderToken(_httpClientService.HttpClient, _userAuthDto.Token);
                var vehicles = JsonConvert.DeserializeObject<List<VehicleEntity>>(await _httpClientService.GetAsync("Vehicles/all"));
                await App.RunOnDispatcher(() =>
                {
                    comboVehicles.ItemsSource = vehicles;
                });
            }
            catch (Exception ex)
            {
                txtError.Text = ex.Message;
            }
        }
    }
}
