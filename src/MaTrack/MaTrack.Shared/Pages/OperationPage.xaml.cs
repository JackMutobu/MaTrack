using MaTrack.Core.Dtos;
using Newtonsoft.Json;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace MaTrack.Shared.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class OperationPage : Page
    {
        private UserAuthDto _userAuthDto;
        private string _userAuthDtoJson;

        public OperationPage()
        {
            this.InitializeComponent();
            btnArrival.Click += BtnArrival_Click;
            btnDeparture.Click += BtnDeparture_Click;
            btnReport.Click += BtnReport_Click;
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _userAuthDto = JsonConvert.DeserializeObject<UserAuthDto>(e.Parameter as string);
            _userAuthDtoJson = JsonConvert.SerializeObject(_userAuthDto);
        }
        private void BtnReport_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(ReportPage),_userAuthDtoJson);
        }

        private void BtnDeparture_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(DepartPage), _userAuthDtoJson);
        }

        private void BtnArrival_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(ArrivalsPage), _userAuthDtoJson);
        }
    }
}
