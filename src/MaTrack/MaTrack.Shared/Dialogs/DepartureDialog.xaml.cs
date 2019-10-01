using Windows.UI.Xaml.Controls;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace MaTrack.Shared.Dialogs
{
    public sealed partial class DepartureDialog : ContentDialog
    {
        public DepartureDialog()
        {
            this.InitializeComponent();
        }
        public string Revenue { get; set; }
        public bool CanAddToDeparture { get; set; } = true;
        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            var revenue = txtRevenue.Text;
            if(!string.IsNullOrEmpty(revenue))
            {
                Revenue = revenue;
                if(btnDeparture.IsChecked == true)
                {
                    CanAddToDeparture = true;
                }
                else
                {
                    CanAddToDeparture = false;
                }
            }
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
    }
}
