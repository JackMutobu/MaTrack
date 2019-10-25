using System;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace MaTrack.Shared.Dialogs
{
    public sealed partial class MapsDialog : ContentDialog
    {
        public MapsDialog()
        {
            this.InitializeComponent();
            // get position
            Geopoint myPoint = new Geopoint(new BasicGeoposition() { Latitude = 51, Longitude = 0 });
#if __ANDROID__
#else

            //create POI
            MapIcon myPOI = new MapIcon { Location = myPoint, NormalizedAnchorPoint = new Point(0.5, 1.0), Title = "My location", ZIndex = 0 };
            //add to map and center it
            mapsControl.MapElements.Add(myPOI);
#endif

            mapsControl.Center = myPoint;
            mapsControl.ZoomLevel = 15;
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
    }
}
