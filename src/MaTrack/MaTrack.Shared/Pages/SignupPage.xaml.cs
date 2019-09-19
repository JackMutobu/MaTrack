using MaTrack.Core.Entities;
using MaTrack.Shared.Helpers;
using Newtonsoft.Json;
using System;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace MaTrack.Shared.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SignupPage : Page
    {
        public SignupPage()
        {
            this.InitializeComponent();
            BtnNextPage.Click += BtnNextPage_Click;
        }

        private void BtnNextPage_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var admin = new AdminEntity
            {
                Address = txtAddress.Text,
                DoB = GlobalHelpers.GetDate(txtDob.Text),
                Firstname = txtFirstname.Text,
                Lastname = txtLastname.Text,
                Phone = txtPhone.Text
            };
            var adminJson = JsonConvert.SerializeObject(admin);
            this.Frame.Navigate(typeof(SignupNextPage),adminJson);
        }

        
    }
}
