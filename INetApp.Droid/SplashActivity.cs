using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using AndroidX.AppCompat.App;
using INetApp.Droid.Services;
using INetApp.Services;

namespace INetApp.Droid.Activities
{
    [Activity(
         Label = "INetApp",
         Icon = "@drawable/icon",
         Theme = "@style/Theme.Splash",
         NoHistory = true,
         MainLauncher = true,
         ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            InvokeMainActivity();
        }
        protected override void OnResume()
        {
            base.OnResume();
            Xamarin.Forms.DependencyService.RegisterSingleton<IDeviceService>(new DeviceService(this));
        }

        private void InvokeMainActivity()
        {
            StartActivity(new Intent(this, typeof(MainActivity)));
        }
    }
}