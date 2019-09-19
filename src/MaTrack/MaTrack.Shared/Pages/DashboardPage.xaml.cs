using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace MaTrack.Shared.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DashboardPage : Page
    {
        public DashboardPage()
        {
            this.InitializeComponent();
            BtnDrivers.Click += BtnDrivers_Click;
            BtnOperation.Click += BtnOperation_Click;
            BtnVehicle.Click += BtnVehicle_Click;
        }

        private void BtnVehicle_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(VehiclesPage));
        }

        private void BtnOperation_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void BtnDrivers_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Drivers));
        }
    }
}
