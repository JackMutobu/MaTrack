using MaTrack.Core.Dtos;
using MaTrack.Shared.Services;
using Newtonsoft.Json;
using System;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace MaTrack.Shared.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginPage : Page
    {
        private IHttpClientService _httpClientService;
        public LoginPage()
        {
            this.InitializeComponent();
            BtnRegister.Click += BtnRegister_Click;
            BtnLogin.Click += BtnLogin_Click;
            progressBar.Visibility = Visibility.Collapsed;
            _httpClientService = new HttpClientService();
        }

        private async void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            progressBar.Visibility = Visibility.Visible;
            var user = new UserAuthDto
            {
                Phone = txtPhone.Text,
                Password = txtPassword.Password
            };
            try
            {
                var response = await _httpClientService.AuthenticateUser(user);
                var responseJson = JsonConvert.SerializeObject(response);
                if (response != null)
                {
                    switch (response.Role)
                    {
                        case Core.Enumerations.EnumRole.Administrator:
                            this.Frame.Navigate(typeof(DashboardPage), responseJson);
                            break;
                        case Core.Enumerations.EnumRole.Driver:
                            this.Frame.Navigate(typeof(DriversDashboardPage), responseJson);
                            break;
                    }
                    var firstItem = this.Frame.BackStack.Distinct().ToList().First();
                    Frame.BackStack.Remove(firstItem);
                }
                else
                {
                    txtError.Text = "An error occured, check your connection or credentials";
                }
            }
            catch(Exception ex)
            {
                txtError.Text = ex.Message;
            }
            progressBar.Visibility = Visibility.Collapsed;
        }

        private void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(SignupPage));
        }
    }
}
