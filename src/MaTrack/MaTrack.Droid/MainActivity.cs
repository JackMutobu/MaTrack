using Android.App;
using Android.OS;
using Android.Content.PM;
using Android.Views;
using System.Net;

namespace MaTrack.Droid
{
    [Activity(
            MainLauncher = true,
            ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize,
            WindowSoftInputMode = SoftInput.AdjustPan | SoftInput.StateHidden
        )]
    public class MainActivity : Windows.UI.Xaml.ApplicationActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            ServicePointManager.ServerCertificateValidationCallback +=
                            (sender, certificate, chain, sslPolicyErrors) => true;
            base.OnCreate(bundle);  
        }
    }
}

