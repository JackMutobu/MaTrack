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
    public sealed partial class AdminsPage : Page
    {
        private UserAuthDto _userAuthDto;
        private string _userAuthDtoJson;
        private IHttpClientService _httpClientService;

        public AdminsPage()
        {
            this.InitializeComponent();
            _httpClientService = new HttpClientService();
            
        }
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            _userAuthDto = JsonConvert.DeserializeObject<UserAuthDto>(e.Parameter as string);
            _userAuthDtoJson = JsonConvert.SerializeObject(_userAuthDto);
            try
            {
                _httpClientService.SetAuthorizationHeaderToken(_httpClientService.HttpClient, _userAuthDto.Token);
                var admins = JsonConvert.DeserializeObject<List<AdminEntity>>(await _httpClientService.GetAsync("Drivers/all"));
                var admin = admins.SingleOrDefault(x => x.Phone == _userAuthDto.Phone);
                listAdmins.ItemsSource = admins.Where(x => x.SACCO == admin?.SACCO);
            }
            catch (Exception ex)
            {
                MessageDialog messageDialog = new MessageDialog(ex.Message);
                await messageDialog.ShowAsync();
            }
        }
    }
}
