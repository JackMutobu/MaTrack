using MaTrack.Core.Entities;
using MaTrack.Shared.Services;
using Newtonsoft.Json;
using System;
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
    public sealed partial class SignupNextPage : Page
    {
        private AdminEntity _admin;
        private IHttpClientService _httpClientService;
        public SignupNextPage()
        {
            this.InitializeComponent();
            btnRegister.Click += BtnRegister_Click;
            _httpClientService = new HttpClientService();
            progressBar.Visibility = Visibility.Collapsed;
        }

        private async void BtnRegister_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            progressBar.Visibility = Visibility.Visible;
            try
            {
                if (txtPassword.Password == txtConfirmPassword.Password)
                {
                    var user = new UserEntity
                    {
                        Phone = _admin.Phone,
                        Password = txtPassword.Password,
                        Firstname = _admin.Firstname,
                        Address = _admin.Address,
                        Lastname = _admin.Lastname,
                        Role = Core.Enumerations.EnumRole.Administrator,
                        UploadDate = DateTime.Now,
                        DoB = _admin.DoB
                    };
                    _admin.SACCO = txtSacco.Text;
                    _admin.UploadDate = DateTime.Now;
                    
                    var response = await _httpClientService.PostAsync(_admin, "Admin/add");
                    if (response.IsSuccessStatusCode)
                    {
                        response = await _httpClientService.PostAsync(user, "Users/register");
                        if (response.IsSuccessStatusCode)
                        {
                            MessageDialog messageDialog = new MessageDialog($"{_admin.Firstname} {_admin.Lastname} has been registered as admin");
                            await messageDialog.ShowAsync();
                            this.Frame.Navigate(typeof(LoginPage));
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
                else
                {
                    txtError.Text = "Passwords don't match";
                }
            }
            catch(Exception ex)
            {
                txtError.Text = ex.Message;
            }
            progressBar.Visibility = Visibility.Collapsed;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _admin = JsonConvert.DeserializeObject<AdminEntity>(e.Parameter as string);
        }

    }
}
