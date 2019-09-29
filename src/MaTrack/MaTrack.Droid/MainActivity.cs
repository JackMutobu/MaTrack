using Android.App;
using Android.OS;
using Android.Content.PM;
using Android.Views;
using System.Net;
using Android.Runtime;

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
            //Xamarin.Essentials.Platform.Init(this, bundle); //
        }
        //    public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        //    {
        //        Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

        //        base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        //    }
    }
}

