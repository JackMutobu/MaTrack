using MaTrack.Core.Dtos;
using Newtonsoft.Json;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace MaTrack.Shared.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DashboardPage : Page
    {
        private UserAuthDto _userAuthDto;
        private string _userAuthDtoJson;
        public DashboardPage()
        {
            this.InitializeComponent();
            btnDrivers.Click += BtnDrivers_Click;
            btnOperation.Click += BtnOperation_Click;
            btnVehicle.Click += BtnVehicle_Click;
            btnRoutes.Click += BtnRoutes_Click;
            btnAdmin.Click += BtnAdmin_Click;
        }

        private void BtnAdmin_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AdminsPage),_userAuthDtoJson);
        }

        private void BtnRoutes_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(RoutesPage),_userAuthDtoJson);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _userAuthDto = JsonConvert.DeserializeObject<UserAuthDto>(e.Parameter as string);
            _userAuthDtoJson = JsonConvert.SerializeObject(_userAuthDto);
        }
        private void BtnVehicle_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(VehiclesPage),_userAuthDtoJson);
        }

        private void BtnOperation_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(OperationPage),_userAuthDtoJson);
        }

        private void BtnDrivers_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Drivers),_userAuthDtoJson);
        }
    }
}
