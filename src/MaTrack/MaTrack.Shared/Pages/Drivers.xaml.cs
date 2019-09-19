using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace MaTrack.Shared.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Drivers : Page
    {
        public Drivers()
        {
            this.InitializeComponent();
            btnAddDriver.Click += BtnAddDriver_Click;
        }

        private void BtnAddDriver_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AddDriverPage));
        }
    }
}
